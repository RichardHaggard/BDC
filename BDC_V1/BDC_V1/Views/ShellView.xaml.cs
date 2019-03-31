using System;
using System.ComponentModel;
using System.Windows.Controls;
using BDC_V1.Properties;
using BDC_V1.Utils;

namespace BDC_V1.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        // **************** Class constructors ********************************************** //

        public ShellView()
        {
            InitializeComponent();

            //EventAggregator.GetEvent<PubSubEvent<WindowVisibilityEvent>>()
            //    .Subscribe((item) =>
            //{
            //    if ((item == null) || (item.WindowName != this.GetType().Name)) return;
            //    switch (item.WindowVisibility)
            //    {
            //        case Visibility.Hidden : Hide(); break;
            //        case Visibility.Visible: Show(); break;
            //    }
            //});
        }


        // **************** Class members *************************************************** //

        protected override void OnClosing( CancelEventArgs e )
        {
            base.OnClosing( e );
    
           // Use the extension method in the WindowPlace class to save this 
           // window's current position and display state.
            Settings.Default.PlacementShell = this.GetPlacement();
            Settings.Default.Save();
        }


        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Use the extension method in the WindowPlace class to retrieve this 
            // window's previous position and display state, if any.
            this.SetPlacement(Settings.Default.PlacementShell, true);
        }
 

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (DataContext is ShellViewModel viewModel)
            //{
            //    viewModel.TabControl_SelectionChanged(sender, e);
            //}
            //else
            //{
            //    Debug.WriteLine("DataContext is NOT ShellViewModel");
            //}
        }

        //private void UpdateToolsMenu()
        //{
        //    if ((DataContext is ShellViewModel viewModel) &&
        //        (viewModel.WindowVisibility == Visibility.Visible) &&
        //        (ViewTabControl.SelectedIndex >= 0) &&
        //        (ViewTabControl.SelectedItem is TabItem tabItem))
        //    {
        //        viewModel.SetToolbarMenuItems(tabItem);
        //    }
        //    else
        //    {
        //        Debug.WriteLine("ViewTabControl.SelectedItem is NOT TabItem");
        //    }
        //}

        // Doesn't work!
        //protected override void OnContentRendered(EventArgs e)
        //{
        //    base.OnContentRendered(e);

        //    // DO the initial update of the Tools Menu
        //    UpdateToolsMenu();
        //}

        // Doesn't work!
        //protected override void OnGotFocus(RoutedEventArgs e)
        //{
        //    base.OnGotFocus(e);
        //    UpdateToolsMenu();
        //}
    }
}
