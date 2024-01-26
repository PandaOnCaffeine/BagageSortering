using Bogus;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BagageSortering
{
    internal class SortingMachine
    {
        private Gates[] _gates;
        
        private Buffer _queue;
        private TextBox _box;
        private bool _open = true;
        public Check_In(Buffer queue, Gates[] gates, TextBox box)
        {
            _queue = queue;
            _box = box;
            _gates = gates;
        }
        public void Run()
        {         
            while (_open)
            {
                _queue.Split(Gates[_queue.Next()]);                              
            }
        }

        public void Stop()
        {
            _open = false;
        }       
    }
}