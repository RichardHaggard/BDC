using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;

namespace BDC_V1.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        private const string ConstBgActive     = "White";
        private const string ConstBgInactive   = "Transparent";
        private const string ConstBorderActive = "Black";

        // **************** Class properties ************************************************ //

        // commands are read only to the outside world
        [NotNull] public ICommand CmdAddComponent { get; }
        [NotNull] public ICommand CmdAddSection { get; }
        [NotNull] public ICommand CmdAddSystem { get; }
        [NotNull] public ICommand CmdCopyInventory { get; }
        [NotNull] public ICommand CmdCopySections { get; }
        [NotNull] public ICommand CmdDefaultInventoryMode { get; }
        [NotNull] public ICommand CmdDeleteSystem { get; }
        [NotNull] public ICommand CmdInspectionMode { get; }
        [NotNull] public ICommand CmdMenuExit { get; }
        [NotNull] public ICommand CmdMenuAbout { get; }
        [NotNull] public ICommand CmdMenuBluebeam { get; }
        [NotNull] public ICommand CmdMenuCalculators { get; }
        [NotNull] public ICommand CmdMenuSwitchFile { get; }
        [NotNull] public ICommand CmdMenuViewAllSystems { get; }
        [NotNull] public ICommand CmdMenuViewAssignedSystems { get; }
        [NotNull] public ICommand CmdMenuInspectionSummary { get; }
        [NotNull] public ICommand CmdMenuQcReports { get; }
        [NotNull] public ICommand CmdMicOn { get; }
        [NotNull] public ICommand CmdMicOff { get; }
        [NotNull] public ICommand CmdTabSelectionChanged { get; }
        [NotNull] public ICommand CmdCopyInspection { get; }


        public string MikeOffBg
        {
            get { return _MikeOffBg; }
            set => SetProperty(ref _MikeOffBg, value);
        }
        private string _MikeOffBg = ConstBgInactive;


        public string MikeOffBorderBrush
        {
            get { return _MikeOffBorderBrush; }
            set => SetProperty(ref _MikeOffBorderBrush, value);
        }
        private string _MikeOffBorderBrush = ConstBgInactive;


        public string MikeOnBg
        {
            get { return _MikeOnBg; }
            set => SetProperty(ref _MikeOnBg, value);
        }
        private string _MikeOnBg = ConstBgActive;


        public string MikeOnBorderBrush
        {
            get { return _MikeOnBorderBrush; }
            set => SetProperty(ref _MikeOnBorderBrush, value);
        }
        private string _MikeOnBorderBrush = ConstBorderActive;


        // these properties are combinatorial, the components need to raise the property changed for each of these
        public string Title => @"Builder DC";

        public string StatusLookup => "Lookup: " + LookupField;

        public string StatusInspector => "Current Inspector: " + SelectedLoginUser ?? "";

        public string StatusInspectedBy => "(Inspected By: " + InspectedByUser?.EntryUser ?? "" + ")";

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


        public string InspectionBg
        {
            get => _inspectionBg;
            set => SetProperty(ref _inspectionBg, value, () =>
            {
                if (value == ConstBgActive)
                    InventoryBg = ConstBgInactive;
            });
        }
        private string _inspectionBg = ConstBgInactive;


        public string InventoryBg
        {
            get => _inventoryBg;
            set => SetProperty(ref _inventoryBg, value, () =>
            {
                if (value == ConstBgActive)
                    InspectionBg = ConstBgInactive;
            });
        }
        private string _inventoryBg = ConstBgActive;


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


        // TODO: are these always set as opposites ?? Perhaps this could be combined ???
        public bool ViewAllSystems
        {
            get => _viewAllSystems;
            set => SetProperty(ref _viewAllSystems, value, () =>
            {
                ViewAssignedSystems = !value;
            });
        }
        private bool _viewAllSystems = true;


        public bool ViewAssignedSystems
        {
            get => _viewAssignedSystems;
            set => SetProperty(ref _viewAssignedSystems, value, () =>
            {
                ViewAllSystems = !value;
            });
        }
        private bool _viewAssignedSystems;


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

        // DON'T USE INTERFACES AS ITEM SOURCE - It breaks the Hierarchical templating
        public ObservableCollection<ComponentBase> TreeItemsViewSource
        {
            get => _treeItemsViewSource ?? (_treeItemsViewSource = new ObservableCollection<ComponentBase>());
            set => SetProperty(ref _treeItemsViewSource, value);
        }
        private ObservableCollection<ComponentBase> _treeItemsViewSource;

        public ObservableCollection<Control> ToolbarMenuItems
        {
            get => _toolbarMenuItems;
            private set => SetProperty(ref _toolbarMenuItems, value);
        }
        private ObservableCollection<Control> _toolbarMenuItems = new ObservableCollection<Control>();

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

                    TreeItemsViewSource = base.LocalBredInfo?.FacilityInfo;
                }

                else
                {
                    BredFilename = string.Empty;

                    TreeItemsViewSource = null;
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

            CmdAddComponent            = new DelegateCommand(OnAddComponent        );
            CmdAddSection              = new DelegateCommand(OnAddSection          );
            CmdAddSystem               = new DelegateCommand(OnAddSystem           );
            CmdCopyInventory           = new DelegateCommand(OnCopyInventory       );
            CmdCopySections            = new DelegateCommand(OnCopySections        );
            CmdDefaultInventoryMode    = new DelegateCommand(OnDefaultInventoryMode);
            CmdDeleteSystem            = new DelegateCommand(OnDeleteSystem        );
            CmdInspectionMode          = new DelegateCommand(OnInspectionMode      );
            CmdMenuAbout               = new DelegateCommand(OnAbout               ); 
            CmdMenuBluebeam            = new DelegateCommand(OnBluebeam            );
            CmdMenuCalculators         = new DelegateCommand(OnCalculators         );
            CmdMenuExit                = new DelegateCommand(OnExit                );
            CmdMenuInspectionSummary   = new DelegateCommand(OnInspectionSummary   );
            CmdMenuQcReports           = new DelegateCommand(OnQcReport            );
            CmdMenuSwitchFile          = new DelegateCommand(OnSwitchFile          );
            CmdMenuViewAllSystems      = new DelegateCommand(OnViewAllSystems      );
            CmdMenuViewAssignedSystems = new DelegateCommand(OnViewAssignedSystems );
            CmdMicOff                  = new DelegateCommand(OnMicOff              );
            CmdMicOn                   = new DelegateCommand(OnMicOn               );
            CmdCopyInspection          = new DelegateCommand(OnCopyInspection      );

            CmdTabSelectionChanged     = new DelegateCommand<TabItem>(OnTabSelectionChanged);

            InvTreeBorderBackgroundColor = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightGray);

            // these should be done with a selection style
            FacilityTabBackgroundColor     = new System.Windows.Media.SolidColorBrush(Color.FromRgb(0xE0, 0xF2, 0xFF));
            InventoryTabBackgroundColor    = new System.Windows.Media.SolidColorBrush(Color.FromRgb(0xBD, 0xD7, 0xEE));
            InspectionTabBackgroundColor   = new System.Windows.Media.SolidColorBrush(Color.FromRgb(0x9B, 0xC2, 0xE6));
            QaInventoryTabBackgroundColor  = new System.Windows.Media.SolidColorBrush(Color.FromRgb(0x76, 0xb2, 0xE8));
            QaInspectionTabBackgroundColor = new System.Windows.Media.SolidColorBrush(Color.FromRgb(0x48, 0x8F, 0xD0));

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

        protected override void ViewActivatedEventHandler(object sender, object e)
        {
            base.ViewActivatedEventHandler(sender, e);

            // does this get rid of the start-up flash?
            WindowVisibility = Visibility.Collapsed;
        }

        protected override bool GetRegionManager()
        {
            return false;
        }

        // place holder
        private void OnCopyCommentary()
        {
            var view = new CopyCommentView();
            if (!(view.DataContext is CopyCommentViewModel model))        
                throw new InvalidCastException("Invalid View Model");

            model.WindowTitle = "SELECT COMMENT TO COPY…";
            model.FacilityBaseInfo = null;      // TODO: Put real data here 

            //if (view.ShowDialog() != true) return;
            if (view.ShowDialogInParent(true) != true) return;

            switch (model.Result)
            {
                case EnumControlResult.ResultDeleteItem:
                case EnumControlResult.ResultCancelled:
                case EnumControlResult.ResultDeferred:
                    break;

                case EnumControlResult.ResultSaveNow:
                    break;
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException();
#endif
            }
        }

        // place holder
        private void OnCopyInspectionCommentary()
        {
            var view = new CopyCommentView();
            if (!(view.DataContext is CopyCommentViewModel model))        
                throw new InvalidCastException("Invalid View Model");

            model.WindowTitle = "SELECT COMMENT TO COPY…";
            model.UnFilteredCommentary.Clear();

            //if (view.ShowDialog() != true) return;
            if (view.ShowDialogInParent(true) != true) return;

            switch (model.Result)
            {
                case EnumControlResult.ResultDeleteItem:
                case EnumControlResult.ResultCancelled:
                case EnumControlResult.ResultDeferred:
                    break;

                case EnumControlResult.ResultSaveNow:
                    break;
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException();
#endif
            }
        }

        private void OnAbout()
        {
            var view = new AboutBdcView();
            if (!(view.DataContext is AboutBdcViewModel model)) 
                throw new InvalidCastException("Invalid View Model");

            //view.ShowDialog();
            view.ShowDialogInParent(true);
        }

        private void OnAddComponent()
        {
            var dlg = new AddNewComponentView();
            //dlg.ShowDialog();
            dlg.ShowDialogInParent(true);
        }

        private void OnAddSystem()
        {
            var dlg = new AddSystemView();
            //dlg.ShowDialog();
            dlg.ShowDialogInParent(true);
        }

        private void OnAddSection()
        {
            MessageBox.Show("Add Section", "NOT IMPLEMENTED", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        // ReSharper disable IdentifierTypo
        // ReSharper disable StringLiteralTypo
        private string BluebeamFilename { get; set; }

        private void OnBluebeam()
        {
            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var openFileDlg = new OpenFileDialog
            {
                // ReSharper disable once StringLiteralTypo
                Title            = "Select Bluebeam Excel File",
                FileName         = BluebeamFilename,
                InitialDirectory = docFolder,
                ReadOnlyChecked  = true,
                Multiselect      = false,
                ShowReadOnly     = false,
                AddExtension     = true,
                CheckFileExists  = true,
                CheckPathExists  = true,
                RestoreDirectory = true,
                DefaultExt       = "xlsx",
                FilterIndex      = 1,
                Filter           = "CSV File (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (openFileDlg.ShowDialog() == true)
            {
                BluebeamFilename = openFileDlg.FileName;
            }
        }
        // ReSharper restore StringLiteralTypo
        // ReSharper restore IdentifierTypo

        private void OnCalculators()
        {
            MessageBox.Show("Calculators", "NOT IMPLEMENTED", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void OnDefaultInventoryMode()
        {
            InventoryBg = ConstBgActive;
            MessageBox.Show("Default Inventory", "NOT IMPLEMENTED", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void OnExit()
        {
            App.Current.Shutdown();
        }

        private void OnInspectionMode()
        {
            InspectionBg = ConstBgActive;
            MessageBox.Show("Inspection Mode", "NOT IMPLEMENTED", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void OnInspectionSummary()
        {
            var view = new GeneralCommentView();
            if (!(view.DataContext is GeneralCommentViewModel model)) 
                throw new InvalidCastException("Invalid View Model");

            model.IsDistressedEnabled = true;
            model.FacilityBaseInfo = null;              //TODO: Put real data in here
            model.CommentText = string.Empty;

            view.Owner = Application.Current.MainWindow;
            //if (view.ShowDialog() != true) return;
            if (view.ShowDialogInParent(true) != true) return;

            // TODO: Fix the CommentViewModel to return a CommentBase class on success
        }

        private void OnMicOff()
        {
            MikeOffBg          = ConstBgActive;
            MikeOffBorderBrush = ConstBorderActive;
            MikeOnBg           = ConstBgInactive;
            MikeOnBorderBrush  = ConstBgInactive;
        }

        private void OnMicOn()
        {
            MikeOffBg          = ConstBgInactive;
            MikeOffBorderBrush = ConstBgInactive;
            MikeOnBg           = ConstBgActive;
            MikeOnBorderBrush  = ConstBorderActive;
        }

        private void OnQcReport()
        {
            MessageBox.Show("QC Report", "NOT IMPLEMENTED", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void OnSwitchFile()
        {
            MessageBox.Show("Switch File", "NOT IMPLEMENTED", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void OnViewAllSystems()
        {
            MessageBox.Show("View All Systems", "NOT IMPLEMENTED", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void OnViewAssignedSystems()
        {
            MessageBox.Show("View Assigned Systems", "NOT IMPLEMENTED", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void OnInv2InspFacility()
        {
            MessageBox.Show("Inventory to Inspection, Facility", "NOT IMPLEMENTED", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void OnInv2InspInventory()
        {
            MessageBox.Show("Inventory to Inspection, Inventory", "NOT IMPLEMENTED", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void OnInv2InspInspection()
        {
            MessageBox.Show("Inventory to Inspection, Inspection", "NOT IMPLEMENTED", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void OnInv2InspQcInventory()
        {
            MessageBox.Show("Inventory to Inspection, QC Inventory", "NOT IMPLEMENTED", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void OnInv2InspQcInspection()
        {
            MessageBox.Show("Inventory to Inspection, QC Inspection", "NOT IMPLEMENTED", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void OnDeleteSystem()
        {
            MessageBox.Show("Do you want to delete the currently-selected System for Facility #####?", 
                "DELETE SYSTEM?", MessageBoxButton.OKCancel, MessageBoxImage.Question);
        }

        private void OnCopySections()
        {
            var view = new CopySectionView();
            if (!(view.DataContext is CopySectionViewModel model))        
                throw new InvalidCastException("Invalid View Model");

            //if (view.ShowDialog() != true) return;
            if (view.ShowDialogInParent(true) != true) return;

            switch (model.Result)
            {
                case EnumControlResult.ResultDeleteItem:
                case EnumControlResult.ResultCancelled:
                case EnumControlResult.ResultDeferred:
                    break;

                case EnumControlResult.ResultSaveNow:
                    break;
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException();
#endif
            }
        }

        private void OnCopyInventory()
        {
            var view = new CopyInventoryView();
            if (!(view.DataContext is CopyInventoryViewModel model))        
                throw new InvalidCastException("Invalid View Model");

            //if (view.ShowDialog() != true) return;
            if (view.ShowDialogInParent(true) != true) return;

            switch (model.Result)
            {
                case EnumControlResult.ResultDeleteItem:
                case EnumControlResult.ResultCancelled:
                case EnumControlResult.ResultDeferred:
                    break;

                case EnumControlResult.ResultSaveNow:
                    break;
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException();
#endif
            }
        }

        private void OnCopyInspection()
        {
            var view = new CopyInspectionView();
            if (!(view.DataContext is CopyInspectionViewModel model))        
                throw new InvalidCastException("Invalid View Model");

            //if (view.ShowDialog() != true) return;
            if (view.ShowDialogInParent(true) != true) return;

            switch (model.Result)
            {
                case EnumControlResult.ResultDeleteItem:
                case EnumControlResult.ResultCancelled:
                case EnumControlResult.ResultDeferred:
                    break;

                case EnumControlResult.ResultSaveNow:
                    break;
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException();
#endif
            }
        }

        private void OnTabSelectionChanged([CanBeNull] TabItem tabItem)
        {
            if (tabItem != null)
            {
                SetToolbarMenuItems(tabItem);

                EventTypeAggregator.GetEvent<PubSubEvent<TabChangeEvent>>()
                    .Publish(new TabChangeEvent("ViewTabControl", tabItem.Name));
            }
        }

        private void UpdateTreeView([CanBeNull] TabItem tabItem)
        {
            // this is a total hack to get the tree view to update when the filter changes
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

                // Filter (hide) items that shouldn't be displayed
                // TODO: Filters aren't working... They set the internal IsActive state but the tree view doesn't update.
                switch (tabItem.Name)
                {
                    case "Facility":
                        VisibilityAddSystemButton    =
                        VisibilityDeleteSystemButton = Visibility.Visible;
                        break;

                    case "Inventory":
                        VisibilityAddComponentButton  =
                        VisibilityAddSectionButton    =
                        VisibilityCopyInventoryButton =
                        VisibilityCopySectionsButton  = Visibility.Visible;

                        filter = (arg) => ((arg.ComponentType == EnumComponentTypes.FacilityType) ||
                                           (arg.ComponentType == EnumComponentTypes.SystemType  ) ||
                                           (arg.ComponentType == EnumComponentTypes.SectionType));
                        break;

                    case "Inspection":
                        VisibilityCopyInspectionButton = Visibility.Visible;

                        filter = (arg) => ((arg.ComponentType == EnumComponentTypes.FacilityType) ||
                                           (arg.ComponentType == EnumComponentTypes.SystemType  ) ||
                                           (arg.ComponentType == EnumComponentTypes.SectionType));
                        break;

                    case "QaInventory":
                    case "QaInspection":
                        filter = (arg) => ((arg.ComponentType == EnumComponentTypes.FacilityType) ||
                                           (arg.ComponentType == EnumComponentTypes.SystemType));
                        break;

                    default:
                        filter = (arg) => false;
                        break;
                }

                // Filter the tree
                // TODO: This will leave tree items that all the children are collapsed showing
                //       expand arrows that don't work
                FilterComponentTree(TreeItemsViewSource, filter);
            }
        }

        protected void FilterComponentTree(
            [CanBeNull] IEnumerable<ComponentBase> componentList, 
            [NotNull] Predicate<IComponentBase> filter)
        {
            if (componentList == null) return;

            foreach (var facility in componentList)
                FilterComponentTree(facility, filter);
        }

        protected void FilterComponentTree(
            [CanBeNull] IComponentBase component, 
            [NotNull] Predicate<IComponentBase> filter)
        {
            if (component == null) return;

            component.IsActive = filter(component);

            if (component.HasChildren) 
                FilterComponentTree(component.Children, filter);
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
                    Name = "AddChild",
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
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Copy.png")),
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
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Copy.png")),
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
                    ToolTip = "Copy Inspection",
                    IsEnabled = true,
                    Command = new DelegateCommand(OnCopyInspection),
                    Content = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Copy.png")),
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
