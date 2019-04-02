﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Enumerations;

namespace BDC_V1.Interfaces
{
    public interface IInspectionType
    {
        string FacilityId     { get; set; }
        string SystemId       { get; set; }
        string ComponentId    { get; set; }
        string TypeName       { get; set; }
        string SectionName    { get; set; }
        string RatingText     { get; }
        EnumRatingType Rating { get; set; }
        string InspectIssue   { get; set; }
    }
}
