using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Hotel : ReceptionBuilding
    {

        protected string _type = "hotel_";
        protected static int _hotelNb = 0;

        public Hotel(int x, int y) : base(x, y)
        {
            _builderNb = 2;
            _turnNb = 2;
            _builderNb = 3;
            _linesNb = 3;
            _columnsNb = 3;
            _totalPlace = 10;
            _hotelNb++;
            _id = _type + _hotelNb.ToString();
        }

        public override string ToString()
        {
            return base.ToString() + "C'est une hotel\n";
        }
    }
}
