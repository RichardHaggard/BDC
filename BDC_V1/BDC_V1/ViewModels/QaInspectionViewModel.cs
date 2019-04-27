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
    //<!-- TODO: Collapse QaInventoryView and QaInspectionView into a single source -->

    /// <inheritdoc />
    public class QaInspectionViewModel : QcViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //
        
        public ObservableCollection<IssueInspection> InspectionInfo { get; } =
            new ObservableCollection<IssueInspection>();

        // **************** Class data members ********************************************** //
        // **************** Class constructors ********************************************** //
        public QaInspectionViewModel()
        {
            RegionManagerName = "QaInspectionItemControl";

            ItemsView = new ListCollectionView(InspectionInfo);

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
                Rating       = EnumRatingType.AMinus,
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
    }
}
