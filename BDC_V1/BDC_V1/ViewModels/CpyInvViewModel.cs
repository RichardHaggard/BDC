using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.ViewModels
{
    public class CpyInvViewModel : CloseableWindow
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull]
        public IList<IItemChecklist> ListOfSystems { get; } = new List<IItemChecklist>();

        // **************** Class constructors ********************************************** //

        public CpyInvViewModel()
        {
            ListOfSystems.Add(new ItemChecklist() {ItemName = "C10 - INTERIOR CONSTRUCTION", ItemIsChecked = false});
            ListOfSystems.Add(new ItemChecklist() {ItemName = "C20 - STAIRS"               , ItemIsChecked = false});
            ListOfSystems.Add(new ItemChecklist() {ItemName = "C30 - INTERIOR FINISHES"    , ItemIsChecked = false});
        }

        // **************** Class members *************************************************** //

    }
}
