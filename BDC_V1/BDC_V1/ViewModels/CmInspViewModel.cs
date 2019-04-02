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

        public bool IsRepairType
        {
            get => _isRepairType;
            set => SetProperty(ref _isRepairType, value);
        }
        private bool _isRepairType;

        public bool IsReplacement
        {
            get => _isReplacement;
            set => SetProperty(ref _isReplacement, value);
        }
        private bool _isReplacement;

        public bool IsNoRecommendation
        {
            get => _isNoRecommendation;
            set => SetProperty(ref _isNoRecommendation, value);
        }
        private bool _isNoRecommendation;

        public EnumRepairType RepairType
        {
            get
            {
                if (IsRepairType ) return EnumRepairType.Repair;
                if (IsReplacement) return EnumRepairType.Replace;
                return EnumRepairType.None;
            }

            set
            {
                switch (value)
                {
                    case EnumRepairType.None:
                        IsNoRecommendation = true;
                        break;
                    case EnumRepairType.Repair:
                        IsRepairType = true;
                        break;
                    case EnumRepairType.Replace:
                        IsReplacement = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
            }
        }

        // **************** Class constructors ********************************************** //

        public CmInspViewModel()
        {
            CmdDistressed = new DelegateCommand(OnDistressed);

            HeaderText1 = "<TYPE> Comments for <IDENTIFIER>";
            HeaderText2 = "<IDENTIFIER CONTINUED>";
            CommentText = "DAMAGED - All the wood doors have 70% severe moisture damage.  CRACKED - All of the doors have 65% severe cracking and splintering.";

            RepairType = EnumRepairType.Replace;
        }

        // **************** Class members *************************************************** //

        private void OnDistressed () { Debug.WriteLine("OnDistressed not implemented"); }
    }
}
