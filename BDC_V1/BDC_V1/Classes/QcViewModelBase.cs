using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.Classes
{
    public abstract class QcViewModelBase : ViewModelBase
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
            set => SetProperty(ref _description, value, () => ItemsView.Refresh());
        }
        private string _description;

        public EnumSortingFilter FilterSource
        {
            get => _filterSource;
            set => SetProperty(ref _filterSource, value);
        }
        private EnumSortingFilter _filterSource;

        public ListCollectionView ItemsView { get; protected set; } 

        // **************** Class data members ********************************************** //
        // **************** Class constructors ********************************************** //
        protected QcViewModelBase()
        {
            CmdFilterButtonChecked  = new DelegateCommand<object>(OnFilterButtonChecked);
        
            CmdRefresh     = new DelegateCommand(OnCmdRefresh    );
            CmdReviewIssue = new DelegateCommand(OnCmdReviewIssue);
            CmdClearFilter = new DelegateCommand(OnCmdClearFilter);
        }

        // **************** Class members *************************************************** //

        protected virtual void OnFilterButtonChecked(object item)
        {
            if (! (item is string headerText)) return;

            var header = headerText.Trim();
            if (DescriptionToEnum.TryParse<EnumSortingFilter>(header, out var filterType))
            {
                FilterSource = filterType;

                switch (filterType)
                {
                    case EnumSortingFilter.Rating:
                    case EnumSortingFilter.None:
                        ItemsView.Filter = null;
                        break;

                    case EnumSortingFilter.FacilityId:
                        ItemsView.Filter = (i) =>
                        {
                            if (i is IIssueInspection issue)
                                return issue.FacilityId.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.SystemId:
                        ItemsView.Filter = (i) =>
                        {
                            if (i is IIssueInspection issue)
                                return issue.SystemId.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.InventoryId:
                        ItemsView.Filter = (i) =>
                        {
                            if (i is IIssueInspection issue)
                                return issue.ComponentId.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.TypeId:
                        ItemsView.Filter = (i) =>
                        {
                            if (i is IIssueInspection issue)
                                return issue.TypeName.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.SectionName:
                        ItemsView.Filter = (i) =>
                        {
                            if (i is IIssueInspection issue)
                                return issue.SectionName.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.InspectionIssue:
                        ItemsView.Filter = (i) =>
                        {
                            if (i is IIssueInspection issue)
                                return issue.InspectionComment.CommentText.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.InventoryIssue:
                        ItemsView.Filter = (i) =>
                        {
                            if (i is IIssueInventory issue)
                                return issue.InventoryComment.CommentText.IsLike(Description);

                            return true;
                        };
                        break;
#if DEBUG
                    default:
                        throw new ArgumentOutOfRangeException();
#endif
                }

                //InspectionInfoView.Refresh();
            }
        }

        protected virtual void OnCmdClearFilter()
        {
            ItemsView.Filter = null;
            FilterSource = EnumSortingFilter.None;
            //Description = string.Empty;
            //InspectionInfoView.Refresh();
        }

        protected virtual void OnCmdRefresh    () { Debug.WriteLine("OnCmdRefresh     not implemented"); }
        protected virtual void OnCmdReviewIssue() { Debug.WriteLine("OnCmdReviewIssue not implemented"); }
    }
}
