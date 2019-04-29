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
            if (string.IsNullOrEmpty(pat)) return true;     // if the pattern is empty, then we are "like" it.
            if (string.IsNullOrEmpty(src)) return false;    // takes care of null / empty source strings when pattern is not

            return src.StartsWith(pat, StringComparison.InvariantCultureIgnoreCase) ||  // like 'pat%' start here as it's the most likely
                   src.Contains  (pat, StringComparison.InvariantCultureIgnoreCase); // like '%pat%'

                   // I think StartsWith takes care of this
                   // src.Equals   (pat, StringComparison.InvariantCultureIgnoreCase) ||  // like 'pat'

                   // I think Contains takes care of this
                   //src.EndsWith  (pat, StringComparison.InvariantCultureIgnoreCase) ||  // like '%pat'
        }
    }
}
