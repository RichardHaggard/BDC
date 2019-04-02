﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class QaInventoryViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //
        public ICommand CmdFilterByFacilityId  { get; }
        public ICommand CmdFilterBySystemId    { get; }
        public ICommand CmdFilterByComponentId { get; }
        public ICommand CmdFilterByTypeName    { get; }
        public ICommand CmdFilterBySection     { get; }
        public ICommand CmdFilterByIssue       { get; }
        
        public ICommand CmdRefresh             { get; }
        public ICommand CmdReviewIssue         { get; }
        public ICommand CmdClearFilter         { get; }

        public BitmapSource ImgClearFilter     { get; }
        public BitmapSource ImgReviewIssue     { get; }
        public BitmapSource ImgFilter          { get; }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private string _description;

        public QuickObservableCollection<IInventoryType> InventoryInfo { get; } =
            new QuickObservableCollection<IInventoryType>();

        // **************** Class data members ********************************************** //

        //[CanBeNull] 
        //protected IFacility LocalFacilityInfo
        //{
        //    get => _localFacilityInfo;
        //    set
        //    {
        //        if (SetProperty(ref _localFacilityInfo, value))
        //        {

        //        }
        //    }
        //}
        //[CanBeNull] private IFacility _localFacilityInfo;

        //protected override IConfigInfo LocalConfigInfo
        //{
        //    get => base.LocalConfigInfo;
        //    set
        //    {
        //        base.LocalConfigInfo = value;
        //    }
        //}

        //protected override IBredInfo LocalBredInfo
        //{
        //    get => base.LocalBredInfo;
        //    set
        //    {
        //        base.LocalBredInfo = value;
        //        LocalFacilityInfo = base.LocalBredInfo?.FacilityInfo;
        //    }
        //}

        // **************** Class constructors ********************************************** //
        public QaInventoryViewModel()
        {
            RegionManagerName = "QaInventoryItemControl";

            CmdFilterByFacilityId  = new DelegateCommand(OnFilterByFacilityId );
            CmdFilterBySystemId    = new DelegateCommand(OnFilterBySystemId   );
            CmdFilterByComponentId = new DelegateCommand(OnFilterByComponentId);
            CmdFilterByTypeName    = new DelegateCommand(OnFilterByTypeName   );
            CmdFilterBySection     = new DelegateCommand(OnFilterBySection    );
            CmdFilterByIssue       = new DelegateCommand(OnFilterByIssue      );
        
            CmdRefresh             = new DelegateCommand(OnCmdRefresh         );
            CmdReviewIssue         = new DelegateCommand(OnCmdReviewIssue     );
            CmdClearFilter         = new DelegateCommand(OnCmdClearFilter     );

            ImgClearFilter = MakeTransparent(@"pack://application:,,,/Resources/Filter_Clear.png");
            ImgReviewIssue = MakeTransparent(@"pack://application:,,,/Resources/ReviewIssue.png");
            ImgFilter      = MakeTransparent(@"pack://application:,,,/Resources/Filter.png");

            InventoryInfo.Add(new InventoryType()
            {
                FacilityId = "11057",
                SystemId = "D30",
                ComponentId = "D3010",
                TypeName = "",
                SectionName = "",
                InventIssue = "Missing Section"
            });

            InventoryInfo.Add(new InventoryType()
            {
                FacilityId = "11057",
                SystemId = "D30",
                ComponentId = "D3010",
                TypeName = "D302001",
                SectionName = "N/A",
                InventIssue = "Missing Photo"
            });

            Description = "Filter: 11057";
            //InspectionInfo.AddRange(Enumerable.Repeat(new InventoryType(), 30));
        }

        // **************** Class members *************************************************** //

        protected override bool GetRegionManager()
        {
            return false;
        }

        private BitmapSource MakeTransparent(string resource, System.Drawing.Color? backColor = null)
        {
            var image  = new BitmapImage(new Uri(resource));
            var bitmap = image.ToBitmap();
            bitmap.MakeTransparent(backColor?? System.Drawing.Color.White);
            return bitmap.ToBitmapSource();
        }

        private void OnFilterByFacilityId () { Debug.WriteLine("OnFilterByFacilityId  not implemented"); }
        private void OnFilterBySystemId   () { Debug.WriteLine("OnFilterBySystemId    not implemented"); }
        private void OnFilterByComponentId() { Debug.WriteLine("OnFilterByComponentId not implemented"); }
        private void OnFilterByTypeName   () { Debug.WriteLine("OnFilterByTypeName    not implemented"); }
        private void OnFilterBySection    () { Debug.WriteLine("OnFilterBySection     not implemented"); }
        private void OnFilterByIssue      () { Debug.WriteLine("OnFilterByIssue       not implemented"); }

        private void OnCmdRefresh         () { Debug.WriteLine("OnCmdRefresh          not implemented"); }
        private void OnCmdReviewIssue     () { Debug.WriteLine("OnCmdReviewIssue      not implemented"); }
        private void OnCmdClearFilter     () { Debug.WriteLine("OnCmdClearFilter      not implemented"); }
    }
}
