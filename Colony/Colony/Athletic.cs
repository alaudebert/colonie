using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Athletic : Settler
    {
        private int _athleticId;
        private static int _athleticNb;
        private int _level;
        private string _sport;
        private string _nationality;

        public Athletic(string nationality, string sport) : base()
        {
            _athleticNb++;
            _athleticId = _athleticNb;
            _nationality = nationality;
            _sport = sport;
        }


        public void Practice()
        {
            _energyState -= 5;
            _hungerState -= 6;
        }

        public override string ToString()
        {
            return base.ToString() + "C'est le sportif n° : " +_athleticId + "\nSon niveau : " + _level + "\nSport qu'il pratique : "
                + _sport + "\nNationalité : " + _nationality + "\n"; ;
        }
    }
}
