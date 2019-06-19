using System;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    /// <inheritdoc cref="IPerson"/>
    public class Person : PropertyBase, IPerson
    {
        /// <inheritdoc />
        public string FirstName
        {
            get => _firstName;
            set => SetPropertyFlagged(ref _firstName, value, new []
            {
                nameof(FirstLast),
                nameof(LastFirst)
            });
        }
        private string _firstName;

        /// <inheritdoc />
        public string LastName
        {
            get => _lastName;
            set => SetPropertyFlagged(ref _lastName, value, new []
            {
                nameof(FirstLast),
                nameof(LastFirst)
            });
        }
        private string _lastName;

        /// <inheritdoc />
        public string FirstLast => FirstName + " "  + LastName;

        /// <inheritdoc />
        public string LastFirst => LastName  + ", " + FirstName;

        /// <inheritdoc />
        public int CompareTo(IPerson other)
        {
            var last = string.Compare(LastName, other.LastName, StringComparison.Ordinal);
            if (last != 0) return last;

            return string.Compare(FirstName, other.FirstName, StringComparison.Ordinal);
        }

        /// <inheritdoc />
        public override string ToString() => LastFirst;
    }
}
