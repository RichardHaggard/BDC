using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using BDC_V1.Classes;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IInventorySectionType
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
        string SectionComment       { get; set; }
        string SectionName          { get; set; }
        string YearInstalledRenewed { get; set; }
        string YearPc               { get; set; }
        Visibility YearPcVisibility { get; set; }

        [NotNull] QuickObservableCollection<string> ComponentTypes      { get; }
        [NotNull] QuickObservableCollection<string> Dcrs                { get; }
        [NotNull] QuickObservableCollection<string> EquipmentCategories { get; } 
        [NotNull] QuickObservableCollection<string> PcRatings           { get; } 
        [NotNull] QuickObservableCollection<string> PcTypes             { get; } 
        [NotNull] QuickObservableCollection<string> SectionNames        { get; } 
        [NotNull] QuickObservableCollection<ImageSource> Images         { get; }
    }
}
