using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using BDC_V1.Mock_Data;
using BDC_V1.Services;
using BDC_V1.Utils;
using BDC_V1.Views;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class InventorySectionViewModel : FacilityBaseClass
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull] public ICommand CmdAddSection         { get; }
        [NotNull] public ICommand CmdCancelEdit         { get; }
        [NotNull] public ICommand CmdDecValue           { get; }
        [NotNull] public ICommand CmdDeleteSection      { get; }
        [NotNull] public ICommand CmdIncValue           { get; }
        [NotNull] public ICommand CmdNextSection        { get; }
        [NotNull] public ICommand CmdNotEnergyEfficient { get; }
        [NotNull] public ICommand CmdPaintedCoated      { get; }
        [NotNull] public ICommand CmdSectionComment     { get; }
        [NotNull] public ICommand CmdToday              { get; }

        // TODO: Should this be part of the IInventorySection instead of here ???
        public bool Estimated
        {
            get => _estimated;
            set => SetProperty(ref _estimated, value);
        }
        private bool _estimated;

        public bool IsRemembered
        {
            get => _isRemembered;
            set =>  SetProperty(ref _isRemembered, value);
        }
        private bool _isRemembered;

        public bool IsAllPaintedChecked
        {
            get => _isAllPaintedChecked;
            set => SetProperty(ref _isAllPaintedChecked, value, OnAllPainted);
        }
        private bool _isAllPaintedChecked;

        public bool NotEnergyEfficient
        {
            get => _notEnergyEfficient;
            set => SetProperty(ref _notEnergyEfficient, value);
        }
        private bool _notEnergyEfficient;

        [NotNull] public IInventorySection InventorySection { get; }

        // **************** Class data members ********************************************** //

        //public override IComponentFacility LocalFacilityInfo
        //{
        //    get => base.LocalFacilityInfo;
        //    set
        //    {
        //        base.LocalFacilityInfo = value;

        //        InventorySection.Images.Clear();
        //        InventorySection.Images.AddRange(base.LocalFacilityInfo?.Images);

        //        // ObservableCollection should raise it's own notify
        //        //RaisePropertyChanged(nameof(InventorySection));
        //    }
        //}

        //protected override IConfigInfo LocalConfigInfo
        //{
        //    get => base.LocalConfigInfo;
        //    set
        //    {
        //        base.LocalConfigInfo = value;
        //    }
        //}

        protected override ObservableCollection<ICommentBase> CommentContainerSource =>
            InventorySection.SectionComments;

        protected override ObservableCollection<ImageSource> ImageContainerSource =>
            InventorySection.Images;

        public IndexedCollection<string> FunctionalArea { get; } =
            new IndexedCollection<string>(new ObservableCollection<string>());

        public override string TabName       => "INVENTORY SECTION";
        public override string PhotoTypeText => "Section photos";
        public override string DetailHeaderText => 
            $@"Section Comment for {InventorySection.SectionName}";

        // **************** Class constructors ********************************************** //

        public InventorySectionViewModel()
        {
            RegionManagerName = "InventorySectionItemControl";

            CmdAddSection         = new DelegateCommand(OnAddSection           );
            CmdCancelEdit         = new DelegateCommand(OnCancelEdit           );
            CmdDecValue           = new DelegateCommand(OnCmdDecValue          );
            CmdDeleteSection      = new DelegateCommand(OnDeleteSection        );
            CmdIncValue           = new DelegateCommand(OnCmdIncValue          );
            CmdNextSection        = new DelegateCommand(OnNextSection          );
            CmdNotEnergyEfficient = new DelegateCommand(OnCmdNotEnergyEfficient);
            CmdPaintedCoated      = new DelegateCommand(OnCmdPaintedCoated     );
            CmdSectionComment     = new DelegateCommand(OnSectionComment       );
            CmdToday              = new DelegateCommand(OnCmdToday             );

            FunctionalArea.Collection.Add("Please select functional area...");
            FunctionalArea.SelectedIndex = 0;
#if DEBUG
            InventorySection = new MockInventorySection();
#endif
            if (InventorySection != null)
            {
                InventorySection.ComponentType = InventorySection.ComponentTypes[0];
                InventorySection.SectionName   = InventorySection.SectionNames  [0];
            }

            OnAllPainted();
        }

        // **************** Class members *************************************************** //

        private void OnAllPainted()
        {
            InventorySection.PcTypes.Clear();

            // When the All checkbox is not checked (default) present these most-common options:
            if (!IsAllPaintedChecked)
            {
                InventorySection.PcTypes.AddRange(new[]
                {
                    "Alkyd Paint",
                    "Latex Paint",
                    "Latex Stain",
                    "Varnish Surface Sealer"
                });
            }

            // When the All checkbox is checked, please present this full list of choices:
            else
            {
                InventorySection.PcTypes.AddRange(new[]
                {
                    "PAINT_TYPE_DESC",
                    "Alkyd Gloss Enamel", 
                    "Alkyd Glss Enl-Low in VOC",
                    "Alkyd High Gloss Enamel", 
                    "Alkyd Modified Oil Paint",
                    "Alkyd Paint",
                    "Alkyd Primer-Enl-Undercoat",
                    "Alkyd Resin Paint",
                    "Alkyd Semigloss Enamel",
                    "Alkyd-Resin Varnish",
                    "Alumin Hear Resist 1200 F",
                    "Aluminum Paint",
                    "Asphalt Varnish",
                    "Chlorin Rubber Intermedia",
                    "Enamel: Floor and Deck",
                    "Gen Purpose Wax, Solvent", 
                    "Gloss & Semigloss Latex",
                    "Heat-Resist 400 degF Enml",
                    "Iron-Oxide Oil Paint",
                    "Latex Acrylic Emulsion",
                    "Latex High Traffic",
                    "Latex Paint",
                    "Latex Primer Coating",
                    "Latex Stain",
                    "Latex Surface Sealer",
                    "Low Sheen Oil Varnish",
                    "Moist Curing Polyurethane",
                    "Oil Stain",
                    "Phenolic-Resin Spar Varnish",
                    "Iron/ZincOx,Lnsd Oil/Alkd",
                    "Rubber Paint (Swim Pools)",
                    "Rubber-Base Paint",
                    "Rubbing Oil Varnish",
                    "Semi-Transparent Oil Stain",
                    "Semigloss Enamel",
                    "Silicone Alkyd Paint",
                    "Textured Coating",
                    "Two-Part Epoxy Coating",
                    "Varnish Surface Sealer",
                    "Water-Emulsion Floor Wax",
                    "Water-Resist Spar Varnish",
                    "Zinc Rich Phenolic Varnish",
                    "Zinc-Molybdate Alkyd Prim"
                });
            }

            InventorySection.PcType = InventorySection.PcTypes.First();
        }

        private void OnCancelEdit() { Debug.WriteLine("OnCancelEdit is not implemented"); }

        private void OnAddSection() { Debug.WriteLine("OnAddSection is not implemented"); }

        private void OnCmdDecValue()
        {
            if (!double.TryParse(InventorySection.Quantity, out var d)) return;

            if (--d < 0) d = 0;
            InventorySection.Quantity = $"{d:0.00}";
        }

        private void OnCmdIncValue()
        {
            if (!double.TryParse(InventorySection.Quantity, out var d)) return;

            InventorySection.Quantity = $"{++d:0.00}";
        }

        private void OnDeleteSection()
        {
            var result = BdcMessageBoxView.Show(
                "Do you want to delete this section and related information?",
                "DELETE SECTION?",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Question);

            switch(result)
            {
                case MessageBoxResult.None:
                case MessageBoxResult.OK:
                case MessageBoxResult.Cancel:
                    break;
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException();
#endif
            }
        }

        private void OnNextSection()
        {
            if (!InventorySection.SectionNames.Any()) return;

            var index = InventorySection.SectionNames.IndexOf(InventorySection.SectionName);

            if (++index >= InventorySection.SectionNames.Count)
                index = 0;

            InventorySection.SectionName = InventorySection.SectionNames[index];
        }


        private void OnCmdNotEnergyEfficient()
        {
            NotEnergyEfficient = !NotEnergyEfficient;
        }


        private void OnCmdPaintedCoated()
        {
            InventorySection.PaintedIsChecked = !InventorySection.PaintedIsChecked;
        }


        private void OnSectionComment() { OnSelectedComment(null); }
        private void OnCmdToday() { InventorySection.Date = DateTime.Now.ToShortDateString(); }
    }
}
