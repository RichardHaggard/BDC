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

        public bool IsSectionNameEnabled
        {
            get
            {
                var enabled = (SourceFacilities.SelectedItem == TargetFacilities.SelectedItem);
                if (!enabled) Sections.SelectedIndex = -1;

                return enabled;
            }
        }

        // TODO: Move these into a data interface / class ???

        // TODO: Get the real data for this
        public ISectionInfo Node
        {
            get => _node;
            set => SetPropertyFlagged(ref _node, value, nameof(IsSectionNameEnabled));
        }
        private ISectionInfo _node;

        // TODO: Get the real data for this
        public string SectionNode
        {
            get => _sectionNode;
            set => SetPropertyFlagged(ref _sectionNode, value, nameof(IsSectionNameEnabled));
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
            set => SetPropertyFlagged(ref _isOverYearChecked, value, nameof(YearBuilt));
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
            set => SetPropertyFlagged(ref _isSectionDetailsChecked, value, nameof(IsDetailCommentsChecked));
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
            set => SetPropertyFlagged(ref _isCopyInspectionsChecked, value, nameof(IsIncludeCommentsChecked));
        }
        private bool _isCopyInspectionsChecked;


        public bool IsIncludeCommentsChecked
        {
            get => IsCopyInspectionsChecked && _isIncludeCommentsChecked;
            set => SetProperty(ref _isIncludeCommentsChecked, value);
        }
        private bool _isIncludeCommentsChecked;

        [NotNull] public IndexedCollection<IFacilityInfoHeader> SourceFacilities { get; } =
            new IndexedCollection<IFacilityInfoHeader>(new ObservableCollection<IFacilityInfoHeader>());

        [NotNull] public IndexedCollection<IFacilityInfoHeader> TargetFacilities { get; } =
            new IndexedCollection<IFacilityInfoHeader>(new ObservableCollection<IFacilityInfoHeader>());

        [NotNull] public IndexedCollection<ISectionInfo> Sections { get; } =
            new IndexedCollection<ISectionInfo>(new ObservableCollection<ISectionInfo>());

        [NotNull] public IndexedCollection<ItemChecklist> Systems { get; } =
            new IndexedCollection<ItemChecklist>(new ObservableCollection<ItemChecklist>());

        // **************** Class constructors ********************************************** //

        public CopyInventoryViewModel()
        {
            CmdCancelButton = new DelegateCommand(OnCancelButton);
            CmdCopyButton   = new DelegateCommand(OnCopyButton  );

#if DEBUG
#warning Using MOCK data for CmInvViewModel

            // TODO: I think this value is coming from thee tree view selection
            SectionNode = "<NODE NAME>";

            TargetFacilities.AddRange(new []
            {
                new FacilityInfoHeader {BuildingIdNumber = 11057, BuildingName = "National Guard Readiness Center"},
                new FacilityInfoHeader {BuildingIdNumber = 11612, BuildingName = "Facility # 2"}
            });

            // TODO: I'm unsure about what this is supposed to be
            SourceFacilities.AddRange(TargetFacilities);
            SourceFacilities.SelectedIndex = 0;

            Sections.AddRange(new[]
            {
                new SectionInfo("FL1"),
                new SectionInfo("FL2"),
                new SectionInfo("EAST WING"),
                new SectionInfo("WEST WING"),
                new SectionInfo("MEZZANINE"),
            });

            foreach (EnumFacilitySystemTypes system in Enum.GetValues(typeof(EnumFacilitySystemTypes)))
            {
                Systems.Add(new ItemChecklist
                    {ItemName = $"{system.ToString()} - {system.Description()}"});
            }
#endif

            SourceFacilities.PropertyChanged += (o, i) =>
            {
                if (i.PropertyName == nameof(SourceFacilities.SelectedItem))
                    RaisePropertyChanged(nameof(IsSectionNameEnabled));
            };

            TargetFacilities.PropertyChanged += (o, i) =>
            {
                if (i.PropertyName == nameof(SourceFacilities.SelectedItem))
                    RaisePropertyChanged(nameof(IsSectionNameEnabled));
            };

            Sections.PropertyChanged += (o, i) =>
            {
                if (i.PropertyName == nameof(SourceFacilities.SelectedItem))
                    RaisePropertyChanged(nameof(IsSectionNameEnabled));
            };
        }

        // **************** Class members *************************************************** //

        private void OnCancelButton()
        {
            Result = EnumControlResult.ResultCancelled;
            DialogResultEx = false;
        }

        private void OnCopyButton()
        {
            Result = EnumControlResult.ResultSaveNow;
            DialogResultEx = true;
        }


    }
}
