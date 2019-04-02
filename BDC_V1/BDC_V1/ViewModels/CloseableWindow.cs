using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;

namespace BDC_V1.ViewModels
{
    public class CloseableWindow : ViewModelBase, ICloseableWindow
    {
        /// <summary>
        /// Setting this will cause the dialog to close
        /// </summary>
        public bool? DialogResultEx
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

        /// <summary>
        /// Setting this value to false will block the window from closing
        /// </summary>
        public bool CanClose
        {
            get => _canClose;
            set => SetProperty(ref _canClose, value);
        }
        private bool _canClose;

        public CloseableWindow()
        {
            CanClose = true;
        }
    }
}
