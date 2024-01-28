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
        private Buffer[] _gates;
        private Buffer _queue;
        private TextBox _box;
        private bool _open = true;
        public SortingMachine(Buffer queue, Buffer[] gates, TextBox box)
        {
            _queue = queue;
            _gates = gates;
            _box = box;
        }
        public void Run()
        {
            while (_open)
            {
                Bagage bagage = _queue.Next();
                _box.WriteAt($"{bagage.Key} | Gate: {bagage.Gate + 1} | {bagage.Name}", ConsoleColor.Yellow);
                _queue.Split(_gates[bagage.Gate]);
                Thread.Sleep(10);
            }
        }
        public void Stop()
        {
            _open = false;
        }
    }
}