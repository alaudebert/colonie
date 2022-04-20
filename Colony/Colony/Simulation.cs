using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Simulation
    {
        private Village Village { get; set; }
        private static int _turn = 1;
        private int TurnNb { get;set; }

        /// <summary>
        /// Constructor that creates a simulation, containing a village
        /// </summary>
        public Simulation()
        {
            TurnNb = _turn;
            Village = new Village();
        }


        public override string ToString()
        {
            string retour = "\nMA SIMULATION : \nMon village est le suivant : \n" + Village.ToString();
            retour += "\nIl y a : " + Village.Buildings.Count() + " batiments\n";
            if (Village.Buildings.Count() != 0)
                retour += "Les batiments existants sont les suivants : \n";
            foreach (Building b in Village.Buildings)
                retour += b.ToString();
            if (Village.GetSettlers().Count() != 0)
                retour += "Les colons existants sont les suivants : \n";
            foreach (Settler s in Village.GetSettlers())
                retour += s.ToString();
            retour += "\n";
            return retour;
        }

        /// <summary>
        /// Explain the instructions of the party
        /// </summary>
        public void Launch()
        {
            string instruction = "Bienvenue dans notre jeu : LE VILLAGE OLYMPIQUE !\n - Si vous voulez lire les règles du jeu, entrez 1 \n- Si vous voulez dirèctement jouer, entrez 2";
            int choice = VerifySyntax(instruction);
            if (choice == 1)
            {
                RulesOfTheGame();
                instruction = "Entrez 2 si maintenant vous souhaitez jouer\n";
                choice = VerifySyntax(instruction);
            }
            if (choice == 2)
                Play();
            else
            {
                Error("Votre réponse n'est pas valide, veuillez entrer une réponse valide \n");
                Launch();
            }
        }
        /// <summary>
        /// Verify if the syntax of the input is a number
        /// </summary>
        /// <param name="phrase">The input</param>
        /// <returns>The number contain in the input chain if it's realy a number</returns>
        public int VerifySyntax(string phrase)
        {
            Console.WriteLine(phrase);
            bool valid = int.TryParse(Console.ReadLine(), out int resultat);
            while (!valid)
            {
                Error("La syntaxe est incorrecte veuillez entrer un nombre");
                Console.WriteLine(phrase);
                valid = int.TryParse(Console.ReadLine(), out resultat);
            }
            return resultat;
        }

        /// <summary>
        /// Display an error message in red color
        /// </summary>
        /// <param name="message">The error message</param>
        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Method that allows us to play, and launching a game and continuing it until it is finished (i.e. won or lost)
        /// </summary>
        public void Play()
        {
            bool end = false;
            while (end == false && TurnNb < 100)
            {
                //The minimum builder required to build a building
                int villageSetllerAvailable = Math.Min(Math.Min(Hotel.BuilderNb, Restaurant.BuilderNb), SportsInfrastructure.BuilderNb);
                
                Console.WriteLine("Vous êtes au tour {0} \n --------- ", TurnNb);
                PendingBuildingCreation();
                DisplayGameBoard();

                    bool buildBuilding = true;
                    bool recruitSettler = true;
                    bool trainAthetics = true;
                    string instruction ="Entrez 0 pour passer au tour suivant sans effectuer aucune action\nEntrez 10 pour avoir le détail des colons de votre colonie\nEntrez 20 pour voir les regles du jeu";
                    
                    //We proposed all the option available
                    if (Village.NbSettlerAvailable("B") >= villageSetllerAvailable)
                    {
                        instruction += "\nEntrez 1 pour créer un batiment";
                    }
                    else
                    {
                        Error("Vous n'avez pas assez de Colon pour construire un batiment");
                        buildBuilding = false;
                    }
                    if (Village.CanRecruit())
                    {
                        instruction += "\nEntrez 2 pour recruter un colon";
                    }
                    else
                    {
                        Error("Vous n'avez pas assez d'infrastructure pour accueillir des colons, ");
                        if (!Village.FreeHotelPlaces())
                        {
                            Error("Il vous faut un hotel");
                        }
                        if (!Village.FreeRestaurantPlaces())
                        {
                            Error("Il vous faut un restaurant");
                        }
                        recruitSettler = false;
                    }
                    if (Village.CanTrain())
                    {
                        instruction += "\nEntrez 3 pour entrainer un sportif";
                    }
                    else
                    {
                        trainAthetics = false;
                        Error("Vous n\'avez pas de sportif à entrainer");
                    }


                bool creation = true;
                    int create = VerifySyntax(instruction); 
                if (create != 10 && create != 20)
                {
                    foreach (Settler settler in Village.GetSettlers())
                    {
                        settler.Play(Village.GameBoardSettler, TurnNb);
                    }
                    end = Village.ProfessionnelNb >= 2;
                }
                switch (create)
                    {
                        case 0:
                            break;
                        case 1:
                            {
                                if (buildBuilding == true)
                                    creation = ChoiceBuilding();
                                else
                                {
                                    Error("Vous n'avez toujours pas assez de Colon pour construire un batiment, entrez une réponse valide");
                                    creation = false;
                                }
                            }
                            break;
                        case 2:
                            if (recruitSettler == true)
                                creation = ChoiceSettler();
                            else
                            {
                                Error("Vous n'avez toujours pas assez d'infrastructure pour accueillir des colons, veuillez entrer une réponse valide ");
                                creation = false;
                            }
                            break;
                        case 3:
                            if (trainAthetics)
                            {
                                creation = Village.CanTrain();
                                Coach coach = Village.CanBeCoach();
                                Athletic athlete = ChooseAnAthlete();
                                if (coach != null)
                                {
                                    athlete.myCoach = coach;
                                }
                                athlete.Train(coach, TurnNb);
                            }else
                            {
                                Error("Vous n'avez toujours pas de sportif à entrainer, veuillez entrer une réponse valide ");
                                creation = false;
                            }
                            break;
                        case 10:
                            Console.WriteLine("Voici vos COLONS");
                            Console.WriteLine("----------------------------");
                            foreach (Settler settler in Village.GetSettlers())
                            {
                                Console.WriteLine(settler.ToString());
                                creation = false;
                            }
                            Console.WriteLine("----------------------------");
                            break;
                        case 20:
                            RulesOfTheGame(); 
                            creation = false;
                            break;
                        default:
                            Error("Votre réponse n'est pas valide");
                            creation = false;
                            break;
                    }

                    if (creation == true)
                        TurnNb++;
            }
            if (end)
            {
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("|Félicitation vous pouvez participer aux|");
                Console.WriteLine("|         Jeux OLYMPIQUES 2024          |");
                Console.WriteLine("-----------------------------------------");
            }
            else
            {
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("|Dommage vous avez perdue vous ne pourrez pas participer aux|");
                Console.WriteLine("|                 Jeux OLYMPIQUES 2024                      |");
                Console.WriteLine("-------------------------------------------------------------");
                Launch();
            }
        }

        /// <summary>
        /// Shows the rules of the game
        /// </summary>
        public void RulesOfTheGame()
        {
            Console.WriteLine("----------LES REGLES DU JEU----------");
            Console.WriteLine();
            Console.WriteLine("Le But :");
            Console.WriteLine();
            Console.WriteLine("     * Vous voulez participer aux jeux Olympiques 2022 qui aurony lieu dans 100 tours.");
            Console.WriteLine("     * Pour y participer il vous faut au moins 2 sportifs suffisamment entrainé (2 sportif au niveau 1) avant le 100 ème tour");
            Console.WriteLine("     * Vous partez avec un hotel (en bleu), un restaurant (en violet) et 3 batisseurs ");
            Console.WriteLine();
            Console.WriteLine("Les fonctionnements : ");
            Console.WriteLine();
            Console.WriteLine("     * Vous pouvez recruter des colons, pour cela il faut assez de place dans les hotels et les restaurants");
            Console.WriteLine("         - Un hotel à 5 places et un restaurant en a 10");
            Console.WriteLine("         - Vous pouvez recruter un batisseur");
            Console.WriteLine("         - Vous pouvez recruter un coach");
            Console.WriteLine("         - Vous pouvez recruter un sportif que si vous avez l'infrastructure sportive lié à son sport \n(le terrain de sport collectif intérieur pour le volley)");
            Console.WriteLine();
            Console.WriteLine("     * Vous pouvez construir si vous avez assez de batisseurs disponnible");
            Console.WriteLine("         - 2 batisseurs pour un Hotel");
            Console.WriteLine("         - 3 batisseurs pour un Restaurant");
            Console.WriteLine("         - 5 batisseurs pour une infrastructure sportive");
            Console.WriteLine();
            Console.WriteLine("      * Les batiments seront construit quand tout les batisseurs seront au bon emplacement pendant un certain temps");
            Console.WriteLine("         - 2 tours pour un Hotel");
            Console.WriteLine("         - 2 tours pour un Restaurant");
            Console.WriteLine("         - 4 tours pour une infrastructure sportive");
            Console.WriteLine();
            Console.WriteLine("     * Vous pouvez entrainer un coach");
            Console.WriteLine("         - Si un coach est disponnible sur le plateau il sera automatiquement attricué au sportif");
            Console.WriteLine("         - Quand un sportif s'entraine il gagne 1 point de session (2 s'il est avec un coach)");
            Console.WriteLine("         - Quand le sportif à 2 points de session il gagne 1 niveau (il est donc au niveau maximum)");
            Console.WriteLine("         - Le sportif est entrainé quand il est sur la case de son infrastructure sportive pendant 4 tours");
            Console.WriteLine(); 
            Console.WriteLine("----------A VOUS DE JOUER----------");
            Console.WriteLine();
        }

        /// <summary>
        /// Allows us to train athletes
        /// </summary>
        /// <returns>We return the athlete we train</returns>
        public Athletic ChooseAnAthlete()
        {
            List<Settler> settlers = Village.GetSettlers();
            string instruction = "";
            for (int i = 0;  i < settlers.Count; i++)
            {
                if (settlers[i] is Athletic athletic && settlers[i].Available)
                {
                    Console.WriteLine("Tapez " + athletic.AthleticNb + " pour entrainer le "+ athletic.Sport + athletic.Nationality);
                }
            }
            int id = VerifySyntax("");
            return Village.FindById(id);
        }

        /// <summary>
        /// Allows you to know if actions are possible on this turn
        /// </summary>
        /// <returns>If we can perform actions on this turn it returns true, otherwise it returns false</returns>
        public bool Proceed() 
        {
            bool proceed = false;
            if (Village.NbSettlerAvailable("B") >= Math.Min(Math.Min(Hotel.BuilderNb, Restaurant.BuilderNb), SportsInfrastructure.BuilderNb))
            {
                proceed = true;
            }
            else
            {
                if (Village.CanRecruit())
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
            foreach ( InConstructionBuilding inConstruction in Village.InConstruction)
            {
                if (inConstruction.CreationTurn == TurnNb) 
                { 
                    if(inConstruction.BuildingType == "H")
                    {
                        Hotel hotel = new Hotel(inConstruction.X, inConstruction.Y);
                        Village.AddBuildings(hotel); 
                        Village.CreationBuilding(hotel);
                    }
                    else if(inConstruction.BuildingType == "R")
                    {
                        Restaurant restaurant = new Restaurant(inConstruction.X, inConstruction.Y);
                        Village.AddBuildings(restaurant);
                        Village.CreationBuilding(restaurant);
                    }
                    else
                    {
                        SportsInfrastructure sportsInfrastructurel = new SportsInfrastructure(inConstruction.X, inConstruction.Y, inConstruction.Name);
                        Village.AddBuildings(sportsInfrastructurel); 
                        Village.CreationBuilding(sportsInfrastructurel);
                        Village.SportsInfrastructures[inConstruction.Name] = true;
                    }
                    foreach (Settler builder in inConstruction.Settlers)
                    {
                        builder.IsInActivity = false; 
                        if (builder.EnergyState == 0 || builder.HungerState == 0)
                        {
                            builder.NaturalNeed(TurnNb);
                        }
                    }
                }
             
            }
        }

        /// <summary>
        /// Displays the game board
        /// </summary>
        public void DisplayGameBoard()
        {
            for (int i = 0; i < Village.Lenght; i++)
            {
                Console.Write("\n");
                for (int j = 0; j < Village.Width; j++)
                {
                    string info = " ";
                    ConsoleColor foreGroundColor = ConsoleColor.White;
                    ConsoleColor backGroundColor = ConsoleColor.Black;
                    if (Village.GameBoardBuilder[i, j] is null && Village.GameBoardSettler[i, j].Count == 0)
                    {
                        Console.Write(".\t");
                    }
                    //display in concstrution building 
                    else if (Village.GameBoardSettler[i, j].Count == 0 && (Village.GameBoardBuilder[i, j] == "XH" || Village.GameBoardBuilder[i, j] == "XR" || Village.GameBoardBuilder[i, j] == "XS"))
                    {
                        if (Village.GameBoardBuilder[i, j] == "XH")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("X\t");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (Village.GameBoardBuilder[i, j] == "XR")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("X\t");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (Village.GameBoardBuilder[i, j] == "XS")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("X\t");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else
                    {
                        //Display color management
                        if (Village.GameBoardBuilder[i, j] != null)
                        {
                            if (foreGroundColor == ConsoleColor.White)
                            {
                                foreGroundColor = ConsoleColor.Black;
                            }
                            else
                            {
                                foreGroundColor = ConsoleColor.Red;
                            }
                            if (Village.GameBoardBuilder[i, j].Equals("H"))
                            {
                                backGroundColor = ConsoleColor.Cyan;
                            }
                            else if (Village.GameBoardBuilder[i, j].Equals("R"))
                            {
                                backGroundColor = ConsoleColor.Magenta;
                            }
                            else if (Village.GameBoardBuilder[i, j].Equals("S"))
                            {
                                backGroundColor = ConsoleColor.Green;
                            }
                        }
                        Console.ForegroundColor = foreGroundColor;
                        Console.BackgroundColor = backGroundColor;
                        if (Village.GameBoardSettler[i, j] != null)
                        {
                            foreach(Settler settler in Village.GameBoardSettler[i, j]) { 
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
        ///Check if the space where you want to build a building is not already occupied or if it does not come out of the board
        /// </summary>
        /// <param name="type">type of building ("H" for a hotel, "R" for a restaurant and "S" for a sports infrastructure)</param>
        /// <param name="x">Abscissa of the position on the board of the top left corner of the building you want to build</param>
        /// <param name="y">Ordinate of the position on the board of the top left corner of the building you want to build</param>
        /// <returns></returns>
        public bool FreeSpaceBuilding(string type, int x, int y) 
        {
            int lines;
            int columns;
            lines = Building.GetLinesNb(type);
            columns = Building.GetColumnsNb(type);
            if (x + lines >= Village.GameBoardBuilder.GetLength(0) || y + columns >= Village.GameBoardBuilder.GetLength(1))
            {
                Console.WriteLine("Vous ne pouvez pas construire ici, vous sortirez du plateau de jeu");
                return false;
            }
            for (int i = x; i <= x + lines - 1; i++)
            {
                for (int j = y; j <= y + columns - 1; j++)
                {
                    if (Village.GameBoardBuilder[i, j] != null)
                    {
                        Error("Tu ne peux pas construire sur un batiment qui existe déjà ou qui est en cours de construction, choisi un autre emplacement!");
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Allows us to choose the type of building we want to build
        /// </summary>
        /// <returns>Returns true if the player has chosen to build a building, false if the player has finally changed his mind</returns>
        public bool ChoiceBuilding()
        {

            string instruction = "Entrez 0 pour revenir en arrière";
            bool creation = false;
            bool createHotel = false;
            bool createRestaurant = false;
            bool createSportsInfrastructure = false;
            if (Village.NbSettlerAvailable("B") >= Hotel.BuilderNb)
            {
                instruction += "\nEntrez 1 pour créer un Hotel";
                createHotel = true; 
            }
            if (Village.NbSettlerAvailable("B") >= Restaurant.BuilderNb)
            {
                instruction += "\nEntrez 2 pour créer un Restaurant";
                createRestaurant = true;
            }
            if (Village.NbSettlerAvailable("B") >= SportsInfrastructure.BuilderNb)
            {
                instruction += "\nEntrez 3 pour créer une Infrastructure Sportive";
                createSportsInfrastructure = true;
            }
            int create = VerifySyntax(instruction);

            if (create == 1 || create == 2 || create == 3)
            {
                if (create == 1)
                {
                    creation = CanCreateBuilding(createHotel,Hotel.type);
                }
                else if (create == 2)
                {
                    creation = CanCreateBuilding(createRestaurant, Restaurant.type);
                }
                else if (create == 3)
                {
                    creation = CanCreateBuilding(createSportsInfrastructure, SportsInfrastructure.type);
                }
            }
            else if (create == 0)
                creation = false;
            else
            {
                Console.WriteLine("Les données que vous avez entrées concernant le type de batiment que vous souhaitez construire ne sont pas valides. Veuillez saisir des informations valables \n");
                ChoiceBuilding();
            }
            return creation;
        }

        /// <summary>
        /// Create a building it's possible
        /// </summary>
        /// <param name="create">Is true if we can build the building</param>
        /// <param name="type">The building's type of our construction</param>
        /// <returns>True if the building can be create </returns>
        public bool CanCreateBuilding(bool create, string type)
        {
            string instruction = "Entrez la ligne de l'angle en haut à gauche de votre batiment";
            int x = VerifySyntax(instruction);
            instruction = "Entrez la colonne de l'angle en haut à gauche de votre batiment";
            int y = VerifySyntax(instruction);

            bool creation = false;
            if (create == true)
            {
                if (FreeSpaceBuilding(type, x, y))
                {
                    CreateBuilding(type, x, y);
                    creation = true;
                }
            }
            else
                Console.WriteLine("Cette réponse n'est pas valide car tu n'as pas assez de batisseur pour construire un hotel, choisi une réponse qui t'es proposé");
            return creation;
        }

        /// <summary>
        /// Allows you to create a building
        /// </summary>
        /// <param name="type">Type of building to be created ("R" for a restaurant, "H" for a hotel and "S" for a sports infrastructure)</param>
        /// <param name="x">Abscissa in the village of the position of the top left corner of the building to be created</param>
        /// <param name="y">Ordinate in the village of the position of the top left corner of the building to be created</param>
        public void CreateBuilding(string type, int x, int y)
        {
            int nbBuilders = 0;
            string name = "";
            int turnNb = 0;
            if (type == "H")
            {
                nbBuilders = Hotel.BuilderNb;
                turnNb = Hotel.TurnNb;
            }
            else if (type == "R")
            {
                nbBuilders = Restaurant.BuilderNb;
                turnNb = Restaurant.TurnNb;
            }
            else if (type == "S")
            {
                name = ChoiceSportsInfrastructure();
                nbBuilders = SportsInfrastructure.BuilderNb;
                turnNb = SportsInfrastructure.TurnNb;
            }
            List<Settler> settlers = BusyBulderList(nbBuilders);
            foreach (Settler settler in settlers)
            {
                settler.CalculatingItinerary(x, y);
            }
            InConstructionBuilding inConstruction = new InConstructionBuilding(type, turnNb + TurnNb, x, y, settlers, name);
            Village.InConstruction.Add(inConstruction);
            Village.CreatePendingBuilding(inConstruction);
        }



        /// <summary>
        /// Allows the player to choose the sports infrastructure he wishes to create
        /// </summary>
        /// <returns>returns true if he has selected a sports infrastructure to create, false if ultimately he does not want to create a sports infrastructure</returns>
        public string ChoiceSportsInfrastructure()
        {
            string instruction = "Choisissez l'infrastructure sportive que vous souhaitez construire\n Entrez 1 pour une piscine olympique \nEntrez 2 pour un terrain de sport collectif intérieur \nEntrez 3 pour un stade";
            int infrasctructure = VerifySyntax(instruction);
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
                Error("Votre réponse n'est pas valable. Veuillez entrer une réponse valide");
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
                nbAvailable = Village.FindAvailable("B").Count();
                Console.WriteLine(nbAvailable);
                busyBuilder.Add(Village.FindAvailable("B")[0]);
                Village.FindAvailable("B")[0].IsInActivity = true;
            }

            return busyBuilder;

        }

        /// <summary>
        /// Allows the player to choose the type of settler they wish to recruit
        /// </summary>
        /// <returns>returns true if he has selected a settler to recruit, false if he does not want to recruit a settler</returns>
        public bool ChoiceSettler()
        {
            string instruction = " Entrez 0 pour revenir en arrière\n Entrez 1 pour recruter un Batisseur\n Entrez 2 pour recruter un Coach";
            if (CanRecruitAthlete())
                instruction += "\nEntrez 3 pour recruter un Sportif";
            else
                Console.WriteLine("Vous ne pouvez pas recruter un sportif car vous n'avez aucune infrastructure sportive");
            int create = VerifySyntax(instruction);
            bool creation = true;
            if (create ==0)
                creation = false;
            else if (create == 1)
            {

                Builder builder = new Builder();
                Village.AddSettler(builder);
            }
            else if (create == 2)
            {
                Coach coach = new Coach();
                Village.AddSettler(coach);
                Console.WriteLine("Athletic.LevelIncrease : " + Athletic.LevelIncrease);
            }
            else if (create == 3)
            {
                bool createAthletic = ChoiceAthletics();
                if (createAthletic == false)
                    ChoiceSettler();
            }
            else
            {
                Error("Votre réponse n'est pas valide");
                ChoiceSettler();
            }
            return creation;
        }

        /// <summary>
        /// Allows the player to choose the athlete he wishes to recruit (his sport and his nationality)
        /// </summary>
        /// <returns>returns true if he has recruited an athlete, false if he does not want to recruit one</returns>
        private bool ChoiceAthletics()
        {
            bool createAthletics = true;

            string nationality2 = "";
            while (nationality2 == "")
            {
                string instruction = "Entrez 0 pour revenir en arrière\n Choisissez sa nationnalité \nEntrez 1 pour que le sportif soit Français \nEntrez 2 pour que le sportif soit Anglais\nEntrez 3 pour que le sportif soit Américain\nEntrez 4 pour que le sportif soit Japonais";//A en rajouter
                int nationality = VerifySyntax(instruction);

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
                {
                    Error("Votre réponse n'est pas valide, veuillez entrer un numéro valide");
                }
            }

            if (createAthletics == true)
            {
                string sport2 = "";
                bool swimingPool = false;
                bool field = false;
                bool stage = false;
                while (sport2 == "")
                {
                    string instruction = "Choisissez son sport :\n";
                    Village.SportsInfrastructures.TryGetValue("Piscine olympique", out bool value);
                    if (value == true)
                    {
                        instruction += "\nEntrez 1 pour de la natation ";
                        swimingPool = true;
                    }
                    else
                    {
                        Village.SportsInfrastructures.TryGetValue("Terrain de sport collectif intérieur", out value);
                        if (value == true)
                        {
                            instruction += "\nEntrez 2 pour du volley ";
                            instruction += "\nEntrez 3 pour du hand";
                            instruction += "\nEntrez 4 pour du basket";
                            field = true;
                        }
                        else
                        {
                            Village.SportsInfrastructures.TryGetValue("Stade", out value);
                            if (value  == true)
                            {
                                instruction += "\nEntrez 5 pour du football ";
                                instruction += "\nEntrez 6 pour du rugby";
                                instruction += "\nEntrez 7 pour de l'athlétisme";
                                stage = true;
                            }
                        }
                    }
                    
                    int sport = VerifySyntax(instruction);

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
                        Error("Votre réponse n'est pas valide, veuillez entrer un numéro valide");
                    }
                }

                    Athletic athletic = new Athletic(nationality2, sport2, Village);
                    Village.AddSettler(athletic);
                    Console.WriteLine("Vous avez recruté un nouveau sportif : ");
                    Console.WriteLine(athletic);
            }
            return createAthletics;
        }

        /// <summary>
        /// Allows you to know if you can rectify an athlete (i.e. if there is a sports infrastructure present on the game board)
        /// </summary>
        /// <returns>returns true if we have a sports infrastructure and therefore if we can recruit an athlete, false otherwise</returns>
        public bool CanRecruitAthlete()
        {
            bool canRecruitAthlete = false;
            foreach (Building building in Village.Buildings)
                if (building is SportsInfrastructure)
                    canRecruitAthlete = true;
            return canRecruitAthlete;
        }
    }
}