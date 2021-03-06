﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Enumerations;

namespace BDC_V1.Interfaces
{
    public interface ICommentary : INotifyPropertyChanged
    {
        string FacilityId  { get; set; }
        string CodeIdText  { get; set; }
        string DCRText     { get; }
        string CommentText { get; set; }
        EnumRatingType Rating { get; set; }
    }
}
