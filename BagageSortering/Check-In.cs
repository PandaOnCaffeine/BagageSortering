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
        private int _checkIns;
        private int _key;
        private object _keyLock;
        public Check_In(Buffer queue, TextBox box, Buffer sorting, int gatesAmount, int key, object keyLock)
        {
            _queue = queue;
            _box = box;
            _sorting = sorting;
            _checkIns = gatesAmount;
            _key = key;
            _keyLock = keyLock;
        }
        public void Run()
        {
            while (_open)
            {
                int amount = _random.Next(1, 3);
                Person person = new Person(_nameGen, amount);

                TakeBagage(person);
                SendToSorting();
                //Thread.Sleep(_random.Next(1500, 3000));
                Thread.Sleep(300);

            }
        }
        public void Stop()
        {
            _open = false;
        }

        private void SendToSorting()
        {
            int amount = _queue.Count;
            for (int i = 0; i < amount; i++)
            {
                _queue.Split(_sorting);
            }
        }

        private void TakeBagage(Person person)
        {
            int randomNr = _random.Next(0, _checkIns);
            for (int i = 0; i < person.BagageAmount; i++)
            {
                Bagage bagage = new Bagage(person.Name, randomNr, $"{_queue.Name}-{_key}");
                _queue.Produce(bagage);
                lock (_keyLock)
                {
                    _key++;
                }
            }
            _box.WriteAt($"Bags:{person.BagageAmount}| Gate:{randomNr + 1}| {person.Name}", ConsoleColor.Green);
        }
    }
}
