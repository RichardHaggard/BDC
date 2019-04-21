using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Utils
{
    public static class DescriptionToEnum
    {
        public static T Parse<T>(string description)
            where T : struct
        {
            if (! TryParse(description, out T value))
                throw new InvalidEnumArgumentException($@"{description} is not an enumeration");

            return value;
        }

        public static bool TryParse<T>(string description, out T value)
            where T : struct
        {
            if (typeof(T).IsEnum)
            {
                foreach (T item in Enum.GetValues(typeof(T)))
                {
                    var desc = EnumDescription.GetDescription(item as Enum);
                    if (desc == description)
                    {
                        value = item;
                        return true;
                    }
                }
            }

            value = default(T);
            return false;
        }
    }
}
