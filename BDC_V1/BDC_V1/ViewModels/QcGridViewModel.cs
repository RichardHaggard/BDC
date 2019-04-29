using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Mock_Data;
using BDC_V1.Utils;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    class QcGridViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //
        
        [NotNull] public ICommand CmdFilterButtonChecked { get; }
        
        [NotNull] public ICommand CmdRefresh     { get; }
        [NotNull] public ICommand CmdReviewIssue { get; }
        [NotNull] public ICommand CmdClearFilter { get; }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value, () => GridItemsView.Refresh());
        }
        private string _description;

        public EnumSortingFilter FilterSource
        {
            get => _filterSource;
            set => SetProperty(ref _filterSource, value);
        }
        private EnumSortingFilter _filterSource;

        public bool HasRatings
        {
            get => _hasRatings.Equals(true);
            set => SetProperty(ref _hasRatings, value, () =>
            {
                RaisePropertyChanged(nameof(CommentType));
#if DEBUG
                GridItemsView.Collection.AddRange(_hasRatings.Equals(true)
                    ? MockQcInspectionData.InspectionData
                    : MockQcInventoryData .InventoryData );

                Description = _hasRatings.Equals(true)
                    ? MockQcInspectionData.Description
                    : MockQcInventoryData .Description;
#endif
            });
        }
        private bool? _hasRatings = null;

        public EnumSortingFilter CommentType => HasRatings
            ? EnumSortingFilter.InspectionIssue
            : EnumSortingFilter.InventoryIssue;

        public IndexedCollection<IQcIssueBase> GridItemsView { get; } =
           new IndexedCollection<IQcIssueBase>(new ObservableCollection<IQcIssueBase>());

        // **************** Class data members ********************************************** //

        // **************** Class constructors ********************************************** //
        public QcGridViewModel()
        {
            RegionManagerName = "QcGridViewModel";

            CmdFilterButtonChecked  = new DelegateCommand<object>(OnFilterButtonChecked);
        
            CmdRefresh     = new DelegateCommand(OnCmdRefresh    );
            CmdReviewIssue = new DelegateCommand(OnCmdReviewIssue);
            CmdClearFilter = new DelegateCommand(OnCmdClearFilter);
        }

        // **************** Class members *************************************************** //

        // **************** Class members *************************************************** //

        protected virtual void OnFilterButtonChecked(object item)
        {
            if (! (item is string headerText)) 
#if DEBUG
                throw new InvalidCastException($@"Cannot convert {nameof(item)} to a string");
#else
                return;
#endif
            var header = headerText.Trim();
            if (! DescriptionToEnum.TryParse<EnumSortingFilter>(header, out var filterType))
            {
                GridItemsView.Filter = null;
                FilterSource = EnumSortingFilter.None;
                return;
            }

            switch (FilterSource = filterType)
            {
                case EnumSortingFilter.FacilityId:
                    GridItemsView.Filter = (i) => ((IQcIssueBase) i).FacilityId.IsLike(Description);
                    break;

                case EnumSortingFilter.SystemId:
                    GridItemsView.Filter = (i) => ((IQcIssueBase) i).SystemId.IsLike(Description);
                    break;

                case EnumSortingFilter.InventoryId:
                    GridItemsView.Filter = (i) => ((IQcIssueBase) i).ComponentId.IsLike(Description);
                    break;

                case EnumSortingFilter.TypeId:
                    GridItemsView.Filter = (i) => ((IQcIssueBase) i).TypeName.IsLike(Description);
                    break;

                case EnumSortingFilter.SectionName:
                    GridItemsView.Filter = (i) => ((IQcIssueBase) i).SectionName.IsLike(Description);
                    break;

                case EnumSortingFilter.InspectionIssue:
                case EnumSortingFilter.InventoryIssue:
                    GridItemsView.Filter = (i) => ((IQcIssueBase) i).Comment.CommentText.IsLike(Description);
                    break;

                default:
#if DEBUG
                    throw new ArgumentOutOfRangeException();
#endif
                case EnumSortingFilter.Rating:
                case EnumSortingFilter.None:
                    GridItemsView.Filter = null;
                    break;

            }
        }

        protected virtual void OnCmdClearFilter()
        {
            GridItemsView.Filter = null;
            FilterSource = EnumSortingFilter.None;
        }

        protected virtual void OnCmdRefresh    () { Debug.WriteLine("OnCmdRefresh     not implemented"); }
        protected virtual void OnCmdReviewIssue() { Debug.WriteLine("OnCmdReviewIssue not implemented"); }
    }
}
