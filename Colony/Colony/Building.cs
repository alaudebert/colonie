using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    abstract class Building
    {
        protected bool _haveFreePlaces;
        public static int _linesNb;
        public static int _columnsNb;
        protected int _totalPlace;
        protected int _nbPlaces;
        protected int _x, _y;
        protected string _id;
        public static string type;
        protected string _type;
        private Village _myVillage;
        private List<Settler> _settlers;
        public string _name;

        public Village MyVillage
        {
            get { return _myVillage; }
        }
        public string Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public int LinesNb
        {
            get { return _linesNb; }
        }

        public int ColumnsNb
        {
            get { return _columnsNb; }
        }
        public int NbPlaces
        {
            get { return _nbPlaces; }
        }

        public Building(int x, int y)
        {
            _x = x;
            _y = y;
            //_myVillage = village;
            _settlers = new List<Settler>();
        }

        public int X
        {
            get { return _x; }
        }


        public int Y
        {
            get { return _y; }
        }

        public int TotalPlace
        {
            get { return _totalPlace; }
            set { _totalPlace = value; }
        }


        public List<Settler> Settlers
        {
            get { return _settlers; }
        }

        public string Type
        {
            get { return _type; }
        }


        /// <summary>
        /// Say if the building have free place
        /// </summary>
        /// <returns>True if the building have free place and false if they are not</returns>
        public bool haveFreePlace()
        {
            int i = 0;
            bool freePlace = false;

            if (_settlers.Count() != 0)
            {
                while (i < _settlers.Count() || !freePlace)
                {
                    if (_settlers[i] != null)
                    {
                        freePlace = true;
                    }
                    i++;
                } 
            }
            else
            {
                freePlace = true;
            }
            return freePlace;
        }
        public override string ToString()
        {
            return "\nIl sera réalisé en : "
                + ColumnsNb + " colonnes \nIl a comme coodronnées : " + _x + " , " + _y + "\n";
        }
    }
}
