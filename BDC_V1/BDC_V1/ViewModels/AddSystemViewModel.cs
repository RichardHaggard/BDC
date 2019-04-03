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
    public class AddSystemViewModel : CloseableWindow
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

        public AddSystemViewModel()
        {
            CmdCancelUndo  = new DelegateCommand(OnCancelUndo );
            CmdOkCommand   = new DelegateCommand(OnOkCommand  );

            Components.Add("F10 SPECIAL CONSTRUCTION");
            Components.Add("F20 SELECTIVE BUILDING DEMOLITION");
            Components.Add("G10 SITE PREPARATIONS");
            Components.Add("G90 OTHER SITE CONSTRUCTION");
            Components.Add("H30 COASTAL PROTECTION");
            Components.Add("H40 NAV DREDGING/RECLAMATION");
            Components.Add("H60 WATERFRONT DEMOLITION");
            Components.Add("H70 WATERFRONT ATFP");
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
