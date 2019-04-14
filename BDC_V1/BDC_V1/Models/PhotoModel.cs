using BDC_V1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Models
{
    public class PhotoModel : PropertyBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public string Description
        {
            get { return _Description; }
            set { SetProperty(ref _Description, value); }
        }
        public string _Description;


        public string Filename
        {
            get { return _Filename; }
            set { SetProperty(ref _Filename, value); }
        }
        public string _Filename;


        public string Title
        {
            get { return _Title; }
            set { SetProperty(ref _Title, value); }
        }
        public string _Title;


        // **************** Class constructors ********************************************** //

        public PhotoModel()
        {

        }


        public PhotoModel( string filename, string title, string description )
            :this()
        {
            Filename    = filename;
            Title       = title;
            Description = description;
        }


        // **************** Class members *************************************************** //


    }
}
