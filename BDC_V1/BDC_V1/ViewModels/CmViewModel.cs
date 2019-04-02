using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDC_V1.Enumerations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class CmViewModel : CommentWindows
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        // **************** Class constructors ********************************************** //

        public CmViewModel()
        {
            HeaderText1 = "<TYPE> Comments for <IDENTIFIER>";
            HeaderText2 = "<IDENTIFIER CONTINUED>";
            CommentText = "This is a comment";
        }

        // **************** Class members *************************************************** //
    }
}
