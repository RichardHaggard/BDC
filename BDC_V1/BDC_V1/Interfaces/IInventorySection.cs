using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Utils;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IInventorySection : INotifyPropertyChanged
    {
        bool   AddCurrentInspector  { get; set; }
        string ComponentType        { get; set; }
        string Date                 { get; set; }
        string Dcr                  { get; set; }
        string EquipmentCategory    { get; set; }
        bool   PaintedIsChecked     { get; set; }
        string PcRating             { get; set; }
        string PcType               { get; set; }
        string Quantity             { get; set; }
        string SectionName          { get; set; }
        string YearInstalledRenewed { get; set; }
        string YearPc               { get; set; }
        Visibility YearPcVisibility { get; set; }

        [NotNull] INotifyingCollection<string> ComponentTypes      { get; }
        [NotNull] INotifyingCollection<string> Dcrs                { get; }
        [NotNull] INotifyingCollection<string> EquipmentCategories { get; } 
        [NotNull] INotifyingCollection<string> PcRatings           { get; } 
        [NotNull] INotifyingCollection<string> PcTypes             { get; } 
        [NotNull] INotifyingCollection<string> SectionNames        { get; } 

        /// <remarks>
        /// on-demand collection storage is allocated on first use
        /// use the <see cref="HasSectionComments"/> property to check for not empty
        /// </remarks>>
        INotifyingCollection<ICommentSection> SectionComments { get; }
        bool HasSectionComments { get; }

        /// <remarks>
        /// on-demand collection storage is allocated on first use
        /// use the <see cref="HasImages"/> property to check for not empty
        ///
        /// If you are going to modify one of these images the ImagesModelBase class may require that
        /// you delete and add the item to get the view to update
        /// </remarks>>
        [NotNull] INotifyingCollection<ImageSource> Images { get; }
        bool HasImages { get; }
    }
}
