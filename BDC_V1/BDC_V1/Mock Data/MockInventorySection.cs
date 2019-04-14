using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Utils;

namespace BDC_V1.Mock_Data
{
    public class MockInventorySection : InventorySection
    {
#if DEBUG
#warning Using MOCK data for InventorySection
        public MockInventorySection()
        {
            SectionNames.Add("Heating System");
            SectionName = "Heating System";

            EquipmentCategories.Add("D302001 BOILERS");
            EquipmentCategory = "D302001 BOILERS";

            // TODO: CHange this into an enumeration
            ComponentTypes.Add("Permanent");
            ComponentTypes.Add("Temporary");
            ComponentType = "ComponentTypes";

            Quantity = "2.00";

            YearPc = "2007";

            // TODO: CHange this into an enumeration
            PcTypes.AddRange(new[]
            {
                "Heat-Resist 400 degF Enml",
                "Moderate 300 degF Enml",
                "Extreme 1400 degF Enml"
            });
            PcType = PcTypes[0];

            SectionComments.Add(new CommentBase
            {
                EntryUser = new Person {FirstName = "Jane", LastName = "Doe"},
                EntryTime = new DateTime(2018, 1, 18, 18, 19, 55),
                CommentText = "[Jane Doe on 1/18/2018 6:19:55 PM]\nThis unit was in a locked room and not visible.\n" +
                              "(Text box large enough for STAMP on line 1 and at least 3 lines of actual comment.)\n" +
                              "(Line 2)\n" +
                              "(Line 3)\n"
            });

            Date = DateTime.Now.ToShortDateString();

            foreach (EnumRatingType rating in Enum.GetValues(typeof(EnumRatingType)))
            {
                if (rating == EnumRatingType.None) continue;

                Dcrs     .Add(rating.Description());
                PcRatings.Add(rating.Description());
            }
            Dcr      = Dcrs[1];
            PcRating = PcRatings[1];

            Images.AddRange(new[]
            {
                new BitmapImage(new Uri(@"pack://application:,,,/Images/th4.jpg")),
                new BitmapImage(new Uri(@"pack://application:,,,/Images/th5.jpg")),
                new BitmapImage(new Uri(@"pack://application:,,,/Images/th6.jpg"))
            });
        }
#endif
    }
}
