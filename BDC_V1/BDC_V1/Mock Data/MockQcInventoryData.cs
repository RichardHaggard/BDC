using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using BDC_V1.Interfaces;

namespace BDC_V1.Mock_Data
{
    public static class MockQcInventoryData
    {
        public static IList<IQcIssueBase> InventoryData { get; }
        public static string              Description => "11057";

        static MockQcInventoryData()
        {
            InventoryData = new List<IQcIssueBase>
            {
                new IssueInventory()
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
                },
                new IssueInventory()
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
                },
                new IssueInventory()
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
                        CommentText =
                            "This is a really, really long comment which should engage the auto-wrap feature of the last column\n" +
                            "It also has an embedded newline character to test that as well"
                    }
                }
            };
        }
    }
}
