using System;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class TimeStamp : PropertyBase, ITimeStamp
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
