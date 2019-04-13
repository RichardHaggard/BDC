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

        public override string ToString() =>
            "[" + EntryUser.FirstLast + " " + EntryTime.ToString("M/d/yyyy hh:mm tt") + "]";

        public static bool TryParse(string formattedString, out TimeStamp results)
        {
            // TODO: either parse the comment string here or have comment dialogs store this type for the text.
            results = null;
            return false;
        }
    }
}
