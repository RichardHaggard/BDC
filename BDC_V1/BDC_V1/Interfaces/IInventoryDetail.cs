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
        //string DetailComment     { get; set; }
        string DetailIdNumber    { get; set; }
        string DetailPhotosCount { get; set; }
        //int DetailSelectedIndex  { get; set; }
        string DetailSelector    { get; set; }
        //string EquipmentMake     { get; set; }
        string EquipmentType     { get; set; }
        string Location          { get; set; }
        //string Manufacturer      { get; set; }
        string Model             { get; set; }
        string SerialNumber      { get; set; }
        string WarrantyCompany   { get; set; }
        string WarrantyCompany2  { get; set; }
        string WarrantyDate      { get; set; }
        string WarrantyDate2     { get; set; }
        string YearInstalled     { get; set; }
        string InventoryDetails  { get; set; }

        [NotNull] ObservableCollection<string> DetailSelectors { get; }
        [NotNull] IndexedCollection<string>    EquipmentMakes  { get; }
        [NotNull] IndexedCollection<string>    Manufacturers   { get; }

        [NotNull] ObservableCollection<ICommentBase> DetailComments { get; }
        bool HasDetailComments    { get; }
        bool HasAnyDetailComments { get; }

        /// <remarks>
        /// on-demand collection storage is allocated on first use
        /// use the <see cref="HasImages"/> property to check for not empty
        ///
        /// If you are going to modify one of these images the ImagesModelBase class may require that
        /// you delete and add the item to get the view to update
        /// </remarks>>
        [NotNull] ObservableCollection<ImageSource> Images { get; }
        bool HasImages { get; }
    }
}
