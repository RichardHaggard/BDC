using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class AddNewComponentViewModel : CloseableWindow
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdCancelUndo  { get; }
        public ICommand CmdOkCommand   { get; }

        public string Component
        {
            get => _component;
            set => SetProperty(ref _component, value);
        }
        private string _component;


        public INotifyingCollection<string> Components
        {
            get => _components;
            set => SetProperty(ref _components, value);
        }
        private INotifyingCollection<string> _components = new NotifyingCollection<string>();

        /// <summary>
        /// EnumControlResult.ResultCancelled indicates cancellation.
        /// EnumControlResult.ResultDeferred  is defer result.
        /// EnumControlResult.ResultSaveNow   is save Comment now.
        /// </summary>
        public EnumControlResult Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }
        private EnumControlResult _result;


        // **************** Class constructors ********************************************** //

        public AddNewComponentViewModel()
        {
            CmdCancelUndo  = new DelegateCommand(OnCancelUndo );
            CmdOkCommand   = new DelegateCommand(OnOkCommand  );

#if DEBUG
#warning Using MOCK data for AddNewComponentViewModel
            Components.AddRange(new[]
            {
                "G2010 ROADWAYS",
                "G2020 PARKING LOTS",
                "G2030 PEDESTRIAN PAVING",
                "G2040 SITE DEVELOPMENT",
                "G2050 LANDSCAPING",
                "G2060 AIRFIELD PACING"
            });
#endif
        }


        // **************** Class members *************************************************** //

        private void OnCancelUndo()
        {
            Result = EnumControlResult.ResultCancelled;
            DialogResultEx = false;
        }

        private void OnOkCommand()
        {
            Result = EnumControlResult.ResultSaveNow;
            DialogResultEx = true;
        }


    }
}
