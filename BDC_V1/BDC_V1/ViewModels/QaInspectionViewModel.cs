using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class QaInspectionViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //
        
        public ICommand CmdFilterButtonChecked { get; }
        
        public ICommand CmdRefresh     { get; }
        public ICommand CmdReviewIssue { get; }
        public ICommand CmdClearFilter { get; }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value, () => InspectionInfoView.Refresh());
        }
        private string _description;

        public EnumSortingFilter FilterSource
        {
            get => _filterSource;
            set => SetProperty(ref _filterSource, value);
        }
        private EnumSortingFilter _filterSource;

        public ObservableCollection<IssueInspection> InspectionInfo { get; } =
            new ObservableCollection<IssueInspection>();

        public ListCollectionView InspectionInfoView { get; } 

        // **************** Class data members ********************************************** //
        // **************** Class constructors ********************************************** //
        public QaInspectionViewModel()
        {
            RegionManagerName = "QaInspectionItemControl";

            CmdFilterButtonChecked  = new DelegateCommand<object>(OnFilterButtonChecked);
        
            CmdRefresh     = new DelegateCommand(OnCmdRefresh    );
            CmdReviewIssue = new DelegateCommand(OnCmdReviewIssue);
            CmdClearFilter = new DelegateCommand(OnCmdClearFilter);

            InspectionInfoView = new ListCollectionView(InspectionInfo);

#if DEBUG
#warning Using MOCK data for QaInspectionViewModel
            InspectionInfo.Clear();
            InspectionInfo.Add(new IssueInspection()
            {
                FacilityId   = "11057",
                SystemId     = "D30",
                ComponentId  = "D3010",
                TypeName     = "D301002",
                SectionName  = "N/A",
                Rating       = EnumRatingType.RPlus,
                InspectionComment = new CommentInspection
                {
                    EntryUser = new Person(),
                    EntryTime = new DateTime(),
                    CommentText = "Missing Photo"
                }
            });

            InspectionInfo.Add(new IssueInspection()
            {
                FacilityId   = "11057",
                SystemId     = "D30",
                ComponentId  = "D3010",
                TypeName     = "D301002",
                SectionName  = "N/A",
                Rating       = EnumRatingType.RPlus,
                InspectionComment = new CommentInspection
                {
                    EntryUser = new Person(),
                    EntryTime = new DateTime(),
                    CommentText = "Missing Comment"
                }
            });

            InspectionInfo.Add(new IssueInspection()
            {
                FacilityId   = "11057",
                SystemId     = "D30",
                ComponentId  = "D3020",
                TypeName     = "D302001",
                SectionName  = "N/A",
                Rating       = EnumRatingType.YMinus,
                InspectionComment = new CommentInspection
                {
                    EntryUser = new Person(),
                    EntryTime = new DateTime(),
                    CommentText = "Missing Inspection Comment photo for Y+ rating"
                }
            });

            InspectionInfo.Add(new IssueInspection()
            {
                FacilityId   = "11057",
                SystemId     = "D30",
                ComponentId  = "D3030",
                TypeName     = "D303001",
                SectionName  = "N/A",
                Rating       = EnumRatingType.None,
                InspectionComment = new CommentInspection
                {
                    EntryUser = new Person(),
                    EntryTime = new DateTime(),
                    CommentText = "Missing Inspection Comment"
                }
            });

            Description = "11057";
#endif
        }

        // **************** Class members *************************************************** //

        protected override bool GetRegionManager()
        {
            return false;
        }

        private void OnFilterButtonChecked(object item)
        {
            if (! (item is string headerText)) return;

            var description = headerText.Trim();
            foreach (EnumSortingFilter filterType in Enum.GetValues(typeof(EnumSortingFilter)))
            {
                if (filterType.Description() != description) continue;

                FilterSource = filterType;

                switch (filterType)
                {
                    case EnumSortingFilter.None:
                        InspectionInfoView.Filter = null;
                        break;

                    case EnumSortingFilter.FacilityId:
                        InspectionInfoView.Filter = (i) =>
                        {
                            if (i is IssueInspection issue)
                                return issue.FacilityId.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.SystemId:
                        InspectionInfoView.Filter = (i) =>
                        {
                            if (i is IssueInspection issue)
                                return issue.SystemId.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.InventoryId:
                        InspectionInfoView.Filter = (i) =>
                        {
                            if (i is IssueInspection issue)
                                return issue.ComponentId.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.TypeId:
                        InspectionInfoView.Filter = (i) =>
                        {
                            if (i is IssueInspection issue)
                                return issue.TypeName.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.SectionName:
                        InspectionInfoView.Filter = (i) =>
                        {
                            if (i is IssueInspection issue)
                                return issue.SectionName.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.InspectionIssue:
                        InspectionInfoView.Filter = (i) =>
                        {
                            if (i is IssueInspection issue)
                                return issue.InspectionComment.CommentText.IsLike(Description);

                            return true;
                        };
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                //InspectionInfoView.Refresh();
                break;
            }
        }

        private void OnCmdClearFilter()
        {
            InspectionInfoView.Filter = null;
            FilterSource = EnumSortingFilter.None;
            //Description = string.Empty;
            //InspectionInfoView.Refresh();
        }

        private void OnCmdRefresh    () { Debug.WriteLine("OnCmdRefresh     not implemented"); }
        private void OnCmdReviewIssue() { Debug.WriteLine("OnCmdReviewIssue not implemented"); }
    }
}
