﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Restaurant : Building
    {

        protected static int _restaurantNb = 0;
        private Settler[] _settlers;
        public static int _builderNb = 2; 
        public static int _turnNb = 2;
        public int _linesNb;
        public int _columnsNb;
        public Restaurant(int x, int y) : base(x, y)
        {
            _totalPlace = 10;
            _nbPlaces = _totalPlace;
            _linesNb = 3;
            _columnsNb = 5;
            _restaurantNb++;
            type = "R";
            _type = type;
            _id = _type + _restaurantNb.ToString();
            _settlers = new Settler[3]; 
        }

        public int LinesNb
        {
            get { return Building._buildingSize.FirstOrDefault(x => x.Key == "R").Value[0]; }
        }
        public int ColumnsNb
        {
            get { return Building._buildingSize.FirstOrDefault(x => x.Key == "R").Value[1]; }
        }

        public override string ToString()
        {
            return base.ToString() + "C'est un restaurant\n";
        }
    }
}
