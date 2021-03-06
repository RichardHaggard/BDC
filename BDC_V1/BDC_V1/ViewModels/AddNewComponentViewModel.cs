﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class AddNewComponentViewModel : CloseableResultsWindow
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public string Component
        {
            get => _component;
            set => SetProperty(ref _component, value);
        }
        private string _component;


        public ObservableCollection<string> Components
        {
            get => _components;
            set => SetProperty(ref _components, value);
        }
        private ObservableCollection<string> _components = new ObservableCollection<string>();


        // **************** Class constructors ********************************************** //

        public AddNewComponentViewModel()
        {
#if DEBUG
#warning Using MOCK data for AddNewComponentViewModel
            Components.AddRange(new[]
            {
                "G2010 ROADWAYS",
                "G2020 PARKING LOTS",
                "G2030 PEDESTRIAN PAVING",
                "G2040 SITE DEVELOPMENT",
                "G2050 LANDSCAPING",
                "G2060 AIRFIELD PACING"
            });
#endif
        }


        // **************** Class members *************************************************** //


    }
}
