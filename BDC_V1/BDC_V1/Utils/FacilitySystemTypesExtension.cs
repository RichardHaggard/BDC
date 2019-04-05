using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Enumerations;

namespace BDC_V1.Utils
{
    public static class FacilitySystemTypesExtension
    {
        public static string GetSystemName<T>(this T enumerationValue)
            where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException(@"EnumerationValue must be of Enum type", nameof(enumerationValue));
            }

            var name = enumerationValue.ToString();

            // see if this is one of the valid types
            if (ValidTypes.Contains(type))
            {
                //Tries to find a DescriptionAttribute for a potential friendly name
                //for the enum
                var memberInfo = type.GetMember(enumerationValue.ToString());
                if (memberInfo.Length > 0)
                {
                    var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attrs.Length > 0)
                    {
                        //Pull out the description value
                        name += " " + ((DescriptionAttribute)attrs[0]).Description;
                    }
                }
            }

            return name;
        }

        private static readonly IReadOnlyCollection<Type> ValidTypes;

        static FacilitySystemTypesExtension()
        {
            var list = new List<Type>
            {
                typeof(EnumFacilitySystemTypes),
                typeof(Enum_A10_SubsystemTypes),
                typeof(Enum_A20_SubsystemTypes),
                typeof(Enum_B10_SubsystemTypes),
                typeof(Enum_B20_SubsystemTypes),
                typeof(Enum_B30_SubsystemTypes),
                typeof(Enum_C10_SubsystemTypes),
                typeof(Enum_C20_SubsystemTypes),
                typeof(Enum_C30_SubsystemTypes),
                typeof(Enum_D10_SubsystemTypes),
                typeof(Enum_D20_SubsystemTypes),
                typeof(Enum_D30_SubsystemTypes),
                typeof(Enum_D40_SubsystemTypes),
                typeof(Enum_D50_SubsystemTypes)
            };

            ValidTypes = new ReadOnlyCollection<Type>(list);
        }
    }
}
