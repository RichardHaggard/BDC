using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class ItemChecklist : PropertyBase, IItemChecklist
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
