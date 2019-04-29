using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;

namespace BDC_V1.Mock_Data
{
    public static class MockQcInspectionData
    {
        public static IList<IQcIssueBase> InspectionData { get; }
        public static string              Description => "11057";

        static MockQcInspectionData()
        {
            InspectionData = new List<IQcIssueBase>
            {
                new IssueInspection()
                {
                    FacilityId = "11057",
                    SystemId = "D30",
                    ComponentId = "D3010",
                    TypeName = "D301002",
                    SectionName = "N/A",
                    Rating = EnumRatingType.RPlus,
                    InspectionComment = new CommentInspection
                    {
                        EntryUser = new Person(), 
                        EntryTime = new DateTime(), 
                        CommentText = "Missing Photo"
                    }
                },
                new IssueInspection()
                {
                    FacilityId = "11057",
                    SystemId = "D30",
                    ComponentId = "D3010",
                    TypeName = "D301002",
                    SectionName = "N/A",
                    Rating = EnumRatingType.RPlus,
                    InspectionComment = new CommentInspection
                    {
                        EntryUser = new Person(),
                        EntryTime = new DateTime(),
                        CommentText = "Missing Comment"
                    }
                },
                new IssueInspection()
                {
                    FacilityId = "11057",
                    SystemId = "D30",
                    ComponentId = "D3020",
                    TypeName = "D302001",
                    SectionName = "N/A",
                    Rating = EnumRatingType.AMinus,
                    InspectionComment = new CommentInspection
                    {
                        EntryUser = new Person(),
                        EntryTime = new DateTime(),
                        CommentText = "Missing Inspection Comment photo for Y+ rating"
                    }
                },
                new IssueInspection()
                {
                    FacilityId = "11057",
                    SystemId = "D30",
                    ComponentId = "D3030",
                    TypeName = "D303001",
                    SectionName = "N/A",
                    Rating = EnumRatingType.None,
                    InspectionComment = new CommentInspection
                    {
                        EntryUser = new Person(),
                        EntryTime = new DateTime(),
                        CommentText = "Missing Inspection Comment"
                    }
                }
            };
        }
    }
}
