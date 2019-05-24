using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Events;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using BDC_V1.Views;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class GeneralCommentViewModel : CommentWindows
    {
        // **************** Class enumerations ********************************************** //

        public enum CommentTypes
        {
            None,               // used for xaml designer, enables everything
            Facility,
            Inspection,
            InventorySection,
            InventoryDetails
        }

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull] public ICommand CmdDistressed { get; }

        public string WindowTitle
        {
            get => _windowTitle;
            set => SetProperty(ref _windowTitle, value);
        }
        private string _windowTitle = "INSPECTION COMMENT";

        public bool ExpanderIsExpanded
        {
            get => _expanderIsExpanded;
            set => SetProperty(ref _expanderIsExpanded, value);
        }
        private bool _expanderIsExpanded;

        public bool IsCopyEnabled       => (CommentType == CommentTypes.None) || 
                                           (CommentType == CommentTypes.Facility);

        public bool IsDistressedEnabled => (CommentType == CommentTypes.None) || 
                                           (CommentType == CommentTypes.Inspection);

        public bool IsStockEnabled      => (CommentType == CommentTypes.None)             || 
                                           (CommentType == CommentTypes.InventorySection) || 
                                           (CommentType == CommentTypes.InventoryDetails);

        public CommentTypes CommentType
        {
            get => _commentType;
            set => SetProperty(ref _commentType, value, () =>
            {
                StockCollection.Collection.Clear();

                switch (_commentType)
                {
                    case CommentTypes.None:
                    case CommentTypes.InventorySection:
                        StockCollection.Collection.AddRange(_inventorySectionStock);
                        break;

                    case CommentTypes.InventoryDetails:
                        StockCollection.Collection.AddRange(_inventoryDetailsStock);
                        break;

                    case CommentTypes.Facility:
                    case CommentTypes.Inspection:
                    default:
                        break;
                }

                RaisePropertyChanged(new []
                {
                    nameof(StockCollection),
                    nameof(IsDistressedEnabled),
                    nameof(IsStockEnabled),
                    nameof(IsCopyEnabled),
                });
            });
        }
        private CommentTypes _commentType = CommentTypes.None;

        public EnumRepairType RepairType
        {
            get => IsDistressedEnabled ? _repairType : EnumRepairType.None;
            set => SetProperty(ref _repairType, value);
        }
        private EnumRepairType _repairType = EnumRepairType.None;

        public IndexedCollection<string> StockCollection { get; } = new IndexedCollection<string>();

        private readonly IList<string> _inventorySectionStock = new List<string>();
        private readonly IList<string> _inventoryDetailsStock = new List<string>();

        // **************** Class constructors ********************************************** //

        public GeneralCommentViewModel()
        {
            CmdDistressed = new DelegateCommand(OnDistressed);
            HeaderText = "Inspection Comment on 11507 - FL1 - D302001 BOILERS - <Inspection Date>";

            _inventorySectionStock.AddRange(new[]
            {
                "Component was not visible for inspection. The component condition will be age-based by BUILDER program degradation curves",

                "The component is not visible for inspection or population of Section Details. The component condition will be age-based by " +
                "BUILDER program degradation curves and no nameplate data or inventory photo will be provided",

                "The component is not visible for inspection and the quantity was estimated based on architect/engineering judgment. " +
                "The component condition will be age-based by BUILDER program degradation curves",

                "The component is located in a secure area of the facility that did not allow electronic devices to be used. " +
                "Photos were not obtainable The component condition will be age-based by BUILDER program degradation curves",
            });

            _inventoryDetailsStock.AddRange(new[]
            {
                "The section is not visible for population of Section Details. No nameplate data or inventory photo is provided",
                "The section is not visible for inspection and the quantity was estimated based on architect/engineering judgment", 
                "The section is located in a secure area of the facility that did not allow electronic devices to be used. Photos were not obtainable",
            });

            StockCollection.PropertyChanged += _stockCollection_PropertyChanged;
        }

        // **************** Class members *************************************************** //

        public override IList<ICommentary> CommentaryList
        {
            get => _commentaryList ?? (_commentaryList = new List<ICommentary>());
            set => SetProperty(ref _commentaryList, value);
        }
        private IList<ICommentary> _commentaryList;

        protected override string CopyWindowTitle => "COPY INSPECTION COMMENT";

        private void OnDistressed()
        {
            if (! IsDistressedEnabled) return;

            var view = new DistressPopupView();
            if (! (view.DataContext is DistressPopupViewModel model))
                throw new ApplicationException("view model is not DistressPopupViewModel");

            if (view.ShowDialog() == true)
            {
                switch (model.Result)
                {
                    case EnumControlResult.ResultDeleteItem:
                    case EnumControlResult.ResultDeferred:
                    case EnumControlResult.ResultCancelled:
                        break;

                    case EnumControlResult.ResultSaveNow:
                        break;
#if DEBUG
                    default:
                        throw new ArgumentOutOfRangeException();
#endif
                }
            }
        }

        private void _stockCollection_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IndexedCollection<string>.SelectedItem) &&
                ! string.IsNullOrEmpty(StockCollection.SelectedItem))
            {
                CommentText = StockCollection.SelectedItem;
                ExpanderIsExpanded = false;
            }
        }

    }
}
