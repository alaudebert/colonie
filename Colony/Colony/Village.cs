using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    struct InConstructionBuilding
    {
        private string _buildingType;
        private int _x;
        private int _y;
        private List<Settler> _settlers;
        private int _creationTurn;
        private int _turnNb;
        private string _name;

        public InConstructionBuilding(string type,int turnNb, int x, int y, List<Settler> builders, string name)
        {
            _buildingType = type;
            _x = x;
            _y = y;
            _settlers = builders;
            _creationTurn = 0;
            _name = name;

            foreach (Builder builder in builders)
            {
                _creationTurn = Math.Max(_creationTurn, Math.Abs(builder._itinerary[0]) + Math.Abs(builder._itinerary[1]) + turnNb);
            }
            _turnNb = turnNb;
        }

        public string Name
        {
            get { return _name; }
        }

        public string BuildingType
        {
            get { return _buildingType; }
        }
        public int X
        {
            get { return _x; }
        }
        public int Y
        {
            get { return _y; }
        }
        public List<Settler> Settlers//TODO cahnger le nom de la variable
        {
            get { return _settlers; }
        }
        public int CreationTurn
        {
            get { return _creationTurn; }
        }
    }

    class Village
    {
        private List<Building> _buildings = new List<Building>();
        private List<InConstructionBuilding> _inConstruction = new List<InConstructionBuilding>(); 
        private int _maxNbSettlers;
        private int _lenght;
        private int _width;
        private string[,] _gameBoardBuilder;
        private List<Settler>[,] _gameBoardSettler;
        private List<Settler> _settlers = new List<Settler>();
        

        public Village()
        {
            _lenght = 20;
            _width = 15;
            _gameBoardSettler = new List<Settler>[_lenght, _width];
            _gameBoardBuilder = new string[_lenght, _width];
            for (int i = 0; i < _width; i++)
            {
                for (int y = 0; y < _lenght; y++)
                {
                    GameBoardSettler[y,i] = new List<Settler>();
                }
            }
            Restaurant restaurant = new Restaurant(8, 8);
            Hotel hotel = new Hotel(0, 0);
            Builder s1 = new Builder();
            Builder s2 = new Builder();
            Builder s3 = new Builder();
            Builder s4 = new Builder();
            s2.X = 0;
            s2.Y = 1;
            s3.X = 0;
            s3.Y = 2;
            s4.X = 0;
            s4.Y = 3;
            _maxNbSettlers = 4;
            _buildings.Add(hotel);
            _buildings.Add(restaurant);
            AddSettler(s1);
            AddSettler(s2);
            AddSettler(s3);
            AddSettler(s4);
        }

        public string[,] GameBoardBuilder
        {
            get { return _gameBoardBuilder; }
        }

        public List<Settler>[,] GameBoardSettler
        {
            get { return _gameBoardSettler; }
        }
        public int Width
        {
            get { return _width; }
        }

        public int Lenght
        {
            get { return _lenght; }
        }

        public List<Building> Buildings
        {
            get { return _buildings; }
        }
        public List<InConstructionBuilding> InConstruction

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


        public void AddBuildings(Building building)
        {
            _buildings.Add(building);
        }

        public void AddSettler(Settler settler)
        {
            while (_gameBoardSettler[settler.X, settler.Y].Count != 0)
            {
                if (settler.Y < _width) 
                {
                    settler.Y++;
                }
                else
                {
                    settler.X++;
                }
            }
            _settlers.Add(settler);
            _gameBoardSettler[settler.X, settler.Y].Add(settler);
            int i = 0;
            bool addHotel = false;
            bool addRestaurant = false;
            while (i<_buildings.Count() && ( addHotel == false || addRestaurant == false) )
            {
                if (_buildings[i].haveFreePlace()) 
                {
                    if (_buildings[i] is Restaurant)
                    {
                        _buildings[i].Settlers.Add(settler);
                        addRestaurant = true;
                    }
                    else if(_buildings[i] is Hotel) 
                    {
                        _buildings[i].Settlers.Add(settler);//Le probleme vient d'ici, on peut pas recruter autre chose qu'un batisseur en premier je sais pas pourquoi
                        addHotel = true;
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



        public void CreationBuilding(Building building) //Création du building dans le plateau
        {
            int nbColumns = 0;
            int nbLines = 0;
            if (building is Hotel)
            {
                nbColumns = Building._buildingSize.FirstOrDefault(x => x.Key == "H").Value[1];
                nbLines = Building._buildingSize.FirstOrDefault(x => x.Key == "H").Value[0];
            }
            else if (building is Restaurant)
            {
                nbColumns = Building._buildingSize.FirstOrDefault(x => x.Key == "R").Value[1];
                nbLines = Building._buildingSize.FirstOrDefault(x => x.Key == "R").Value[0];
            }
            else if(building is SportsInfrastructure)
            {
                nbColumns = Building._buildingSize.FirstOrDefault(x => x.Key == "S").Value[1];
                nbLines = Building._buildingSize.FirstOrDefault(x => x.Key == "S").Value[0];

            }
            for (int x = building.X; x < nbLines + building.X; x++)
            {
                for (int y = building.Y; y < nbColumns + building.Y; y++)
                    _gameBoardBuilder[x, y] = building.Type;
            }
        }

        public void CreatePendingBuilding(InConstructionBuilding inConstruction)
        {
            int nbColumns = 0;
            int nbLines = 0;
            string inCreation = "";
            if (inConstruction.BuildingType == "H")
            {
                nbColumns = Building._buildingSize.FirstOrDefault(x => x.Key == "H").Value[1];
                nbLines = Building._buildingSize.FirstOrDefault(x => x.Key == "H").Value[0];
                inCreation = "XH";
            }
            else if (inConstruction.BuildingType == "R")
            {
                nbColumns = Building._buildingSize.FirstOrDefault(x => x.Key == "R").Value[1];
                nbLines = Building._buildingSize.FirstOrDefault(x => x.Key == "R").Value[0];
                inCreation = "XR";
            }
            else if (inConstruction.BuildingType == "S")
            {
                nbColumns = Building._buildingSize.FirstOrDefault(x => x.Key == "S").Value[1];
                nbLines = Building._buildingSize.FirstOrDefault(x => x.Key == "S").Value[0];
                inCreation = "XS";

            }
            for (int x = inConstruction.X; x < nbLines + inConstruction.X; x++)
            {
                for (int y = inConstruction.Y; y < nbColumns + inConstruction.Y; y++)
                    _gameBoardBuilder[x, y] = inCreation;
            }
        }
    }
}
