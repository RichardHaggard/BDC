using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Utils
{
    public static class NullableTypes
    {
        public static bool IsOfNullableType<T>(T o)
        {
            var type = typeof(T);
            return Nullable.GetUnderlyingType(type) != null;
        }

        public static bool IsNull<T>(T o)
        {
            return (IsOfNullableType(o) && (o == null));
        }
    }
}
