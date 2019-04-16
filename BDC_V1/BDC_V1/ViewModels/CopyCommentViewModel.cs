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
    public class CopyCommentViewModel : CloseableWindow
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

        public ICommand CmdNextButton   { get; }
        public ICommand CmdPrevButton   { get; }
        public ICommand CmdCopyButton   { get; }
        public ICommand CmdCancelButton { get; }

        public string WindowTitle
        {
            get => _windowTitle;
            set => SetProperty(ref _windowTitle, value);
        }
        private string _windowTitle = "COPY COMMENTS";

        // TODO: Move these properties into a separate interface / class

        [CanBeNull]
        public string SelectedFacility
        {
            get => (FilterSource == EnumFilterSourceType.SavedFilter)
                ? _selectedFacility
                : ListOfFacilities.FirstOrDefault()?.ToString();

            set => SetProperty(ref _selectedFacility, value, OnChangeFilter);
        }
        private string _selectedFacility;

        public string SearchTerm
        {
            get => _searchTerm;
            set => SetProperty(ref _searchTerm, value, OnChangeFilter);
        }
        private string _searchTerm;

        public EnumFilterSourceType FilterSource
        {
            get => _filterSource;
            set => SetProperty(ref _filterSource, value, OnChangeFilter);
        }
        private EnumFilterSourceType _filterSource;

        public EnumFilterRelatedType RelatedSource
        {
            get => _relatedSource;
            set => SetProperty(ref _relatedSource, value, OnChangeFilter);
        }
        private EnumFilterRelatedType _relatedSource;

        public EnumRatingColors FilterRatingColor
        {
            get => _filterRatingColor;
            set => SetProperty(ref _filterRatingColor, value, OnChangeFilter);
        }
        private EnumRatingColors _filterRatingColor;

        public string MatchingResultsText
        {
            get => _matchingResultsText;
            set => SetProperty(ref _matchingResultsText, value);
        }
        private string _matchingResultsText;

        public ObservableCollection<IComponentFacility> ListOfFacilities { get; } =
            new ObservableCollection<IComponentFacility>();

        public ObservableCollection<Commentary> FilteredCommentary { get; } =
            new ObservableCollection<Commentary>();

        public ObservableCollection<Commentary> UnFilteredCommentary { get; } =
            new ObservableCollection<Commentary>();


        public IFacilityBase FacilityBaseInfo
        {
            get => _facilityBaseInfo;
            set => SetProperty(ref _facilityBaseInfo, value, () =>
            {
                UnFilteredCommentary.Clear();
                FilteredCommentary  .Clear();
                ListOfFacilities    .Clear();
                SelectedFacility = null;

                if (!((_facilityBaseInfo?.Facilities == null) || (_facilityBaseInfo.Facilities.Count == 0)))
                {
                    ListOfFacilities.AddRange(_facilityBaseInfo.Facilities);

                    foreach (var facility in ListOfFacilities)
                        FindComments(facility);
                }

                RaisePropertyChanged(new []
                {
                    nameof(UnFilteredCommentary),
                    nameof(FilteredCommentary),
                    nameof(ListOfFacilities)
                });
            });
        }
        private IFacilityBase _facilityBaseInfo;


        // **************** Class constructors ********************************************** //

        public CopyCommentViewModel()
        {
            CmdNextButton   = new DelegateCommand(OnNextButton);
            CmdPrevButton   = new DelegateCommand(OnPrevButton);
            CmdCopyButton   = new DelegateCommand(OnCopyButton);
            CmdCancelButton = new DelegateCommand(OnCancelButton);

            FilterSource      = EnumFilterSourceType.BredFilter;
            RelatedSource     = EnumFilterRelatedType.SystemFilter;
            FilterRatingColor = EnumRatingColors.Green;
            SearchTerm = "";

            // Hook changes in the Facility list
            ListOfFacilities.CollectionChanged +=
                new NotifyCollectionChangedEventHandler(OnFacilitiesCollectionChanged);

            // Hook changes in the Commentary list changes
            UnFilteredCommentary.CollectionChanged +=
                new NotifyCollectionChangedEventHandler(OnCommentaryCollectionChanged);
        }

        // **************** Class members *************************************************** //

        private void FindComments([CanBeNull] IComponentBase component)
        {
            if (component == null) return;

            if ((component is IComponentFacility facility) && (facility.HasFacilityComments))
                UnFilteredCommentary.AddRange(facility.FacilityComments.Cast<Commentary>());

            if ((component is IComponentSection section) && (section.HasComments))
            {
                //UnFilteredCommentary.AddRange(section.Comments.Cast<Commentary>());
            }
             
            if ((component is IComponentSystem system) && (system.HasComments))
            {
                //UnFilteredCommentary.AddRange(system.Comments.Cast<Commentary>());
            }
             
            if (component is IComponentInventory inventory)
            {
                if (inventory.Detail.HasDetailComments)
                    UnFilteredCommentary.AddRange(inventory.Detail.DetailComments.Cast<Commentary>());

                if (inventory.Section.HasSectionComments)
                    UnFilteredCommentary.AddRange(inventory.Section.SectionComments.Cast<Commentary>());
            }

            if (component.HasChildren)
            {
                foreach (var child in component.Children)
                    FindComments(child);
            }
        }

        private void OnCommentaryCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnChangeFilter();
        }

        private void OnFacilitiesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ListOfFacilities.Any(item => item.ToString() == SelectedFacility) == false)
                SelectedFacility = ListOfFacilities.FirstOrDefault()?.ToString();
        }

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
            IEnumerable<Commentary> filterList = new List<Commentary>(UnFilteredCommentary);

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
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException(nameof(FilterSource),
                        FilterSource, @"Invalid FilterSource");
#endif
            }

            // ??? don't know what to do here ???
            switch (RelatedSource)
            {
                case EnumFilterRelatedType.SystemFilter:
                    break;

                case EnumFilterRelatedType.ComponentFilter:
                    break;
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException(nameof(RelatedSource),
                        RelatedSource, @"Invalid RelatedSource");
#endif
            }

            switch (FilterRatingColor)
            {
                case EnumRatingColors.None:
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
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException(nameof(FilterRatingColor),
                        FilterRatingColor, @"Invalid FilterRatingColor");
#endif
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