using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Utils
{
    public static class StringExtensions
    {
        public static bool Contains(
            this string str, 
            [NotNull] string substring, 
            StringComparison comp)
        {                            
            if (substring == null)
                throw new ArgumentNullException(nameof(substring), $@"{nameof(substring)} cannot be null.");

            if (! Enum.IsDefined(typeof(StringComparison), comp))
                throw new ArgumentException($@"{nameof(comp)} is not a member of StringComparison",
                    nameof(comp));

            return str.IndexOf(substring, comp) >= 0;                      
        }

        public static bool IsLike(this string src, string pat)
        {
            return string.IsNullOrEmpty(src) || string.IsNullOrEmpty(pat) ||
                   src.Equals    (pat, StringComparison.InvariantCultureIgnoreCase) ||
                   src.StartsWith(pat, StringComparison.InvariantCultureIgnoreCase) ||  // like 'pat%'
                   src.EndsWith  (pat, StringComparison.InvariantCultureIgnoreCase) ||  // like '%pat'
                   src.Contains  (pat, StringComparison.InvariantCultureIgnoreCase);    // like '%pat%'
        }
    }
}
