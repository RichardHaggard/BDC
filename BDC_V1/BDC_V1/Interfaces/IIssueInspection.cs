using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IIssueInspection : INotifyPropertyChanged
    {
        string FacilityId     { get; set; }
        string SystemId       { get; set; }
        string ComponentId    { get; set; }
        string TypeName       { get; set; }
        string SectionName    { get; set; }
        string RatingText     { get; }
        EnumRatingType Rating { get; set; }

        /// <remarks>
        /// on-demand collection storage is allocated on first us
        /// use the <see cref="HasInspectionComments"/> property to check for not empty
        /// </remarks>
        [NotNull] 
        ObservableCollection<CommentInspection> InspectionComments { get; }
        bool HasInspectionComments    { get; }
        bool HasAnyInspectionComments { get; }
    }
}
