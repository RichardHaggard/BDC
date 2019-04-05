using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;

namespace BDC_V1.Services
{
    public static class MockFacility
    {
        public static IList<IFacilitySystems> Facilities { get; } =  new List<IFacilitySystems>();

        static MockFacility()
        {
            Facilities.Add(MockFacility1());
            Facilities.Add(MockFacility2());
        }

        private static IFacilitySystems MockFacility1()
        {
            var facility = new Facility()
            {
                ComponentType     = EnumComponentTypes.FacilityType,
                ConstType         = EnumConstType.Permanent,
                BuildingId        = "ARMRY",
                BuildingIdNumber  = 17180,
                BuildingName      = "National Guard Readiness Center",
                YearBuilt         = 2007,
                AlternateId       = "350939",
                AlternateIdSource = "hqlis",
                TotalArea         = 87840.0M,
                Width             = 500.0M,
                Depth             = 175.7M,
                Height            = 8.0M,
                NumFloors         = 1
            };

            facility.BuildingUse = facility.BuildingIdNumber + " - " + "ARNG ARMORY";

            facility.FacilityComments.Add(new Comment()
            {
                EntryUser = new Person() {FirstName = "Brian", LastName = "Rupert"},
                EntryTime = new DateTime(2019, 1, 8, 11, 38, 0),
                CommentText = "No A20 and D10 systems present. Could not gain access to Supply RM C342."
            });

            facility.Address = new Address
            {
                Street1 = "4500 Silverado Ranch Road",
                Street2 = "",
                City = "Las Vegas",
                State = "NV",
                Zipcode = "89139-8366"
            };

            facility.Contact = new Contact
            {
                Name = new Person() {FirstName = "Robert", LastName = "Murphy"},
                Phone = "555-123-4567 x 201",
                EMail = "robert.murphy@somedomain.mil"
            };

            facility.Images.Add(new BitmapImage(new Uri(@"pack://application:,,,/Resources/EmeraldHils.jpg"  )));
            facility.Images.Add(new BitmapImage(new Uri(@"pack://application:,,,/Resources/FlamingoWater.jpg")));
            facility.Images.Add(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Reactor.png")));

            facility.Inspections.Add(new Inspection()
            {
                EntryUser = new Person() {FirstName = "George", LastName = "Jetson"},
                EntryTime = DateTime.Now
            });

            // add facility specific subsystems and components
 
            if (facility.TryGet(
                EnumComponentTypes.SubsystemType, 
                Enum_C30_SubsystemTypes.C3010.GetSystemName(),
                out var c3010))
            {
                var c301001 = new FacilitySystems()
                {
                    ComponentName = "C301001 CONCRETE WALL FINISHES - General",
                    ComponentType = EnumComponentTypes.ComponentType
                };

                c3010.SubSystems.Add(c301001);
            }

            if (facility.TryGet(
                EnumComponentTypes.SubsystemType,
                Enum_C30_SubsystemTypes.C3020.GetSystemName(),
                out var c3020))
            {
                var list = new List<IComponent>
                {
                    new Component()
                    {
                        ComponentName = "NORTH BAY - C302001 TILE FLOOR FINISHES - General",
                        ComponentType = EnumComponentTypes.ComponentType
                    },
                    new Component()
                    {
                        ComponentName = "WEST BAY - C302001 TILE FLOOR FINISHES - General",
                        ComponentType = EnumComponentTypes.ComponentType
                    }
                };

                c3020.SubSystems.AddRange(list);
            }
 
            if (facility.TryGet(
                EnumComponentTypes.SubsystemType, 
                Enum_D30_SubsystemTypes.D3010.GetSystemName(),
                out var d3010))
            {
                var d3010002 = new FacilitySystems()
                {
                    ComponentName = "D301002 GAS SUPPLY SYSTEM - General",
                    ComponentType = EnumComponentTypes.ComponentType
                };
                d3010.SubSystems.Add(d3010002);
            }
 
            var d3020001 = new FacilitySystems()
            {
                ComponentName = "D302001 BOILERS - General",
                ComponentType = EnumComponentTypes.ComponentType
            };

            if (facility.TryGet(
                EnumComponentTypes.SubsystemType, 
                Enum_D30_SubsystemTypes.D3020.GetSystemName(),
                out var d3020))
            {
                d3010.SubSystems.Add(d3020001);
            }

            // add components
            var fNode = facility.Get(d3020001);
            if (fNode != null)
            {
                var northSide = new Component()
                {
                    // ReSharper disable once StringLiteralTypo
                    ComponentName = "Northside",
                    ComponentType = EnumComponentTypes.ComponentType
                };

                fNode.SubSystems.Add(northSide);
            }
 
            var d5010001 = new Component()
            {
                ComponentName = "EAST BAY - D501003 - INTERIOR DISTRIBUTION SYSTEMS",
                ComponentType = EnumComponentTypes.ComponentType
            };

            if (facility.TryGet(
                EnumComponentTypes.SubsystemType, 
                Enum_D50_SubsystemTypes.D5010.GetSystemName(),
                out var d5010))
            {
                d3010.SubSystems.Add(d5010001);
            }

            return facility;
        }

        private static IFacilitySystems MockFacility2()
        {
            var facility = new Facility()
            {
                ComponentType     = EnumComponentTypes.FacilityType,
                ConstType         = EnumConstType.Permanent,
                BuildingId        = "GILLS",
                BuildingIdNumber  = 11057,
                BuildingName      = "Gillette Stadium",
                YearBuilt         = 2000,
                AlternateId       = "11057000",
                AlternateIdSource = "hqlis",
                Width             = 600.0M,
                Depth             = 200.7M,
                Height            = 16.0M,
                NumFloors         = 2
            };

            facility.BuildingUse = facility.BuildingIdNumber + " - " + facility.BuildingName;

            facility.FacilityComments.Add(new Comment()
            {
                EntryUser = new Person() {FirstName = "Leroy", LastName = "Brown"},
                EntryTime = new DateTime(2017, 8, 18, 13, 54, 0),
                CommentText = "Birthday cake is spread over all of the counters."
            });

            facility.Address = new Address
            {
                Street1 = "1 Patriot Place",
                Street2 = "Business Office",
                City = "Foxborough",
                State = "MA",
                Zipcode = "02305"
            };

            facility.Contact = new Contact
            {
                Name = new Person() {FirstName = "Lance", LastName = "Armstrong"},
                Phone = "800-123-4567",
                EMail = "Lance_Armstrong@gillette.org"
            };

            facility.Images.Add(new BitmapImage(new Uri(@"pack://application:,,,/Resources/GilletteStadium_1.jpg")));
            facility.Images.Add(new BitmapImage(new Uri(@"pack://application:,,,/Resources/GilletteStadium_2.jpg")));

            facility.Inspections.Add(new Inspection()
            {
                EntryUser = new Person() {FirstName = "Rolling", LastName = "Stones"},
                EntryTime = new DateTime(2014, 7, 4, 8, 0, 0)
            });

            facility.Inspections.Add(new Inspection()
            {
                EntryUser = new Person() {FirstName = "Tina", LastName = "Turner"},
                EntryTime = new DateTime(2017, 8, 18, 13, 54, 0)
            });

            // add facility specific subsystems and components
 
            if (facility.TryGet(
                EnumComponentTypes.SubsystemType, 
                Enum_C30_SubsystemTypes.C3010.GetSystemName(),
                out var c3010))
            {
                var c301001 = new Component()
                {
                    ComponentName = "C301001 CONCRETE WALL FINISHES - General",
                    ComponentType = EnumComponentTypes.ComponentType
                };

                c3010.SubSystems.Add(c301001);
            }
 
            if (facility.TryGet(
                EnumComponentTypes.SubsystemType, 
                Enum_C30_SubsystemTypes.C3030.GetSystemName(),
                out var c3030))
            {
                // ReSharper disable StringLiteralTypo
                var list = new List<IComponent>
                {
                    new Component()
                    {
                        ComponentName = "Men's Restroom 1 - C303001 SUSPENDED CEILING - General",
                        ComponentType = EnumComponentTypes.ComponentType
                    },
                    new Component()
                    {
                        ComponentName = "Men's Restroom 2 - C303001 SUSPENDED CEILING - General",
                        ComponentType = EnumComponentTypes.ComponentType
                    },
                    new Component()
                    {
                        ComponentName = "Men's Restroom 3 - C303001 SUSPENDED CEILING - General",
                        ComponentType = EnumComponentTypes.ComponentType
                    }
                };
                // ReSharper restore StringLiteralTypo

                c3030.SubSystems.AddRange(list);
            }
 
            var d3020001 = new Component()
            {
                ComponentName = "D302001 BOILERS - General",
                ComponentType = EnumComponentTypes.ComponentType,
                Description   =  "dry-type, 480V primary 120/208V secondary, 225kVA"
            };

            if (facility.TryGet(
                EnumComponentTypes.SubsystemType, 
                Enum_D30_SubsystemTypes.D3020.GetSystemName(),
                out var d3020))
            {
                d3020.SubSystems.Add(d3020001);
            }

            // add components
            var fNode = facility.Get(d3020001);
            if (fNode != null)
            {
                var list = new List<IComponent>
                {
                    new Component()
                    {
                        ComponentName = "Boiler 1", 
                        ComponentType = EnumComponentTypes.ComponentType
                    },
                    new Component()
                    {
                        ComponentName = "Boiler 2",
                        ComponentType = EnumComponentTypes.ComponentType
                    },
                    new Component()
                    {
                        ComponentName = "Boiler 3", 
                        ComponentType = EnumComponentTypes.ComponentType
                    }
                };

                fNode.SubSystems.AddRange(list);
            }

            return facility;
        }
    }
}
