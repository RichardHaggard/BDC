using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface ICommentCollection : INotifyPropertyChanged
    {
        /// <summary>
        /// Comments collection.
        /// </summary>
        /// <remarks>
        /// Contains default value, selected index, selected item and <see cref="System.Collections.ICollection"/> functionality
        /// </remarks>
        [NotNull] IIndexedCollection<IComment> Comments { get; }
    }
}
