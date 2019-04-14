using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Events;
using BDC_V1.Views;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class CommentInspectionViewModel : CommentWindows
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdDistressed { get; }

        public bool IsDistressedEnabled
        {
            get => _isDistressedEnabled;
            set => SetProperty(ref _isDistressedEnabled, value);
        }
        private bool _isDistressedEnabled = true;

        // TODO: Move these properties into a separate interface / class

        public EnumRepairType RepairType
        {
            get => _repairType;
            set => SetProperty(ref _repairType, value);
        }
        private EnumRepairType _repairType;

        // **************** Class constructors ********************************************** //

        public CommentInspectionViewModel()
        {
            CmdDistressed = new DelegateCommand(OnDistressed);

            HeaderText = "INSPECTION COMMENTS\n" +
                         "This is a two-line auto-wrap text field";

#if DEBUG
#warning Using MOCK data for CommentInspectionViewModel
            CommentText =
                "DAMAGED - All the wood doors have 70% severe moisture damage.  CRACKED - All of the doors have 65% severe cracking and splintering.";

            RepairType = EnumRepairType.Replace;

            // gets around the "cannot access virtual member in constructor" issue
            _commentaryList.AddRange(new [] {
                new Commentary
                {
                    FacilityId  = "0000",
                    CodeIdText  = "000000",
                    Rating      = EnumRatingType.GPlus,
                    CommentText = "These comments come from the Inspection Comment Window"
                },
                new Commentary
                {
                    FacilityId  = "11057",
                    CodeIdText  = "C102001",
                    Rating      = EnumRatingType.R,
                    CommentText = "DAMAGED - All the wood doors have 70% severe moisture damage. " +
                                  "CRACKED - All of the doors have 65% severe cracking and splintering."
                },
                new Commentary
                {
                    FacilityId  = "11057",
                    CodeIdText  = "C102001",
                    Rating      = EnumRatingType.R,
                    CommentText = "DAMAGED - All the wood doors have 70% severe moisture damage. " + 
                                  "CRACKED - All of the doors have 65% severe cracking and splintering. " +
                                  "Replacement is recommended"
                },
                new Commentary
                {
                    FacilityId  = "12022",
                    CodeIdText  = "D501003",
                    Rating      = EnumRatingType.G,
                    CommentText = "The nameplate on the component was missing certain Section Detail fields. " +
                                  "Section Detail fields have been populated and fields with NA represent data not found."
                },
                new Commentary
                {
                    FacilityId  = "17180",
                    CodeIdText  = "D302001",
                    Rating      = EnumRatingType.GPlus,
                    CommentText = "This unit was in a locked room and not visible."
                },
                new Commentary
                {
                    FacilityId  = "17180",
                    CodeIdText  = "D501003",
                    Rating      = EnumRatingType.AMinus,
                    CommentText = "No A20 and D10 systems present. Could not gain access to Supply RM C342."
                }
            });
#endif
        }

        // **************** Class members *************************************************** //

        protected override List<Commentary> CommentaryList
        {
            get => _commentaryList ?? (_commentaryList = new List<Commentary>());
            set => SetProperty(ref _commentaryList, value);
        }
        private List<Commentary> _commentaryList = new List<Commentary>();

        protected override string CopyWindowTitle => "COPY INSPECTION COMMENT";

        private void OnDistressed()
        {
            Debug.WriteLine("OnDistressed not implemented");
        }

    }
}
