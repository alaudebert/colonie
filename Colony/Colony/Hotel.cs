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
        protected bool _fool; //Inutile je crois
        private Settler[] _settlers;
        public static int _builderNb = 2;// TODO vérifier que les statiques sont bien defini ne dehors du constructeur
        public static int _turnNb = 2;
        public int _linesNb;
        public int _columnsNb;

        public Hotel(int x, int y) : base(x, y)
        {
            _totalPlace = 5;
            _nbPlaces = _totalPlace;
            _hotelNb++;
            type = "H";
            _type = type;
            _id = _type + _hotelNb.ToString();
            _settlers = new Settler[5];
            _x = x;
            _y = y;

        }

        public override string ToString()
        {
            return base.ToString() + "C'est un hotel\n";
        }

        public Settler[] GetSettlers() //Je crois que cette méthode est inutile
        {
            return _settlers;
        }

        public int LinesNb
        {
            get {return Building._buildingSize.FirstOrDefault(x => x.Key == "H").Value[0];}
        }
        public int ColumnsNb
        {
            get { return Building._buildingSize.FirstOrDefault(x => x.Key == "H").Value[1]; }
        }
    }
}
