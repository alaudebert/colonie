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
        protected int _totalPlace;
        protected int _nbPlaces;
        protected int _x, _y;
        protected string _id;
        public static string type;
        protected string _type;
        private Village _myVillage;
        private List<Settler> _settlers;
        protected string _name;
        public static Dictionary<string, int[]> _buildingSize = new Dictionary<string, int[]> { { "H", new int[2] { 3, 3 } }, { "R", new int[2]{ 3, 5 } }, { "S", new int[2]{ 5, 5 } } };


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

        public Dictionary<string, int[]> BuildingSize
        {
            get { return _buildingSize; }
        }

        public static int GetLinesNb(string type)
        {
            return Building._buildingSize.FirstOrDefault(x => x.Key == type).Value[0];
        }
        public static int GetColumnsNb(string type)
        {
            return Building._buildingSize.FirstOrDefault(x => x.Key == type).Value[1];
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
            return "\ncolonnes \nIl a comme coodronnées : " + _x + " , " + _y
                + "\n Son nom est : " + _name + "\n";
        }
    }
}
