using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Utils;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public interface IComponentFacility : IComponentBase, INotifyPropertyChanged
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

        /// <remarks>
        /// on-demand collection storage is allocated on first us
        /// use the <see cref="HasImages"/> property to check for not empty
        ///
        /// If you are going to modify one of these images the ImagesModelBase class may require that
        /// you delete and add the item to get the view to update
        /// </remarks>
        [NotNull] ObservableCollection<ImageSource> Images { get; }

        /// <remarks>
        /// on-demand collection storage is allocated on first us
        /// use the <see cref="HasInspections"/> property to check for not empty
        /// </remarks>
        [NotNull] ObservableCollection<InspectionInfo> Inspections { get; }

        /// <remarks>
        /// on-demand collection storage is allocated on first us
        /// use the  <see cref="HasFacilityComments"/> property to check for not empty
        /// </remarks>
        [NotNull] ObservableCollection<CommentBase> FacilityComments { get; }
    }
}
