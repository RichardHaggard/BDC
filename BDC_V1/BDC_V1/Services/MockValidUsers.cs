using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Services
{
    public class MockValidUsers : ValidUsers
    {
        public MockValidUsers()
        {
            ValidUserDictionary.Add(new Person() {FirstName = "Rick"  , LastName = "Wakeman"}, "Yes");
            ValidUserDictionary.Add(new Person() {FirstName = "Keith" , LastName = "Emerson"}, "ELP");
            ValidUserDictionary.Add(new Person() {FirstName = "Carlos", LastName = "Santana"}, "EvilWoman");
            ValidUserDictionary.Add(new Person() {FirstName = "George", LastName = "Jetson" }, "Leroy");
        }
    }
}
