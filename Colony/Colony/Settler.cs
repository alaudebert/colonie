using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    abstract class Settler
    {
        protected string _id;
        public static int Energy = 50;
        public static int Hunger = 30;
        protected int _energyState;
        protected int _hungerState;
        public bool IsInActivity { get; set; }
        protected bool _available;
        public int NbTunrBeforeAvailable { get; set; }
        protected int _decreasingEnergy = 1;
        protected int _decreasingHunger = 2;
        protected int _timeToEat = 3;
        protected int _timeToSleep = 5;
        protected int _x, _y;
        public int[] _itinerary = { 0, 0 };
        protected Building[] _buildings;
        public string SettlerType { get; set; }


        public Settler()
        {
            _buildings = new Building[2];
            NbTunrBeforeAvailable = 0;
            IsInActivity = false;
            _energyState = Energy;
            _hungerState = Hunger;
            _x = 0;
            _y = 0;
        }
 
        public int EnergyState
        {
            get { return _energyState; }
            set { _energyState = value; }
        }

        public int HungerState
        {
            get { return _hungerState; }
            set { _hungerState = value; }
        }

        public Building[] Buildings
        {
            get { return _buildings; }
        }

        public int TimeToEat
        {
            get { return _timeToEat; }
        }

        public int TimeToSleep
        {
            get { return _timeToSleep; }
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value;  }
        }
        

        public bool Available
        {
            get
            {
                if (NbTunrBeforeAvailable == 0 && !IsInActivity)
                {
                    _available = true;
                }
                else
                {
                    _available = false;
                }
                return _available;
            }
            set { _available = value; }
        }

        public virtual void Play(List<Settler>[,] GameBoardSettler, int turnNb)
        {
            if (Math.Abs(_itinerary[0]) + Math.Abs(_itinerary[1]) != 0) 
            {
                GameBoardSettler[_x, _y].Remove(this);
                this.Move();
                GameBoardSettler[_x, _y].Add(this);
            }
            
            _energyState -= _decreasingEnergy;
            _hungerState -= _decreasingHunger;

            if (_energyState <= 0)
            {
                _energyState = 0;
                if (!IsInActivity)
                {
                    this.CalculatingItinerary(_buildings[0].X, _buildings[0].Y);
                    NbTunrBeforeAvailable = Math.Abs(_itinerary[0]) + Math.Abs(_itinerary[1]) + 2 + turnNb;
                    Console.WriteLine("nb tour " + NbTunrBeforeAvailable);
                    IsInActivity = true;
                }
                else
                {
                    if (NbTunrBeforeAvailable == turnNb)
                    {
                        _energyState = Energy;
                        IsInActivity = false;
                        NbTunrBeforeAvailable = 0;
                    }
                }
            }
            if (_hungerState <= 0)
            {
                _hungerState = 0;
                if (!IsInActivity)
                {
                    this.CalculatingItinerary(_buildings[1].X, _buildings[1].Y);
                    NbTunrBeforeAvailable = Math.Abs(_itinerary[0]) + Math.Abs(_itinerary[1]) + 2 + turnNb;
                    Console.WriteLine("nb tour " + NbTunrBeforeAvailable);
                    IsInActivity = true;
                }
                else
                {
                    Console.WriteLine("tour " + NbTunrBeforeAvailable);
                    if (NbTunrBeforeAvailable == turnNb)
                    {
                        Console.WriteLine("test");
                        _hungerState = Hunger;
                        IsInActivity = false;
                        NbTunrBeforeAvailable = 0;
                    }
                }
            }

        }

        public override string ToString()
        {
            return _id + "\nNiveau d'énergie : " + _energyState + "\nNiveau de faim : " + _hungerState
                + "\nCoodronnées : " + _x + " , " + _y + "\nDisponibilité : " + _available + "\n";
        }

        /// <summary>
        /// Calculate the distance between the destination and the setller location
        /// </summary>
        /// <param name="xDestination">Abscisse</param>
        /// <param name="yDestination"></param>
        public void CalculatingItinerary(int xDestination, int yDestination)
        {
            _itinerary[0] =  _x - xDestination;
            _itinerary[1] =  _y - yDestination;
        }

        public void Move()
        {
            if (_itinerary[0] != 0)
            {
                _x = _itinerary[0] > 0 ? _x - 1 : _x + 1;
                _itinerary[0] = _itinerary[0] > 0 ? _itinerary[0] - 1 : _itinerary[0] + 1;
            }
            else if (_itinerary[1] != 0)
            {
                _y = _itinerary[1] > 0 ? _y - 1 : _y + 1;
                _itinerary[1] = _itinerary[1] > 0 ? _itinerary[1] - 1 : _itinerary[1] + 1;
            }
        }

        //This method makes it possible to know if the colon must go to eat
        public bool IsHungry()
        {
            bool isHungry = false;
            if (_hungerState == 0)
                isHungry = true;
            return isHungry;
        }

        //This method allows you to know if the colon should go to sleep
        public bool IsSleepy()
        {
            bool isSleepy = false;
            if (_energyState == 0)
                isSleepy = true;
            return isSleepy;
        }

    }
}