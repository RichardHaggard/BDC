using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class CommentViewModel : CommentWindows
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public string SourceFacility
        {
            get => _sourceFacility;
            set => SetProperty(ref _sourceFacility, value);
        }
        private string _sourceFacility;

        public ObservableCollection<string> ListOfFacilities { get; } =
            new ObservableCollection<string>();

        public string SelectedFacility
        {
            get => _selectedFacility;
            set => SetProperty(ref _selectedFacility, value);
        }
        private string _selectedFacility;

        public Color SectionNameBackground
        {
            get => _sectionNameBackground;
            set => SetProperty(ref _sectionNameBackground, value);
        }
        private Color _sectionNameBackground;

        public bool IsSectionNameEnabled
        {
            get => _isSectionNameEnabled;
            set => SetProperty(ref _isSectionNameEnabled, value);
        }
        private bool _isSectionNameEnabled;

        public string SectionNameText
        {
            get => _sectionNameText;
            set => SetProperty(ref _sectionNameText, value);
        }
        private string _sectionNameText;

        public ObservableCollection<IItemChecklist> ListOfSystems { get; } = 
            new ObservableCollection<IItemChecklist>();

        public string YearBuilt
        {
            get => _yearBuilt;
            set => SetProperty(ref _yearBuilt, value);
        }
        private string _yearBuilt;

        public bool IsOverYearChecked
        {
            get => _isOverYearChecked;
            set => SetProperty(ref _isOverYearChecked, value);
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
            set => SetProperty(ref _isSectionDetailsChecked, value);
        }
        private bool _isSectionDetailsChecked;

        public bool IsDetailCommentsChecked
        {
            get => _isDetailCommentsChecked;
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
            set => SetProperty(ref _isCopyInspectionsChecked, value);
        }
        private bool _isCopyInspectionsChecked;

        public bool IsIncludeCommentsChecked
        {
            get => _isIncludeCommentsChecked;
            set => SetProperty(ref _isIncludeCommentsChecked, value);
        }
        private bool _isIncludeCommentsChecked;

        /// <summary>
        /// EnumControlResult.ResultCancelled indicates cancellation.
        /// EnumControlResult.ResultDeferred  is defer result.
        /// EnumControlResult.ResultSaveNow   is save Comment now.
        /// </summary>
        public EnumControlResult Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }
        private EnumControlResult _result;

        // **************** Class constructors ********************************************** //

        public CommentViewModel()
        {
            IsSectionNameEnabled       = false;  
            IsOverYearChecked          = false;
            IsSectionCommentsChecked   = false;
            IsSectionDetailsChecked    = false;
            IsDetailCommentsChecked    = false;
            IsExistingInventoryChecked = false;
            IsCopyInspectionsChecked   = false;
            IsIncludeCommentsChecked   = false;

            SectionNameBackground = Colors.LightGray;

            SourceFacility = "<SOURCE FACILITY>";

#if DEBUG
#warning Using MOCK data for CommentViewModel
            ListOfFacilities.AddRange(new []
            {
                "Facility 1",
                "Facility 2",
                "Facility 3",
                "Facility 4"
            });

            ListOfSystems.AddRange(new[]
            {
                new ItemChecklist {ItemName = "C10 - INTERIOR CONSTRUCTION", ItemIsChecked = false},
                new ItemChecklist {ItemName = "C20 - STAIRS"               , ItemIsChecked = false},
                new ItemChecklist {ItemName = "C30 - INTERIOR FINISHES"    , ItemIsChecked = false},
                new ItemChecklist {ItemName = "C10 - INTERIOR CONSTRUCTION", ItemIsChecked = false}
            });

            SelectedFacility = ListOfFacilities[0];
#endif
        }

        // **************** Class members *************************************************** //

    }
}
