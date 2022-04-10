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

        /// <summary>
        /// Constructor that allows to create a sportsman
        /// </summary>
        /// <param name="nationality">Attributes the chosen nationality to the athlete</param>
        /// <param name="sport">Attributes the chosen sport to the athlete</param>
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


        /// <summary>
        /// Allows you to recover your level
        /// </summary>
        public int Level//TODO je crois que c'est inutile a supprimer apres accord d 'alex
        {
            get { return _level; }
        }

        /// <summary>
        /// Allows the athlete to play, i.e. to go to an additional training session, and if we are at the 3rd session then it increases if level
        /// </summary>
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
    }
}
