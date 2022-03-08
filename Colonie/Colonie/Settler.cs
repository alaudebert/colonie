using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    abstract class Settler
    {
        private static int _settlersNb = 0;
        private int _id;
        private int _energyState;
        private int _hungerState;
        private int _x;
        private int _y;
        private bool _available;

        public Settler()
        {
            _settlersNb++;
            _id = _settlersNb;
            _energyState = 15;
            _hungerState = 10;
            _x = 0;
            _y = 0;
            _available = true;
        }

        public virtual void Play() { }

    }
}
