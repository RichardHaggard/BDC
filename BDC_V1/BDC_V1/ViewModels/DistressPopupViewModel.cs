using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using BDC_V1.Utils;
using JetBrains.Annotations;

namespace BDC_V1.ViewModels
{
    public class DistressPopupViewModel : CloseableResultsWindow
    {
        // **************** Class enumerations ********************************************** //

        public enum DistressTypes
        {
            None,

            [Description("ANIMAL/INSECT DAMAGED")]
            Animal,

            [Description("BLISTERED")]
            Blistered,

            [Description("BROKEN")]
            Broken,

            [Description("CAPABILITY/CAPACITY DEFICIENT")]
            Deficient,

            [Description("CLOGGED")]
            Clogged,

            [Description("CORRODED")]
            Corroded,

            [Description("CRACKED")]
            Cracked,

            [Description("DAMAGED")]
            Damaged,

            [Description("DETERIORATED")]
            Deteriorated,

            [Description("DISPLACED")]
            Displaced,

            [Description("EFFLORESCENT")]
            Efflorescent,

            [Description("ELECTRICAL GROUND INADEQUATE OR UNINTENTIONAL")]
            ElectricalGround,

            [Description("HOLES")]
            Holes,

            [Description("LEAKS")]
            Leaks,

            [Description("LOOSE")]
            Loose,

            [Description("MISSING")]
            Missing,

            [Description("MOISTURE/DEBRIS CONTAMINATED")]
            Contaminated,

            [Description("NOISE/VIBRATION EXCESSIVE")]
            ExcessiveNoise,

            [Description("OPERATIONALLY IMPAIRED")]
            Impaired,

            [Description("OVERHEATED")]
            Overheated,

            [Description("PATCHED")]
            Patched,

            [Description("ROTTEN")]
            Rotten,

            [Description("STAINED/DIRTY")]
            Dirty
        }

        // **************** Class properties ************************************************ //

        private DistressTypes _distressType;
        public DistressTypes DistressType =>
            Enum.TryParse(DistressCollection.SelectedItem, true, out _distressType)
                ? _distressType
                : DistressTypes.None;

        [NotNull] public IndexedCollection<string> DistressCollection { get; } = new IndexedCollection<string>();

        // **************** Class data members ********************************************** //

        // **************** Class constructors ********************************************** //

        public DistressPopupViewModel()
        {
            var types = Enum.GetValues(typeof(DistressTypes)).Cast<DistressTypes>();
            DistressCollection.Collection.AddRange(types
                .Where(i => i != DistressTypes.None)
                .Select(i => i.Description()));
        }

        // **************** Class members *************************************************** //

    }
}
