using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class QaInspectionViewModel : ViewModelBase
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

        //public BitmapSource ImgClearFilter     { get; }
        //public BitmapSource ImgReviewIssue     { get; }
        //public BitmapSource ImgFilter          { get; }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private string _description;

        public QuickObservableCollection<IInspectionType> InspectionInfo { get; } =
            new QuickObservableCollection<IInspectionType>();

        // **************** Class data members ********************************************** //
        // **************** Class constructors ********************************************** //
        public QaInspectionViewModel()
        {
            RegionManagerName = "QaInspectionItemControl";

            CmdFilterByFacilityId  = new DelegateCommand(OnFilterByFacilityId );
            CmdFilterBySystemId    = new DelegateCommand(OnFilterBySystemId   );
            CmdFilterByComponentId = new DelegateCommand(OnFilterByComponentId);
            CmdFilterByTypeName    = new DelegateCommand(OnFilterByTypeName   );
            CmdFilterBySection     = new DelegateCommand(OnFilterBySection    );
            CmdFilterByIssue       = new DelegateCommand(OnFilterByIssue      );
        
            CmdRefresh             = new DelegateCommand(OnCmdRefresh         );
            CmdReviewIssue         = new DelegateCommand(OnCmdReviewIssue     );
            CmdClearFilter         = new DelegateCommand(OnCmdClearFilter     );

            //ImgClearFilter = MakeBitmapTransparent.MakeTransparent(@"pack://application:,,,/Resources/Filter_Clear.png");
            //ImgReviewIssue = MakeBitmapTransparent.MakeTransparent(@"pack://application:,,,/Resources/ReviewIssue.png");
            //ImgFilter      = MakeBitmapTransparent.MakeTransparent(@"pack://application:,,,/Resources/Filter.png");

            InspectionInfo.Add(new InspectionType()
            {
                FacilityId   = "11057",
                SystemId     = "D30",
                ComponentId  = "D3010",
                TypeName     = "D301002",
                SectionName  = "N/A",
                Rating       = EnumRatingType.RPlus,
                InspectIssue = "Missing Photo"
            });

            InspectionInfo.Add(new InspectionType()
            {
                FacilityId   = "11057",
                SystemId     = "D30",
                ComponentId  = "D3010",
                TypeName     = "D301002",
                SectionName  = "N/A",
                Rating       = EnumRatingType.RPlus,
                InspectIssue = "Missing Comment"
            });

            InspectionInfo.Add(new InspectionType()
            {
                FacilityId   = "11057",
                SystemId     = "D30",
                ComponentId  = "D3020",
                TypeName     = "D302001",
                SectionName  = "N/A",
                Rating       = EnumRatingType.YMinus,
                InspectIssue = "Missing Inspection photo for Y+ rating"
            });

            InspectionInfo.Add(new InspectionType()
            {
                FacilityId   = "11057",
                SystemId     = "D30",
                ComponentId  = "D3030",
                TypeName     = "D303001",
                SectionName  = "N/A",
                Rating       = EnumRatingType.Unknown,
                InspectIssue = "Missing Inspection"
            });

            Description = "Filter: 11057";
            //InspectionInfo.AddRange(Enumerable.Repeat(new InspectionType(), 30));
        }

        // **************** Class members *************************************************** //

        protected override bool GetRegionManager()
        {
            return false;
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
