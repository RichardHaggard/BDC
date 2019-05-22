using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
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

        protected override ObservableCollection<ICommentBase> CommentContainerSource =>
            LocalFacilityInfo?.FacilityComments;

        protected override ObservableCollection<ImageSource> ImageContainerSource =>
            LocalFacilityInfo?.Images;

        public override string TabName       => "FACILITY";
        public override string PhotoTypeText => "Facility photos";
        public override string DetailHeaderText => LocalFacilityInfo != null 
            ? $@"Facility Comment for Building {LocalFacilityInfo.BuildingIdNumber} - {LocalFacilityInfo.BuildingName}" 
            : string.Empty;

        // **************** Class constructors ********************************************** //

        public FacilityViewModel()
        {
            RegionManagerName = "FacilityItemControl";
        }

        // **************** Class members *************************************************** //

        protected override void OnSelectedComment(ICommentBase comment, bool isInspection=false, bool isFacility=false) => 
            // ReSharper disable once BaseMethodCallWithDefaultParameter
            base.OnSelectedComment(comment, isFacility:true);
    }
}
