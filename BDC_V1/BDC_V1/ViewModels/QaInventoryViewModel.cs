using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using BDC_V1.ViewModels;
using Prism.Mvvm;

namespace BDC_V1.ViewModels
{
    public class QaInventoryViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //
        public ObservableCollection<IInventoryType> InventoryInfo { get; } =
            new ObservableCollection<IInventoryType>();

        // **************** Class constructors ********************************************** //
        public QaInventoryViewModel()
        {
            InventoryInfo.Add(new InventoryType()
            {
                FacilityId = "11057",
                SystemId = "D30",
                ComponentId = "D3010",
                TypeName = "",
                SectionName = "",
                InventIssue = "Missing Section"
            });

            InventoryInfo.Add(new InventoryType()
            {
                FacilityId = "11057",
                SystemId = "D30",
                ComponentId = "D3010",
                TypeName = "D302001",
                SectionName = "N/A",
                InventIssue = "Missing Photo"
            });

            InventoryInfo.AddRange(Enumerable.Repeat(new InventoryType(), 30));
        }

        // **************** Class members *************************************************** //
    }
}
