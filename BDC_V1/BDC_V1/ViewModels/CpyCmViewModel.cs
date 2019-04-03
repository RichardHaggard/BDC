using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class CpyCmViewModel : CloseableWindow
    {
        // **************** Class enumerations ********************************************** //

        public enum EnumFilterSystemType
        {
            SystemTypeAny,
            // ReSharper disable once InconsistentNaming
            SystemTypeBRED,
            SystemTypeSystem
        }

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //
        public ICommand CmdNextButton   { get; }
        public ICommand CmdPrevButton   { get; }
        public ICommand CmdCopyButton   { get; }
        public ICommand CmdCancelButton { get; }

        public bool IsSourceSourceChecked
        {
            get => _isSourceSourceChecked;
            set
            {
                if ( SetProperty(ref _isSourceSourceChecked, value))
                    OnChangeFilter();
            }
        }
        private bool _isSourceSourceChecked;

        public bool IsSourceCurrentChecked
        {
            get => _isSourceCurrentChecked;
            set
            {
                if ( SetProperty(ref _isSourceCurrentChecked, value))
                    OnChangeFilter();
            }
        }
        private bool _isSourceCurrentChecked;

        public bool IsSourceRatedChecked
        {
            get => _isSourceRatedChecked;
            set
            {
                if ( SetProperty(ref _isSourceRatedChecked, value))
                    OnChangeFilter();
            }
        }
        private bool _isSourceRatedChecked;

        // ReSharper disable once InconsistentNaming
        public bool IsSysTypeBREDChecked
        {
            get => _isSysTypeBredChecked;
            set
            {
                if ( SetProperty(ref _isSysTypeBredChecked, value))
                    OnChangeFilter();
            }
        }
        private bool _isSysTypeBredChecked;

        public bool IsSysTypeSystemChecked
        {
            get => _isSysTypeSystemChecked;
            set
            {
                if ( SetProperty(ref _isSysTypeSystemChecked, value))
                    OnChangeFilter();
            }
        }
        private bool _isSysTypeSystemChecked;

        public bool IsSysTypeAnyChecked
        {
            get => _isSysTypeAnyChecked;
            set
            {
                if ( SetProperty(ref _isSysTypeAnyChecked, value))
                    OnChangeFilter();
            }
        }
        private bool _isSysTypeAnyChecked;

        public bool IsColorYellowChecked
        {
            get => _isColorYellowChecked;
            set => SetProperty(ref _isColorYellowChecked, value);
        }
        private bool _isColorYellowChecked;

        public bool IsColorAmberChecked
        {
            get => _isColorAmberChecked;
            set => SetProperty(ref _isColorAmberChecked, value);
        }
        private bool _isColorAmberChecked;

        public bool IsColorRedChecked
        {
            get => _isColorRedChecked;
            set => SetProperty(ref _isColorRedChecked, value);
        }
        private bool _isColorRedChecked;

        public ObservableCollection<string> ListOfFacilities { get; } =
            new ObservableCollection<string>();

        public string SelectedFacility
        {
            get => _selectedFacility;
            set
            {
                if ( SetProperty(ref _selectedFacility, value))
                    OnChangeFilter();
            }
        }
        private string _selectedFacility;

        public string SearchTerm
        {
            get => _searchTerm;
            set => SetProperty(ref _searchTerm, value);
        }
        private string _searchTerm;

        public string MatchingResultsText
        {
            get => _matchingResultsText;
            set => SetProperty(ref _matchingResultsText, value);
        }
        private string _matchingResultsText;

        public ObservableCollection<ICommentaryType> FilteredCommentary { get; } =
            new ObservableCollection<ICommentaryType>();

        public ObservableCollection<ICommentaryType> UnFilteredCommentary { get; } =
            new ObservableCollection<ICommentaryType>();

        /// <summary>
        /// EnumCommentResult.ResultCancelled indicates cancellation.
        /// EnumCommentResult.ResultDeferred  is defer result.
        /// EnumCommentResult.ResultSaveNow   is save Comment now.
        /// </summary>
        public EnumCommentResult Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }
        private EnumCommentResult _result;

        public EnumFilterSystemType FilterSystemType
        {
            get
            {
                if (IsSysTypeBREDChecked  ) return EnumFilterSystemType.SystemTypeBRED;
                if (IsSysTypeSystemChecked) return EnumFilterSystemType.SystemTypeSystem;
                return EnumFilterSystemType.SystemTypeAny;
            }

            set
            {
                if (SetProperty(ref _filterSystemType, value))
                {
                    switch (value)
                    {
                        case EnumFilterSystemType.SystemTypeBRED:
                            IsSysTypeBREDChecked = true;
                            break;

                        case EnumFilterSystemType.SystemTypeSystem:
                            IsSysTypeSystemChecked = true;
                            break;

                        // ReSharper disable once RedundantCaseLabel
                        case EnumFilterSystemType.SystemTypeAny :
                        default:
                            IsSysTypeAnyChecked = true;
                            break;
                    }
                }
            }
        }
        private EnumFilterSystemType _filterSystemType;

        public EnumRatingColors FilterRatingColor
        {
            get
            {
                if (IsColorYellowChecked) return EnumRatingColors.Yellow;
                if (IsColorAmberChecked ) return EnumRatingColors.Amber;
                if (IsColorRedChecked   ) return EnumRatingColors.Red;
                return EnumRatingColors.Green;
            }

            set
            {
                if (SetProperty(ref _filterRatingColor, value))
                {
                    switch (value)
                    {
                        case EnumRatingColors.Yellow:
                            IsColorYellowChecked = true;
                            break;

                        case EnumRatingColors.Amber:
                            IsColorAmberChecked = true;
                            break;

                        case EnumRatingColors.Red:
                            IsColorRedChecked = true;
                            break;

                        // ReSharper disable once RedundantCaseLabel
                        case EnumRatingColors.Green:
                        default:
                            IsColorYellowChecked = false;
                            IsColorAmberChecked  = false;
                            IsColorRedChecked    = false;
                            break;
                    }

                    OnChangeFilter();
                }
            }
        }
        private EnumRatingColors _filterRatingColor;

        // **************** Class constructors ********************************************** //

        public CpyCmViewModel()
        {
            CmdNextButton   = new DelegateCommand(OnNextButton  );
            CmdPrevButton   = new DelegateCommand(OnPrevButton  );
            CmdCopyButton   = new DelegateCommand(OnCopyButton  );
            CmdCancelButton = new DelegateCommand(OnCancelButton);

            FilterRatingColor = EnumRatingColors.Red;

            ListOfFacilities.Add("<ANY FACILITY>");
            ListOfFacilities.Add("17180 - ARNG ARMORY");
            ListOfFacilities.Add("11057 - Facility #2");

            UnFilteredCommentary.Add(new CommentaryType()
            {
                FacilityId  = "11057",
                CodeIdText  = "C102001",
                Rating      = EnumRatingType.R,
                CommentText = "DAMAGED - All the wood doors have 70% severe moisture damage. " +
                              "CRACKED - All of the doors have 65% severe cracking and splintering."
            });

            UnFilteredCommentary.Add(new CommentaryType()
            {
                FacilityId  = "17180",
                CodeIdText  = "C102002",
                Rating      = EnumRatingType.Y,
                CommentText = "HOLED - Holes have been punched thru 20% of the doors."
            });
        }

        // **************** Class members *************************************************** //

        private void OnCancelButton()
        {
            Result = EnumCommentResult.ResultCancelled;
            DialogResultEx = false;
        }

        private void OnCopyButton()
        {
            Result = EnumCommentResult.ResultSaveNow;
            DialogResultEx = true;
        }

        private void OnNextButton() { Debug.WriteLine("OnNextButton is not implemented"); }
        private void OnPrevButton() { Debug.WriteLine("OnPrevButton is not implemented"); }

        private void OnChangeFilter()
        {
            IEnumerable<ICommentaryType> filterList = new List<ICommentaryType>(UnFilteredCommentary);

            // ??? don't know what to do here ???
            if (IsSourceSourceChecked)
            {
            }

            // ??? don't know what to do here ???
            if (IsSourceCurrentChecked)
            {
            }

            if (IsSourceRatedChecked && !IsSysTypeAnyChecked)
            {
                switch (FilterRatingColor)
                {
                    case EnumRatingColors.Green:
                        filterList = filterList.Where(item => 
                            item.Rating.ToRatingColor() == EnumRatingColors.Green);
                        break;

                    case EnumRatingColors.Yellow:
                        filterList = filterList.Where(item => 
                            item.Rating.ToRatingColor() == EnumRatingColors.Yellow);
                        break;

                    case EnumRatingColors.Amber:
                        filterList = filterList.Where(item => 
                            item.Rating.ToRatingColor() == EnumRatingColors.Amber);
                        break;

                    case EnumRatingColors.Red:
                        filterList = filterList.Where(item => 
                            item.Rating.ToRatingColor() == EnumRatingColors.Red);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            // ??? don't know what to do here ???
            switch (FilterSystemType)
            {
                case EnumFilterSystemType.SystemTypeAny:
                    break;
                case EnumFilterSystemType.SystemTypeBRED:
                    break;
                case EnumFilterSystemType.SystemTypeSystem:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                filterList = filterList.Where(item =>
                    item.CommentText.Contains(SearchTerm));
            }

            if (SelectedFacility != "<ANY FACILITY>")
            {
                filterList = filterList.Where(item =>
                    item.FacilityId == SelectedFacility);
            }

            FilteredCommentary.Clear();
            FilteredCommentary.AddRange(filterList);

            MatchingResultsText = "Matching Results: " + FilteredCommentary.Count;
        }
    }
}
