using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BDC_V1.Classes;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IInventoryDetailsType
    {
        string Capacity          { get; set; }
        string ControlTypeMake   { get; set; }
        string CurrentSection    { get; set; }
        string DateManufactured  { get; set; }
        string DetailComment     { get; set; }
        string DetailIdNumber    { get; set; }
        string DetailPhotosCount { get; set; }
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

        [NotNull] QuickObservableCollection<string> DetailSelectors { get; }
        [NotNull] QuickObservableCollection<string> EquipmentMakes  { get; }
        [NotNull] QuickObservableCollection<string> Manufacturers   { get; }
        [NotNull] QuickObservableCollection<ImageSource> Images     { get; }
    }
}
