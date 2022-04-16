using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Hotel : Building
    {
        protected static int _hotelNb = 0;
        private Settler[] _settlers;
        public static string type = "H";
        public static int BuilderNb = 2;
        public static int TurnNb = 2;

        /// <summary>
        /// Builder who wants to create a hotel, with 5 places available for 
        /// </summary>
        /// <param name="x">Abscisse dans le village de l'angle en haut à gauche de l'hotel</param>
        /// <param name="y">Ordinate dans le village de l'angle en haut à gauche de l'hotel</param>
        public Hotel(int x, int y) : base(x, y)
        {
            _totalPlace = 5;
            _nbPlaces = _totalPlace;
            _hotelNb++;
            _type = type;
            _settlers = new Settler[5];
            _x = x;
            _y = y;

        }

        public override string ToString()
        {
            return base.ToString() + "C'est un hotel\n";
        }
    }
}
