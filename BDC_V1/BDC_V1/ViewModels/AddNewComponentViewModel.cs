using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDC_V1.Enumerations;
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


        public List<string> Components
        {
            get => _components;
            set => SetProperty(ref _components, value);
        }
        private List<string> _components = new List<string>();

        /// <summary>
        /// EnumCommentResult.ResultCancelled indicates cancellation.
        /// EnumCommentResult.ResultDeferred  is defer result.
        /// EnumCommentResult.ResultSaveNow   is save Comment now.
        /// </summary>
        public EnumCommentResult Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }
        private EnumCommentResult _result;


        // **************** Class constructors ********************************************** //

        public AddNewComponentViewModel()
        {
            CmdCancelUndo  = new DelegateCommand(OnCancelUndo );
            CmdOkCommand   = new DelegateCommand(OnOkCommand  );

            Components.Add("G2010 ROADWAYS");
            Components.Add("G2020 PARKING LOTS");
            Components.Add("G2030 PEDESTRIAN PAVING");
            Components.Add("G2040 SITE DEVELOPMENT");
            Components.Add("G2050 LANDSCAPING");
            Components.Add("G2060 AIRFIELD PACING");
        }


        // **************** Class members *************************************************** //

        private void OnCancelUndo()
        {
            Result = EnumCommentResult.ResultCancelled;
            DialogResultEx = false;
        }

        private void OnOkCommand()
        {
            Result = EnumCommentResult.ResultSaveNow;
            DialogResultEx = true;
        }


    }
}
