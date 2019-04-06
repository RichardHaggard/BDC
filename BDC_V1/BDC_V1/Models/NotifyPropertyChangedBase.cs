using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BDC_V1.Models
{
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        // **************** Class constructors ********************************************** //

        // **************** INotifyPropertyChanged interface implementation ***************** //

        /// <summary>
        /// This is the data member that subscribers register themselves with
        /// in order to be notified when something has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Publisher calls here to notify subscribers that a property has changed.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    // **************** Class members *************************************************** //

}
