using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;

namespace BDC_V1.ViewModels
{
    public class SelectUserViewModel : CloseableResultsWindow
    {
        public bool IsOkEnabled
        {
            get => _isOkEnabled;
            set => SetProperty(ref _isOkEnabled, value);
        }
        private bool _isOkEnabled;

        public IndexedCollection<Person> Users { get; } = new IndexedCollection<Person>();

    }
}
