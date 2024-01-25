using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace BagageSortering
{
    internal class Person
    {
        public string Name { get; set; }
        public int BagageAmount { get; private set; }
        public Person(Faker test, int amount)
        {
            Name = test.Name.FullName();
            BagageAmount = amount;
        }
    }
}
