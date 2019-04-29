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
    public interface IQcIssueBase : INotifyPropertyChanged
    {
        string FacilityId     { get; set; }
        string SystemId       { get; set; }
        string ComponentId    { get; set; }
        string TypeName       { get; set; }
        string SectionName    { get; set; }
        CommentBase Comment   { get; set; }
        EnumRatingType Rating { get; set; }
        bool HasRating        { get; }
    }
}
