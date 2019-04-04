using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class TimeStamp : BindableBase, ITimeStamp
    {
        public IPerson EntryUser
        {
            get => _entryUser;
            set => SetProperty(ref _entryUser, value);
        }
        private IPerson _entryUser;

        public DateTime EntryTime
        {
            get => _entryTime;
            set => SetProperty(ref _entryTime, value);
        }
        private DateTime _entryTime;
    }
}
