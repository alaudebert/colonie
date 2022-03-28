using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Village
    {
        private List<Building> _buildings = new List<Building>();
        private List<Tuple<string, int, int, int, List<Settler>>> _inConstruction = new List<Tuple<string, int, int, int, List<Settler>>>(); 
        private int _maxNbSettlers;
        private string[,] _gameBoard = new string[20, 40];
        private List<Settler> _settlers = new List<Settler>();
        

        public Village()
        {
            Restaurant r1 = new Restaurant(8, 8);
            Hotel hotel = new Hotel(0, 0);
            Builder s1 = new Builder();
            Builder s2 = new Builder();
            Builder s3 = new Builder();
            Builder s4 = new Builder();
            _maxNbSettlers = 4;
            _buildings.Add(hotel);
            _buildings.Add(r1);
            AddSettler(s1);
            AddSettler(s2);
            AddSettler(s3);
            AddSettler(s4);
        }

        public string[,] GameBoard
        {
            get { return _gameBoard; }
        }

        public List<Building> Buildings
        {
            get { return _buildings; }
        }
        public List<Tuple<string, int, int, int, List<Settler>>> InConstruction
        {
            get { return _inConstruction; }
            set { _inConstruction = value;  }
        }

        public override string ToString()
        {
            string retour = "Mon village est composé de " + _settlers.Count() + " colons. Les voicis : \n";
            foreach (Settler settler in _settlers)
                retour += settler.ToString(); 
            retour += "Mon village est composé de " + _buildings.Count() + " batiments. Les voici : \n ";
            foreach (Building building in _buildings)
                retour += building.ToString();
            return retour;
        }

        public List<Settler> GetSettlers()
        {
            return _settlers;
        }

        public int NbSettlerAvailable(string type)
        {
            return FindAvailable(type).Count();
        }

        /// <summary>
        /// Return a table of settlers free
        /// </summary>
        /// <param name="type">The type of the settlers required</param>
        /// <returns>A table of settlers available with the good type</returns>
        public List<Settler> FindAvailable(string type)
        {
            List<Settler> availables = new List<Settler>();
            foreach (Settler settler in _settlers)
            {
                if (settler.IsAvailable() && settler.Type.Equals(type))
                {
                    availables.Add(settler);
                }
            }
            return availables;
        }


        public void addBuildings(Building building)
        {
            Console.WriteLine("coucou je suis un test");
            _buildings.Add(building);
        }

        public void AddSettler(Settler settler)
        {
            if (_gameBoard[settler.X, settler.Y] != "C")
            {
                _gameBoard[settler.X, settler.Y] = "C";
            }
            _settlers.Add(settler);
            int i = 0;
            bool addHotel = false;
            bool addRestaurant = false;
            while (i<_buildings.Count() && ( addHotel == false || addRestaurant == false) )
            {
                if (_buildings[i].haveFreePlace()) { 
                    if (_buildings[i].Type == "H")
                    {
                        _buildings[i].Settlers.Add(settler);
                        addHotel = true;
                    }
                    if (_buildings[i].Type == "R")
                    {
                        _buildings[i].Settlers.Add(settler);
                        addRestaurant = true;
                    }
                }
                i++;
            }

        }

     
        public bool FreeRestaurantPlaces()
        {
            bool places = false;
            foreach (Building building in _buildings)
            {
                if (building.Type == "R" && building.Settlers.Count() < building.TotalPlace){
                        places = true;
                }
            }
            return places;
        }
        public bool FreeHotelPlaces()
        {
            bool places = false;
            foreach (Building building in _buildings)
            {
                if (building.Type == "H" && building.Settlers.Count() < building.TotalPlace)
                {
                    places = true;
                }
            }
            return places;
        }

        public bool CanRecruit()
        {
            return FreeHotelPlaces() && FreeRestaurantPlaces();
        }



        public void LocationOccupiedBuilding(Building building) //Création du building dans le plateau
        {
            for (int x = building.X; x < building.LinesNb + building.X; x++)
            {
                for (int y = building.Y; y < building.ColumnsNb + building.Y; y++)
                    GameBoard[x, y] = building.Id;
            }
        }
    }
}
