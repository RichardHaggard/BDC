using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using BDC_V1.Views;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class FacilityViewModel : FacilityBaseClass
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdFacilityComment { get; }

        public override ObservableCollection<CommentBase> CommentContainer =>
            LocalFacilityInfo?.FacilityComments;

        public override ObservableCollection<ImageSource> ImageContainer => 
            LocalFacilityInfo?.Images;

        // **************** Class constructors ********************************************** //

        public FacilityViewModel()
        {
            RegionManagerName = "FacilityItemControl";

            CmdFacilityComment = new DelegateCommand(OnFacilityComment);
        }

        // **************** Class members *************************************************** //

        private void OnFacilityComment() 
        {             
            OnSelectedComment(null);
        }
    }
}
