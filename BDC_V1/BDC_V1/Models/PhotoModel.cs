using BDC_V1.Classes;

namespace BDC_V1.Models
{
    public class PhotoModel : PropertyBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private string _description;


        public string Filename
        {
            get => _filename;
            set => SetProperty(ref _filename, value);
        }
        private string _filename;


        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _title;


        // **************** Class constructors ********************************************** //

        public PhotoModel()
        {

        }


        public PhotoModel(string filename, string title, string description)
            : this()
        {
            Filename = filename;
            Title = title;
            Description = description;
        }


        // **************** Class members *************************************************** //


    }
}

