using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    public interface ICloseableWindow : INotifyPropertyChanged
    {
        /// <summary>
        /// Setting this will cause the dialog to close
        /// </summary>
        /// <remarks>
        /// Setting <see cref="CanClose"/> to false will prevent this from closing the dialog
        /// </remarks>
        bool? DialogResultEx { get; set; }

        /// <summary>
        /// Setting this value to false will block the window from closing via <see cref="DialogResultEx"/>
        /// </summary>
        bool CanClose { get; set; }
    }
}
