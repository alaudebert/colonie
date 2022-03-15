using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Athletic : Settler
    {
        private static int _athleticNb;
        private int _level;
        private string _sport;
        private string _nationality;
        private int _session = 0;

        public Athletic(string nationality, string sport) : base()
        {
            _athleticNb++;
            _type = "athlete_";
            _id = _type + _athleticNb.ToString();
            _nationality = nationality;
            _sport = sport;
        }


        public override void Play()
        {
            _session++;
            if (_session == 3)
            {
                _session = 0;
                _level++;
            }
            _energyState -= 5;
            if (_energyState < 0)
                _energyState = 0;
            _hungerState -= 6;
            if (_hungerState < 0)
                _hungerState = 0;
        }


        public override string ToString()
        {
            return base.ToString() + "Son niveau : " + _level + "\nSport qu'il pratique : "
                + _sport + "\nNationalité : " + _nationality + "\n"; ;
        }
    }
}
