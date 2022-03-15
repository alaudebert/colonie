using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Restaurant : ReceptionBuilding
    {

        protected string _type = "restaurant_";
        protected static int _restaurantNb = 0;
        private Settler[] _settlers;
        public Restaurant(int x, int y) : base(x, y)
        {
            _totalPlace = 10;
            _builderNb = 2;
            _turnNb = 2;
            _linesNb = 4;
            _columnsNb = 3;
            _restaurantNb++;
            _id = _type + _restaurantNb.ToString();
            _settlers = new Settler[3];
        }

        public override string ToString()
        {
            return base.ToString() + "C'est un restaurant\n";
        }

        public Settler[] getSettlers()
        {
            return _settlers;
        }
    }
}
