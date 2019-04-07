using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Utils;

namespace BDC_V1.Interfaces
{
    public interface IInspectionInfo : INotifyPropertyChanged
    {
        EnumInspectionType InspectionType { get; set; }
        string   Component         { get; set; }
        string   Section           { get; set; }
        string   Category          { get; set; }
        string   ComponentType     { get; set; }
        decimal  Quantity          { get; set; }
        DateTime InspectionDate    { get; set; }
        string   Note              { get; set; }

        /// <remarks>
        /// on-demand collection storage is allocated on first use
        /// use the <see cref="HasInspectionComments"/> property to check for not empty
        /// </remarks>>
        INotifyingCollection<CommentInspection> InspectionComments { get; }
        bool HasInspectionComments { get; }

        /// <remarks>
        /// on-demand collection storage is allocated on first use
        /// use the <see cref="HasImages"/> property to check for not empty
        /// </remarks>>
        INotifyingCollection<ImageSource> Images { get; }
        bool HasImages { get; }
    }
}
