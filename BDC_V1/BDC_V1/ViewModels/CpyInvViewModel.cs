﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class CpyInvViewModel : CloseableWindow
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdCancelButton { get; }
        public ICommand CmdCopyButton   { get; }

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

        [NotNull]
        public INotifyingCollection<IItemChecklist> ListOfSystems { get; } = 
            new NotifyingCollection<IItemChecklist>();

        // **************** Class constructors ********************************************** //

        public CpyInvViewModel()
        {
            CmdCancelButton = new DelegateCommand(OnCancelButton);
            CmdCopyButton   = new DelegateCommand(OnCopyButton  );

#if DEBUG
#warning Using MOCK data for CmInvViewModel
            ListOfSystems.AddRange(new []
            {
                new ItemChecklist {ItemName = "C10 - INTERIOR CONSTRUCTION", ItemIsChecked = false},
                new ItemChecklist {ItemName = "C20 - STAIRS"               , ItemIsChecked = false},
                new ItemChecklist {ItemName = "C30 - INTERIOR FINISHES"    , ItemIsChecked = false}
            });
#endif
        }

        // **************** Class members *************************************************** //

        private void OnCancelButton()
        {
            Result = EnumControlResult.ResultCancelled;
            DialogResultEx = false;
        }

        private void OnCopyButton()
        {
            Result = EnumControlResult.ResultSaveNow;
            DialogResultEx = true;
        }


    }
}
