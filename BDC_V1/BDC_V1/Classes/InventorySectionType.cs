﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using BDC_V1.Interfaces;
using BDC_V1.ViewModels;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class InventorySectionType : BindableBase, IInventorySectionType
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        public bool AddCurrentInspector
        {
            get => _addCurrentInspector;
            set => SetProperty(ref _addCurrentInspector, value);
        }
        private bool _addCurrentInspector;

        public string ComponentType
        {
            get => _componentType;
            set => SetProperty(ref _componentType, value);
        }
        private string _componentType;

        public string Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        private string _date;

        public string Dcr
        {
            get => _dcr;
            set => SetProperty(ref _dcr, value);
        }
        private string _dcr;

        public string EquipmentCategory
        {
            get => _equipmentCategory;
            set => SetProperty(ref _equipmentCategory, value);
        }
        private string _equipmentCategory;

        public bool PaintedIsChecked
        {
            get => _paintedIsChecked;
            set
            {
                if (SetProperty(ref _paintedIsChecked, value ))
                    YearPcVisibility = _paintedIsChecked 
                        ? Visibility.Visible 
                        : Visibility.Hidden;
            }
        }
        private bool _paintedIsChecked = false;

        public string PcRating
        {
            get => _pcRating;
            set => SetProperty(ref _pcRating, value);
        }
        private string _pcRating = "";

        public string PcType
        {
            get => _pcType;
            set => SetProperty(ref _pcType, value);
        }
        private string _pcType = "";

        public string Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
        private string _quantity = "";

        public string SectionComment
        {
            get => _sectionComment;
            set => SetProperty(ref _sectionComment, value);
        }
        private string _sectionComment;

        public string SectionName
        {
            get => _sectionName;
            set => SetProperty(ref _sectionName, value);
        }
        private string _sectionName;

        public string YearInstalledRenewed
        {
            get => _yearInstalledRenewed;
            set => SetProperty(ref _yearInstalledRenewed, value);
        }
        private string _yearInstalledRenewed;

        public string YearPc
        {
            get => _yearPc;
            set => SetProperty(ref _yearPc, value);
        }
        private string _yearPc;

        public Visibility YearPcVisibility
        {
            get => _yearPcVisibility;
            set => SetProperty(ref _yearPcVisibility, value);
        }
        private Visibility _yearPcVisibility = Visibility.Hidden;

        public QuickObservableCollection<string> ComponentTypes { get; } = 
            new QuickObservableCollection<string>();

        public QuickObservableCollection<string> Dcrs { get; } = 
            new QuickObservableCollection<string>();

        public QuickObservableCollection<string> EquipmentCategories { get; } = 
            new QuickObservableCollection<string>();

        public QuickObservableCollection<string> PcRatings { get; } = 
            new QuickObservableCollection<string>();

        public QuickObservableCollection<string> PcTypes { get; } = 
            new QuickObservableCollection<string>();

        public QuickObservableCollection<string> SectionNames { get; } = 
            new QuickObservableCollection<string>();

        public QuickObservableCollection<ImageSource> Images { get; } = 
            new QuickObservableCollection<ImageSource>();
    }
}