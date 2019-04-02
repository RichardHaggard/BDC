using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class ItemChecklist : BindableBase, IItemChecklist
    {
        public string ItemName
        {
            get => _itemName;
            set => SetProperty(ref _itemName, value);
        }
        private string _itemName;

        public bool ItemIsChecked
        {
            get => _itemIsChecked;
            set => SetProperty(ref _itemIsChecked, value);
        }
        private bool _itemIsChecked;
    }
}
