using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Enumerations;

namespace BDC_V1.Services
{
    public class MockFacility : Facility
    {
        public MockFacility()
        {
            ConstType         = EnumConstType.Permanent;
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

            Address = new Address
            {
                Street1 = "4500 Silverado Ranch Road",
                Street2 = "",
                City = "Las Vegas",
                State = "NV",
                Zipcode = "89139-8366"
            };

            Contact = new Contact
            {
                Name = new Person() {FirstName = "Robert", LastName = "Murphy"},
                Phone = "555-123-4567 x 201",
                EMail = "robert.murphy@somedomain.mil"
            };

            Images.Add(new BitmapImage(new Uri(@"pack://application:,,,/Resources/EmeraldHils.jpg"  )));
            Images.Add(new BitmapImage(new Uri(@"pack://application:,,,/Resources/FlamingoWater.jpg")));
            Images.Add(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Reactor.png")));

            Inspections.Add(new Inspector()
            {
                InspectionDate = DateTime.Now,
                InspectorName  = new Person() {FirstName = "George", LastName = "Jetson"}
            });


            // Facility 1
            FacilityTreeNodes.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.FacilityNode,
                Description = BuildingId + " - " + BuildingName

            });

            FacilityTreeNodes.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.SystemNode,
                Description = "A10 - Foundations"
            });

            FacilityTreeNodes.Last().Children.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.ComponentNode,
                Description = "A1010 - Standard Foundations"
            });

            FacilityTreeNodes.Last().Children.Last().Children.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.ComponentNode,
                Description = "A101001 - Wall Foundations"
            });

            FacilityTreeNodes.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.SystemNode,
                Description = "A20 - BASEMENT CONSTRUCTION"
            });

            FacilityTreeNodes.Last().Children.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.ComponentNode,
                Description = "A2020 - Basement Walls"
            });

            FacilityTreeNodes.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.SystemNode,
                Description = "B10 - SUPERSTRUCTURE"
            });

            FacilityTreeNodes.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.SystemNode,
                Description = "B20 - EXTERIOR ENCLOSURE"
            });

            FacilityTreeNodes.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.SystemNode,
                Description = "B30 - ROOFING"
            });

            FacilityTreeNodes.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.SystemNode,
                Description = "C10 - INTERIOR CONSTRUCTION"
            });

            FacilityTreeNodes.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.SystemNode,
                Description = "C20 - STAIRS"
            });

            FacilityTreeNodes.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.SystemNode,
                Description = "C30 - INTERIOR FINISHES"
            });

            FacilityTreeNodes.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.SystemNode,
                Description = "D10 - CONVEYING"
            });

            FacilityTreeNodes.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.SystemNode,
                Description = "D20 - PLUMBING"
            });

            FacilityTreeNodes.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.SystemNode,
                Description = "D30 - HVAC"
            });

            FacilityTreeNodes.Last().Children.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.SystemNode,
                Description = "D3010 - ENERGY SUPPLY"
            });

            FacilityTreeNodes.Last().Children.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.SystemNode,
                Description = "D3020 - HEAT GENERATING SYSTEM"
            });

            FacilityTreeNodes.Last().Children.Last().Children.Last().Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.SystemNode,
                Description = "Heating System - D302001 Boilers"
            });


            // Facility 2
            FacilityTreeNodes.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.FacilityNode,
                Description = "Facility # 2"
            });

            foreach (var item in FacilityTreeNodes[0].Children)
            {
                FacilityTreeNodes.Last().Children.Add(new TreeNode(item));
            }

            // add components
            var fNode = TreeNode.FindNode(FacilityTreeNodes[0], EnumTreeNodeType.SystemNode,
                "Heating System - D302001 Boilers");

            fNode?.Children.Add(new TreeNode()
            {
                NodeType = EnumTreeNodeType.ComponentNode,
                Description = "Northside"
            });

        }
    }
}
