using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BDC_V1.Enumerations;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IFacilityInfo : INotifyPropertyChanged, IImageCollection, ICommentCollection
    {
        EnumConstType ConstType    { get; set; }
        string   BuildingId        { get; set; }
        uint     BuildingIdNumber  { get; set; }
        string   BuildingName      { get; set; }
        string   BuildingUse       { get; set; }
        int      YearBuilt         { get; set; }
        string   AlternateId       { get; set; }
        string   AlternateIdSource { get; set; }
        decimal  Width             { get; set; }
        decimal  Depth             { get; set; }
        decimal  Height            { get; set; }
        int      NumFloors         { get; set; }

        /// <remarks>
        /// writing to this overrides the default calculation
        /// </remarks>
        decimal  TotalArea         { get; set; }

        // internal data
        [NotNull] IAddress Address { get; set; }
        [NotNull] IContact Contact { get; set; }

        /// <summary>
        /// Inspections collection.
        /// </summary>
        /// <remarks>
        /// Contains default value, selected index, selected item and <see cref="System.Collections.ICollection"/> functionality
        /// </remarks>
        [NotNull] IIndexedCollection<IInspectionInfo> Inspections { get; }
    }
}
