using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.ViewModels
{
    public class PhotoManagementViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        // singleton instance to block multiple instances 
        private static PhotoManagementViewModel _instance;
        public static PhotoManagementViewModel Instance => _instance ?? (_instance = new PhotoManagementViewModel());

        // **************** Class constructors ********************************************** //

        // **************** Class members *************************************************** //

    }
}
