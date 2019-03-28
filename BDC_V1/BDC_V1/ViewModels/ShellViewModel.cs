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
using BDC_V1.Interfaces;
using BDC_V1.Views;
using JetBrains.Annotations;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BDC_V1.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        // commands are read only to the outside world
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
        public string Title => @"Builder DC - " + ConfigurationFilename;

        public string StatusLookup => "Lookup: " + LookupField;

        public string StatusInspector => "Current Inspector: " + SelectedLoginUser;

        public string StatusInspectedBy => "(Inspected By: " + InspectedByUser + ")";

        public string StatusDateTimeString =>
            StatusDateTime.ToShortDateString() + " " + StatusDateTime.ToShortTimeString();

        // these properties all raise their own changed events
        public Visibility WindowVisibility
        {
            get => _windowVisibility;
            set => SetProperty(ref _windowVisibility, value);
        }
        private Visibility _windowVisibility;


        public string BredFilename
        {
            get => _bredFilename;
            set => SetProperty(ref _bredFilename, value);
        }
        private string _bredFilename;


        public IPerson SelectedLoginUser
        {
            get => _selectedLoginUser;
            set
            {
                if (_selectedLoginUser != value)
                {
                    SetProperty(ref _selectedLoginUser, value);
                    RaisePropertyChanged(nameof(StatusInspector));
                }
            } 
        }
        private IPerson _selectedLoginUser;


        public string ConfigurationFilename
        {
            get => _configurationFilename;
            set
            {
                if (_configurationFilename != value)
                {
                    SetProperty(ref _configurationFilename, value);
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }
        private string _configurationFilename;


        public DateTime StatusDateTime   
        {
            get => _statusDateTime;
            set
            {
                if (_statusDateTime != value)
                {
                    SetProperty(ref _statusDateTime, value);
                    RaisePropertyChanged(nameof(StatusDateTimeString));
                }
            }
        }
        private DateTime _statusDateTime;


        public string LookupField
        {
            get => _lookupField;
            set
            {
                if (_lookupField != value)
                {
                    SetProperty(ref _lookupField, value);
                    RaisePropertyChanged(nameof(StatusLookup));
                }
            }
        }
        private string _lookupField;


        public string InspectedByUser
        {
            get => _inspectedByUser;
            set
            {
                if (_inspectedByUser != value)
                {
                    SetProperty(ref _inspectedByUser, value);
                    RaisePropertyChanged(nameof(StatusInspectedBy));
                }
            }
        }
        private string _inspectedByUser;


        // Used to force the Tab selection internally
        public int ViewTabIndex
        {
            get => _viewTabIndex;
            set => SetProperty(ref _viewTabIndex, value);
        }
        private int _viewTabIndex;


        public ObservableCollection<Control> ToolbarMenuItems { get; } = new ObservableCollection<Control>();

        // **************** Class data members ********************************************** //

        private readonly Dictionary<string, IEnumerable<Control>> _toolBarMenuItemsDictionary = 
            new Dictionary<string, IEnumerable<Control>>();


        // **************** Class constructors ********************************************** //

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public ShellViewModel()
        {
            CmdMenuExit                = new DelegateCommand(OnCmdExit             );
            CmdMenuAbout               = new DelegateCommand(OnCmdAbout            ); 
            CmdMenuBluebeam            = new DelegateCommand(OnCmdBluebeam         );
            CmdMenuCalculators         = new DelegateCommand(OnCmdCalculators      );
            CmdMenuSwitchFile          = new DelegateCommand(OnCmdSwitchFile       );
            CmdMenuViewAllSystems      = new DelegateCommand(OnCmdViewAllSystems   );
            CmdMenuViewAssignedSystems = new DelegateCommand(OnCmdViewAssignedSystems   );
            CmdMenuInspectionSummary   = new DelegateCommand(OnCmdInspectionSummary);
            CmdMenuQcReports           = new DelegateCommand(OnCmdQcReport         );
            CmdMicOn               = new DelegateCommand(OnCmdMicOn            );
            CmdMicOff              = new DelegateCommand(OnCmdMicOff           );
            CmdTabSelectionChanged = new DelegateCommand<TabItem>(OnTabSelectionChanged);

            // Setup the toolbar menu items dictionary
            SetUpToolbarMenuItems();

            ViewTabIndex = 1;   // force a change at initialization time to update the menu items

            LookupField     = "XXXX";
            InspectedByUser = "Last, First";
            StatusDateTime  = DateTime.Now;

            // Does this work???
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                WindowVisibility = Visibility.Visible;
            else
                WindowVisibility = Visibility.Collapsed;
        }

        // **************** Class members *************************************************** //

        private void OnCmdAbout()
        {
            MessageBox.Show( "OnCmdAbout not implemented");
        }

        private void OnCmdBluebeam()
        {
            MessageBox.Show( "CmdBluebeam not implemented");
        }

        private void OnCmdCalculators()
        {
            MessageBox.Show( "CmdCalculators not implemented");
        }

        private void OnCmdExit()
        {
            App.Current.Shutdown();
        }

        private void OnCmdInspectionSummary()
        {
            MessageBox.Show( "CmdInspectionSummary not implemented");
        }

        private void OnCmdMicOff()
        {
            MessageBox.Show( "CmdMicOff not implemented");
        }

        private void OnCmdMicOn()
        {
            MessageBox.Show( "CmdMicOn not implemented");
        }

        private void OnCmdQcReport()
        {
            MessageBox.Show( "CmdQcReports not implemented");
        }

        private void OnCmdSwitchFile()
        {
            MessageBox.Show( "CmdSwitchFile not implemented");
        }

        private void OnCmdViewAllSystems()
        {
            MessageBox.Show( "CmdViewAllSystems not implemented");
        }

        private void OnCmdViewAssignedSystems()
        {
            MessageBox.Show( "CmdViewAssignedSystems not implemented");
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

        private void OnCmdAddSystem()
        {
            Debug.WriteLine("OnCmdAddSystem not implemented");
        }

        private void OnCmdDeleteSystem()
        {
            Debug.WriteLine("OnCmdDeleteSystem not implemented");
        }

        private void OnCmdAddComponent()
        {
            Debug.WriteLine("OnCmdAddComponent not implemented");
        }

        private void OnCmdAddSection()
        {
            Debug.WriteLine("OnCmdAddSection not implemented");
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
            }
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
