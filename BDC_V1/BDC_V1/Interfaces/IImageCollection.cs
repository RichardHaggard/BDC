using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IImageCollection : INotifyPropertyChanged
    {
        /// <summary>
        /// Images collection.
        /// </summary>
        /// <remarks>
        /// Contains default value, selected index, selected item and <see cref="System.Collections.ICollection"/> functionality
        /// </remarks>
        [NotNull] ObservableCollection<ImageSource> Images { get; }
    }
}
