using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class CpyCmViewModel : CloseableWindow
    {
        // **************** Class enumerations ********************************************** //

        public enum EnumFilterSourceType
        {
            BredFilter,
            SavedFilter
        }

        public enum EnumFilterRelatedType
        {
            SystemFilter,
            ComponentFilter
        }

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //
        public ICommand CmdNextButton { get; }
        public ICommand CmdPrevButton { get; }
        public ICommand CmdCopyButton { get; }
        public ICommand CmdCancelButton { get; }

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

        [CanBeNull]
        public string SelectedFacility
        {
            get => (FilterSource == EnumFilterSourceType.SavedFilter)
                ? _selectedFacility
                : ListOfFacilities.FirstOrDefault();
            set
            {
                if (SetProperty(ref _selectedFacility, value))
                    OnChangeFilter();
            }
        }
        private string _selectedFacility;

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                if (SetProperty(ref _searchTerm, value))
                    OnChangeFilter();
            }
        }
        private string _searchTerm;

        public EnumFilterSourceType FilterSource
        {
            get => _filterSource;
            set
            {
                if (SetProperty(ref _filterSource, value))
                {
                    RaisePropertyChanged(nameof(SelectedFacility));
                    OnChangeFilter();
                }
            }
        }
        private EnumFilterSourceType _filterSource;

        public EnumFilterRelatedType RelatedSource
        {
            get => _relatedSource;
            set
            {
                if (SetProperty(ref _relatedSource, value))
                    OnChangeFilter();
            }
        }
        private EnumFilterRelatedType _relatedSource;

        public EnumRatingColors FilterRatingColor
        {
            get => _filterRatingColor;
            set
            {
                if (SetProperty(ref _filterRatingColor, value))
                    OnChangeFilter();
            }
        }
        private EnumRatingColors _filterRatingColor;

        public string MatchingResultsText
        {
            get => _matchingResultsText;
            set => SetProperty(ref _matchingResultsText, value);
        }
        private string _matchingResultsText;

        public ObservableCollection<string> ListOfFacilities { get; } =
            new ObservableCollection<string>();

        public ObservableCollection<ICommentaryType> FilteredCommentary { get; } =
            new ObservableCollection<ICommentaryType>();

        public ObservableCollection<ICommentaryType> UnFilteredCommentary { get; } =
            new ObservableCollection<ICommentaryType>();

        // **************** Class constructors ********************************************** //

        public CpyCmViewModel()
        {
            CmdNextButton = new DelegateCommand(OnNextButton);
            CmdPrevButton = new DelegateCommand(OnPrevButton);
            CmdCopyButton = new DelegateCommand(OnCopyButton);
            CmdCancelButton = new DelegateCommand(OnCancelButton);

            FilterSource      = EnumFilterSourceType.BredFilter;
            RelatedSource     = EnumFilterRelatedType.SystemFilter;
            FilterRatingColor = EnumRatingColors.Green;
            SearchTerm = "";

            ListOfFacilities.Add("<ANY FACILITY>");
            ListOfFacilities.Add("17180 - ARNG ARMORY");
            ListOfFacilities.Add("11057 - Facility #2");

            SelectedFacility = ListOfFacilities.FirstOrDefault();

            UnFilteredCommentary.Add(new CommentaryType()
            {
                FacilityId = "11057",
                CodeIdText = "C102001",
                Rating = EnumRatingType.R,
                CommentText = "DAMAGED - All the wood doors have 70% severe moisture damage. " +
                              "CRACKED - All of the doors have 65% severe cracking and splintering."
            });

            UnFilteredCommentary.Add(new CommentaryType()
            {
                FacilityId = "17180",
                CodeIdText = "C102002",
                Rating = EnumRatingType.Y,
                CommentText = "HOLED - Holes have been punched thru 20% of the doors."
            });

            // Hook changes in the Facility list
            ListOfFacilities.CollectionChanged +=
                new NotifyCollectionChangedEventHandler(OnFacilitiesCollectionChanged);

            // Hook changes in the input source
            UnFilteredCommentary.CollectionChanged +=
                new NotifyCollectionChangedEventHandler(OnCommentaryCollectionChanged);

            OnChangeFilter();
        }

        // **************** Class members *************************************************** //

        private void OnCommentaryCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnChangeFilter();
        }

        private void OnFacilitiesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!ListOfFacilities.Contains(SelectedFacility))
                SelectedFacility = ListOfFacilities.FirstOrDefault();
        }

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

        private void OnNextButton()
        {
            Debug.WriteLine("OnNextButton is not implemented");
        }

        private void OnPrevButton()
        {
            Debug.WriteLine("OnPrevButton is not implemented");
        }

        private void OnChangeFilter()
        {
            IEnumerable<ICommentaryType> filterList = new List<ICommentaryType>(UnFilteredCommentary);

            // ??? don't know what to do here ???
            switch (FilterSource)
            {
                case EnumFilterSourceType.BredFilter:
                    break;

                case EnumFilterSourceType.SavedFilter:
                    if (!string.IsNullOrEmpty(SelectedFacility) &&
                        (SelectedFacility != "<ANY FACILITY>"))
                    {
                        var facilityId = SelectedFacility.Substring(0, 5);

                        filterList = filterList.Where(item =>
                            item.FacilityId == facilityId);
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(FilterSource),
                        FilterSource, @"Invalid FilterSource");
            }

            // ??? don't know what to do here ???
            switch (RelatedSource)
            {
                case EnumFilterRelatedType.SystemFilter:
                    break;

                case EnumFilterRelatedType.ComponentFilter:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(RelatedSource),
                        RelatedSource, @"Invalid RelatedSource");
            }

            switch (FilterRatingColor)
            {
                case EnumRatingColors.Green:
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
                    throw new ArgumentOutOfRangeException(nameof(FilterRatingColor),
                        FilterRatingColor, @"Invalid FilterRatingColor");
            }

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                filterList = filterList.Where(item =>
                    item.CommentText.Contains(SearchTerm));
            }

            FilteredCommentary.Clear();
            FilteredCommentary.AddRange(filterList);

            MatchingResultsText = "Matching Results: " + FilteredCommentary.Count;
        }
    }
}