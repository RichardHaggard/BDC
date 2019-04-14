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
using BDC_V1.Views;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class CommentViewModel : CommentWindows
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        // TODO: Move these properties into a separate interface / class

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

        public ObservableCollection<ItemChecklist> ListOfSystems { get; } = 
            new ObservableCollection<ItemChecklist>();

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

        // **************** Class constructors ********************************************** //

        public CommentViewModel()
        {
            IsSectionNameEnabled       = 
            IsOverYearChecked          = 
            IsSectionCommentsChecked   = 
            IsSectionDetailsChecked    = 
            IsDetailCommentsChecked    = 
            IsExistingInventoryChecked = 
            IsCopyInspectionsChecked   = 
            IsIncludeCommentsChecked   = false;

            SectionNameBackground = Colors.LightGray;

            SourceFacility = "<SOURCE FACILITY>";

            HeaderText1 = "COMMENTS";
            HeaderText2 = "Comments for non-inspection items";

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

            // gets around the "cannot access virtual member in constructor" issue
            _commentaryList.AddRange(new [] {
                new Commentary
                {
                    FacilityId  = "0000",
                    CodeIdText  = "000000",
                    Rating      = EnumRatingType.GPlus,
                    CommentText = "These comments come from the Comment Window"
                },
                new Commentary
                {
                    FacilityId  = "11057",
                    CodeIdText  = "C102001",
                    Rating      = EnumRatingType.R,
                    CommentText = "DAMAGED - All the wood doors have 70% severe moisture damage. " +
                                  "CRACKED - All of the doors have 65% severe cracking and splintering."
                },
                new Commentary
                {
                    FacilityId  = "11057",
                    CodeIdText  = "C102001",
                    Rating      = EnumRatingType.R,
                    CommentText = "DAMAGED - All the wood doors have 70% severe moisture damage. " + 
                                  "CRACKED - All of the doors have 65% severe cracking and splintering. " +
                                  "Replacement is recommended"
                },
                new Commentary
                {
                    FacilityId  = "12022",
                    CodeIdText  = "D501003",
                    Rating      = EnumRatingType.G,
                    CommentText = "The nameplate on the component was missing certain Section Detail fields. " +
                                  "Section Detail fields have been populated and fields with NA represent data not found."
                },
                new Commentary
                {
                    FacilityId  = "17180",
                    CodeIdText  = "D302001",
                    Rating      = EnumRatingType.GPlus,
                    CommentText = "This unit was in a locked room and not visible."
                },
                new Commentary
                {
                    FacilityId  = "17180",
                    CodeIdText  = "D501003",
                    Rating      = EnumRatingType.AMinus,
                    CommentText = "No A20 and D10 systems present. Could not gain access to Supply RM C342."
                }
            });
#endif
        }

        // **************** Class members *************************************************** //

        protected override List<Commentary> CommentaryList
        {
            get => _commentaryList ?? (_commentaryList = new List<Commentary>());
            set => SetProperty(ref _commentaryList, value);
        }
        private List<Commentary> _commentaryList = new List<Commentary>();

        protected override string CopyWindowTitle => "COPY COMMENT";
    }
}
