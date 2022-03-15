using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    abstract class Settler
    {
        protected static int _settlersNb = 0;
        protected string _id;
        protected int _energyState;
        protected int _hungerState;
        protected int _x, _y;
        protected bool _available;
        protected string _type;


        public Settler()
        {
            _settlersNb++;
            _energyState = 15;
            _hungerState = 10;
            _x = 0;
            _y = 0;
            _available = true;
        }

        public void Play()
        {
            _energyState--;
            _hungerState--;
        }

        public override string ToString()
        {
            return _id + "\nNiveau d'énergie : " + _energyState + "\nNiveau de faim : " + _hungerState
                + "\nCoodronnées : " + _x + " , " + _y + "\nDisponibilité : " + _available + "\n";
        }

    }
}
