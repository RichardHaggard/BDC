using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;

namespace BDC_V1.Services
{
    public static class MockFacility
    {
        public static IList<IComponentFacility> Facilities { get; } = new List<IComponentFacility>();

#if DEBUG
#warning Using MOCK data for Facilities
        static MockFacility()
        {
            Facilities.Add(MockFacility1());
            Facilities.Add(MockFacility2());
        }

        [NotNull]
        private static IComponentFacility CreateFacilityFramework()
        {
            var facility = new ComponentFacility();

            foreach (EnumFacilitySystemTypes item in Enum.GetValues(typeof(EnumFacilitySystemTypes)))
            {
                facility.Components.Add(new ComponentSystem()
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
                if (tmp is IComponentFacility a10)
                {
                    foreach (Enum_A10_SubsystemTypes subType in Enum.GetValues(typeof(Enum_A10_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            a10.Components.Add(new ComponentFacility()
                            {
                                ComponentType = EnumComponentTypes.SubsystemType,
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
                if (tmp is IComponentFacility a20)
                {
                    foreach (Enum_A20_SubsystemTypes subType in Enum.GetValues(typeof(Enum_A20_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            a20.Components.Add(new ComponentFacility()
                            {
                                ComponentType = EnumComponentTypes.SubsystemType,
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
                if (tmp is IComponentFacility b10)
                {
                    foreach (Enum_B10_SubsystemTypes subType in Enum.GetValues(typeof(Enum_B10_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            b10.Components.Add(new ComponentFacility()
                            {
                                ComponentType = EnumComponentTypes.SubsystemType,
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
                if (tmp is IComponentFacility b20)
                {
                    foreach (Enum_B20_SubsystemTypes subType in Enum.GetValues(typeof(Enum_B20_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            b20.Components.Add(new ComponentFacility()
                            {
                                ComponentType = EnumComponentTypes.SubsystemType,
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
                if (tmp is IComponentFacility b30)
                {
                    foreach (Enum_B30_SubsystemTypes subType in Enum.GetValues(typeof(Enum_B30_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            b30.Components.Add(new ComponentFacility()
                            {
                                ComponentType = EnumComponentTypes.SubsystemType,
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
                if (tmp is IComponentFacility c10)
                {
                    foreach (Enum_C10_SubsystemTypes subType in Enum.GetValues(typeof(Enum_C10_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            c10.Components.Add(new ComponentFacility()
                            {
                                ComponentType = EnumComponentTypes.SubsystemType,
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
                if (tmp is IComponentFacility c20)
                {
                    foreach (Enum_C20_SubsystemTypes subType in Enum.GetValues(typeof(Enum_C20_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            c20.Components.Add(new ComponentFacility()
                            {
                                ComponentType = EnumComponentTypes.SubsystemType,
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
                if (tmp is IComponentFacility c30)
                {
                    foreach (Enum_C30_SubsystemTypes subType in Enum.GetValues(typeof(Enum_C30_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            c30.Components.Add(new ComponentFacility()
                            {
                                ComponentType = EnumComponentTypes.SubsystemType,
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
                if (tmp is IComponentFacility d10)
                {
                    foreach (Enum_D10_SubsystemTypes subType in Enum.GetValues(typeof(Enum_D10_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            d10.Components.Add(new ComponentFacility()
                            {
                                ComponentType = EnumComponentTypes.SubsystemType,
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
                if (tmp is IComponentFacility d20)
                {
                    foreach (Enum_D20_SubsystemTypes subType in Enum.GetValues(typeof(Enum_D20_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            d20.Components.Add(new ComponentFacility()
                            {
                                ComponentType = EnumComponentTypes.SubsystemType,
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
                if (tmp is IComponentFacility d30)
                {
                    foreach (Enum_D30_SubsystemTypes subType in Enum.GetValues(typeof(Enum_D30_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            d30.Components.Add(new ComponentFacility()
                            {
                                ComponentType = EnumComponentTypes.SubsystemType,
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
                if (tmp is IComponentFacility d40)
                {
                    foreach (Enum_D40_SubsystemTypes subType in Enum.GetValues(typeof(Enum_D40_SubsystemTypes)))
                    {
                        if (subType.ToString() != "None")
                        {
                            d40.Components.Add(new ComponentFacility()
                            {
                                ComponentType = EnumComponentTypes.SubsystemType,
                                ComponentName = subType.GetSystemName()
                            });
                        }
                    }
                }
            }

            return facility;
        }

        private static IComponentFacility MockFacility1()
        {
            var facility = CreateFacilityFramework();
            facility.ConstType = EnumConstType.Permanent;
            facility.BuildingId = "ARMRY";
            facility.BuildingIdNumber = 17180;
            facility.BuildingName = "National Guard Readiness Center";
            facility.YearBuilt = 2007;
            facility.AlternateId = "350939";
            facility.AlternateIdSource = "hqlis";
            facility.TotalArea = 87840.0M;
            facility.Width = 500.0M;
            facility.Depth = 175.7M;
            facility.Height = 8.0M;
            facility.NumFloors = 1;

            facility.BuildingUse = facility.BuildingIdNumber + " - " + "ARNG ARMORY";

            facility.FacilityComments.Add(new CommentFacility()
            {
                EntryUser = new Person("Brian", "Rupert"),
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

            facility.Contact = new Contact("Robert", "Murphy")
            {
                Phone = "555-123-4567 x 201",
                EMail = "robert.murphy@somedomain.mil"
            };

            facility.Images.Add(new BitmapImage(new Uri(@"pack://application:,,,/Resources/EmeraldHils.jpg")));
            facility.Images.Add(new BitmapImage(new Uri(@"pack://application:,,,/Resources/FlamingoWater.jpg")));
            facility.Images.Add(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Reactor.png")));

            facility.Inspections.Add(new InspectionInfo
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

            facility.Inspections.FirstOrDefault()?.InspectionComments.Add(new CommentInspection
            {
                EntryUser = new Person("Jane", "Doe"),
                EntryTime = new DateTime(2018, 1, 18, 18, 19, 55),
                CommentText = "This unit was in a locked room and not visible"
            });

            // add facility specific subsystems and components

            // ReSharper disable once InlineOutVariableDeclaration
            IComponentBase tmp;

            if (facility.TryGetComponent(
                EnumComponentTypes.SubsystemType,
                Enum_C30_SubsystemTypes.C3010.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentFacility c3010)
                {
                    var c301001 = new ComponentFacility()
                    {
                        ComponentName = "C301001 CONCRETE WALL FINISHES - General",
                        ComponentType = EnumComponentTypes.ComponentType
                    };

                    c3010.Components.Add(c301001);
                }
            }

            if (facility.TryGetComponent(
                EnumComponentTypes.SubsystemType,
                Enum_C30_SubsystemTypes.C3020.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentFacility c3020)
                {
                    var list = new List<IComponentInventory>
                    {
                        new ComponentInventory()
                        {
                            ComponentName = "NORTH BAY - C302001 TILE FLOOR FINISHES - General",
                            ComponentType = EnumComponentTypes.ComponentType
                        },
                        new ComponentInventory()
                        {
                            ComponentName = "WEST BAY - C302001 TILE FLOOR FINISHES - General",
                            ComponentType = EnumComponentTypes.ComponentType
                        }
                    };

                    c3020.Components.AddRange(list);
                }
            }

            if (facility.TryGetComponent(
                EnumComponentTypes.SubsystemType,
                Enum_D30_SubsystemTypes.D3010.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentFacility d3010)
                {
                    var d3010002 = new ComponentFacility()
                    {
                        ComponentName = "D301002 GAS SUPPLY SYSTEM - General",
                        ComponentType = EnumComponentTypes.ComponentType
                    };

                    d3010.Components.Add(d3010002);
                }
            }

            var d3020001 = new ComponentInventory()
            {
                ComponentName = "D302001 BOILERS - General",
                ComponentType = EnumComponentTypes.ComponentType
            };

            if (facility.TryGetComponent(
                EnumComponentTypes.SubsystemType,
                Enum_D30_SubsystemTypes.D3020.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentFacility d3010)
                {
                    d3010.Components.Add(d3020001);
                }
            }

            // add components
            tmp = facility.GetComponent(d3020001);
            if (tmp is IComponentInventory fNode)
            {
                var northSide = new ComponentInventory()
                {
                    // ReSharper disable once StringLiteralTypo
                    ComponentName = "Northside",
                    ComponentType = EnumComponentTypes.ComponentType
                };

                fNode.Components.Add(northSide);

                fNode.InspectionIssues.Add(new IssueInspection
                {
                    FacilityId     = facility.BuildingId,
                    SystemId       = EnumFacilitySystemTypes.C30.GetSystemName(),
                    SectionName    = Enum_D30_SubsystemTypes.D3020.GetSystemName(),
                    ComponentId    = tmp.ComponentName,
                    TypeName       = tmp.ComponentType.Description(),
                    Rating         = EnumRatingType.RMinus,
                });

                fNode.InspectionIssues.FirstOrDefault()?.InspectionComments.Add(
                    new CommentInspection()
                    {
                        EntryUser = new Person("Darrell", "Setser"),
                        EntryTime = new DateTime(2018, 1, 18, 18, 19, 55),
                        CommentText = "DAMAGED - All the wood doors have 70% severe structure damage. " +
                                      "CRACKED - All of the doors have 65% severe cracking and splintering. " +
                                      "Replacement is recommended."
                    });
                }

            var d5010001 = new ComponentInventory()
            {
                ComponentName = "EAST BAY - D501003 - INTERIOR DISTRIBUTION SYSTEMS",
                ComponentType = EnumComponentTypes.ComponentType
            };

            if (facility.TryGetComponent(
                EnumComponentTypes.SubsystemType,
                Enum_D50_SubsystemTypes.D5010.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentFacility d3010)
                {
                    d3010.Components.Add(d5010001);
                }
            }

            if (facility.TryGetComponent(d3020001, out tmp))
            {
                if (tmp is IComponentInventory comp)
                {
                    comp.InventoryIssues.Add(new IssueInventory()
                    {
                        FacilityId  = facility.BuildingId,
                        SystemId    = "D30",
                        SectionName = "D3020",
                        ComponentId = tmp.ComponentName,
                        TypeName    = tmp.ComponentType.Description(),
                    });

                    comp.InventoryIssues.LastOrDefault()?.InventoryComments.Add(
                        new CommentInventory()
                        {
                            EntryUser = new Person("George", "Jetson"),
                            EntryTime = new DateTime(2014, 11, 1, 17, 13, 15),
                            CommentText = "Toilet paper was strewn all about the facility by halloween \"trick or treat\" hooligans."
                         });
                }
            }

            if (facility.TryGetComponent(d5010001, out tmp))
            {
                if (tmp is IComponentInventory comp)
                {
                    comp.InventoryIssues.Add(new IssueInventory()
                    {
                        FacilityId  = facility.BuildingId,
                        SystemId    = "D50",
                        SectionName = "D5010",
                        ComponentId = tmp.ComponentName,
                        TypeName    = tmp.ComponentType.Description(),
                    });

                    comp.InventoryIssues.LastOrDefault()?.InventoryComments.Add(
                        new CommentInventory()
                        {
                            EntryUser = new Person("Kurt", "Benson"),
                            EntryTime = new DateTime(2019, 1, 17, 10, 13, 3),
                            CommentText = "The nameplate on the component was missing certain Section Detail fields. " +
                                          "Section Detail fields have been populated and fields with NA representing data not found."
                        });
                }
            }

            return facility;
        }

        private static IComponentFacility MockFacility2()
        {
            var facility = CreateFacilityFramework();
            facility.ConstType = EnumConstType.Permanent;
            facility.BuildingId = "GILLS";
            facility.BuildingIdNumber = 11057;
            facility.BuildingName = "Gillette Stadium";
            facility.YearBuilt = 2000;
            facility.AlternateId = "11057000";
            facility.AlternateIdSource = "hqlis";
            facility.Width = 600.0M;
            facility.Depth = 200.7M;
            facility.Height = 16.0M;
            facility.NumFloors = 2;

            facility.BuildingUse = facility.BuildingIdNumber + " - " + facility.BuildingName;

            facility.FacilityComments.Add(new CommentFacility
            {
                EntryUser = new Person("Leroy", "Brown"),
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

            facility.Contact = new Contact("Lance", "Armstrong")
            {
                Phone = "800-123-4567",
                EMail = "Lance_Armstrong@gillette.org"
            };

            facility.Images.Add(new BitmapImage(new Uri(@"pack://application:,,,/Resources/GilletteStadium_1.jpg")));
            facility.Images.Add(new BitmapImage(new Uri(@"pack://application:,,,/Resources/GilletteStadium_2.jpg")));

            facility.Inspections.AddRange(new[]
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

            facility.Inspections.FirstOrDefault()?.InspectionComments.AddRange(new[]
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

            facility.Inspections.LastOrDefault()?.InspectionComments.AddRange(new[]
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

            if (facility.TryGetComponent(
                EnumComponentTypes.SubsystemType,
                Enum_C30_SubsystemTypes.C3010.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentFacility c3010)
                {
                    var c301001 = new ComponentInventory()
                    {
                        ComponentName = "C301001 CONCRETE WALL FINISHES - General",
                        ComponentType = EnumComponentTypes.ComponentType
                    };

                    c3010.Components.Add(c301001);
                }
            }

            if (facility.TryGetComponent(
                EnumComponentTypes.SubsystemType,
                Enum_C30_SubsystemTypes.C3030.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentFacility c3030)
                {
                    // ReSharper disable StringLiteralTypo
                    var list = new List<IComponentInventory>
                    {
                        new ComponentInventory()
                        {
                            ComponentName = "Men's Restroom 1 - C303001 SUSPENDED CEILING - General",
                            ComponentType = EnumComponentTypes.ComponentType
                        },
                        new ComponentInventory()
                        {
                            ComponentName = "Men's Restroom 2 - C303001 SUSPENDED CEILING - General",
                            ComponentType = EnumComponentTypes.ComponentType
                        },
                        new ComponentInventory()
                        {
                            ComponentName = "Men's Restroom 3 - C303001 SUSPENDED CEILING - General",
                            ComponentType = EnumComponentTypes.ComponentType
                        }
                    };
                    // ReSharper restore StringLiteralTypo

                    c3030.Components.AddRange(list);
                }
            }

            var d3020001 = new ComponentInventory()
            {
                ComponentName = "D302001 BOILERS - General",
                ComponentType = EnumComponentTypes.ComponentType,
                Description = "dry-type, 480V primary 120/208V secondary, 225kVA"
            };

            if (facility.TryGetComponent(
                EnumComponentTypes.SubsystemType,
                Enum_D30_SubsystemTypes.D3020.GetSystemName(),
                out tmp))
            {
                if (tmp is IComponentFacility d3020)
                {
                    d3020.Components.Add(d3020001);
                }
            }

            // add components
            tmp = facility.GetComponent(d3020001);
            if (tmp is IComponentFacility fNode)
            {
                var list = new List<IComponentInventory>
                {
                    new ComponentInventory()
                    {
                        ComponentName = "Boiler 1",
                        ComponentType = EnumComponentTypes.ComponentType
                    },
                    new ComponentInventory()
                    {
                        ComponentName = "Boiler 2",
                        ComponentType = EnumComponentTypes.ComponentType
                    },
                    new ComponentInventory()
                    {
                        ComponentName = "Boiler 3",
                        ComponentType = EnumComponentTypes.ComponentType
                    }
                };

                fNode.Components.AddRange(list);
            }

            return facility;
        }
#endif
    }
}
