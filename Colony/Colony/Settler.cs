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
        protected bool _available;
        protected string _type;
        protected int _decreasingEnergy = 1;
        protected int _decreasingHunger = 1;

        protected int _x, _y;
        public int[] _itinerary = { 0, 0 }; 


        public Settler()
        {
            _settlersNb++;
            _energyState = 15;
            _hungerState = 10;
            _x = 0;
            _y = 0;
            _available = true;
        }

        public int X
        {
            get { return _x; }
        }

        public int Y
        {
            get { return _y; }
        }
        public string Type
        {
            get { return _type; }
        } 
        
        public bool Available
        {
            set { _available = value; }
        }

        public bool IsAvailable() {

            return _available;
        }

        public virtual void Play()
        {
            _energyState -= _decreasingEnergy;
            _hungerState -= _decreasingHunger; 

            if (_energyState < 0)
                _energyState = 0;
            if (_hungerState < 0)
                _hungerState = 0;
        }

        public override string ToString()
        {
            return _id + "\nNiveau d'énergie : " + _energyState + "\nNiveau de faim : " + _hungerState
                + "\nCoodronnées : " + _x + " , " + _y + "\nDisponibilité : " + _available + "\n";
        }

        public void calculatingItinerary(int xDestination, int yDestination)
        {
            _itinerary[0] =  _x - xDestination;
            _itinerary[1] =  _y - yDestination;
        }

        public void move()
        {
            if (_itinerary[0] != 0)
            {
               _x = _itinerary[0] > 0 ? _x + 1 : _x - 1;
                _itinerary[0]--;
            }
            else if (_itinerary[1] > 0)
            {
                _y = _itinerary[1] > 0 ? _y + 1 : _y - 1;
                _itinerary[1]--;
            }

        }

    }
}