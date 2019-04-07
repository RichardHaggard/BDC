using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;

namespace BDC_V1.Services
{
    public class MockInventorySection : InventorySection
    {
#if DEBUG
#warning Using MOCK data for InventorySection
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

            PcTypes.AddRange(new[]
            {
                "Heat-Resist 400 degF Enml",
                "Moderate 300 degF Enml",
                "Extreme 1400 degF Enml"
            });
            PcType = PcTypes[0];

            SectionComment = "[Jane Doe on 1/18/2018 6:19:55 PM]\nThis unit was in a locked room and not visible.\n(Text box large enough for STAMP on line 1 and at least 3 lines of actual comment.)";

            Date = DateTime.Now.ToShortDateString();

            Dcrs.AddRange(new[]
            {
                "D1",
                "D2",
                "D3"
            });
            Dcr = Dcrs[1];

            PcRatings.AddRange(new[]
            {
                "PcRating1",
                "PcRating2",
                "PcRating3"
            });
            PcRating = PcRatings[1];
        }
#endif
    }
}
