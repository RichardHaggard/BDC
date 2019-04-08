using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Utils;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IInventoryDetail : INotifyPropertyChanged
    {
        string Capacity          { get; set; }
        string ControlTypeMake   { get; set; }
        string CurrentSection    { get; set; }
        string DateManufactured  { get; set; }
        string DetailComment     { get; set; }
        string DetailIdNumber    { get; set; }
        string DetailPhotosCount { get; set; }
        int DetailSelectedIndex  { get; set; }
        string DetailSelector    { get; set; }
        string EquipmentMake     { get; set; }
        string EquipmentType     { get; set; }
        string Location          { get; set; }
        string Manufacturer      { get; set; }
        string Model             { get; set; }
        string SerialNumber      { get; set; }
        string WarrantyCompany   { get; set; }
        string WarrantyCompany2  { get; set; }
        string WarrantyDate      { get; set; }
        string WarrantyDate2     { get; set; }
        string YearInstalled     { get; set; }
        string InventoryDetails  { get; set; }

        [NotNull] INotifyingCollection<string> DetailSelectors { get; }
        [NotNull] INotifyingCollection<string> EquipmentMakes  { get; }
        [NotNull] INotifyingCollection<string> Manufacturers   { get; }

        /// <remarks>
        /// on-demand collection storage is allocated on first use
        /// use the <see cref="HasImages"/> property to check for not empty
        /// </remarks>>
        [NotNull] INotifyingCollection<ImageSource> Images { get; }
        bool HasImages { get; }
    }
}
