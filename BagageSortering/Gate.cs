using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortering
{
    internal class Gate
    {
        private Buffer _queue;
        private TextBox _box;
        private bool _open = true;
        public Gate(Buffer queue, TextBox box)
        {
            _queue = queue;
            _box = box;
        }
    }
}
