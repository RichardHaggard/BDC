using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class Contact : Person, IContact
    {
        // **************** Class enumerations ********************************************** //


        // **************** Class properties ************************************************ //

        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }
        private string _phone;

        public string EMail
        {
            get => _eMail;
            set => SetProperty(ref _eMail, value);
        }
        private string _eMail;

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //

        public Contact() 
            : base()
        {
        }

        public Contact(string firstName, string lastName)
            : base(firstName, lastName)
        {
        }


        // **************** Class members *************************************************** //
    }
}
