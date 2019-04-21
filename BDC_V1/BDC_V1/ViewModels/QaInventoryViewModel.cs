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
    //<!-- TODO: Collapse QaInventoryView and QaInspectionView into a single source -->

    /// <inheritdoc />
    public class QaInventoryViewModel : QcViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //
        
        public ObservableCollection<IssueInventory> InventoryInfo { get; } =
            new ObservableCollection<IssueInventory>();

        // **************** Class data members ********************************************** //

        // **************** Class constructors ********************************************** //
        public QaInventoryViewModel()
        {
            RegionManagerName = "QaInventoryItemControl";

            ItemsView = new ListCollectionView(InventoryInfo);

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
    }
}
