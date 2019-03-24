using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BDC_V1.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        // **************** Class constructors ********************************************** //

        // **************** INotifyPropertyChanged implementation *************************** //

        /// <summary>
        /// This is the data member that external clients will subscribe to in order to
        /// receive property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// This method is called by the Set accessor of each property. 
        /// The CallerMemberName attribute that is applied to the optional propertyName
        /// parameter causes the property name of the caller to be substituted as an argument.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        
        // **************** Class members *************************************************** //


    }
}
