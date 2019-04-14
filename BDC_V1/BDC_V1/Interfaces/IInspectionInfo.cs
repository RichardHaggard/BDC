using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        string   Component      { get; set; }
        string   Section        { get; set; }
        string   Category       { get; set; }
        string   ComponentType  { get; set; }
        decimal  Quantity       { get; set; }
        DateTime InspectionDate { get; set; }
        string   Note           { get; set; }
        bool     IsPainted      { get; set; }
        EnumRatingType DirectCondition    { get; set; }
        EnumRatingType PaintedCondition   { get; set; }
        EnumInspectionType InspectionType { get; set; }
                                         
        /// <remarks>
        /// on-demand collection storage is allocated on first use
        /// use the <see cref="HasInspectionComments"/> property to check for not empty
        /// </remarks>>
        ObservableCollection<CommentBase> InspectionComments { get; }
        bool HasInspectionComments    { get; }
        bool HasAnyInspectionComments { get; }

        /// <remarks>
        /// on-demand collection storage is allocated on first use
        /// use the <see cref="HasImages"/> property to check for not empty
        ///
        /// If you are going to modify one of these images the ImagesModelBase class may require that
        /// you delete and add the item to get the view to update
        /// </remarks>>
        ObservableCollection<ImageSource> Images { get; }
        bool HasImages { get; }
    }
}
