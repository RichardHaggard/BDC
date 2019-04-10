using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class Person : PropertyBase, IPerson
    {
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

        public string FirstLast => FirstName + " "  + LastName;
        public string LastFirst => LastName  + ", " + FirstName;

        public Person()
        {
        }

        public Person(string firstName, string lastName)
            : this()
        {
            FirstName = firstName;
            LastName  = lastName;
        }

        public override string ToString() => LastFirst;
    }
}
