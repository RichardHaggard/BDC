using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class CopyInventoryViewModel : CloseableResultsWindow
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull] public ICommand CmdCancelButton { get; }
        [NotNull] public ICommand CmdCopyButton   { get; }

        public bool IsSectionNameEnabled => 
            (IsInventory == false) || (SourceFacility == TargetFacilities.SelectedItem);

        public string WindowTitle => IsInventory ? "COPY INVENTORY" : "COPY SECTION(S)...";

        public bool IsInventory
        {
            get => _isInventory;
            set => SetProperty(ref _isInventory, value, () =>
            {
                OnIsInventory();
                RaisePropertyChanged( new [] 
                {
                    nameof(WindowTitle),
                    nameof(SectionName),
                    nameof(IsSectionNameEnabled)
                });
            });
        }
        private bool _isInventory = true;

        // TODO: Move these into a data interface / class ???

        public string SectionName
        {
            get => (IsSectionNameEnabled)? _sectionName : string.Empty;
            set => SetProperty(ref _sectionName, value);
        }
        private string _sectionName;

        public IFacilityInfoHeader SourceFacility
        {
            get => _sourceFacility;
            set => SetPropertyFlagged(ref _sourceFacility, value, new []
            {
                nameof(SectionName),
                nameof(IsSectionNameEnabled)
            });
        }
        private IFacilityInfoHeader _sourceFacility;

        // TODO: Get the real data for this
        public ISectionInfo Node
        {
            get => _node;
            set => SetPropertyFlagged(ref _node, value, new []
            {
                nameof(SectionName),
                nameof(IsSectionNameEnabled)
            });
        }
        private ISectionInfo _node;

        // TODO: Get the real data for this
        public string SectionNode
        {
            get => _sectionNode;
            set => SetPropertyFlagged(ref _sectionNode, value, new []
            {
                nameof(SectionName),
                nameof(IsSectionNameEnabled)
            });
        }
        private string _sectionNode;

        public string YearBuilt
        {
            get
            {
                if (_yearBuilt == 0) _yearBuilt = (uint) DateTime.Now.Year;
                return IsOverYearChecked ? _yearBuilt.ToString() : string.Empty;
            }
            set 
            {
                // TODO: field validation ???
                if (uint.TryParse(value, out var tryValue))
                    SetProperty(ref _yearBuilt, tryValue);
            }
        }
        private uint _yearBuilt;

        public bool IsOverYearChecked
        {
            get => _isOverYearChecked;
            set => SetPropertyFlagged(ref _isOverYearChecked, value, 
                nameof(YearBuilt));
        }
        private bool _isOverYearChecked;


        public bool IsSectionCommentsChecked
        {
            get => _isSectionCommentsChecked;
            set => SetProperty(ref _isSectionCommentsChecked, value);
        }
        private bool _isSectionCommentsChecked;


        public bool IsSectionDetailsChecked
        {
            get => _isSectionDetailsChecked;
            set => SetPropertyFlagged(ref _isSectionDetailsChecked, value, 
                nameof(IsDetailCommentsChecked));
        }
        private bool _isSectionDetailsChecked;


        public bool IsDetailCommentsChecked
        {
            get => IsSectionDetailsChecked && _isDetailCommentsChecked;
            set => SetProperty(ref _isDetailCommentsChecked, value);
        }
        private bool _isDetailCommentsChecked;


        public bool IsExistingInventoryChecked
        {
            get => _isExistingInventoryChecked;
            set => SetProperty(ref _isExistingInventoryChecked, value);
        }
        private bool _isExistingInventoryChecked;


        public bool IsCopyInspectionsChecked
        {
            get => _isCopyInspectionsChecked;
            set => SetPropertyFlagged(ref _isCopyInspectionsChecked, value, 
                nameof(IsIncludeCommentsChecked));
        }
        private bool _isCopyInspectionsChecked;


        public bool IsIncludeCommentsChecked
        {
            get => IsCopyInspectionsChecked && _isIncludeCommentsChecked;
            set => SetProperty(ref _isIncludeCommentsChecked, value);
        }
        private bool _isIncludeCommentsChecked;

        public string UserEnteredSection
        {
            get => _userEnteredSection = Sections.SelectedItem;
            set => SetProperty(ref _userEnteredSection, value, () =>
            {
                if (! string.IsNullOrEmpty(value))
                {
                    if (! Sections.Contains(value))
                        Sections.Collection.Add(value);

                    Sections.SelectedItem = value;
                }
            });
        }
        private string _userEnteredSection = string.Empty;

        [NotNull] public IndexedCollection<IFacilityInfoHeader> TargetFacilities { get; } =
            new IndexedCollection<IFacilityInfoHeader>(new ObservableCollection<IFacilityInfoHeader>());

        [NotNull] public IndexedCollection<ItemChecklist> Systems { get; } =
            new IndexedCollection<ItemChecklist>(new ObservableCollection<ItemChecklist>());

        [NotNull] public IndexedCollection<string> Sections { get; } =
            new IndexedCollection<string>(new ObservableCollection<string>());

        // **************** Class constructors ********************************************** //

        public CopyInventoryViewModel()
        {
            CmdCancelButton = new DelegateCommand(OnCancelButton);
            CmdCopyButton   = new DelegateCommand(OnCopyButton  );

#if DEBUG
#warning Using MOCK data for CmInvViewModel

            // TODO: I think this value is coming from thee tree view selection
            SectionNode = "<NODE NAME>";

            // cannot directly manipulate a ListCollectionView at initialization,
            // manipulate the base collection instead
            TargetFacilities.Collection.AddRange(new []
            {
                new FacilityInfoHeader {BuildingIdNumber = 11057, 
                    BuildingName = "National Guard Readiness Center"},

                new FacilityInfoHeader {BuildingIdNumber = 11612, 
                    BuildingName = "Facility # 2"}
            });

            // TODO: I'm unsure about what this is supposed to be
            SourceFacility = TargetFacilities.FirstOrDefault();

            foreach (EnumFacilitySystemTypes system in Enum.GetValues(typeof(EnumFacilitySystemTypes)))
            {
                Systems.Collection.Add(new ItemChecklist
                    {ItemName = $"{system.ToString()} - {system.Description()}"});
            }
#endif
            TargetFacilities.PropertyChanged += (o, i) =>
            {
                if (i.PropertyName == nameof(TargetFacilities.SelectedItem))
                    RaisePropertyChanged(nameof(IsSectionNameEnabled));
            };
        }

        // **************** Class members *************************************************** //

        private void OnIsInventory()
        {
            Sections.Collection.Clear();

            if (! IsInventory)
            {
                Sections.Collection.AddRange(new []
                {
                    "D201001 Waterclosets",
                    "D201002 Urinals",
                    "D201003 Lavatories",
                    "D201004 Sinks",
                    "D201005 Shower/Tubs",
                    "D201006 Drinking Fountains and Coolers",
                    "D201007 Bidets",
                    "D201090 Other Plumbing Fixtures",
                    "D201099 Emergency Fixtures",
                });
            }

            Sections.SelectedItem = Sections.FirstOrDefault();
        }

        private void OnCancelButton()
        {
            Result = EnumControlResult.ResultCancelled;
            DialogResultEx = true;
        }

        private void OnCopyButton()
        {
            Result = EnumControlResult.ResultSaveNow;
            DialogResultEx = true;
        }

    }
}
