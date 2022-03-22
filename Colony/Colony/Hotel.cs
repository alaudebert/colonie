using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Hotel : ReceptionBuilding
    {
        protected static int _hotelNb = 0;
        protected bool _fool;
        private Settler[] _settlers;

        public Hotel(int x, int y) : base(x, y)
        {
            _builderNb = 2;
            _turnNb = 2;
            _builderNb = 3;
            _linesNb = 3;
            _columnsNb = 3;
            _totalPlace = 10;
            _hotelNb++;
            Type = "H";
            _id = Type + _hotelNb.ToString();
            _settlers = new Settler[5];

        }

        public override string ToString()
        {
            return base.ToString() + "C'est un hotel\n";
        }

        public Settler[] getSettlers()
        {
            return _settlers;
        }
    }
}
