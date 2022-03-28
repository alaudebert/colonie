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
        protected bool _fool;
        private Settler[] _settlers;
        public static int _builderNb = 2;// TODO vérifier que les statiques sont bien defini ne dehors du constructeur
        public static int _turnNb = 3;

        public Hotel(int x, int y) : base(x, y)
        {
            _linesNb = 3;
            _columnsNb = 3;
            _totalPlace = 5;
            _nbPlaces = _totalPlace;
            _hotelNb++;
            _type = "H";
            type = _type;
            _id = _type + _hotelNb.ToString();
            _settlers = new Settler[5];
            _x = x;
            _y = y;

        }

        public override string ToString()
        {
            return base.ToString() + "C'est un hotel\n";
        }

        public Settler[] GetSettlers()
        {
            return _settlers;
        }
    }
}
