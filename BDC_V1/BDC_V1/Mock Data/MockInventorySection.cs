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
        public MockInventorySection()
        {
            SectionNames.AddRange(new []
            {
                "Heating System",
                "Floor Finish",
                "Energy Supply"
            });
            SectionName = SectionNames[0];

            EquipmentCategories.AddRange(new[]
            {
                "D302001 BOILERS",
                "D301002 GAS SUPPLY SYSTEM - General",
                "NORTH BAY - C302001 TILE FLOOR FINISHES - General",
                "WEST BAY - C302001 TILE FLOOR FINISHES - General"
            });
            EquipmentCategory = EquipmentCategories[0];

            // TODO: Change this into an enumeration
            ComponentTypes.AddRange(new[]
            {
                "Permanent",
                "Temporary"
            });
            ComponentType = ComponentTypes[0];

            Quantity = "2.00";

            YearPc = "2007";

            // TODO: Change this into an enumeration
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
                CommentText = "This unit was in a locked room and not visible.\n" +
                              "(Text box large enough for STAMP on line 1 and at least 3 lines of actual comment.)\n" +
                              "(Line 3)"
            });

            Date = DateTime.Now.ToShortDateString();

            foreach (EnumRatingType rating in Enum.GetValues(typeof(EnumRatingType)))
            {
                switch (rating)
                {
                    case EnumRatingType.GPlus:
                    case EnumRatingType.G:
                    case EnumRatingType.GMinus:
                    case EnumRatingType.APlus:
                    case EnumRatingType.A:
                    case EnumRatingType.AMinus:
                    case EnumRatingType.RPlus:
                    case EnumRatingType.R:
                    case EnumRatingType.RMinus:
                        Dcrs     .Add(rating.Description());
                        PcRatings.Add(rating.Description());
                        break;

                    case EnumRatingType.None:
                        break;

                    // 4/19: G:IS51 - Change DCR and P/C Rating amber colors to Yellow. Text becomes Y+, Y, Y-.
                    // 4/30: G:IS51 - Nope. Ditch the Yellow. Keep the Amber.
                    case EnumRatingType.YPlus:
                    case EnumRatingType.Y:
                    case EnumRatingType.YMinus:
                        break;
#if DEBUG
                    default:
                        throw new ArgumentOutOfRangeException();
#endif
                }
            }
            Dcr      = Dcrs     [1];
            PcRating = PcRatings[1];

            Images.AddRange(new[]
            {
                new BitmapImage(new Uri(@"pack://application:,,,/Images/th4.jpg")),
                new BitmapImage(new Uri(@"pack://application:,,,/Images/th5.jpg")),
                new BitmapImage(new Uri(@"pack://application:,,,/Images/th6.jpg"))
            });

            YearInstalledRenewed = "2007";
        }
#endif
    }
}
