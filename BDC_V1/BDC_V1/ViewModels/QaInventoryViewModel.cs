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
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class QaInventoryViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        [DefaultValue(None)]
        public enum EnumSortingFilter
        {
            None,

            [Description("Facility ID")]
            FacilityId,

            [Description("System ID")]
            SystemId,

            [Description("Inventory")]
            InventoryId,

            [Description("Type")]
            TypeId,

            [Description("Section")]
            SectionName,

            [Description("Inventory Issue")]
            InventoryIssue,
        }

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //
        
        public ICommand CmdFilterButtonChecked { get; }
        
        public ICommand CmdRefresh     { get; }
        public ICommand CmdReviewIssue { get; }
        public ICommand CmdClearFilter { get; }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value, () =>
            {
                InventoryInfoView.Refresh();
            });
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

            InventoryInfoView = new ListCollectionView(InventoryInfo)
            {
                Filter = InventoryFilter
            };

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

            Description = "11057";
#endif
        }

        // **************** Class members *************************************************** //

        private bool InventoryFilter(object item)
        {
            if (!(item is IssueInventory issue)) return true;

            return true;
        }

        protected override bool GetRegionManager()
        {
            return false;
        }

        private bool IsLike(string src, string pat)
        {
            if (src.Contains  (pat)) return true; // like '%pat%'
            if (src.StartsWith(pat)) return true; // like 'pat%'
            if (src.EndsWith  (pat)) return true; // like '%pat'

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
                        InventoryInfoView.Filter = (i) => true;
                        break;

                    case EnumSortingFilter.FacilityId:
                        InventoryInfoView.Filter = (i) =>
                        {
                            if (i is IssueInventory issue)
                                return IsLike(issue.FacilityId, Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.SystemId:
                        InventoryInfoView.Filter = (i) =>
                        {
                            if (i is IssueInventory issue)
                                return IsLike(issue.SystemId, Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.InventoryId:
                        InventoryInfoView.Filter = (i) =>
                        {
                            if (i is IssueInventory issue)
                                return IsLike(issue.ComponentId, Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.TypeId:
                        InventoryInfoView.Filter = (i) =>
                        {
                            if (i is IssueInventory issue)
                                return IsLike(issue.TypeName, Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.SectionName:
                        InventoryInfoView.Filter = (i) =>
                        {
                            if (i is IssueInventory issue)
                                return IsLike(issue.SectionName, Description);

                            return true;
                        };
                        break;

                    case EnumSortingFilter.InventoryIssue:
                        InventoryInfoView.Filter = (i) =>
                        {
                            if (i is IssueInventory issue)
                                return IsLike(issue.InventoryComment.CommentText, 
                                    Description);

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

        private void OnCmdRefresh    () { Debug.WriteLine("OnCmdRefresh     not implemented"); }
        private void OnCmdReviewIssue() { Debug.WriteLine("OnCmdReviewIssue not implemented"); }

        private void OnCmdClearFilter()
        {
            InventoryInfoView.Filter = (i) => true;
            FilterSource = EnumSortingFilter.None;
            Description = string.Empty;

            //InventoryInfoView.Refresh();
        }
    }
}
