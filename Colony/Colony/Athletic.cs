using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Athletic : Settler
    {
        public static int LevelIncrease = 1;
        private static int _athleticNb;
        private int _level;
        private string _sport;
        private string _nationality;
        private int _session = 0;
        private string _type;


        public Athletic(string nationality, string sport) : base()
        {
            _athleticNb++;
            SettlerType = _type;
            _type = "A";
            _id = _type + _athleticNb.ToString();
            _nationality = nationality;
            _sport = sport;
            _decreasingEnergy = 5;
            _decreasingHunger = 6;
        }

        public string Type
        {
            get { return _type; }
        }
        public override void Play(List<Settler>[,] gameBoardSettler, int turnNb)
        {
            if (Math.Abs(_itinerary[0]) + Math.Abs(_itinerary[1]) != 0)
            {
                gameBoardSettler[_x, _y].Remove(this);
                this.Move();
                gameBoardSettler[_x, _y].Add(this);
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
                    Console.WriteLine("tour " + NbTunrBeforeAvailable);
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

            _session++;
            if (_session == 3)
            {
                _session = 0;
                _level+= LevelIncrease;
            }
        }


        public override string ToString()
        {
            return base.ToString() + "Son niveau : " + _level + "\nSport qu'il pratique : "
                + _sport + "\nNationalité : " + _nationality + "\n"; ;
        }

        public int getLevel()
        {
            return _level;
        }
    }
}
