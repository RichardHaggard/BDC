using BDC_V1.Properties;
using BDC_V1.Utils;
using BDC_V1.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BDC_V1.Views
{
    /// <summary>
    /// Interaction logic for PhotoManagementView.xaml
    /// </summary>
    public partial class PhotoManagementView
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        // **************** Class constructors ********************************************** //

        public PhotoManagementView()
        {
            InitializeComponent();
        }

        public PhotoManagementView(string tab, string photoTypeText)
            : this()
            {
            PhotoManagementViewModel vm = DataContext as PhotoManagementViewModel;
            if (vm != null)
            {
                vm.Tab = tab;
                vm.PhotoType = photoTypeText;
            }
        }


        // **************** Class members *************************************************** //

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            // Use the extension method in the WindowPlace class to save this 
            // window's current position and display state.
            Settings.Default.PlacementPM = this.GetPlacement();
            Settings.Default.Save();
        }


        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Use the extension method in the WindowPlace class to retrieve this 
            // window's previous position and display state, if any.
            // ??? For some reason the wrong string is ending up here so ignore for now.
            this.SetPlacement(Settings.Default.PlacementPM, false);
        }


    }
}
