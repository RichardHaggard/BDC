﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public abstract class CloseableWindow : ViewModelBase, ICloseableWindow
    {
        /// <inheritdoc/>
        public virtual bool? DialogResultEx
        {
            get => _dialogResultEx;
            set
            {
                if (CanClose)
                {
                    SetProperty(ref _dialogResultEx, value);
                }
            }
        }
        private bool? _dialogResultEx;

        /// <inheritdoc/>
        public virtual bool CanClose
        {
            get => _canClose;
            set => SetProperty(ref _canClose, value);
        }
        private bool _canClose = true;
    }
}
