using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using BDC_V1.Enumerations;
using BDC_V1.Events;
using BDC_V1.Views;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class CmInspViewModel : CommentWindows
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdDistressed  { get; }

        public EnumRepairType RepairType
        {
            get => _repairType;
            set => SetProperty(ref _repairType, value);
        }
        private EnumRepairType _repairType;

        // **************** Class constructors ********************************************** //

        public CmInspViewModel()
        {
            CmdDistressed = new DelegateCommand(OnDistressed);

#if DEBUG
#warning Using MOCK data for CmInspViewModel
            HeaderText1 = "<TYPE> Comments for <IDENTIFIER>";
            HeaderText2 = "<IDENTIFIER CONTINUED>";
            CommentText = "DAMAGED - All the wood doors have 70% severe moisture damage.  CRACKED - All of the doors have 65% severe cracking and splintering.";

            RepairType = EnumRepairType.Replace;
#endif
        }

        // **************** Class members *************************************************** //

        private void OnDistressed () { Debug.WriteLine("OnDistressed not implemented"); }
    }
}
