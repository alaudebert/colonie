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


        public Athletic(string nationality, string sport) : base()
        {
            _athleticNb++;
            _type = "A";
            _id = _type + _athleticNb.ToString();
            _nationality = nationality;
            _sport = sport;
            _decreasingEnergy = 5;
            _decreasingHunger = 6;
        }


        public override void Play()
        {
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
