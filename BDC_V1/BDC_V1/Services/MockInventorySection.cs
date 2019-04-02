using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;

namespace BDC_V1.Services
{
    public class MockInventorySection : InventorySectionType
    {
        public MockInventorySection()
        {
            // get rid of some bad binding messages
            Images.AddRange(Enumerable.Repeat(new BitmapImage(), 5));

            SectionNames.Add("Heating System");
            SectionName = "Heating System";

            EquipmentCategories.Add("D302001 BOILERS");
            EquipmentCategory = "D302001 BOILERS";

            ComponentTypes.Add("Permanent");
            ComponentType = "ComponentTypes";

            Quantity = "2.00";

            YearPc = "2007";

            PcTypes.Add("Heat-Resist 400 degF Enml");
            PcTypes.Add("Moderate 300 degF Enml");
            PcTypes.Add("Extreme 1400 degF Enml");
            PcType = "Heat-Resist 400 degF Enml";

            SectionComment = "[Jane Doe on 1/18/2018 6:19:55 PM]\nThis unit was in a locked room and not visible.\n(Text box large enough for STAMP on line 1 and at least 3 lines of actual comment.)";

            Date = DateTime.Now.ToShortDateString();

            Dcrs.Add("D1");
            Dcrs.Add("D2");
            Dcrs.Add("D3");
            Dcr = "D2";

            PcRatings.Add("PcRating1");
            PcRatings.Add("PcRating2");
            PcRatings.Add("PcRating3");
            PcRating = "PcRating2";
        }
    }

}
