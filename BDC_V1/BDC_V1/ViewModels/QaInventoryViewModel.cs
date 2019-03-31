using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using BDC_V1.ViewModels;
using JetBrains.Annotations;
using Prism.Commands;
using Prism.Mvvm;

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
            CmdFilterByFacilityId  = new DelegateCommand(OnFilterByFacilityId );
            CmdFilterBySystemId    = new DelegateCommand(OnFilterBySystemId   );
            CmdFilterByComponentId = new DelegateCommand(OnFilterByComponentId);
            CmdFilterByTypeName    = new DelegateCommand(OnFilterByTypeName   );
            CmdFilterBySection     = new DelegateCommand(OnFilterBySection    );
            CmdFilterByIssue       = new DelegateCommand(OnFilterByIssue      );

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

            InventoryInfo.AddRange(Enumerable.Repeat(new InventoryType(), 30));
        }

        // **************** Class members *************************************************** //
        private void OnFilterByFacilityId () { Debug.WriteLine("OnFilterByFacilityId  not implemented"); }
        private void OnFilterBySystemId   () { Debug.WriteLine("OnFilterBySystemId    not implemented"); }
        private void OnFilterByComponentId() { Debug.WriteLine("OnFilterByComponentId not implemented"); }
        private void OnFilterByTypeName   () { Debug.WriteLine("OnFilterByTypeName    not implemented"); }
        private void OnFilterBySection    () { Debug.WriteLine("OnFilterBySection     not implemented"); }
        private void OnFilterByIssue      () { Debug.WriteLine("OnFilterByIssue       not implemented"); }

    }
}
