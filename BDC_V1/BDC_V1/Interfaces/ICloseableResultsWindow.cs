using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using BDC_V1.Enumerations;

namespace BDC_V1.Interfaces
{
    public interface ICloseableResultsWindow : ICloseableWindow, INotifyPropertyChanged
    {
        /// <summary>
        /// If <see cref="CloseableWindow.DialogResultEx"/>  is true, this indicates what action is to follow
        /// </summary>
        /// <returns>
        /// <see cref="EnumControlResult.ResultDeleteItem"/> delete was selected.
        /// <see cref="EnumControlResult.ResultCancelled "/> indicates cancellation.
        /// <see cref="EnumControlResult.ResultDeferred  "/> is defer result.
        /// <see cref="EnumControlResult.ResultSaveNow   "/> is save Comment now.
        /// </returns>
        EnumControlResult Result { get; set; }
    }
}
