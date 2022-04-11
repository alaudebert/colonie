using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    /// <summary>
    /// structure that create a building under construction
    /// </summary>
    struct InConstructionBuilding
    {
        private string _buildingType;
        private int _x;
        private int _y;
        private List<Settler> _settlers;
        private int _creationTurn;
        private int _turnNb;
        private string _name;

        /// <summary>
        /// Builder that saves a building under construction
        /// </summary>
        /// <param name="type">Type of building under construction</param>
        /// <param name="turnNb">Tower where the creation of the building began</param>
        /// <param name="x">Abscissa of the location where the building must be built (top left corner)</param>
        /// <param name="y">Ordinate of the location where the building must be built (top left corner)</param>
        /// <param name="builders">List of builders who build this building</param>
        /// <param name="name">Name of Building (It allows to put the name of the sports infrastructure at its creation)</param>
        public InConstructionBuilding(string type,int turnNb, int x, int y, List<Settler> builders, string name)
        {
            _buildingType = type;
            _x = x;
            _y = y;
            _settlers = builders;
            _creationTurn = 0;
            _name = name;

            //Calculates the turn where the creation of the building will be finished
            foreach (Builder builder in builders)
            {
                _creationTurn = Math.Max(_creationTurn, Math.Abs(builder._itinerary[0]) + Math.Abs(builder._itinerary[1]) + turnNb );  //TODO Je crois que ça prend pas en compte le temps de construction du batiment
            }
            _turnNb = turnNb;
        }

        /// <summary>
        /// Returns the name of the building
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Returns the building type
        /// </summary>
        public string BuildingType
        {
            get { return _buildingType; }
        }

        /// <summary>
        /// Returns the abscissa of the top left corner of the building
        /// </summary>
        public int X
        {
            get { return _x; }
        }

        /// <summary>
        /// Returns the ordinate of the top left corner of the building
        /// </summary>
        public int Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Returns the list of builders who build this building
        /// </summary>
        public List<Settler> Settlers//TODO cahnger le nom de la variable
        {
            get { return _settlers; }
        }

        /// <summary>
        /// Returns the turn where the building creation will be finished
        /// </summary>
        public int CreationTurn
        {
            get { return _creationTurn; }
        }
    }

    class Village
    {
        private List<Building> _buildings = new List<Building>();
        private List<InConstructionBuilding> _inConstruction = new List<InConstructionBuilding>(); 
        private int _maxNbSettlers; //TOTO inutile je crois Alex
        private int _lenght;
        private int _width;
        private string[,] _gameBoardBuilder;
        private List<Settler>[,] _gameBoardSettler;
        private List<Settler> _settlers = new List<Settler>();
        public Dictionary<string, bool> SportsInfrastructures { get; set; }




        /// <summary>
        /// Builder that allows you to create a village
        /// </summary>
        public Village()
        {
            SportsInfrastructures = new Dictionary<string, bool>();
            SportsInfrastructures.Add("Piscine olympique", false);
            SportsInfrastructures.Add("Terrain de sport collectif intérieur", false);
            SportsInfrastructures.Add("Stade", false);

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
            CreationBuilding(hotel);
            CreationBuilding(restaurant);
            Builder s1 = new Builder();
            Builder s2 = new Builder();
            Builder s3 = new Builder();
            s2.X = 0;
            s2.Y = 1;
            s3.X = 0;
            s3.Y = 2;
            //s4.X = 0;
            //s4.Y = 3;
            _maxNbSettlers = 4;
            _buildings.Add(hotel);
            _buildings.Add(restaurant);
            AddSettler(s1);
            AddSettler(s2);
            AddSettler(s3);
        }

        /// <summary>
        /// Return the game board containing the buildings
        /// </summary>
        public string[,] GameBoardBuilder
        {
            get { return _gameBoardBuilder; }
        }

        /// <summary>
        /// Return the game board containing the settlers
        /// </summary>
        public List<Settler>[,] GameBoardSettler
        {
            get { return _gameBoardSettler; }
        }

        /// <summary>
        /// Returns the width of the board
        /// </summary>
        public int Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Returns the length of the board
        /// </summary>
        public int Lenght
        {
            get { return _lenght; }
        }

        /// <summary>
        /// Returns the list of buildings present in the village
        /// </summary>
        public List<Building> Buildings
        {
            get { return _buildings; }
        }

        /// <summary>
        /// Returns and modifies the list of buildings under construction
        /// </summary>
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

        /// <summary>
        /// Returns the list of settlers present in the village
        /// </summary>
        public List<Settler> GetSettlers()
        {
            return _settlers;
        }

        /// <summary>
        /// Returns the number of settlers available in the village
        /// </summary>
        /// <param name="type">We enter the type of settlers whose information we want to know ("A" for an athlete, "B" for a builder and "C" for a coach)</param>
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
                if (settler.Available && settler.SettlerType.Equals(type))
                {
                    availables.Add(settler);
                }
            }
            return availables;
        }


        /// <summary>
        /// Allows you to add a building in the game by adding it to the list of buildings present in the village
        /// </summary>
        /// <param name="building">We enter the building we want to add</param>
        public void AddBuildings(Building building)
        {
            _buildings.Add(building);
        }

        /// <summary>
        /// Allows you to add a settler to the list of settlers present in the village
        /// </summary>
        /// <param name="settler">Settler you want to add</param>
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
                        settler.Buildings[1] = _buildings[i];
                        addRestaurant = true;
                    }
                    else if(_buildings[i] is Hotel) 
                    {
                        _buildings[i].Settlers.Add(settler);
                        settler.Buildings[0] = _buildings[i]; 
                    }
                }
                i++;
            }

        }


        /// <summary>
        /// See if there's room left in the restaurants
        /// </summary>
        /// <returns>returns true if there is space, false otherwise</returns>
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

        /// <summary>
        /// See if there's room left in the hotels
        /// </summary>
        /// <returns>returns true if there is space, false otherwise</returns>
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

        /// <summary>
        /// See if it is possible to welcome new settlers in the village
        /// </summary>
        /// <returns>Returns true if there is at least one place left, false otherwise</returns>
        public bool CanRecruit()
        {
            return FreeHotelPlaces() && FreeRestaurantPlaces();
        }


        /// <summary>
        /// Creation of the building in the board
        /// </summary>
        /// <param name="building">We enter the building we want to create</param>
        public void CreationBuilding(Building building) 
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

        /// <summary>
        /// Creates a building under construction
        /// </summary>
        /// <param name="inConstruction">The building that is under construction</param>
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
