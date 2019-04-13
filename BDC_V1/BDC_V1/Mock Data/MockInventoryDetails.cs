using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;

namespace BDC_V1.Mock_Data
{
    public class MockInventoryDetails : InventoryDetail
    {
#if DEBUG
#warning Using MOCK data for InventoryDetail
        public MockInventoryDetails()
        {
            CurrentSection = "11057 - EAST BAY - D501003 INTERIOR DISTRIBUTION SERVICES dry-type, 480V primary 120/208V secondary, 225kVA";

            DetailSelectors.Add("FL2 - TL12412C - GE - Rm207 - DAMAGED - The nameplate on component was missing ...");
            DetailSelectors.Add("AB3 - GH504xx - AP - Rm111 - DAMAGE - Scratched and defaced");
            DetailSelectors.Add("XY8 - OP583C - PD - Rm456 - SOMET - In very poor taste");

            DetailSelector = "FL2 - TL12412C - GE - Rm207 - DAMAGED - The nameplate on component was missing ...";

            DetailIdNumber = "N/A";

            Location = "Rm 107";

            Model = "TL12412C";

            SerialNumber = "";

            Manufacturers.AddRange(new []
            {
                "GE",
                "Westinghouse",
                "Craftsman",
                "Whirlpool"
            });
            Manufacturer = Manufacturers[0];

            Capacity = "200 amp";

            EquipmentType = "MLO Panel";

            EquipmentMakes.AddRange(new []
            {
                "GE",
                "Westinghouse",
                "Craftsman",
                "Whirlpool"
            });
            EquipmentMake = EquipmentMakes[0];

            var dateMfg = new DateTime(1956, 8, 18, 01, 13, 45);
            DateManufactured = dateMfg.ToShortDateString();

            YearInstalled = "2007";

            ControlTypeMake = "N/A";

            WarrantyCompany  = "";
            WarrantyDate     = "";
            WarrantyCompany2 = "";
            WarrantyDate2    = "";

            DetailComments.Add(new CommentBase
            {
                EntryUser = new Person() {FirstName = "Kurt", LastName = "Benson"},
                EntryTime = new DateTime(2019, 1, 17, 10, 13, 03),
                CommentText = "The nameplate on the component was missing certain Section Detail fields." + 
                              " Section Detail fields have been populated and fields with NA represent data not found."
            });

            InventoryDetails = "What is supposed to go here?";

            Images.AddRange(new[]
            {
                new BitmapImage(new Uri(@"pack://application:,,,/Images/th1.jpg")),
                new BitmapImage(new Uri(@"pack://application:,,,/Images/th2.jpg")),
                new BitmapImage(new Uri(@"pack://application:,,,/Images/th3.jpg"))
            });
        }
#endif
    }
}
