using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;

namespace BDC_V1.Services
{
    public class MockFacility : Facility
    {
        public MockFacility()
        {
            ConstType         = EnumConstType.Permanant;
            BuildingId        = "ARMRY";
            BuildingName      = "National Guard Readiness Center";
            BuildingUse       = "17180 - ARNG ARMORY";
            YearBuilt         = 2007;
            AlternateId       = "350939";
            AlternateIdSource = "hqlis";
            TotalArea         = 87840.0M;
            Width             = 500.0M;
            Depth             = 175.7M;
            Height            = 8.0M;
            NumFloors         = 1;
            FacilityComments  = "[Brian Rupert 01/08/19 11:38 AM] No A20 and D10 systems present. Could not gain access to Supply RM C342.";
            Address           = new Address();
            Contact           = new Contact();

            Images.Add(new BitmapImage(new Uri(@"pack://application:,,,/Resources/EmeraldHils.jpg"  )));
            Images.Add(new BitmapImage(new Uri(@"pack://application:,,,/Resources/FlamingoWater.jpg")));

            Inspections.Add(new Inspector()
            {
                InspectionDate = DateTime.Now,
                InspectorName  = new Person() {FirstName = "George", LastName = "Jetson"}
            });
        }
    }
}
