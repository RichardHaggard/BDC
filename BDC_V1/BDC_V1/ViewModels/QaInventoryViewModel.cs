using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Converters;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class QaInventoryViewModel : ViewModelBase
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
            set => SetProperty(ref _description, value, () => InventoryInfoView.Refresh());
        }
        private string _description;

        public EnumSortingFilter FilterSource
        {
            get => _filterSource;
            set => SetProperty(ref _filterSource, value);
        }
        private EnumSortingFilter _filterSource;

        public ObservableCollection<IssueInventory> InventoryInfo { get; } =
            new ObservableCollection<IssueInventory>();

        public ListCollectionView InventoryInfoView { get; } 

        // **************** Class data members ********************************************** //

        // **************** Class constructors ********************************************** //
        public QaInventoryViewModel()
        {
            RegionManagerName = "QaInventoryItemControl";

            CmdFilterButtonChecked  = new DelegateCommand<object>(OnFilterButtonChecked);
        
            CmdRefresh     = new DelegateCommand(OnCmdRefresh    );
            CmdReviewIssue = new DelegateCommand(OnCmdReviewIssue);
            CmdClearFilter = new DelegateCommand(OnCmdClearFilter);

            InventoryInfoView = new ListCollectionView(InventoryInfo);

            //InventoryInfoView.SortDescriptions .Add(new SortDescription ());
            //InventoryInfoView.GroupDescriptions.Add(new GroupDescription());

#if DEBUG
#warning Using MOCK data for QaInventoryViewModel
            InventoryInfo.Clear();
            InventoryInfo.Add(new IssueInventory()
            {
                FacilityId = "11057",
                SystemId = "D30",
                ComponentId = "D3010",
                TypeName = "",
                SectionName = "",
                InventoryComment = new CommentInventory
                {
                    EntryUser = new Person(),
                    EntryTime = new DateTime(),
                    CommentText = "Missing Section"
                }
            });

            InventoryInfo.Add(new IssueInventory()
            {
                FacilityId = "11057",
                SystemId = "D30",
                ComponentId = "D3010",
                TypeName = "D302001",
                SectionName = "N/A",
                InventoryComment = new CommentInventory
                {
                    EntryUser = new Person(),
                    EntryTime = new DateTime(),
                    CommentText = "Missing Photo"
                }
            });

            InventoryInfo.Add(new IssueInventory()
            {
                FacilityId = "11444",
                SystemId = "A10",
                ComponentId = "A1010",
                TypeName = "A101001",
                SectionName = "Boilers",
                InventoryComment = new CommentInventory
                {
                    EntryUser = new Person(),
                    EntryTime = new DateTime(),
                    CommentText = "This is a really, really long comment which should engage the auto-wrap feature of the last column\n" +
                                  "It also has an embedded newline character to test that as well"
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
                        InventoryInfoView.Filter = null;
                        break;

                    case EnumSortingFilter.FacilityId:
                        InventoryInfoView.Filter = (i) =>
                        {
                            if (i is IssueInventory issue)
                                return issue.FacilityId.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.SystemId:
                        InventoryInfoView.Filter = (i) =>
                        {
                            if (i is IssueInventory issue)
                                return issue.SystemId.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.InventoryId:
                        InventoryInfoView.Filter = (i) =>
                        {
                            if (i is IssueInventory issue)
                                return issue.ComponentId.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.TypeId:
                        InventoryInfoView.Filter = (i) =>
                        {
                            if (i is IssueInventory issue)
                                return issue.TypeName.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.SectionName:
                        InventoryInfoView.Filter = (i) =>
                        {
                            if (i is IssueInventory issue)
                                return issue.SectionName.IsLike(Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.InventoryIssue:
                        InventoryInfoView.Filter = (i) =>
                        {
                            if (i is IssueInventory issue)
                                return issue.InventoryComment.CommentText.IsLike(Description);

                            return true;
                        };
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                //InventoryInfoView.Refresh();
                break;
            }
        }

        private void OnCmdClearFilter()
        {
            InventoryInfoView.Filter = null;
            FilterSource = EnumSortingFilter.None;
            //Description = string.Empty;
            //InventoryInfoView.Refresh();
        }

        private void OnCmdRefresh    () { Debug.WriteLine("OnCmdRefresh     not implemented"); }
        private void OnCmdReviewIssue() { Debug.WriteLine("OnCmdReviewIssue not implemented"); }
    }
}
