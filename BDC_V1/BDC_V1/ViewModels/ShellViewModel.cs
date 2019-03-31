using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Events;
using BDC_V1.Interfaces;
using BDC_V1.Services;
using BDC_V1.Views;
using CommonServiceLocator;
using JetBrains.Annotations;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using EventAggregator = BDC_V1.Events.EventAggregator;

namespace BDC_V1.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        // commands are read only to the outside world
        public ICommand CmdAddComponent            { get; }
        public ICommand CmdAddSection              { get; }
        public ICommand CmdAddSystem               { get; }
        public ICommand CmdCopyInventory           { get; }
        public ICommand CmdCopySections            { get; }
        public ICommand CmdDefaultInventoryMode    { get; }
        public ICommand CmdDeleteSystem            { get; }
        public ICommand CmdInspectionMode          { get; }
        public ICommand CmdMenuExit                { get; }
        public ICommand CmdMenuAbout               { get; }
        public ICommand CmdMenuBluebeam            { get; }
        public ICommand CmdMenuCalculators         { get; }
        public ICommand CmdMenuSwitchFile          { get; }
        public ICommand CmdMenuViewAllSystems      { get; }
        public ICommand CmdMenuViewAssignedSystems { get; }
        public ICommand CmdMenuInspectionSummary   { get; }
        public ICommand CmdMenuQcReports           { get; }
        public ICommand CmdMicOn                   { get; }
        public ICommand CmdMicOff                  { get; }
        public ICommand CmdTabSelectionChanged     { get; }

        // these properties are combinatorial, the components need to raise the property changed for each of these
        public string Title => @"Builder DC - " + BredFilename;

        public string StatusLookup => "Lookup: " + LookupField;

        public string StatusInspector => "Current Inspector: " + SelectedLoginUser?? "";

        public string StatusInspectedBy => "(Inspected By: " + InspectedByUser?.InspectorName?? "" + ")";

        public string StatusDateTimeString =>
            StatusDateTime.ToShortDateString() + " " + StatusDateTime.ToShortTimeString();

        public QuickObservableCollection<TreeViewItem> TreeItemsViewSource { get; } = new QuickObservableCollection<TreeViewItem>();

        // these properties all raise their own changed events
        public Visibility WindowVisibility
        {
            get => _windowVisibility;
            set => SetProperty(ref _windowVisibility, value);
        }
        private Visibility _windowVisibility;


        public System.Windows.Media.Brush InvTreeBorderBackgroundColor
        {
            get => _invTreeBorderBackgroundColor;
            set => SetProperty(ref _invTreeBorderBackgroundColor, value);
        }
        private System.Windows.Media.Brush _invTreeBorderBackgroundColor;


        public System.Windows.Media.Brush FacilityTabBackgroundColor
        {
            get => _facilityTabBackgroundColor;
            set => SetProperty(ref _facilityTabBackgroundColor, value);
        }
        private System.Windows.Media.Brush _facilityTabBackgroundColor;


        public System.Windows.Media.Brush InventoryTabBackgroundColor
        {
            get => _inventoryTabBackgroundColor;
            set => SetProperty(ref _inventoryTabBackgroundColor, value);
        }
        private System.Windows.Media.Brush _inventoryTabBackgroundColor;


        public System.Windows.Media.Brush InspectionTabBackgroundColor
        {
            get => _inspectionTabBackgroundColor;
            set => SetProperty(ref _inspectionTabBackgroundColor, value);
        }
        private System.Windows.Media.Brush _inspectionTabBackgroundColor;


        public System.Windows.Media.Brush QaInventoryTabBackgroundColor
        {
            get => _qaInventoryTabBackgroundColor;
            set => SetProperty(ref _qaInventoryTabBackgroundColor, value);
        }
        private System.Windows.Media.Brush _qaInventoryTabBackgroundColor;


        public System.Windows.Media.Brush QaInspectionTabBackgroundColor
        {
            get => _qaInspectionTabBackgroundColor;
            set => SetProperty(ref _qaInspectionTabBackgroundColor, value);
        }
        private System.Windows.Media.Brush _qaInspectionTabBackgroundColor;


        [CanBeNull]
        public string ConfigurationFilename
        {
            get => _configurationFilename;
            set => SetProperty(ref _configurationFilename, value);
        }
        [CanBeNull] private string _configurationFilename;


        public string BredFilename
        {
            get => _bredFilename;
            set
            {
                if (SetProperty(ref _bredFilename, value))
                    RaisePropertyChanged(nameof(Title));
            }
        }
        private string _bredFilename;


        public DateTime StatusDateTime
        {
            get => _statusDateTime;
            private set
            {
                if (SetProperty(ref _statusDateTime, value))
                    RaisePropertyChanged(nameof(StatusDateTimeString));
            }
        }
        private DateTime _statusDateTime;


        [CanBeNull]
        public IPerson SelectedLoginUser
        {
            get => _selectedLoginUser;
            set
            {
                if (SetProperty(ref _selectedLoginUser, value))
                    RaisePropertyChanged(nameof(StatusInspector));
            } 
        }
        [CanBeNull] private IPerson _selectedLoginUser;


        public string LookupField
        {
            get => _lookupField;
            set
            {
                if (SetProperty(ref _lookupField, value))
                    RaisePropertyChanged(nameof(StatusLookup));;
            }
        }
        private string _lookupField;


        [CanBeNull]
        public IInspector InspectedByUser
        {
            get => _inspectedByUser;
            private set
            {
                if (SetProperty(ref _inspectedByUser, value))
                {
                    StatusDateTime = _inspectedByUser?.InspectionDate?? DateTime.Now;
                    RaisePropertyChanged(nameof(StatusInspectedBy));
                }
            }
        }
        [CanBeNull] private IInspector _inspectedByUser;


        // Used to force the Tab selection internally
        public int ViewTabIndex
        {
            get => _viewTabIndex;
            set => SetProperty(ref _viewTabIndex, value);
        }
        private int _viewTabIndex;

        [CanBeNull]
        public TabItem ViewTabItem
        {
            get => _viewTabItem;
            set
            {
                if (SetProperty(ref _viewTabItem, value))
                    UpdateTreeView(_viewTabItem);
            }
        }
        [CanBeNull] private TabItem _viewTabItem;


        public Visibility VisibilityAddComponentButton
        {
            get { return _VisibilityAddComponentButton; }
            set { SetProperty(ref _VisibilityAddComponentButton, value); }
        }
        private Visibility _VisibilityAddComponentButton = Visibility.Collapsed;


        public Visibility VisibilityAddSectionButton
        {
            get { return _VisibilityAddSectionButton; }
            set { SetProperty(ref _VisibilityAddSectionButton, value); }
        }
        private Visibility _VisibilityAddSectionButton = Visibility.Collapsed;


        public Visibility VisibilityAddSystemButton
        {
            get { return _VisibilityAddSystemButton; }
            set { SetProperty(ref _VisibilityAddSystemButton, value); }
        }
        private Visibility _VisibilityAddSystemButton = Visibility.Collapsed;


        public Visibility VisibilityCopyInspectionButton
        {
            get { return _VisibilityCopyInspectionButton; }
            set { SetProperty(ref _VisibilityCopyInspectionButton, value); }
        }
        private Visibility _VisibilityCopyInspectionButton = Visibility.Collapsed;


        public Visibility VisibilityCopyInventoryButton
        {
            get { return _VisibilityCopyInventoryButton; }
            set { SetProperty(ref _VisibilityCopyInventoryButton, value); }
        }
        private Visibility _VisibilityCopyInventoryButton = Visibility.Collapsed;


        public Visibility VisibilityCopySectionsButton
        {
            get { return _VisibilityCopySectionsButton; }
            set { SetProperty(ref _VisibilityCopySectionsButton, value); }
        }
        private Visibility _VisibilityCopySectionsButton = Visibility.Collapsed;


        public Visibility VisibilityDeleteSystemButton
        {
            get { return _VisibilityDeleteSystemButton; }
            set { SetProperty(ref _VisibilityDeleteSystemButton, value); }
        }
        private Visibility _VisibilityDeleteSystemButton = Visibility.Collapsed;


        public Visibility VisibilityInspectionButton
        {
            get { return _VisibilityInspectionButton; }
            set { SetProperty(ref _VisibilityInspectionButton, value); }
        }
        private Visibility _VisibilityInspectionButton = Visibility.Collapsed;


        public QuickObservableCollection<Control> ToolbarMenuItems { get; } = new QuickObservableCollection<Control>();


        // **************** Class data members ********************************************** //

        private readonly Dictionary<string, IEnumerable<Control>> _toolBarMenuItemsDictionary = 
            new Dictionary<string, IEnumerable<Control>>();

        [CanBeNull] 
        protected IFacility LocalFacilityInfo
        {
            get => _localFacilityInfo;
            set
            {
                if (SetProperty(ref _localFacilityInfo, value))
                    UpdateTreeView(ViewTabItem);
            }
        }
        [CanBeNull] private IFacility _localFacilityInfo;

        protected override IConfigInfo LocalConfigInfo
        {
            get => base.LocalConfigInfo;
            set
            {
                base.LocalConfigInfo = value;
                ConfigurationFilename = base.LocalConfigInfo?.FileName;
            }
        }

        protected override IBredInfo LocalBredInfo
        {
            get => base.LocalBredInfo;
            set
            {
                base.LocalBredInfo = value;
                LocalFacilityInfo = base.LocalBredInfo?.FacilityInfo;
                BredFilename      = base.LocalBredInfo?.FileName;
                InspectedByUser   = LocalFacilityInfo?.Inspections?.LastOrDefault();
            }
        }

        // **************** Class constructors ********************************************** //

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public ShellViewModel()
        {
            RegionManagerName = "ShellItemControl";

            CmdInspectionMode          = new DelegateCommand(OnCmdInspectionMode       );
            CmdAddComponent            = new DelegateCommand(OnCmdAddComponent         );
            CmdAddSystem               = new DelegateCommand(OnCmdAddSystem            );
            CmdCopyInventory           = new DelegateCommand(OnCmdCopyInventory        );
            CmdCopySections            = new DelegateCommand(OnCmdCopySections         );
            CmdDefaultInventoryMode    = new DelegateCommand(OnCmdDefaultInventoryMode );
            CmdDeleteSystem            = new DelegateCommand(OnCmdDeleteSystem         );
            CmdInspectionMode          = new DelegateCommand(OnCmdInspectionMode       );
            CmdMenuAbout               = new DelegateCommand(OnCmdAbout                ); 
            CmdMenuBluebeam            = new DelegateCommand(OnCmdBluebeam             );
            CmdMenuCalculators         = new DelegateCommand(OnCmdCalculators          );
            CmdMenuExit                = new DelegateCommand(OnCmdExit                 );
            CmdMenuInspectionSummary   = new DelegateCommand(OnCmdInspectionSummary    );
            CmdMenuQcReports           = new DelegateCommand(OnCmdQcReport             );
            CmdMenuSwitchFile          = new DelegateCommand(OnCmdSwitchFile           );
            CmdMenuViewAllSystems      = new DelegateCommand(OnCmdViewAllSystems       );
            CmdMenuViewAssignedSystems = new DelegateCommand(OnCmdViewAssignedSystems  );
            CmdMicOff                  = new DelegateCommand(OnCmdMicOff               );
            CmdMicOn                   = new DelegateCommand(OnCmdMicOn                );
            CmdTabSelectionChanged     = new DelegateCommand<TabItem>(OnTabSelectionChanged);

            InvTreeBorderBackgroundColor   = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightGray);

            // these should be done with a selection style
            FacilityTabBackgroundColor     = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightGreen);
            InventoryTabBackgroundColor    = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            InspectionTabBackgroundColor   = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            QaInventoryTabBackgroundColor  = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            QaInspectionTabBackgroundColor = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);

            // Setup the toolbar menu items dictionary
            SetUpToolbarMenuItems();

            ViewTabIndex = 1;   // force a change at initialization time to update the menu items

            LookupField = "XXXX";

            // Does this work???
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                WindowVisibility = Visibility.Visible;
            else
                WindowVisibility = Visibility.Collapsed;
        }

        // **************** Class members *************************************************** //

        protected override bool GetRegionManager()
        {
            return false;
        }

        private void OnCmdAbout()
        {
            Debug.WriteLine("OnCmdAbout not implemented");
        }

        private void OnCmdAddComponent()
        {
            Debug.WriteLine("OnCmdAddComponent not implemented");
        }

        private void OnCmdAddSystem()
        {
            Debug.WriteLine("OnCmdAddSystem not implemented");
        }

        private void OnCmdAddSection()
        {
            Debug.WriteLine("OnCmdAddSection not implemented");
        }

        private void OnCmdBluebeam()
        {
            Debug.WriteLine("CmdBluebeam not implemented");
        }

        private void OnCmdCalculators()
        {
            Debug.WriteLine("CmdCalculators not implemented");
        }

        private void OnCmdDefaultInventoryMode()
        {
            Debug.WriteLine("OnCmdDefaultInventoryMode not implemented");
        }

        private void OnCmdExit()
        {
            App.Current.Shutdown();
        }

        private void OnCmdInspectionMode()
        {
            Debug.WriteLine("OnCmdInspectionMode not implemented");
        }

        private void OnCmdInspectionSummary()
        {
            Debug.WriteLine("CmdInspectionSummary not implemented");
        }

        private void OnCmdMicOff()
        {
            Debug.WriteLine("CmdMicOff not implemented");
        }

        private void OnCmdMicOn()
        {
            Debug.WriteLine("CmdMicOn not implemented");
        }

        private void OnCmdQcReport()
        {
            MessageBox.Show( "CmdQcReports not implemented");
        }

        private void OnCmdSwitchFile()
        {
            Debug.WriteLine("CmdSwitchFile not implemented");
        }

        private void OnCmdViewAllSystems()
        {
            Debug.WriteLine("CmdViewAllSystems not implemented");
        }

        private void OnCmdViewAssignedSystems()
        {
            Debug.WriteLine("CmdViewAssignedSystems not implemented");
        }

        private void OnCmdInv2InspFacility()
        {
            Debug.WriteLine("OnCmdInv2InspFacility not implemented");
        }

        private void OnCmdInv2InspInventory()
        {
            Debug.WriteLine("OnCmdInv2InspInventory not implemented");
        }

        private void OnCmdInv2InspInspection()
        {
            Debug.WriteLine("OnCmdInv2InspInspection not implemented");
        }

        private void OnCmdInv2InspQcInventory()
        {
            Debug.WriteLine("OnCmdInv2InspQcInventory not implemented");
        }

        private void OnCmdInv2InspQcInspection()
        {
            Debug.WriteLine("OnCmdInv2InspQcInspection not implemented");
        }

        private void OnCmdDeleteSystem()
        {
            Debug.WriteLine("OnCmdDeleteSystem not implemented");
        }

        private void OnCmdCopySections()
        {
            Debug.WriteLine("OnCmdCopySections not implemented");
        }

        private void OnCmdCopyInventory()
        {
            Debug.WriteLine("OnCmdCopyInventory not implemented");
        }

        private void OnCmdCopyInspection()
        {
            Debug.WriteLine("OnCmdCopyInspection not implemented");
        }

        private void OnTabSelectionChanged([CanBeNull] TabItem tabItem)
        {
            if (tabItem != null)
            {
                SetToolbarMenuItems(tabItem);

                EventAggregator.GetEvent<PubSubEvent<TabChangeEvent>>()
                    .Publish(new TabChangeEvent("ViewTabControl", tabItem.Name));
            }
        }

        private void UpdateTreeView([CanBeNull] TabItem tabItem)
        {
            TreeItemsViewSource.Clear();

            if ((tabItem != null) && (LocalFacilityInfo != null))
            {
                Predicate<TreeNode> filter = (arg) => true;;

                VisibilityAddComponentButton   =
                VisibilityAddSectionButton     =
                VisibilityAddSystemButton      = 
                VisibilityCopyInspectionButton =
                VisibilityCopyInventoryButton  =
                VisibilityCopySectionsButton   =
                VisibilityDeleteSystemButton   = Visibility.Collapsed;

                switch (tabItem.Name)
                {
                    case "Facility":
                        {
                            VisibilityAddSystemButton    = 
                            VisibilityDeleteSystemButton = Visibility.Visible;
                            break;
                        }

                    case "Inventory":
                        {
                            VisibilityAddComponentButton =
                            VisibilityAddSectionButton =
                            VisibilityCopyInventoryButton =
                            VisibilityCopySectionsButton = Visibility.Visible;

                            filter = (arg) =>
                            {
                                if ((arg.NodeType == EnumTreeNodeType.FacilityNode) ||
                                    (arg.NodeType == EnumTreeNodeType.SystemNode))
                                {
                                    return arg.Children.Any();
                                }

                                return (arg.NodeType == EnumTreeNodeType.ComponentNode);
                            };
                            break;
                        }

                    case "Inspection":
                        {
                            VisibilityCopyInspectionButton = Visibility.Visible;

                            filter = (arg) =>
                            {
                                if ((arg.NodeType == EnumTreeNodeType.FacilityNode) ||
                                    (arg.NodeType == EnumTreeNodeType.SystemNode))
                                {
                                    return arg.Children.Any();
                                }

                                return (arg.NodeType == EnumTreeNodeType.ComponentNode);
                            };
                            break;
                        }

                    case "QaInventory":
                    case "QaInspection":
                        {
                            filter = (arg) => ((arg.NodeType == EnumTreeNodeType.FacilityNode) ||
                                           (arg.NodeType == EnumTreeNodeType.SystemNode));
                            break;
                        }
                    default:
                        filter = (arg) => false;
                        break;
                }

                foreach (var facility in LocalFacilityInfo.FacilityTreeNodes)
                    TreeItemsViewSource.Add(TreeNode.BuildTree(facility, filter));
            }

            RaisePropertyChanged(nameof(TreeItemsViewSource));
        }

        public void SetToolbarMenuItems([NotNull] TabItem tabItem)
        {
            ToolbarMenuItems.Clear();

            if (_toolBarMenuItemsDictionary.ContainsKey(tabItem.Name))
            {
                ToolbarMenuItems.AddRange(_toolBarMenuItemsDictionary[tabItem.Name]);
            }
            else
            {
                Debug.WriteLine("Invalid tabItem=\"" + tabItem.Name + "\"");
            }
        }

        public void SetUpToolbarMenuItems()
        {
            var sepStyle = Application.Current.FindResource("MenuSeparatorStyle") as Style;

            _toolBarMenuItemsDictionary.Clear();

            // ******************************
            // Facility Menu
            // ******************************
            var lFacilityMenuItems = new List<Control>
            {
                new Button
                {
                    Name = "Inv2Insp",
                    ToolTip = "Inv <-> Insp",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnCmdInv2InspFacility),
                    Content = new System.Windows.Controls.Image
                    {
                        Source =
                            new BitmapImage(new Uri(@"pack://application:,,,/Resources/Inventory.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                },
                new Separator() {Style = sepStyle},
                new Button
                {
                    Name = "AddSystem",
                    ToolTip = "Add System",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnCmdAddSystem),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Add.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                },
                new Separator() {Style = sepStyle},
                new Button
                {
                    Name = "DeleteSystem",
                    ToolTip = "Delete System",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnCmdDeleteSystem),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Delete.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                }
            };

            _toolBarMenuItemsDictionary.Add("Facility", lFacilityMenuItems);

            // ******************************
            // Inventory Menu
            // ******************************
            var lInventoryMenuItems = new List<Control>
            {
                new Button
                {
                    Name = "Inv2Insp",
                    ToolTip = "Inv <-> Insp",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnCmdInv2InspInventory),
                    Content = new System.Windows.Controls.Image
                    {
                        Source =
                            new BitmapImage(new Uri(@"pack://application:,,,/Resources/Inventory.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                },
                new Separator() {Style = sepStyle},
                new Button
                {
                    Name = "AddComponent",
                    ToolTip = "Add Component",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnCmdAddComponent),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Add.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                },
                new Separator() {Style = sepStyle},
                new Button
                {
                    Name = "AddSection",
                    ToolTip = "Add Section",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnCmdAddSection),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Add.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                },
                new Separator() {Style = sepStyle},
                new Button
                {
                    Name = "CopySections",
                    ToolTip = "Copy Sections",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnCmdCopySections),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Copy.jpg")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                },
                new Separator() {Style = sepStyle},
                new Button
                {
                    Name = "CopyInventory",
                    ToolTip = "Copy Inventory",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnCmdCopyInventory),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Copy.jpg")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                }
            };

            _toolBarMenuItemsDictionary.Add("Inventory", lInventoryMenuItems);

            // ******************************
            // Inspection Menu
            // ******************************
            var lInspectionMenuItems = new List<Control>
            {
                new Button
                {
                    Name = "Inv2Insp",
                    ToolTip = "Inv <-> Insp",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnCmdInv2InspInspection),
                    Content = new System.Windows.Controls.Image
                    {
                        Source =
                            new BitmapImage(new Uri(@"pack://application:,,,/Resources/Inventory.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                },
                new Separator() {Style = sepStyle},
                new Button
                {
                    Name = "CopyInspection",
                    ToolTip = "Copy Inspection",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnCmdCopyInspection),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Copy.jpg")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                }
            };

            _toolBarMenuItemsDictionary.Add("Inspection", lInspectionMenuItems);

            // ******************************
            // QaInventory Menu
            // ******************************
            var lQaInventoryMenuItems = new List<Control>
            {
                new Button
                {
                    Name = "Inv2Insp",
                    ToolTip = "Inv <-> Insp",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnCmdInv2InspQcInventory),
                    Content = new System.Windows.Controls.Image
                    {
                        Source =
                            new BitmapImage(new Uri(@"pack://application:,,,/Resources/Inventory.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                }
            };

            _toolBarMenuItemsDictionary.Add("QaInventory", lQaInventoryMenuItems);

            // ******************************
            // QaInspection Menu
            // ******************************
            var lQaInspectionMenuItems = new List<Control>
            {
                new Button
                {
                    Name = "Inv2Insp",
                    ToolTip = "Inv <-> Insp",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnCmdInv2InspQcInspection),
                    Content = new System.Windows.Controls.Image
                    {
                        Source =
                            new BitmapImage(new Uri(@"pack://application:,,,/Resources/Inventory.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                }
            };

            _toolBarMenuItemsDictionary.Add("QaInspection", lQaInspectionMenuItems);
        }
    }
}
