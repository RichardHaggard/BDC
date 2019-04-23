using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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
    public class CopyCommentViewModel : CloseableResultsWindow
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

        [NotNull] public ICommand CmdNextButton   { get; }
        [NotNull] public ICommand CmdPrevButton   { get; }
        [NotNull] public ICommand CmdCopyButton   { get; }
        [NotNull] public ICommand CmdCancelButton { get; }

        public string WindowTitle
        {
            get => _windowTitle;
            set => SetProperty(ref _windowTitle, value);
        }
        private string _windowTitle = "COPY COMMENTS";

        // TODO: Move these properties into a separate interface / class

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

        public IndexedCollection<IComponentFacilityHeader> ListOfFacilities { get; } =
            new IndexedCollection<IComponentFacilityHeader>(new ObservableCollection<IComponentFacilityHeader>());

        private IndexedCollection<ICommentary> _filteredCommentary;
        public IndexedCollection<ICommentary> FilteredCommentary => _filteredCommentary ??
            (_filteredCommentary = new IndexedCollection<ICommentary>(UnFilteredCommentary));
       
        public ObservableCollection<ICommentary> UnFilteredCommentary { get; } =
            new ObservableCollection<ICommentary>();

        public IFacilityBase FacilityBaseInfo
        {
            get => _facilityBaseInfo;
            set => SetProperty(ref _facilityBaseInfo, value, () =>
            {
                UnFilteredCommentary.Clear();

                // cannot directly manipulate a ListCollectionView at initialization,
                // manipulate the base collection instead
                ListOfFacilities.SelectedIndex = -1;
                ListOfFacilities.Collection.Clear();

                if (!((_facilityBaseInfo?.Facilities == null) || (_facilityBaseInfo.Facilities.Count == 0)))
                {
                    ListOfFacilities.Collection.AddRange(_facilityBaseInfo.Facilities);

                    foreach (var facility in ListOfFacilities.Collection)
                        FindComments(facility);
                }

                RaisePropertyChanged(new []
                {
                    nameof(UnFilteredCommentary),
                    nameof(ListOfFacilities)
                });

                FilteredCommentary.Refresh();
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
            SearchTerm        = string.Empty;

#if DEBUG
            ListOfFacilities.Collection.AddRange(new[]
            {
                new ComponentFacilityHeader
                {
                    BuildingIdNumber = 11507,
                    BuildingId   = "ARMRY",
                    BuildingName = "National Guard Readiness Center"
                },

                new ComponentFacilityHeader
                {
                    BuildingIdNumber = 11444,
                    BuildingId   = "Facility # 2",
                    BuildingName = "Facility # 2"
                } 
            });
#endif
            // Hook changes in the Commentary list changes
            UnFilteredCommentary.CollectionChanged += OnCommentaryCollectionChanged;
        }

        // **************** Class members *************************************************** //

        private void FindComments([CanBeNull] IComponentBase component)
        {
            switch (component)
            {
                case null: return;

                case IComponentFacility facility when (facility.HasFacilityComments):
                    UnFilteredCommentary.AddRange(facility.FacilityComments.Cast<ICommentary>());
                    break;

                case IComponentSection section when (section.HasComments):
                    //UnFilteredCommentary.AddRange(section.Comments.Cast<ICommentary>());
                    break;

                case IComponentSystem system when (system.HasComments):
                    //UnFilteredCommentary.AddRange(system.Comments.Cast<ICommentary>());
                    break;
            }

            if (component is IComponentInventory inventory)
            {
                if (inventory.Detail.HasDetailComments)
                    UnFilteredCommentary.AddRange(inventory.Detail.DetailComments.Cast<ICommentary>());

                if (inventory.Section.HasSectionComments)
                    UnFilteredCommentary.AddRange(inventory.Section.SectionComments.Cast<ICommentary>());
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
            var filter = PredicateBuilder.True<ICommentary>();

            // ??? don't know what to do here ???
            switch (FilterSource)
            {
                case EnumFilterSourceType.BredFilter:
                    break;

                case EnumFilterSourceType.SavedFilter:
                    if (ListOfFacilities.SelectedIndex != -1)
                    {
                        var facilityId = ListOfFacilities.SelectedItem?.BuildingIdNumber.ToString();

                        Expression<Func<ICommentary, bool>> savedFilter =
                            item => item.FacilityId == facilityId;

                        filter.And(savedFilter);
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

                case EnumRatingColors.Amber:
                case EnumRatingColors.Yellow:
                case EnumRatingColors.Red:
                {
                    Expression<Func<ICommentary, bool>> ratingColorFilter =
                        item => item.Rating.ToRatingColor() == FilterRatingColor;

                    filter.And(ratingColorFilter);
                    break;
                } 
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException(nameof(FilterRatingColor),
                        FilterRatingColor, @"Invalid FilterRatingColor");
#endif
            }

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Expression<Func<ICommentary, bool>> savedFilter =
                    item => item.CommentText.Contains(SearchTerm);

                filter.And(savedFilter);
            }

            var func = filter.Compile();
            var predicate = new Predicate<ICommentary>(func);
            FilteredCommentary.Filter = (Predicate<object>) predicate;

            MatchingResultsText = $@"Matching Results: {FilteredCommentary.Count}";
        }
    }
}