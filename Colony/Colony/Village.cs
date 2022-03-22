using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Village
    {
        private List<Building> _buildings;
        private int _maxNbSettlers;
        private string[,] _gameBoard = new string[20, 40];
        private List<Settler> _settlers;

        public Village()
        {
            Builder s1 = new Builder();
            Builder s2 = new Builder();
            Builder s3 = new Builder();
            Builder s4 = new Builder();
            _settlers = new List<Settler> { s1, s2, s3, s4 };
            _maxNbSettlers = 4;
            Hotel h1 = new Hotel(0, 0);
            Restaurant r1 = new Restaurant(8, 8);
            _buildings = new List<Building> { h1, r1 };
        }

        public string[,] GameBoard
        {
            get { return _gameBoard; }
        }

        public List<Building> Buildings
        {
            get { return _buildings; }
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

        public List<Settler> getSettlers()
        {
            return _settlers;
        }

        public void addBuildings(Building building)
        {
            _buildings.Add(building);
        }

        public void addSettler(Settler settler)
        {
            _settlers.Add(settler);

        }

        public int freeHotelPlaces()
        {
            int places = 0;
            foreach (Hotel hotel in _buildings)
            {
                foreach (Settler settler in hotel.getSettlers())
                {
                    if (settler is null)
                    {
                        places++;
                    }
                }
            }
            return places;
        }


        public int freeRestaurantPlaces()
        {
            int places = 0;
            foreach (Restaurant restaurant in _buildings)
            {
                foreach (Settler settler in restaurant.getSettlers())
                {
                    if (settler is null)
                    {
                        places++;
                    }
                }
            }
            return places;
        }

        public int placesNb()
        {
            return Math.Min(freeRestaurantPlaces(),freeHotelPlaces());
        }
        public bool freePlaces()
        {
            return placesNb() != 0;
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
