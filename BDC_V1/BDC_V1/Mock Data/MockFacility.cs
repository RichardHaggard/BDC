using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;

namespace BDC_V1.Mock_Data
{
    public static class MockFacility
    {
        public static IList<ComponentFacility> Facilities { get; } = new List<ComponentFacility>();

#if DEBUG
#warning Using MOCK data for Facilities
        static MockFacility()
        {
            Facilities.Add(MockFacility1());
            Facilities.Add(MockFacility2());
        }

        [NotNull]
        private static ComponentFacility CreateFacilityFramework()
        {
            var facility = new ComponentFacility();

            foreach (EnumFacilitySystemTypes item in Enum.GetValues(typeof(EnumFacilitySystemTypes)))
            {
                facility.AddChild(new ComponentSystem()
                {
                    ComponentType = EnumComponentTypes.SystemType,
                    ComponentName = item.GetSystemName()
                });
            }

            // ReSharper disable once InlineOutVariableDeclaration
            IComponentBase tmp;

            if (facility.TryGetComponent(EnumComponentTypes.SystemType, 
                EnumFacilitySystemTypes.A10.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSystem a10)
                {
                    foreach (Enum_A10_SubsystemTypes subType in Enum.GetValues(typeof(Enum_A10_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            a10.AddChild(new ComponentSection
                            {
                                ComponentType = EnumComponentTypes.SectionType,
                                ComponentName = subType.GetSystemName()
                            });
                        }
                    }
                }
            }

            if (facility.TryGetComponent(EnumComponentTypes.SystemType, 
                EnumFacilitySystemTypes.A20.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSystem a20)
                {
                    foreach (Enum_A20_SubsystemTypes subType in Enum.GetValues(typeof(Enum_A20_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            a20.AddChild(new ComponentSection
                            {
                                ComponentType = EnumComponentTypes.SectionType,
                                ComponentName = subType.GetSystemName()
                            });
                        }
                    }
                }
            }

            if (facility.TryGetComponent(EnumComponentTypes.SystemType, 
                EnumFacilitySystemTypes.B10.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSystem b10)
                {
                    foreach (Enum_B10_SubsystemTypes subType in Enum.GetValues(typeof(Enum_B10_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            b10.AddChild(new ComponentSection
                            {
                                ComponentType = EnumComponentTypes.SectionType,
                                ComponentName = subType.GetSystemName()
                            });
                        }
                    }
                }
            }

            if (facility.TryGetComponent(EnumComponentTypes.SystemType, 
                EnumFacilitySystemTypes.B20.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSystem b20)
                {
                    foreach (Enum_B20_SubsystemTypes subType in Enum.GetValues(typeof(Enum_B20_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            b20.AddChild(new ComponentSection
                            {
                                ComponentType = EnumComponentTypes.SectionType,
                                ComponentName = subType.GetSystemName()
                            });
                        }
                    }
                }
            }

            if (facility.TryGetComponent(EnumComponentTypes.SystemType, 
                EnumFacilitySystemTypes.B30.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSystem b30)
                {
                    foreach (Enum_B30_SubsystemTypes subType in Enum.GetValues(typeof(Enum_B30_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            b30.AddChild(new ComponentSection
                            {
                                ComponentType = EnumComponentTypes.SectionType,
                                ComponentName = subType.GetSystemName()
                            });
                        }
                    }
                }
            }

            if (facility.TryGetComponent(EnumComponentTypes.SystemType, 
                EnumFacilitySystemTypes.C10.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSystem c10)
                {
                    foreach (Enum_C10_SubsystemTypes subType in Enum.GetValues(typeof(Enum_C10_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            c10.AddChild(new ComponentSection
                            {
                                ComponentType = EnumComponentTypes.SectionType,
                                ComponentName = subType.GetSystemName()
                            });
                        }
                    }
                }
            }

            if (facility.TryGetComponent(EnumComponentTypes.SystemType, 
                EnumFacilitySystemTypes.C20.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSystem c20)
                {
                    foreach (Enum_C20_SubsystemTypes subType in Enum.GetValues(typeof(Enum_C20_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            c20.AddChild(new ComponentSection
                            {
                                ComponentType = EnumComponentTypes.SectionType,
                                ComponentName = subType.GetSystemName()
                            });
                        }
                    }
                }
            }

            if (facility.TryGetComponent(EnumComponentTypes.SystemType, 
                EnumFacilitySystemTypes.C30.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSystem c30)
                {
                    foreach (Enum_C30_SubsystemTypes subType in Enum.GetValues(typeof(Enum_C30_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            c30.AddChild(new ComponentSection
                            {
                                ComponentType = EnumComponentTypes.SectionType,
                                ComponentName = subType.GetSystemName()
                            });
                        }
                    }
                }
            }

            if (facility.TryGetComponent(EnumComponentTypes.SystemType, 
                EnumFacilitySystemTypes.D10.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSystem d10)
                {
                    foreach (Enum_D10_SubsystemTypes subType in Enum.GetValues(typeof(Enum_D10_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            d10.AddChild(new ComponentSection
                            {
                                ComponentType = EnumComponentTypes.SectionType,
                                ComponentName = subType.GetSystemName()
                            });
                        }
                    }
                }
            }

            if (facility.TryGetComponent(EnumComponentTypes.SystemType, 
                EnumFacilitySystemTypes.D20.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSystem d20)
                {
                    foreach (Enum_D20_SubsystemTypes subType in Enum.GetValues(typeof(Enum_D20_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            d20.AddChild(new ComponentSection
                            {
                                ComponentType = EnumComponentTypes.SectionType,
                                ComponentName = subType.GetSystemName()
                            });
                        }
                    }
                }
            }

            if (facility.TryGetComponent(EnumComponentTypes.SystemType, 
                EnumFacilitySystemTypes.D30.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSystem d30)
                {
                    foreach (Enum_D30_SubsystemTypes subType in Enum.GetValues(typeof(Enum_D30_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            d30.AddChild(new ComponentSection
                            {
                                ComponentType = EnumComponentTypes.SectionType,
                                ComponentName = subType.GetSystemName()
                            });
                        }
                    }
                }
            }

            if (facility.TryGetComponent(EnumComponentTypes.SystemType, 
                EnumFacilitySystemTypes.D40.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSystem d40)
                {
                    foreach (Enum_D40_SubsystemTypes subType in Enum.GetValues(typeof(Enum_D40_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            d40.AddChild(new ComponentSection
                            {
                                ComponentType = EnumComponentTypes.SectionType,
                                ComponentName = subType.GetSystemName()
                            });
                        }
                    }
                }
            }

            return facility;
        }

        private static ComponentFacility MockFacility1()
        {
            var facility1 = CreateFacilityFramework();
            facility1.ConstType = EnumConstType.Permanent;
            facility1.BuildingId = "ARMRY";
            facility1.BuildingIdNumber = 17180;
            facility1.BuildingName = "National Guard Readiness Center";
            facility1.YearBuilt = 2007;
            facility1.AlternateId = "350939";
            facility1.AlternateIdSource = "hqlis";
            //facility.TotalArea = 87840.0M;
            facility1.Width = 500.0M;
            facility1.Depth = 175.7M;
            facility1.Height = 8.0M;
            facility1.NumFloors = 1;

            facility1.BuildingUse = facility1.BuildingIdNumber + " - " + "ARNG ARMORY";

            facility1.FacilityComments.Add(new CommentFacility()
            {
                EntryUser = new Person("Brian", "Rupert"),
                EntryTime = new DateTime(2019, 1, 8, 11, 38, 0),
                CommentText = "No A20 and D10 systems present. Could not gain access to Supply RM C342."
            });

            facility1.Address = new Address
            {
                Street1 = "4500 Silverado Ranch Road",
                Street2 = "",
                City = "Las Vegas",
                State = "NV",
                Zipcode = "89139-8366"
            };

            facility1.Contact = new Contact("Robert", "Murphy")
            {
                Phone = "555-123-4567 x 201",
                EMail = "robert.murphy@somedomain.mil"
            };

            facility1.Images.AddRange(new[]
            {
                new BitmapImage(new Uri(@"pack://application:,,,/Images/EmeraldHils.jpg")),
                new BitmapImage(new Uri(@"pack://application:,,,/Images/FlamingoWater.jpg")),
                new BitmapImage(new Uri(@"pack://application:,,,/Images/Reactor.png")),
                //new BitmapImage(new Uri(@"pack://application:,,,/Images/th1.jpg")),
                //new BitmapImage(new Uri(@"pack://application:,,,/Images/th2.jpg")),
                //new BitmapImage(new Uri(@"pack://application:,,,/Images/th3.jpg"))
            });

            facility1.Inspections.Add(new InspectionInfo
            {
                InspectionType = EnumInspectionType.DirectRating,
                Section = "D30",
                Component = "D3020",
                Category = "Permanent",
                ComponentType = "D302001 Boilers",
                Quantity = 2.0M,
                InspectionDate = new DateTime(2018, 1, 18, 18, 19, 55),
                Note = "This is a note",
            });

            facility1.Inspections[0].InspectionComments.Add(new CommentInspection
            {
                EntryUser = new Person("Jane", "Doe"),
                EntryTime = new DateTime(2018, 1, 18, 18, 19, 55),
                CommentText = "This unit was in a locked room and not visible"
            });

            // add facility specific subsystems and components

            // ReSharper disable once InlineOutVariableDeclaration
            IComponentBase tmp;

            if (facility1.TryGetComponent(
                EnumComponentTypes.SectionType,
                Enum_C30_SubsystemTypes.C3010.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSection c3010)
                {
                    var c301001 = new ComponentInventory
                    {
                        ComponentName = "C301001 CONCRETE WALL FINISHES - General",
                        ComponentType = EnumComponentTypes.InventoryType
                    };

                    c3010.AddChild(c301001);
                }
            }

            if (facility1.TryGetComponent(
                EnumComponentTypes.SectionType,
                Enum_C30_SubsystemTypes.C3020.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSection c3020)
                {
                    var list = new List<ComponentInventory>
                    {
                        new ComponentInventory()
                        {
                            ComponentName = "NORTH BAY - C302001 TILE FLOOR FINISHES - General",
                            ComponentType = EnumComponentTypes.InventoryType
                        },
                        new ComponentInventory()
                        {
                            ComponentName = "WEST BAY - C302001 TILE FLOOR FINISHES - General",
                            ComponentType = EnumComponentTypes.InventoryType
                        }
                    };

                    c3020.AddChildren(list);
                }
            }

            if (facility1.TryGetComponent(
                EnumComponentTypes.SectionType,
                Enum_D30_SubsystemTypes.D3010.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSection d3010)
                {
                    var d3010002 = new ComponentInventory
                    {
                        ComponentName = "D301002 GAS SUPPLY SYSTEM - General",
                        ComponentType = EnumComponentTypes.InventoryType
                    };

                    d3010.AddChild(d3010002);
                }
            }

            var d3020001 = new ComponentInventory()
            {
                ComponentName = "D302001 BOILERS - General",
                ComponentType = EnumComponentTypes.InventoryType
            };

            if (facility1.TryGetComponent(
                EnumComponentTypes.SectionType,
                Enum_D30_SubsystemTypes.D3020.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSection d3010)
                {
                    d3010.AddChild(d3020001);
                }
            }

            // add components
            tmp = facility1.GetComponent(d3020001);
            if (tmp is IComponentInventory fNode)
            {
                var northSide = new ComponentInventory()
                {
                    // ReSharper disable once StringLiteralTypo
                    ComponentName = "Northside",
                    ComponentType = EnumComponentTypes.InventoryType
                };

                fNode.AddChild(northSide);

                fNode.InspectionIssues.Add(new IssueInspection
                {
                    FacilityId  = facility1.BuildingId,
                    SystemId    = EnumFacilitySystemTypes.C30.GetSystemName(),
                    SectionName = Enum_D30_SubsystemTypes.D3020.GetSystemName(),
                    ComponentId = tmp.ComponentName,
                    TypeName    = tmp.ComponentType.Description(),
                    Rating      = EnumRatingType.RMinus,
                    InspectionComment =
                        new CommentInspection
                        {
                            EntryUser = new Person("Darrell", "Setser"),
                            EntryTime = new DateTime(2018, 1, 18, 18, 19, 55),
                            CommentText = "DAMAGED - All the wood doors have 70% severe structure damage. " +
                                          "CRACKED - All of the doors have 65% severe cracking and splintering. " +
                                          "Replacement is recommended."
                        }
                });
            }

            var d5010001 = new ComponentInventory()
            {
                ComponentName = "EAST BAY - D501003 - INTERIOR DISTRIBUTION SYSTEMS",
                ComponentType = EnumComponentTypes.InventoryType
            };

            if (facility1.TryGetComponent(
                EnumComponentTypes.SectionType,
                Enum_D50_SubsystemTypes.D5010.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSection d3010)
                {
                    d3010.AddChild(d5010001);
                }
            }

            if (facility1.TryGetComponent(d3020001, out tmp))
            {
                if (tmp is IComponentInventory comp)
                {
                    comp.InventoryIssues.Add(new IssueInventory()
                    {
                        FacilityId  = facility1.BuildingId,
                        SystemId    = "D30",
                        SectionName = "D3020",
                        ComponentId = tmp.ComponentName,
                        TypeName    = tmp.ComponentType.Description(),
                        InventoryComment = 
                            new CommentInventory()
                            {
                                EntryUser = new Person("George", "Jetson"),
                                EntryTime = new DateTime(2014, 11, 1, 17, 13, 15),
                                CommentText = "Toilet paper was strewn all about the facility by halloween \"trick or treat\" hooligans."
                            }
                    });
                }
            }

            if (facility1.TryGetComponent(d5010001, out tmp))
            {
                if (tmp is IComponentInventory comp)
                {
                    comp.InventoryIssues.Add(new IssueInventory()
                    {
                        FacilityId  = facility1.BuildingId,
                        SystemId    = "D50",
                        SectionName = "D5010",
                        ComponentId = tmp.ComponentName,
                        TypeName    = tmp.ComponentType.Description(),
                        InventoryComment =
                            new CommentInventory()
                            {
                                EntryUser = new Person("Kurt", "Benson"),
                                EntryTime = new DateTime(2019, 1, 17, 10, 13, 3),
                                CommentText = "The nameplate on the component was missing certain Section Detail fields. " +
                                              "Section Detail fields have been populated and fields with NA representing data not found."
                            }
                    });
                }
            }

            return facility1;
        }

        private static ComponentFacility MockFacility2()
        {
            var facility2 = CreateFacilityFramework();
            facility2.ConstType = EnumConstType.Permanent;
            facility2.BuildingId = "GILLS";
            facility2.BuildingIdNumber = 11057;
            facility2.BuildingName = "Gillette Stadium";
            facility2.YearBuilt = 2000;
            facility2.AlternateId = "11057000";
            facility2.AlternateIdSource = "hqlis";
            facility2.Width = 600.0M;
            facility2.Depth = 200.7M;
            facility2.Height = 16.0M;
            facility2.NumFloors = 2;

            facility2.BuildingUse = facility2.BuildingIdNumber + " - " + facility2.BuildingName;

            facility2.FacilityComments.Add(new CommentFacility
            {
                EntryUser = new Person("Leroy", "Brown"),
                EntryTime = new DateTime(2017, 8, 18, 13, 54, 0),
                CommentText = "Birthday cake is spread over all of the counters."
            });

            facility2.Address = new Address
            {
                Street1 = "1 Patriot Place",
                Street2 = "Business Office",
                City = "Foxborough",
                State = "MA",
                Zipcode = "02305"
            };

            facility2.Contact = new Contact("Lance", "Armstrong")
            {
                Phone = "800-123-4567",
                EMail = "Lance_Armstrong@gillette.org"
            };

            facility2.Images.AddRange(new[]
            {
                new BitmapImage(new Uri(@"pack://application:,,,/Images/GilletteStadium_1.jpg")),
                new BitmapImage(new Uri(@"pack://application:,,,/Images/GilletteStadium_2.jpg"))
            });

            facility2.Inspections.AddRange(new[]
            {
                new InspectionInfo
                {
                    InspectionType = EnumInspectionType.DirectRating,
                    Section = "D30",
                    Component = "D3010",
                    Category = "Permanent",
                    ComponentType = "D301001 Boilers",
                    Quantity = 2.0M,
                    InspectionDate = new DateTime(2018, 1, 18, 18, 19, 55),
                    Note = "This is a musical masterpiece note"
                },
                new InspectionInfo
                {
                    InspectionType = EnumInspectionType.DistressSurvey,
                    Section = "A10",
                    Component = "A1010",
                    Category = "Temporary",
                    ComponentType = "A101001 Boilers",
                    Quantity = 2.0M,
                    InspectionDate = new DateTime(2018, 1, 18, 18, 19, 55),
                    Note = "This is a a distressful note"
                },
            });

            facility2.Inspections[0].InspectionComments.AddRange(new[]
            {
                new CommentInspection()
                {
                    EntryUser = new Person("Rolling", "Stones"),
                    EntryTime = new DateTime(2014, 7, 4, 8, 0, 0),
                    CommentText = "Too old to rock n roll, too young to die"
                },
                new CommentInspection()
                {
                    EntryUser = new Person("Tina", "Turner"),
                    EntryTime = new DateTime(2018, 1, 18, 18, 19, 55),
                    CommentText = "Love in the ThunderDome"
                },
            });

            facility2.Inspections.Last().InspectionComments.AddRange(new[]
            {
                new CommentInspection()
                {
                    EntryUser = new Person("CSN", "and Young"),
                    EntryTime = new DateTime(2014, 7, 4, 8, 0, 0),
                    CommentText = "Stop everybody, what's that sound"
                },
                new CommentInspection()
                {
                    EntryUser = new Person("Harry", "Chapman"),
                    EntryTime = new DateTime(2018, 1, 18, 18, 19, 55),
                    CommentText = "Cats in the cradle and a silver spoon"
                },
            });

            // add facility specific subsystems and components

            // ReSharper disable once InlineOutVariableDeclaration
            IComponentBase tmp;

            if (facility2.TryGetComponent(
                EnumComponentTypes.SectionType,
                Enum_C30_SubsystemTypes.C3010.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSection c3010)
                {
                    var c301001 = new ComponentInventory()
                    {
                        ComponentName = "C301001 CONCRETE WALL FINISHES - General",
                        ComponentType = EnumComponentTypes.InventoryType
                    };

                    c3010.AddChild(c301001);
                }
            }

            if (facility2.TryGetComponent(
                EnumComponentTypes.SectionType,
                Enum_C30_SubsystemTypes.C3030.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSection c3030)
                {
                    // ReSharper disable StringLiteralTypo
                    var list = new List<ComponentInventory>
                    {
                        new ComponentInventory()
                        {
                            ComponentName = "Men's Restroom 1 - C303001 SUSPENDED CEILING - General",
                            ComponentType = EnumComponentTypes.InventoryType
                        },
                        new ComponentInventory()
                        {
                            ComponentName = "Men's Restroom 2 - C303001 SUSPENDED CEILING - General",
                            ComponentType = EnumComponentTypes.InventoryType
                        },
                        new ComponentInventory()
                        {
                            ComponentName = "Men's Restroom 3 - C303001 SUSPENDED CEILING - General",
                            ComponentType = EnumComponentTypes.InventoryType
                        }
                    };
                    // ReSharper restore StringLiteralTypo

                    c3030.AddChildren(list);
                }
            }

            var d3020001 = new ComponentInventory()
            {
                ComponentName = "D302001 BOILERS - General",
                ComponentType = EnumComponentTypes.InventoryType,
                Description = "dry-type, 480V primary 120/208V secondary, 225kVA"
            };

            if (facility2.TryGetComponent(
                EnumComponentTypes.SectionType,
                Enum_D30_SubsystemTypes.D3020.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentSection d3020)
                {
                    d3020.AddChild(d3020001);
                }
            }

            // add components
            tmp = facility2.GetComponent(d3020001);
            if (tmp is IComponentSystem fNode)
            {
                var list = new List<ComponentInventory>
                {
                    new ComponentInventory()
                    {
                        ComponentName = "Boiler 1",
                        ComponentType = EnumComponentTypes.InventoryType
                    },
                    new ComponentInventory()
                    {
                        ComponentName = "Boiler 2",
                        ComponentType = EnumComponentTypes.InventoryType
                    },
                    new ComponentInventory()
                    {
                        ComponentName = "Boiler 3",
                        ComponentType = EnumComponentTypes.InventoryType
                    }
                };

                fNode.AddChildren(list);
            }

            return facility2;
        }
#endif
    }
}
