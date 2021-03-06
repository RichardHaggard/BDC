﻿using System;
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
    /// <inheritdoc />
    public interface IIssueInspection : IQcIssueBase
    {
        CommentBase InspectionComment { get; set; }
    }
}
