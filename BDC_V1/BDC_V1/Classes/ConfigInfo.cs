using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class ConfigInfo : PropertyBase, IConfigInfo
    {
        // **************** Class enumerations ********************************************** //


        // **************** Class properties ************************************************ //

        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }
        private string _fileName;

        public IValidUsers ValidUsers
        {
            get => _validUsers;
            protected set => SetProperty(ref _validUsers, value);
        }
        private IValidUsers _validUsers = new ValidUsers();

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //


        // **************** Class members *************************************************** //
    }
}
