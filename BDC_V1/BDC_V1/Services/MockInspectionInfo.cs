using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Enumerations;

namespace BDC_V1.Services
{
    public class MockInspectionInfo : InspectionInfoType
    {
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
            Note              = "Note: Inspection Comment & Photo required.";
            InspectionComment = 
                @"[Darrell Setser on 1/18/2018 6:19:55 PM]" +
                @"DAMAGED - All the wood doors have 70% severe moisture damage.  CRACKED - All of the doors have 65% severe cracking and splintering. " +
                @"Replacement is recommended.";
        }
    }
}
