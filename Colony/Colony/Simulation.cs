using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Simulation
    {
        private Village _village;
        private static int _turn = 1;
        private int _turnNb;

        /// <summary>
        /// Constructor that allows you to create a simulation, with an initial turn number of 1, and that contains a village
        /// </summary>
        public Simulation()
        {
            _turnNb = _turn;
            _village = new Village();
        }


        public override string ToString()
        {

            string retour = "\nMA SIMULATION : \nMon village est le suivant : \n" + _village.ToString();
            retour += "\nIl y a : " + _village.Buildings.Count() + " batiments\n";
            if (_village.Buildings.Count() != 0)
                retour += "Les batiments existants sont les suivants : \n";
            foreach (Building b in _village.Buildings)
                retour += b.ToString();
            if (_village.GetSettlers().Count() != 0)
                retour += "Les colons existants sont les suivants : \n";
            foreach (Settler s in _village.GetSettlers())
                retour += s.ToString();
            retour += "\n";
            return retour;
        }

        /// <summary>
        /// This method allows the launch and continuation of the game
        /// </summary>
        public void Play()
        {
            bool end = false;
            while (!end && _turnNb < 100) //As long as we haven't exceeded the number of turns around, or we haven't won the game, we play and move on to the next round
            {
                PendingBuildingCreation();
                foreach (Settler settler in _village.GetSettlers())
                {
                    settler.Play(_village.GameBoardSettler, _turnNb);
                    Console.WriteLine(settler);
                }
                if (_village.CanRecruit() || _village.NbSettlerAvailable("B") >= Math.Min(Math.Min(Hotel._builderNb, Restaurant._builderNb), SportsInfrastructure._builderNb)) //Verifie qu'on peut effectuer une action sur ce tour, ou alors ça passe tout seul au tour suivant
                {
                    Console.WriteLine("Vous êtes au tour {0} \n --------- ", _turnNb);
                    DisplayGameBoard();

                    bool buildBuilding = true;
                    bool recruitSettler = true;
                    Console.WriteLine("Entrez 0 pour passer au tour suivant sans effectuer aucune action");
                    if (_village.NbSettlerAvailable("B") >= Math.Min(Math.Min(Hotel._builderNb, Restaurant._builderNb), SportsInfrastructure._builderNb))
                    {
                        Console.WriteLine("Entrez 1 pour créer un batiment");
                    }
                    else
                    {
                        Console.WriteLine("Vous n'avez pas assez de Colon pour construire un batiment");
                        buildBuilding = false;
                    }
                    if (_village.CanRecruit())
                    {
                        Console.WriteLine("Entrez 2 pour recruter un colon");
                    }
                    else
                    {
                        Console.WriteLine("Vous n'avez pas assez d'infrastructure pour accueillir des colons, ");
                        if (!_village.FreeHotelPlaces())
                        {
                            Console.WriteLine("Il vous faut un hotel");
                        }
                        if (!_village.FreeRestaurantPlaces())
                        {
                            Console.WriteLine("Il vous faut un restaurant");
                        }
                        recruitSettler = false;
                    }


                    bool creation = true;
                    int create = int.Parse(Console.ReadLine());
                    switch (create)
                    {
                        case 0:
                            break;
                        case 1:
                            {
                                if (buildBuilding == true)
                                    creation = CreateBuilding();
                                else
                                {
                                    Console.WriteLine("Vous n'avez toujours pas assez de Colon pour construire un batiment, entrez une réponse valide");
                                    creation = false;
                                }
                            }
                            break;
                        case 2:
                            if (recruitSettler == true)
                                creation = createSettler();
                            else
                            {
                                Console.WriteLine("Vous n'avez toujours pas assez d'infrastructure pour accueillir des colons, veuillez entrer une réponse valide ");
                                creation = false;
                            }
                            break;
                        default:
                            Console.WriteLine("Votre réponse n'est pas valide");
                            creation = false;
                            break;
                    }

                    if (creation == true)
                        _turnNb++;
                }
                else
                {
                    _turnNb++;
                }
            }

        }

        /// <summary>
        /// Allows you to know if actions are possible on this turn
        /// </summary>
        /// <returns>If we can perform actions on this turn it returns true, otherwise it returns false</returns>
        public bool Proceed() 
        {
            bool proceed = false;
            if (_village.NbSettlerAvailable("B") >= Math.Min(Math.Min(Hotel._builderNb, Restaurant._builderNb), SportsInfrastructure._builderNb))
            {
                proceed = true;
            }
            else
            {
                if (_village.CanRecruit())
                {
                    proceed = true;
                }
            }
            return proceed;
        }


        /// <summary>
        /// Create the buildings that are being created if it is their turn to create
        /// </summary>
        public void PendingBuildingCreation()
        {
            foreach ( InConstructionBuilding inConstruction in _village.InConstruction)
            {
                if (inConstruction.CreationTurn == _turnNb) 
                { 
                    if(inConstruction.BuildingType == "H")
                    {
                        Hotel hotel = new Hotel(inConstruction.X, inConstruction.Y);
                        _village.AddBuildings(hotel); 
                        _village.CreationBuilding(hotel);
                    }
                    else if(inConstruction.BuildingType == "R")
                    {
                        Restaurant restaurant = new Restaurant(inConstruction.X, inConstruction.Y);
                        _village.AddBuildings(restaurant);
                        _village.CreationBuilding(restaurant);
                    }
                    else
                    {
                        SportsInfrastructure sportsInfrastructurel = new SportsInfrastructure(inConstruction.X, inConstruction.Y, inConstruction.Name);
                        _village.AddBuildings(sportsInfrastructurel); 
                        _village.CreationBuilding(sportsInfrastructurel);
                        _village.SportsInfrastructures[inConstruction.Name] = true;
                    }
                    foreach (Settler builder in inConstruction.Settlers)
                    {
                        builder.IsInActivity = false;
                    }
                }
             
            }
        }

        /// <summary>
        /// Display the game board
        /// </summary>
        public void DisplayGameBoard()
        {
            for (int i = 0; i < _village.Lenght; i++)
            {
                Console.Write("\n");
                for (int j = 0; j < _village.Width; j++)
                {
                    string info = " ";
                    ConsoleColor foreGroundColor = ConsoleColor.White;
                    ConsoleColor backGroundColor = ConsoleColor.Black;
                    if (_village.GameBoardBuilder[i, j] is null && _village.GameBoardSettler[i, j].Count == 0)
                    {
                        Console.Write(".\t");
                    }
                    else if (_village.GameBoardSettler[i, j].Count == 0 && _village.GameBoardBuilder[i, j] == "XH" || _village.GameBoardBuilder[i, j] == "XR" || _village.GameBoardBuilder[i, j] == "XS")
                    {
                        if (_village.GameBoardBuilder[i, j] == "XH")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("X\t");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (_village.GameBoardBuilder[i, j] == "XR")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("X\t");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (_village.GameBoardBuilder[i, j] == "XS")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("X\t");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else
                    {
                        if (_village.GameBoardBuilder[i, j] != null)
                        {
                            if (foreGroundColor == ConsoleColor.White)
                            {
                                foreGroundColor = ConsoleColor.Black;
                            }
                            else
                            {
                                foreGroundColor = ConsoleColor.Red;
                            }
                            if (_village.GameBoardBuilder[i, j].Equals("H"))
                            {
                                backGroundColor = ConsoleColor.Cyan;
                            }
                            else if (_village.GameBoardBuilder[i, j].Equals("R"))
                            {
                                backGroundColor = ConsoleColor.Red;
                            }
                            else if (_village.GameBoardBuilder[i, j].Equals("S"))
                            {
                                backGroundColor = ConsoleColor.Green;
                            }
                        }
                        Console.BackgroundColor = backGroundColor;
                        if (_village.GameBoardSettler[i, j] != null)
                        {
                            foreach(Settler settler in _village.GameBoardSettler[i, j]) { 
                                foreGroundColor = settler.Available ? ConsoleColor.White : ConsoleColor.Red;
                                if (settler is Builder)
                                {
                                    info = "B";
                                }
                                else if (settler is Athletic)
                                {
                                    info = "A";
                                }
                                else if (settler is Coach)
                                {
                                    info = "C";
                                }
                                Console.ForegroundColor = foreGroundColor;
                                Console.Write(info);
                            }
                        }
                        Console.Write("\t");
                        Console.ResetColor();
                    }
                }
            }
            Console.Write("\n");
        }


        /// <summary>
        /// Check if the space is not already occupied or if it does not come out of the board
        /// </summary>
        /// <param name="type">type of building you want to build</param>
        /// <param name="x">Abscissa of the top left corner of where you want to build the building</param>
        /// <param name="y">Ordinate of the top left corner of where you want to build the building</param>
        /// <returns>returns true if space is free, false if space is unavailable or does not exist (game board exit)</returns>
        public bool FreeSpaceBuilding(string type, int x, int y) 
        {
            int lines;
            int columns;
            bool freeSpaceBuilding = true;
            lines = Building.GetLinesNb(type);
            columns = Building.GetColumnsNb(type);
            if (x + lines >= _village.GameBoardBuilder.GetLength(0) || y + columns >= _village.GameBoardBuilder.GetLength(1))
            {
                Console.WriteLine("Vous ne pouvez pas construire ici, vous sortirez du plateau de jeu");
                freeSpaceBuilding = false;
            }
            for (int i = x; i <= x + lines - 1; i++)
            {
                for (int j = y; j <= y + columns - 1; j++)
                {
                    if (_village.GameBoardBuilder[i, j] != null)//O=Place  occupé par batiment, j'ai pas toruvé mieux, dans l'idéal juste colorier case
                    {
                        Console.WriteLine("Tu ne peux pas construire sur un batiment qui existe déjà ou qui est en cours de construction, choisi un autre emplacement!");
                        freeSpaceBuilding = false;
                    }
                }
            }
            return freeSpaceBuilding;
        }

        /// <summary>
        /// Method that offers the player to create a building
        /// </summary>
        /// <returns>Returns true if the player has chosen to build a building, false otherwise</returns>
        public bool CreateBuilding()
        {
            Console.WriteLine("Entrez 0 pour revenir en arrière");
            bool creation = false;
            bool createHotel = false;
            bool createRestaurant = false;
            bool createSportsInfrastructure = false;
            if (_village.NbSettlerAvailable("B") >= Hotel._builderNb)
            {
                Console.WriteLine("Entrez 1 pour créer un Hotel");
                createHotel = true; 
            }
            if (_village.NbSettlerAvailable("B") >= Restaurant._builderNb)
            {
                Console.WriteLine("Entrez 2 pour créer un Restaurant");
                createRestaurant = true;
            }
            if (_village.NbSettlerAvailable("B") >= SportsInfrastructure._builderNb)//TODO les conditions pour construire une infrastructure ne sont pas les mêmes
            {
                Console.WriteLine("Entrez 3 pour créer une Infrastructure Sportive");
                createSportsInfrastructure = true;
            }
            int create = int.Parse(Console.ReadLine());

            if (create == 1 || create == 2 || create == 3)
            {
                Console.WriteLine("Entrez la ligne de l'angle en haut à gauche de votre batiment");
                int x = int.Parse(Console.ReadLine());
                Console.WriteLine("Entrez la colonne de l'angle en haut à gauche de votre batiment");
                int y = int.Parse(Console.ReadLine());

                int nbBuilders = 0;

                if (create == 1)
                {
                    if (createHotel == true)
                    {
                        if (FreeSpaceBuilding(Hotel.type, x, y))//TODO faire une fonction pour empecher la recurrence de code
                        {
                            nbBuilders = Hotel._builderNb;
                            List<Settler> settlers = BusyBulderList(nbBuilders); 
                            foreach (Settler settler in settlers)
                            {
                                settler.CalculatingItinerary(x, y);
                            }
                            InConstructionBuilding inConstruction = new InConstructionBuilding("H", Hotel._turnNb+_turnNb, x, y, settlers, "");
                            _village.InConstruction.Add(inConstruction);
                            creation = true;
                            _village.CreatePendingBuilding(inConstruction);//J'essaye
                        }
                    }
                    else
                        Console.WriteLine("Cette réponse n'est pas valide car tu n'as pas assez de batisseur pour construire un hotel, choisi une réponse qui t'es proposé");

                }
                else if (create == 2)
                {
                    if (createRestaurant == true)
                    {
                        if (FreeSpaceBuilding(Restaurant.type, x, y))
                        {
                            nbBuilders = Restaurant._builderNb;
                            List<Settler> settlers = BusyBulderList(nbBuilders);
                            foreach (Settler settler in settlers)
                            {
                                settler.CalculatingItinerary(x, y);
                            }
                            InConstructionBuilding inConstruction = new InConstructionBuilding("R", Restaurant._turnNb + _turnNb, x, y, settlers, "");
                            _village.InConstruction.Add(inConstruction);
                            creation = true;
                            _village.CreatePendingBuilding(inConstruction);//J'essaye
                        }
                    }
                    else
                        Console.WriteLine("Cette réponse n'est pas valide car tu n'as pas assez de batisseur pour construire un restaurant, choisi une réponse qui t'es proposé");
                }
                else if (create == 3)
                {
                    if (createSportsInfrastructure == true)
                    {
                        if (FreeSpaceBuilding(SportsInfrastructure.type, x, y))
                        {
                            string sportsinfrasctructure = ChoiceSportsInfrastructure();
                            nbBuilders = SportsInfrastructure._builderNb;
                            List<Settler> settlers = BusyBulderList(nbBuilders);
                            foreach (Settler settler in settlers)
                            {
                                settler.CalculatingItinerary(x, y);
                            }
                            InConstructionBuilding inConstruction = new InConstructionBuilding("S", SportsInfrastructure._turnNb + _turnNb, x, y, settlers, sportsinfrasctructure);
                            _village.InConstruction.Add(inConstruction);
                            creation = true;
                            _village.CreatePendingBuilding(inConstruction);//J'essaye
                        }
                    }
                    else 
                        Console.WriteLine("Cette réponse n'est pas valide car tu n'as pas assez de batisseur pour construire une infrastructure sportive, choisi une réponse qui t'es proposé");
                }
            }
            else if (create == 0)
                creation = false;
            else
            {
                Console.WriteLine("Les données que vous avez entrées concernant le type de batiment que vous souhaitez construire ne sont pas valides. Veuillez saisir des informations valables \n");
                CreateBuilding();
            }
            return creation;
        }

        /// <summary>
        /// Method that allows the player to choose which sports infrastructure he wants to build
        /// </summary>
        /// <returns>returns the name of the sports infrastructure to create</returns>
        public string ChoiceSportsInfrastructure()
        {
            Console.WriteLine("Choisissez l'infrastructure sportive que vous souhaitez construire");
            Console.WriteLine("Entrez 1 pour une piscine olympique \nEntrez 2 pour un terrain de sport collectif intérieur \nEntrez 3 pour un stade");
            int infrasctructure = int.Parse(Console.ReadLine());
            string sportsinfrasctructure = "";
            if (infrasctructure == 1)
                sportsinfrasctructure = "Piscine olympique";
            else if (infrasctructure == 2)
            {
                sportsinfrasctructure = "Terrain de sport collectif intérieur";
            }
            else if (infrasctructure == 3)
            {
                sportsinfrasctructure = "Stade";
            }
            else
            {
                Console.WriteLine("Votre réponse n'est pas valable. Veuillez entrer une réponse valide");
                ChoiceSportsInfrastructure();
            }
            return sportsinfrasctructure; 
        }

        /// <summary>
        /// Available bulder assigment at the building
        /// </summary>
        /// <param name="nbBuilders">The number of builder assigments</param>
        /// <returns>A list of builder assigment's</returns>
        public List<Settler> BusyBulderList(int nbBuilders)
        {
            List<Settler> busyBuilder = new List<Settler>();
            int nbAvailable = 0;
            for (int i = 0; i < nbBuilders; i++)
            {
                nbAvailable = _village.FindAvailable("B").Count();
                Console.WriteLine(nbAvailable);
                busyBuilder.Add(_village.FindAvailable("B")[0]);
                _village.FindAvailable("B")[0].IsInActivity = true;
            }

            return busyBuilder;

        }

        /// <summary>
        /// Méthode qui permet au joueur de choisir le colon qu'il souhaite recruter
        /// </summary>
        /// <returns>Returns true if he chose to recruit a colonist, false otherwise</returns>
        public bool createSettler()
        {
            Console.WriteLine("Entrez 0 pour revenir en arrière");
            Console.WriteLine("Entrez 1 pour recruter un Batisseur");
            Console.WriteLine("Entrez 2 pour recruter un Coach");
            if (CanRecruitAthlete())
                Console.WriteLine("Entrez 3 pour recruter un Sportif");
            else
                Console.WriteLine("Vous ne pouvez pas recruter un sportif car vous n'avez aucune infrastructure sportive");
            int create = int.Parse(Console.ReadLine());
            bool creation = true;
            if (create ==0)
                creation = false;
            else if (create == 1)
            {

                Builder builder = new Builder();
                _village.AddSettler(builder);
            }
            else if (create == 2)
            {
                Coach coach = new Coach();
                _village.AddSettler(coach); //Ca nous fait sortir de la boucle
                Athletic.LevelIncrease++;
                Console.WriteLine("Athletic.LevelIncrease : " + Athletic.LevelIncrease);
            }
            else if (create == 3)
            {
                bool createAthletic = CreateAthletics();
                if (createAthletic == false)
                    createSettler();
            }
            else
            {
                Console.WriteLine("Votre réponse n'est pas valide");
                createSettler();
            }
            return creation;
        }

        /// <summary>
        /// Method which allows the player to choose the nationality and the sport of the athlete he wants to recruit, and to create it
        /// </summary>
        /// <returns>Returns false if ultimately he does not wish to recruit an athlete, true otherwise</returns>
        private bool CreateAthletics()
        {
            bool createAthletics = true;

            string nationality2 = "";
            while (nationality2 == "")
            {
                Console.WriteLine("Entrez 0 pour revenir en arrière");
                Console.WriteLine("Choisissez sa nationnalité \nEntrez 1 pour que le sportif soit Français \nEntrez 2 pour que le sportif soit Anglais\nEntrez 3 pour que le sportif soit Américain\nEntrez 4 pour que le sportif soit Japonais");//A en rajouter
                int nationality = int.Parse(Console.ReadLine());

                if (nationality == 0)
                {
                    createAthletics = false; 
                    nationality2 = "rien";
                }
                else if (nationality == 1)
                    nationality2 = "Francais";
                else if (nationality == 2)
                    nationality2 = "Anglais";
                else if (nationality == 3)
                    nationality2 = "Americain";
                else if (nationality == 4)
                    nationality2 = "Japonais";
                else
                    Console.WriteLine("Votre réponse n'est pas valide, veuillez entrer un numéro valide");
            }

            if (createAthletics == true)
            {
                string sport2 = "";
                string infrastructure = "";
                bool swimingPool = false;
                bool field = false;
                bool stage = false;
                bool value;
                while (sport2 == "")
                {
                    Console.WriteLine("Choisissez son sport :");
                    _village.SportsInfrastructures.TryGetValue("Piscine olympique", out value);
                    if (value == true)
                    {
                        Console.WriteLine("Entrez 1 pour de la natation ");
                        swimingPool = true;
                    }
                    else
                    {
                        _village.SportsInfrastructures.TryGetValue("Terrain de sport collectif intérieur", out value);
                        if (value == true)
                        {
                            Console.WriteLine("Entrez 2 pour du volley ");
                            Console.WriteLine("Entrez 3 pour du hand");
                            Console.WriteLine("Entrez 4 pour du basket");
                            field = true;
                        }
                        else
                        {
                            _village.SportsInfrastructures.TryGetValue("Stade", out value);
                            if (value  == true)
                            {
                                Console.WriteLine("Entrez 5 pour du football ");
                                Console.WriteLine("Entrez 6 pour du rugby");
                                Console.WriteLine("Entrez 7 pour de l'athlétisme");
                                stage = true;
                            }
                        }
                    }
                    
                    int sport = int.Parse(Console.ReadLine());

                    if (sport == 1 && swimingPool)
                    {
                        sport2 = "Natation";
                    }
                    else if (field)
                    {
                        if (sport == 2)
                        {
                            sport2 = "Volley";
                        }
                        else if (sport == 3)
                        {
                            sport2 = "Hand";
                        }
                        else if (sport == 4 && field)
                        {
                            sport2 = "Basketball";
                        }
                    }
                    else if (stage)
                    {
                        if (sport == 5)
                        {
                            sport2 = "Football";
                        }
                        else if (sport == 6 && stage)
                        {
                            sport2 = "Rugby";
                        }
                        else if (sport == 7 && stage)
                        {
                            sport2 = "Athlétisme";
                        }
                    }
                    else
                    {
                        Console.WriteLine("Votre réponse n'est pas valide, veuillez entrer un numéro valide");
                    }
                }

                    Athletic athletic = new Athletic(nationality2, sport2);
                    _village.AddSettler(athletic);
                    Console.WriteLine("Vous avez recruté un nouveau sportif : ");
                    Console.WriteLine(athletic);
            }
            return createAthletics;            
        }

        /// <summary>
        /// Method that allows you to know if you can recruit an athlete (you can if there is at least one sports infrastructure on the game board)
        /// </summary>
        /// <returns>Returns true if there is a sports infrastructure present and therefore if an athlete can be recruited, false otherwise</returns>
        public bool CanRecruitAthlete()
        {
            bool canRecruitAthlete = false;
            foreach (Building building in _village.Buildings)
                if (building is SportsInfrastructure)
                    canRecruitAthlete = true;
            return canRecruitAthlete;
        }
    }
}