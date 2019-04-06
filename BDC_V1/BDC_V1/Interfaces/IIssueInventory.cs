﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IIssueInventory : INotifyPropertyChanged
    {
        string FacilityId  { get; set; }
        string SystemId    { get; set; }
        string ComponentId { get; set; }
        string TypeName    { get; set; }
        string SectionName { get; set; }

        /// <remarks>
        /// on-demand collection storage is allocated on first us
        /// use the <see cref="HasInventoryComments"/> property to check for not empty
        /// </remarks>
        [NotNull] 
        INotifyingCollection<ICommentInventory> InventoryComments { get; }
        bool HasInventoryComments { get; }
    }
}