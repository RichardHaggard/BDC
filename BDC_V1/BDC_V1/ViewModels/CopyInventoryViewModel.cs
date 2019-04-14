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
    public class CopyInventoryViewModel : CloseableWindow
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdCancelButton { get; }
        public ICommand CmdCopyButton   { get; }

        public bool IsSectionNameEnabled
        {
            get
            {
                var isEnabled = 
                    (!string.IsNullOrEmpty(SelectedFacility) && SelectedFacility == SourceFacility) || 
                    (!string.IsNullOrEmpty(SectionNode     ) && SectionNode      == Section);

                return _isSectionNameEnabled = isEnabled;
            }
        }
        private bool _isSectionNameEnabled;


        // TODO: Move these into a data interface / class

        public string SelectedFacility
        {
            get => _selectedFacility;
            set => SetPropertyFlagged(ref _selectedFacility, value, nameof(IsSectionNameEnabled));
        }
        private string _selectedFacility;

        public string SourceFacility
        {
            get => _sourceFacility;
            set => SetPropertyFlagged(ref _sourceFacility, value, nameof(IsSectionNameEnabled));
        }
        private string _sourceFacility;

        // TODO: Get the real data for this
        public string Section
        {
            get => _section;
            set => SetPropertyFlagged(ref _section, value, nameof(IsSectionNameEnabled));
        }
        private string _section;

        // TODO: Get the real data for this
        public string SectionNode
        {
            get => _sectionNode;
            set => SetPropertyFlagged(ref _sectionNode, value, nameof(IsSectionNameEnabled));
        }
        private string _sectionNode;

        public string SectionNameText
        {
            get => IsSectionNameEnabled ? _sectionNameText : string.Empty;
            set => SetProperty(ref _sectionNameText, value);
        }
        private string _sectionNameText;

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


        [NotNull]
        public ObservableCollection<string> ListOfFacilities { get; } =
            new ObservableCollection<string>();

        [NotNull]
        public ObservableCollection<ItemChecklist> ListOfSystems { get; } = 
            new ObservableCollection<ItemChecklist>();

        // **************** Class constructors ********************************************** //

        public CopyInventoryViewModel()
        {
            CmdCancelButton = new DelegateCommand(OnCancelButton);
            CmdCopyButton   = new DelegateCommand(OnCopyButton  );

#if DEBUG
#warning Using MOCK data for CmInvViewModel

            SourceFacility  = "11057 - EAST BAY";
            SectionNameText = "<SECTION NAME>";
            SectionNode     = "<NODE NAME>";

            ListOfFacilities.AddRange(new []
            {
                "17180 - ARNG ARMORY",
                "11057 - EAST BAY"
            });
            SelectedFacility = ListOfFacilities[0];

            foreach (EnumFacilitySystemTypes system in Enum.GetValues(typeof(EnumFacilitySystemTypes)))
            {
                ListOfSystems.Add(new ItemChecklist
                    {ItemName = $"{system.ToString()} - {system.Description()}"});
            }
#endif
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
