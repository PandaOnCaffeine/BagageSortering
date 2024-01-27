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
    internal class Check_In
    {
        private Faker _nameGen = new Faker();
        private Buffer _queue;
        private TextBox _box;
        private Random _random = new Random();
        private bool _open = true;
        private Buffer _sorting;
        public Check_In(Buffer queue, TextBox box, Buffer sortingQueue)
        {
            _queue = queue;
            _box = box;
            _sorting = sortingQueue;
        }
        public void Run()
        {
            int key = 1;

            while (_open)
            {
                int amount = _random.Next(1, 4);
                Person person = new Person(_nameGen, amount);

                TakeBagage(person, key);
                Thread.Sleep(_random.Next(1500,5000));
            }
        }
        public void Stop()
        {
            _open = false;
        }

        private void TakeBagage(Person person, int key)
        {
            int randomNr = _random.Next(0, 3);
            for (int i = 0; i < person.BagageAmount; i++)
            {
                Bagage bagage = new Bagage(person.Name, randomNr, key);
                _queue.Produce(bagage);
                key++;
            }
            _box.WriteAt($"{person.Name}|Bagages:{person.BagageAmount}|Gate:{randomNr}| Check-in {_queue.Name}", ConsoleColor.Green);
        }
    }
}
