using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public string LabelContent
        {
            get { return _LabelContent; }
            set
            {
                if (_LabelContent != value)
                {
                    _LabelContent = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _LabelContent;


        // **************** Class constructors ********************************************** //

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public ShellViewModel()
        {
            LabelContent = "Something to confirm ShellViewModel is properly bound.";
        }

        // **************** Class members *************************************************** //


    }
}
