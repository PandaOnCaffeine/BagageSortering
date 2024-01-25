using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortering
{
    internal class Bagage
    {
        public string Name { get; private set; }
        public int Gate { get; private set; }
        public int Key { get; private set; }


        public Bagage(string name, int gate, int key)
        {
            Name = name;
            Gate = gate;
            Key = key;
        }
    }
}
