using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Enumerations;

namespace BDC_V1.Services
{
    public class MockInspectionInfo : InspectionInfo
    {
#if DEBUG
#warning Using MOCK data for InspectionInfo
        public MockInspectionInfo()
        {
            // get rid of some bad binding messages
            Images.AddRange(Enumerable.Repeat(new BitmapImage(), 5));

            InspectionType    = EnumInspectionType.DirectRating;
            Component         = "D3020 - HEAT GENERATING SYSTEMS";
            Section           = "Northside";
            Category          = "D302001 BOILERS";
            ComponentType     = "General";
            Quantity          = 2000.00M;
            InspectionDate    = DateTime.Parse("12/18/2012");
            Note              = "Note: InspectionComment Comment & Photo required.";

            InspectionComments.Add(
                new CommentInspection
                {
                    EntryUser = new Person("Darrell", "Setser"),
                    EntryTime = new DateTime(2018, 1, 18, 18, 19, 55),
                    CommentText = @"DAMAGED - All the wood doors have 70% severe moisture damage.  CRACKED - All of the doors have 65% severe cracking and splintering. " +
                                  @"Replacement is recommended."
                });
        }
#endif
    }
}
