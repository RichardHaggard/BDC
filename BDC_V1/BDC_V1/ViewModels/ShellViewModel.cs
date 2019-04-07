using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Converters;
using BDC_V1.Enumerations;
using BDC_V1.Events;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using BDC_V1.Views;
using JetBrains.Annotations;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
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
        public ICommand CmdCopyInspection          { get; }
        public ICommand CmdCopyCommentary          { get; }

        // these properties are combinatorial, the components need to raise the property changed for each of these
        public string Title => @"Builder DC - " + BredFilename;

        public string StatusLookup => "Lookup: " + LookupField;

        public string StatusInspector => "Current Inspector: " + SelectedLoginUser?? "";

        public string StatusInspectedBy => "(Inspected By: " + InspectedByUser?.EntryUser?? "" + ")";

        public string StatusDateTimeString =>
            StatusDateTime.ToShortDateString() + " " + StatusDateTime.ToShortTimeString();

        // these properties all raise their own changed events
        public Visibility WindowVisibility
        {
            get => _windowVisibility;
            set => SetProperty(ref _windowVisibility, value);
        }
        private Visibility _windowVisibility = Visibility.Collapsed;


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
        private string _configurationFilename;


        public string BredFilename
        {
            get => _bredFilename;
            set => SetPropertyFlagged(ref _bredFilename, value, nameof(Title));
        }
        private string _bredFilename;


        public DateTime StatusDateTime
        {
            get => _statusDateTime;
            private set => SetPropertyFlagged(ref _statusDateTime, value, nameof(StatusDateTimeString));
        }
        private DateTime _statusDateTime;


        [CanBeNull]
        public IPerson SelectedLoginUser
        {
            get => _selectedLoginUser;
            set => SetPropertyFlagged(ref _selectedLoginUser, value, nameof(StatusInspector));
        }
        private IPerson _selectedLoginUser;


        public string LookupField
        {
            get => _lookupField;
            set => SetPropertyFlagged(ref _lookupField, value, nameof(StatusLookup));
        }
        private string _lookupField;


        [CanBeNull]
        public ICommentInspection InspectedByUser
        {
            get => _inspectedByUser;
            private set => SetProperty(ref _inspectedByUser, value, () =>
            {
                StatusDateTime = _inspectedByUser?.EntryTime ?? DateTime.Now;
                RaisePropertyChanged(nameof(StatusInspectedBy));
            });
        }
        private ICommentInspection _inspectedByUser;


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
            set => SetProperty(ref _viewTabItem, value, () =>
            {
                UpdateTreeView(_viewTabItem);
            });
        }
        private TabItem _viewTabItem;


        public Visibility VisibilityAddComponentButton
        {
            get => _visibilityAddComponentButton;
            set => SetProperty(ref _visibilityAddComponentButton, value);
        }
        private Visibility _visibilityAddComponentButton = Visibility.Collapsed;


        public Visibility VisibilityAddSectionButton
        {
            get => _visibilityAddSectionButton;
            set => SetProperty(ref _visibilityAddSectionButton, value);
        }
        private Visibility _visibilityAddSectionButton = Visibility.Collapsed;


        public Visibility VisibilityAddSystemButton
        {
            get => _visibilityAddSystemButton;
            set => SetProperty(ref _visibilityAddSystemButton, value);
        }
        private Visibility _visibilityAddSystemButton = Visibility.Collapsed;


        public Visibility VisibilityCopyInspectionButton
        {
            get => _visibilityCopyInspectionButton;
            set => SetProperty(ref _visibilityCopyInspectionButton, value);
        }
        private Visibility _visibilityCopyInspectionButton = Visibility.Collapsed;


        public Visibility VisibilityCopyInventoryButton
        {
            get => _visibilityCopyInventoryButton;
            set => SetProperty(ref _visibilityCopyInventoryButton, value);
        }
        private Visibility _visibilityCopyInventoryButton = Visibility.Collapsed;


        public Visibility VisibilityCopySectionsButton
        {
            get => _visibilityCopySectionsButton;
            set => SetProperty(ref _visibilityCopySectionsButton, value);
        }
        private Visibility _visibilityCopySectionsButton = Visibility.Collapsed;


        public Visibility VisibilityDeleteSystemButton
        {
            get => _visibilityDeleteSystemButton;
            set => SetProperty(ref _visibilityDeleteSystemButton, value);
        }
        private Visibility _visibilityDeleteSystemButton = Visibility.Collapsed;


        public Visibility VisibilityInspectionButton
        {
            get => _visibilityInspectionButton;
            set => SetProperty(ref _visibilityInspectionButton, value);
        }
        private Visibility _visibilityInspectionButton = Visibility.Collapsed;


        public INotifyingCollection<TreeViewItem> TreeItemsViewSource =>
            PropertyCollection<TreeViewItem>(ref _treeItemsViewSource);

        [CanBeNull] private INotifyingCollection<TreeViewItem> _treeItemsViewSource;

        public INotifyingCollection<Control> ToolbarMenuItems =>
            PropertyCollection<Control>(ref _toolbarMenuItems);

        [CanBeNull] private INotifyingCollection<Control> _toolbarMenuItems;


        // **************** Class data members ********************************************** //

        private readonly Dictionary<string, IEnumerable<Control>> _toolBarMenuItemsDictionary = 
            new Dictionary<string, IEnumerable<Control>>();

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
                if (base.LocalBredInfo != null)
                {
                    BredFilename = base.LocalBredInfo?.FileName;
                    UpdateTreeView(ViewTabItem);
                }

                else
                {
                    BredFilename = string.Empty;
                    TreeItemsViewSource.Clear();
                    InspectedByUser = null;
                }
            }
        }

        // **************** Class constructors ********************************************** //

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public ShellViewModel()
        {
            RegionManagerName = "ShellItemControl";

            CmdAddComponent            = new DelegateCommand(OnAddComponent         );
            CmdAddSection              = new DelegateCommand(OnAddSection           );
            CmdAddSystem               = new DelegateCommand(OnAddSystem            );
            CmdCopyInventory           = new DelegateCommand(OnCopyInventory        );
            CmdCopySections            = new DelegateCommand(OnCopySections         );
            CmdDefaultInventoryMode    = new DelegateCommand(OnDefaultInventoryMode );
            CmdDeleteSystem            = new DelegateCommand(OnDeleteSystem         );
            CmdInspectionMode          = new DelegateCommand(OnInspectionMode       );
            CmdMenuAbout               = new DelegateCommand(OnAbout                ); 
            CmdMenuBluebeam            = new DelegateCommand(OnBluebeam             );
            CmdMenuCalculators         = new DelegateCommand(OnCalculators          );
            CmdMenuExit                = new DelegateCommand(OnExit                 );
            CmdMenuInspectionSummary   = new DelegateCommand(OnInspectionSummary    );
            CmdMenuQcReports           = new DelegateCommand(OnQcReport             );
            CmdMenuSwitchFile          = new DelegateCommand(OnSwitchFile           );
            CmdMenuViewAllSystems      = new DelegateCommand(OnViewAllSystems       );
            CmdMenuViewAssignedSystems = new DelegateCommand(OnViewAssignedSystems  );
            CmdMicOff                  = new DelegateCommand(OnMicOff               );
            CmdMicOn                   = new DelegateCommand(OnMicOn                );
            CmdCopyInspection          = new DelegateCommand(OnCopyInspection       );

            // DEBUG PURPOSES
            CmdCopyCommentary          = new DelegateCommand(OnCopyCommentary       );

            CmdTabSelectionChanged     = new DelegateCommand<TabItem>(OnTabSelectionChanged);

            InvTreeBorderBackgroundColor = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightGray);

            // these should be done with a selection style
            FacilityTabBackgroundColor     = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightSkyBlue);
            InventoryTabBackgroundColor    = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.SkyBlue);
            InspectionTabBackgroundColor   = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DeepSkyBlue);
            QaInventoryTabBackgroundColor  = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
            QaInspectionTabBackgroundColor = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DarkBlue);

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

        private void OnCopyCommentary()
        {
            var dlg = new CpyCmView();
            dlg.ShowDialog();
        }

        private void OnAbout()
        {
            Debug.WriteLine("OnCmdAbout not implemented");
        }

        private void OnAddComponent()
        {
            var dlg = new AddNewComponentView();
            dlg.ShowDialog();
        }

        private void OnAddSystem()
        {
           var dlg = new AddSystemView();
            dlg.ShowDialog();
        }

        private void OnAddSection()
        {
            Debug.WriteLine("OnAddSection not implemented");
        }

        private void OnBluebeam()
        {
            Debug.WriteLine("CmdBluebeam not implemented");
        }

        private void OnCalculators()
        {
            Debug.WriteLine("CmdCalculators not implemented");
        }

        private void OnDefaultInventoryMode()
        {
            Debug.WriteLine("OnDefaultInventoryMode not implemented");
        }

        private void OnExit()
        {
            App.Current.Shutdown();
        }

        private void OnInspectionMode()
        {
            Debug.WriteLine("OnInspectionMode not implemented");
        }

        private void OnInspectionSummary()
        {
           var dlg = new CmInspView();
           dlg.ShowDialog();
        }

        private void OnMicOff()
        {
            Debug.WriteLine("CmdMicOff not implemented");
        }

        private void OnMicOn()
        {
            Debug.WriteLine("CmdMicOn not implemented");
        }

        private void OnQcReport()
        {
            MessageBox.Show( "CmdQcReports not implemented");
        }

        private void OnSwitchFile()
        {
            Debug.WriteLine("CmdSwitchFile not implemented");
        }

        private void OnViewAllSystems()
        {
            Debug.WriteLine("CmdViewAllSystems not implemented");
        }

        private void OnViewAssignedSystems()
        {
            Debug.WriteLine("CmdViewAssignedSystems not implemented");
        }

        private void OnInv2InspFacility()
        {
            Debug.WriteLine("OnInv2InspFacility not implemented");
        }

        private void OnInv2InspInventory()
        {
            Debug.WriteLine("OnInv2InspInventory not implemented");
        }

        private void OnInv2InspInspection()
        {
            Debug.WriteLine("OnInv2InspInspection not implemented");
        }

        private void OnInv2InspQcInventory()
        {
            Debug.WriteLine("OnInv2InspQcInventory not implemented");
        }

        private void OnInv2InspQcInspection()
        {
            Debug.WriteLine("OnInv2InspQcInspection not implemented");
        }

        private void OnDeleteSystem()
        {
            Debug.WriteLine("OnDeleteSystem not implemented");
        }

        private void OnCopySections()
        {
            Debug.WriteLine("OnCopySections not implemented");
        }

        private void OnCopyInventory()
        {
            var dlg = new CpyInvView();
            dlg.ShowDialog();
        }

        private void OnCopyInspection()
        {
            Debug.WriteLine("OnCopyInspection not implemented");
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

            if ((tabItem != null) && (LocalBredInfo != null))
            {
                Predicate<IComponentBase> filter = (arg) => true;;

                VisibilityAddComponentButton   =
                VisibilityAddSectionButton     =
                VisibilityAddSystemButton      = 
                VisibilityCopyInspectionButton =
                VisibilityCopyInventoryButton  =
                VisibilityCopySectionsButton   =
                VisibilityDeleteSystemButton   = Visibility.Collapsed;

                // build the total list of treeItem nodes
                TreeItemsViewSource.AddRange(BuildTreeItems());

                // Filter (hide) items that shouldn't be displayed
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

                            //filter = (arg) => ((arg.ComponentType == EnumComponentTypes.FacilityType) ||
                            //                   (arg.ComponentType == EnumComponentTypes.SystemType) ||
                            //                   (arg.ComponentType == EnumComponentTypes.SectionType));
                            break;
                        }

                    case "Inspection":
                        {
                            VisibilityCopyInspectionButton = Visibility.Visible;

                            //filter = (arg) => ((arg.ComponentType == EnumComponentTypes.FacilityType) ||
                            //                   (arg.ComponentType == EnumComponentTypes.SystemType) ||
                            //                   (arg.ComponentType == EnumComponentTypes.SectionType));
                            break;
                        }

                    case "QaInventory":
                    case "QaInspection":
                        {
                            //filter = (arg) => ((arg.ComponentType == EnumComponentTypes.FacilityType) ||
                            //                   (arg.ComponentType == EnumComponentTypes.SystemType));
                            break;
                        }

                    default:
                        //filter = (arg) => false;
                        break;
                }

                FilterNodeTree(TreeItemsViewSource, filter);
            }

            RaisePropertyChanged(nameof(TreeItemsViewSource));
        }

        [CanBeNull]
        protected IList<TreeViewItem> BuildTreeItems()
        {
            if (!(LocalBredInfo?.HasFacilities).Equals(true)) return null;

            var facilitiesList = new List<TreeViewItem>();

            // ReSharper disable once PossibleNullReferenceException
            foreach (var item in LocalBredInfo.FacilityInfo)
            {
                var treeItem = BuildTreeItems(item);
                if (treeItem != null) facilitiesList.Add(treeItem);
            }

            return facilitiesList;
        }

        [CanBeNull]
        protected TreeViewItem BuildTreeItems([CanBeNull] IComponentBase component)
        {
            if (component == null) return null;

            var safeName = new string(component.ComponentName.Where(char.IsLetterOrDigit).ToArray());

            var node = new TreeViewItem
            {
                Name                       = safeName,
                Foreground                 = Brushes.Black,
                HorizontalContentAlignment = HorizontalAlignment.Left,  // removes some mysterious runtime warnings
                VerticalContentAlignment   = VerticalAlignment.Center,
                DataContext                = component,
            };

            node.SetBinding(HeaderedItemsControl.HeaderProperty, new Binding
            {
                Path = new PropertyPath("ComponentName")
            });

            node.SetBinding(Control.FontWeightProperty, new Binding
            {
                Path      = new PropertyPath("ComponentType"),
                Converter = new SystemElementFontWeightConverter()
            });

            node.SetBinding(Control.FontSizeProperty, new Binding
            {
                Path               = new PropertyPath("ComponentType"),
                Converter          = new SystemElementFontSizeConverter(),
                ConverterParameter = node.FontSize
            });

            // ReSharper disable once UseObjectOrCollectionInitializer
            var bindsBackground = new MultiBinding();
            bindsBackground.Converter = new SystemElementBackgroundConverter();
            bindsBackground.Bindings.AddRange(new[]
            {
                new Binding("ComponentType"),
                new Binding("HasAnyQaIssues"),
            });
            node.SetBinding(Control.BackgroundProperty, bindsBackground);

            if (component.HasComponents)
            {
                foreach (var item in component.Components)
                {
                    var treeItem = BuildTreeItems(item);
                    if (treeItem != null) node.Items.Add(treeItem);
                }
            }

            return node;
        }

        protected void FilterNodeTree([CanBeNull] ItemCollection nodeList, [NotNull] Predicate<IComponentBase> filter)
        {
            if (nodeList == null) return;

            foreach (var node in nodeList)
                FilterNodeTree(node as TreeViewItem, filter);
        }

        protected void FilterNodeTree([CanBeNull] TreeViewItem treeNode, [NotNull] Predicate<IComponentBase> filter)
        {
            if (treeNode == null) return;

            if (treeNode.DataContext is IComponentBase component)
            {
                if (filter(component))
                {
                    treeNode.Visibility = Visibility.Visible;
                    treeNode.IsEnabled = true;
                }
                else
                {
                    treeNode.Visibility = Visibility.Collapsed;
                    treeNode.IsEnabled = false;
                }
            }

            if (treeNode.Items.Count > 0) FilterNodeTree(treeNode.Items, filter);
        }

        protected void FilterNodeTree([CanBeNull] IEnumerable<TreeViewItem> nodeList, [NotNull] Predicate<IComponentBase> filter)
        {
            if (nodeList == null) return;

            foreach (var treeNode in nodeList)
                FilterNodeTree(treeNode, filter);
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
            // ComponentFacility Menu
            // ******************************
            var lFacilityMenuItems = new List<Control>
            {
                new Button
                {
                    Name = "Inv2Insp",
                    ToolTip = "Inv <-> Insp",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnInv2InspFacility),
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
                    Command = new DelegateCommand(OnAddSystem),
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
                    Command = new DelegateCommand(OnDeleteSystem),
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
                    Command = new DelegateCommand(OnInv2InspInventory),
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
                    ToolTip = "Add ComponentInventory",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnAddComponent),
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
                    Command = new DelegateCommand(OnAddSection),
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
                    Command = new DelegateCommand(OnCopySections),
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
                    Command = new DelegateCommand(OnCopyInventory),
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
            // InspectionComment Menu
            // ******************************
            var lInspectionMenuItems = new List<Control>
            {
                new Button
                {
                    Name = "Inv2Insp",
                    ToolTip = "Inv <-> Insp",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnInv2InspInspection),
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
                    ToolTip = "Copy InspectionComment",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnCopyInspection),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Copy.jpg")),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                }
            };

            _toolBarMenuItemsDictionary.Add("InspectionComment", lInspectionMenuItems);

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
                    Command = new DelegateCommand(OnInv2InspQcInventory),
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
                    Command = new DelegateCommand(OnInv2InspQcInspection),
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
