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
using BDC_V1.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BDC_V1.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdAbout               { get; }
        public ICommand CmdBluebeam            { get; }
        public ICommand CmdCalculators         { get; }
        public ICommand CmdExit                { get; }
        public ICommand CmdInspectionSummary   { get; }
        public ICommand CmdMicOff              { get; }
        public ICommand CmdMicOn               { get; }
        public ICommand CmdQaReports           { get; }
        public ICommand CmdSwitchFile          { get; }
        public ICommand CmdViewAllSystems      { get; }
        public ICommand CmdViewAssignedSystems { get; }

        public Visibility WindowVisibility
        {
            get => _windowVisibility;
            set
            {
                if (_windowVisibility != value)
                {
                    SetProperty(ref _windowVisibility, value);

                    //if ((_windowVisibility == Visibility.Visible) &&
                    //    (ViewTabItem is TabItem tabItem))
                    //{
                    //    SetToolbarMenuItems(tabItem);
                    //}
                }
            }
        }
        private Visibility _windowVisibility;
         

        public string InventoryTreeContent
        {
            get => _inventoryTreeContent;
            set => SetProperty(ref _inventoryTreeContent, value);
        }
        private string _inventoryTreeContent;


        public string Title => @"Builder DC - " + ConfigurationFilename;


        public string SelectedLoginUser
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
        private string _selectedLoginUser;


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

        public string BredFilename
        {
            get => _bredFilename;
            set => SetProperty(ref _bredFilename, value);
        }
        private string _bredFilename;

        public string StatusLookup => "Lookup: " + LookupField;

        public string StatusInspector => "Current Inspector: " + SelectedLoginUser;

        public string StatusInspectedBy => "(Inspected By: " + InspectedByUser + ")";

        public string StatusDateTimeString =>
            StatusDateTime.ToShortDateString() + " " + StatusDateTime.ToShortTimeString();

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

        // this is OneWayToSource so change shouldn't be notified
        // set should only come from the UI
        public int ViewTabIndex { get; set; }

        // this is OneWayToSource so change shouldn't be notified
        // set should only come from the UI
        public object ViewTabItem
        {
            get => _viewTabItem;
            set
            {
                if (_viewTabItem != value)
                {
                    _viewTabItem = value;

                    if (_viewTabItem is TabItem tabItem)
                    {
                        SetToolbarMenuItems(tabItem);
                    }
                }
            }
        }
        private object _viewTabItem;

        public ObservableCollection<object> ToolbarMenuItems { get; } = new ObservableCollection<object>();


        // **************** Class constructors ********************************************** //

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public ShellViewModel()
        {
            CmdExit                = new DelegateCommand(OnCmdExit             );
            CmdAbout               = new DelegateCommand(OnCmdAbout            ); 
            CmdBluebeam            = new DelegateCommand(OnCmdBluebeam         );
            CmdCalculators         = new DelegateCommand(OnCmdCalculators      );
            CmdSwitchFile          = new DelegateCommand(OnCmdSwitchFile       );
            CmdViewAllSystems      = new DelegateCommand(OnCmdViewAllSystems   );
            CmdViewAssignedSystems = new DelegateCommand(OnCmdViewAssignedSystems   );
            CmdInspectionSummary   = new DelegateCommand(OnCmdInspectionSummary);
            CmdQaReports           = new DelegateCommand(OnCmdQcReport         );
            CmdMicOn               = new DelegateCommand(OnCmdMicOn            );
            CmdMicOff              = new DelegateCommand(OnCmdMicOff           );

            ViewTabIndex = -1;

            LookupField     = "XXXX";
            InspectedByUser = "Last, First";
            StatusDateTime  = DateTime.Now;

            // Does this work???
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                WindowVisibility = Visibility.Visible;
            else
                WindowVisibility = Visibility.Collapsed;

            // doesn't do anything here
            //if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            //{
            //    ToolbarMenuItems.Add(new Button
            //    {
            //        Name = "Inv2Insp", 
            //        Header = "Inv <-> Insp", 
            //        IsEnabled = true,
            //        Command = new DelegateCommand(CmdInv2InspFacility)
            //    });

            //    ToolbarMenuItems.Add(new Separator());

            //    ToolbarMenuItems.Add(new Button
            //    {
            //        Name = "AddSystem", 
            //        Header = "Add System", 
            //        IsEnabled = true,
            //        Command = new DelegateCommand(CmdAddSystem)
            //    });

            //    ToolbarMenuItems.Add(new Separator());

            //    ToolbarMenuItems.Add(new Button
            //    {
            //        Name = "DeleteSystem", 
            //        Header = "Delete System", 
            //        IsEnabled = true,
            //        Command = new DelegateCommand(CmdDeleteSystem)
            //    });
            //}
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
            MessageBox.Show( "CmdQaReports not implemented");
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

        private void CmdInv2InspFacility()
        {
            MessageBox.Show( "CmdInv2InspFacility not implemented");
        }

        private void CmdInv2InspInventory()
        {
            MessageBox.Show( "CmdInv2InspInventory not implemented");
        }

        private void CmdInv2InspInspection()
        {
            MessageBox.Show( "CmdInv2InspInspection not implemented");
        }

        private void CmdInv2InspQcInventory()
        {
            MessageBox.Show( "CmdInv2InspQcInventory not implemented");
        }

        private void CmdInv2InspQcInspection()
        {
            MessageBox.Show( "CmdInv2InspQcInspection not implemented");
        }

        private void CmdAddSystem()
        {
            MessageBox.Show( "CmdAddSystem not implemented");
        }

        private void CmdDeleteSystem()
        {
            MessageBox.Show( "CmdDeleteSystem not implemented");
        }

        private void CmdAddComponent()
        {
            MessageBox.Show( "CmdAddComponent not implemented");
        }

        private void CmdAddSection()
        {
            MessageBox.Show( "CmdAddSection not implemented");
        }

        private void CmdCopySections()
        {
            MessageBox.Show( "CmdCopySections not implemented");
        }

        private void CmdCopyInventory()
        {
            MessageBox.Show( "CmdCopyInventory not implemented");
        }

        private void CmdCopyInspection()
        {
            MessageBox.Show( "CmdCopyInspection not implemented");
        }

        //public void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if ((e.Source is TabControl) &&
        //        (sender is TabControl tabControl) &&
        //        (tabControl.SelectedIndex >= 0) &&      // -1 for empty tab items
        //        (tabControl.SelectedItem is TabItem tabItem))
        //    {
        //        SetToolbarMenuItems(tabItem);
        //    }
        //}

        // ??? I don't understand why this doesn't update on the first window showing ???
        public void SetToolbarMenuItems(IFrameworkInputElement tabItem)
        {
            ToolbarMenuItems.Clear();

            var sepStyle = Application.Current.FindResource("MenuSeparatorStyle") as Style;
            //var separatorStyle = new Style {TargetType = typeof (Separator)};
            //var setter = new Setter
            //{
            //    Property = Margin,
            //    Value = "0,2,0,2"
            //};
            //separatorStyle.Setters.Add(setter);

            // Make a button with both an image and text
            //    <Button HorizontalAlignment="Center" VerticalAlignment="Center" Margin="10" >
            //      <StackPanel>
            //          <Image Source="Misc-Settings-icon.png" Height="64" Width="64"/>
            //          <Label Content="Settings" HorizontalAlignment="Center"/>
            //      </StackPanel>
            //    </Button>

            if (tabItem.Name == "Facility")
            {
                ToolbarMenuItems.Add(new Button
                {
                    Name = "Inv2Insp", 
                    ToolTip = "Inv <-> Insp", 
                    IsEnabled = true,
                    Command = new DelegateCommand(CmdInv2InspFacility),
                    Content = new System.Windows.Controls.Image
                    { 
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Inventory.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });

                ToolbarMenuItems.Add(new Separator() {Style = sepStyle});

                ToolbarMenuItems.Add(new Button
                {
                    Name = "AddSystem", 
                    ToolTip = "Add System", 
                    IsEnabled = true,
                    Command = new DelegateCommand(CmdAddSystem),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Add.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });

                ToolbarMenuItems.Add(new Separator() {Style = sepStyle});

                ToolbarMenuItems.Add(new Button
                {
                    Name = "DeleteSystem", 
                    ToolTip = "Delete System", 
                    IsEnabled = true,
                    Command = new DelegateCommand(CmdDeleteSystem),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Delete.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });
            }
            else if (tabItem.Name == "Inventory")
            {
                ToolbarMenuItems.Add(new Button
                {
                    Name = "Inv2Insp", 
                    ToolTip = "Inv <-> Insp", 
                    IsEnabled = true,
                    Command = new DelegateCommand(CmdInv2InspInventory),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Inventory.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });

                ToolbarMenuItems.Add(new Separator() {Style = sepStyle});

                ToolbarMenuItems.Add(new Button
                {
                    Name = "AddComponent", 
                    ToolTip = "Add Component", 
                    IsEnabled = true,
                    Command = new DelegateCommand(CmdAddComponent),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Add.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });

                ToolbarMenuItems.Add(new Separator() {Style = sepStyle});

                ToolbarMenuItems.Add(new Button
                {
                    Name = "AddSection",
                    ToolTip = "Add Section",
                    IsEnabled = true,
                    Command = new DelegateCommand(CmdAddSection),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Add.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });

                ToolbarMenuItems.Add(new Separator() {Style = sepStyle});

                ToolbarMenuItems.Add(new Button
                {
                    Name = "CopySections",
                    ToolTip = "Copy Sections",
                    IsEnabled = true,
                    Command = new DelegateCommand(CmdCopySections),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Copy.jpg")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });

                ToolbarMenuItems.Add(new Separator() {Style = sepStyle});

                ToolbarMenuItems.Add(new Button
                {
                    Name = "CopyInventory",
                    ToolTip = "Copy Inventory",
                    IsEnabled = true,
                    Command = new DelegateCommand(CmdCopyInventory),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Copy.jpg")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });
            }
            else if (tabItem.Name == "Inspection")
            {
                ToolbarMenuItems.Add(new Button
                {
                    Name = "Inv2Insp", 
                    ToolTip = "Inv <-> Insp", 
                    IsEnabled = true,
                    Command = new DelegateCommand(CmdInv2InspInspection),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Inventory.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });

                ToolbarMenuItems.Add(new Separator() {Style = sepStyle});

                ToolbarMenuItems.Add(new Button
                {
                    Name = "CopyInspection", 
                    ToolTip = "Copy Inspection", 
                    IsEnabled = true,
                    Command = new DelegateCommand(CmdCopyInspection),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Copy.jpg")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });
            }
            else if (tabItem.Name == "QaInventory")
            {
                ToolbarMenuItems.Add(new Button
                {
                    Name = "Inv2Insp", 
                    ToolTip = "Inv <-> Insp", 
                    IsEnabled = true,
                    Command = new DelegateCommand(CmdInv2InspQcInventory),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Inventory.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });
            }
            else if (tabItem.Name == "QaInspection")
            {
                ToolbarMenuItems.Add(new Button
                {
                    Name = "Inv2Insp", 
                    ToolTip = "Inv <-> Insp", 
                    IsEnabled = true,
                    Command = new DelegateCommand(CmdInv2InspQcInspection),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Inventory.png")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });
            }
            else
            {
                Debug.WriteLine("Found unknown tab: " + tabItem.Name);
            }

            RaisePropertyChanged(nameof(ToolbarMenuItems));
        }
    }
}
