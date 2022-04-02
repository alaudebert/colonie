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

        //Méthode qui nous permet de jouer
        public void Play()
        {
            bool end = false;
            while (!end && _turnNb < 100)
            {
                Console.WriteLine("turn :" +  _turnNb); //A supprimer
                PendingBuildingCreation();

                if (_village.CanRecruit() || _village.NbSettlerAvailable("B") >= Math.Min(Math.Min(Hotel._builderNb, Restaurant._builderNb), SportsInfrastructure._builderNb)) //Verifie qu'on peut effectuer une action sur ce tour, ou alors ça passe tout seul au tour suivant
                {


                    foreach (Building building in _village.Buildings)
                    {
                        _village.CreationBuilding(building);
                    }
                    Console.WriteLine("Vous êtes au tour {0} \n --------- ", _turnNb);
                    DisplayGameBoard();


                    //Debug
                    Console.WriteLine("nb colon : " + _village.NbSettlerAvailable("B"));
                    Console.WriteLine("nombre de constructeur necessaire :" + Math.Max(Math.Max(Hotel._builderNb, Restaurant._builderNb), SportsInfrastructure._builderNb));
                    Console.WriteLine("Colon dans le village : ");
                    foreach (Settler settler in _village.GetSettlers())
                    {
                        Console.WriteLine(settler.IsAvailable());
                    }

                    Console.WriteLine("nb place resto : " + _village.FreeRestaurantPlaces());
                    Console.WriteLine("nb place hotel : " + _village.FreeHotelPlaces());


                    Console.WriteLine("Entrez 0 pour passer au tour suivant sans effectuer aucune action");
                    if (_village.NbSettlerAvailable("B") >= Math.Min(Math.Min(Hotel._builderNb, Restaurant._builderNb), SportsInfrastructure._builderNb))
                    {
                        Console.WriteLine("Entrez 1 pour créer un batiment");
                    }
                    else
                    {
                        Console.WriteLine("Vous n'avez pas assez de Colon pour construire un batiment");
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
                    }


                    bool creation = true;
                    int create = int.Parse(Console.ReadLine());
                    switch (create)
                    {
                        case 0:
                            break;
                        case 1:
                            creation = CreateBuilding();
                            break;
                        case 2:
                            creation = AddSettler();
                            break;
                        default:
                            Console.WriteLine("Votre réponse n'est pas valide");
                            creation = false;
                            break;
                    }

                    Console.WriteLine(_village); //A supprimer

                    if (creation == true)
                        _turnNb++;
                }
                else
                {
                    _turnNb++;
                }


                    
            }

        }


        public bool Proceed() //Permet de savoir si des actions sont réalisable, si c'est pas le cas on passe au tour suivant
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


        //Crée les building qui sont en cours de création si on est bien à leur tour de création
        public void PendingBuildingCreation()
        {
            foreach ( InConstructionBuilding inConstruction in _village.InConstruction)
            {
                foreach (Settler settler in inConstruction.Settlers)
                {
                    _village.GameBoardSettler[settler.X, settler.Y] = null;
                    settler.Move();
                }
                Console.WriteLine("tour de construction"+ inConstruction.CreationTurn);
                Console.WriteLine("tout actuel"+_turnNb);
                if (inConstruction.CreationTurn == _turnNb) 
                { 
                    if(inConstruction.BuildingType == "H")
                    {
                        Hotel hotel = new Hotel(inConstruction.X, inConstruction.Y);
                        _village.addBuildings(hotel); 
                        _village.CreationBuilding(hotel);
                    }
                    else if(inConstruction.BuildingType == "R")
                    {
                        Restaurant restaurant = new Restaurant(inConstruction.X, inConstruction.Y);
                        _village.addBuildings(restaurant);
                        _village.CreationBuilding(restaurant);
                    }
                    else
                    {
                        SportsInfrastructure sportsInfrastructurel = new SportsInfrastructure(inConstruction.X, inConstruction.Y, inConstruction.Name);
                        _village.addBuildings(sportsInfrastructurel); 
                        _village.CreationBuilding(sportsInfrastructurel);
                    }
                    foreach (Settler builder in inConstruction.Settlers)
                    {
                        builder.Available = true;
                    }
                }
             
            }
        }

        //Affiche le plateau de jeu
        public void DisplayGameBoard()
        {
            foreach (Settler settler in _village.GetSettlers())
            {
                _village.GameBoardSettler[settler.X, settler.Y] = settler;
            }
            for (int i = 0; i < _village.Lenght; i++)
            {
                Console.Write("\n");
                for (int j = 0; j < _village.Width; j++)
                {
                    char info = ' ';
                    ConsoleColor foreGroundColor = ConsoleColor.White;
                    ConsoleColor backGroundColor = ConsoleColor.Black;
                    if (_village.GameBoardBuilder[i, j] is null && _village.GameBoardSettler[i, j] is null)
                    {
                        Console.Write('.');
                    }
                    else
                    {
                        if (_village.GameBoardSettler[i, j] != null)
                        {
                            foreGroundColor = _village.GameBoardSettler[i, j].Available ? ConsoleColor.White : ConsoleColor.Red;
                            if (_village.GameBoardSettler[i, j] is Builder)
                            {
                                info = 'B';
                            }
                            else if (_village.GameBoardSettler[i, j] is Athletic)
                            {
                                info = 'A';
                            }
                            else if (_village.GameBoardSettler[i, j] is Coach)
                            {
                                info = 'C';
                            }
                        }
                        if (_village.GameBoardBuilder[i, j] != null)
                        {
                            if(foreGroundColor == ConsoleColor.White)
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
                        Console.ForegroundColor = foreGroundColor;
                        Console.Write(info);
                        Console.ResetColor();
                    }
                }
            }
            Console.Write("\n");
        }

       

        public bool FreeSpaceBuilding(string building, int x, int y) //Verifie si l 'espace est pas déjà occupé ou si ca sort pas du plateau
        {
            int lines;
            int columns;
            if (building == Hotel.type)
            {
                lines = Hotel._linesNb;
                columns = Hotel._columnsNb;
            }
            else if (building == Restaurant.type)
            {
                lines = Restaurant._linesNb;
                columns = Restaurant._columnsNb;
            }
            else
            {
                lines = SportsInfrastructure._linesNb;
                columns = SportsInfrastructure._columnsNb;
            }
            if (x + lines >= _village.GameBoardBuilder.GetLength(0) || y + columns >= _village.GameBoardBuilder.GetLength(1))
            {
                Console.WriteLine("Vous ne pouvez pas construire ici, vous sortirez du plateau de jeu");
                return false;
            }
            for (int i = x; i <= x + lines - 1; i++)
            {
                for (int j = y; j <= y + columns - 1; j++)
                {
                    if (_village.GameBoardBuilder[i, j] != null)//O=Place  occupé par batiment, j'ai pas toruvé mieux, dans l'idéal juste colorier case
                    {
                        Console.WriteLine("Tu ne peux pas construire sur un batiment qui existe déjà, choisi un autre emplacement!");
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CreateBuilding()
        {
            Console.WriteLine("Entrez 0 pour revenir en arrière");
            bool creation = true;
            if (_village.NbSettlerAvailable("B") >= Hotel._builderNb)
            {
                Console.WriteLine("Entrez 1 pour créer un Hotel");
            }
            if (_village.NbSettlerAvailable("B") >= Restaurant._builderNb)
            {
                Console.WriteLine("Entrez 2 pour créer un Restaurant");
            }
            if (_village.NbSettlerAvailable("B") >= SportsInfrastructure._builderNb)//TODO les conditions pour construire une infrastructure ne sont pas les mêmes
            {
                Console.WriteLine(_village.NbSettlerAvailable("B") + " "+SportsInfrastructure._builderNb);
                Console.WriteLine("Entrez 3 pour créer une Infrastructure Sportive");
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
                    if (FreeSpaceBuilding(Hotel.type, x, y))//TODO faire une fonction pour empecher la recurrence de code
                    {
                        nbBuilders = Hotel._builderNb;
                        List<Settler> settlers = busyBulderList(nbBuilders); foreach (Settler settler in settlers)
                        {
                            settler.CalculatingItinerary(x, y);
                        }
                        InConstructionBuilding inConstruction = new InConstructionBuilding("H", Hotel._turnNb, x, y, settlers, "");
                        _village.InConstruction.Add(inConstruction);
                    }
                    else
                        creation = false;

                }
                else if (create == 2)
                {
                    if (FreeSpaceBuilding(Restaurant.type, x, y))
                    {
                        nbBuilders = Restaurant._builderNb;
                        List<Settler> settlers = busyBulderList(nbBuilders);
                        foreach (Settler settler in settlers)
                        {
                            settler.CalculatingItinerary(x, y);
                        }
                        _village.InConstruction.Add(new InConstructionBuilding("R", Restaurant._turnNb, x, y, settlers, ""));
                    }
                    else
                        creation = false;
                }
                else if (create == 3)
                {
                    if (FreeSpaceBuilding(SportsInfrastructure.type, x, y)) // TODO je pense a supprimer cette condition parce qu'une IS peut accueilr autant d'athlète qu'on veut
                    {
                        string sportsinfrasctructure = ChoiceSportsInfrastructure();
                        nbBuilders = SportsInfrastructure._builderNb;
                        List<Settler> settlers = busyBulderList(nbBuilders);
                        foreach (Settler settler in settlers)
                        {
                            settler.CalculatingItinerary(x, y);
                        }
                        _village.InConstruction.Add(new InConstructionBuilding("S", SportsInfrastructure._turnNb, x, y, settlers, sportsinfrasctructure));
                    }
                    else
                        creation = false;
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


        public string ChoiceSportsInfrastructure()
        {
            Console.WriteLine("Choisissez l'infrastructure sportive que vous souhaitez construire");
            Console.WriteLine("Entrez 1 pour une salle de musculation \nEntrez 2 pour une piste d'athlétisme \nEntrez 3 pour une piscine olympique \nEntrez 4 pour un terrain de tennis \nEntrez 5 pour un terrain de basket \nEntrez 6 pour un terrain de football \nEntrez 7 pour un terrain de volley");
            int infrasctructure = int.Parse(Console.ReadLine());
            string sportsinfrasctructure = "";
            if (infrasctructure == 1)
                sportsinfrasctructure = "Salle de musculation";
            else if (infrasctructure == 2)
            {
                sportsinfrasctructure = "Piste d'athlétisme";
            }
            else if (infrasctructure == 3)
            {
                sportsinfrasctructure = "Piscine olymptique";
            }
            else if (infrasctructure == 4)
            {
                sportsinfrasctructure = "Terrain de tennis";
            }
            else if (infrasctructure == 5)
            {
                sportsinfrasctructure = "Terrain de basket";
            }
            else if (infrasctructure == 6)
            {
                sportsinfrasctructure = "Terrain de football";
            }
            else if (infrasctructure == 7)
            {
                sportsinfrasctructure = "Terrain de volley";
            }
            else
            {
                Console.WriteLine("Votre réponse n'est pas valable. Veuillez entrer une réponse valide");
                ChoiceSportsInfrastructure();
            }
            Console.WriteLine("Infrastrucutre : " + sportsinfrasctructure);//A supprimer
            return sportsinfrasctructure; 
        }

        /// <summary>
        /// Available bulder assigment at the building
        /// </summary>
        /// <param name="nbBuilders">The number of builder assigments</param>
        /// <returns>A list of builder assigment's</returns>
        public List<Settler> busyBulderList(int nbBuilders)
        {
            List<Settler> busyBuilder = new List<Settler>();
            int nbAvailable = 0;
            for (int i = 0; i < nbBuilders; i++)
            {
                nbAvailable = _village.FindAvailable("B").Count();
                Console.WriteLine(nbAvailable);
                busyBuilder.Add(_village.FindAvailable("B")[0]);
                _village.FindAvailable("B")[0].Available = false;
            }

            return busyBuilder;

        }


        public bool AddSettler() //Je l'ai mis en public temporairement pour les tests
                                 //Faut ajouter que quand on crée bonhomme ça remplie son emplacement dans le plateau
                                 //Jsp si le mieux c'est de remplir de tableau, et modifier à chaque fois que le personnage bouge, ou si c'est dans
                                 //l'affichage qu'on cherche les position x et y  de chaque sellter (risquer car peut sortir du plateau)
        {
            Console.WriteLine("Entrez 0 pour revenir en arrière");
            Console.WriteLine("Entrez 1 pour recruter un Batisseur");
            Console.WriteLine("Entrez 2 pour recruter un Sportif");
            Console.WriteLine("Entrez 3 pour recruter un Coach");
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
                bool createAthletic = CreateAthletics();
                if (createAthletic == false)
                    AddSettler();
            }
            else if (create == 3)
            {
                Coach coach = new Coach();
                _village.AddSettler(coach);
                Console.WriteLine("Vous avez recruté un nouveau coach : ");
                Console.WriteLine(coach);
                Athletic.LevelIncrease++;
                Console.WriteLine("Athletic.LevelIncrease : " + Athletic.LevelIncrease);
            }
            else
            {
                Console.WriteLine("Votre réponse n'est pas valide");
                AddSettler();
            }
            return creation;
        }


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

                while (sport2 == "")
                {
                    Console.WriteLine("Choisissez son sport : \nEntrez 1 pour du football \nEntrez 2 pour du volley \nEntrez 3 pour du tennis \nEntrez 4 pour du basketball \nEntrez 5 pour de la natation \nEntrez 6 pour de l'athlétisme \nEntrez 7 pour du crossfit");//A en rajouter 
                    int sport = int.Parse(Console.ReadLine());

                    switch (sport)
                    {
                        case 1:
                            sport2 = "Football";
                            infrastructure = "Terrain de football";
                            break;
                        case 2:
                            sport2 = "Volley";
                            infrastructure = "Terrain de volley";
                            break;
                        case 3:
                            sport2 = "Tennis";
                            infrastructure = "Terrain de tennis";
                            break;
                        case 4:
                            sport2 = "Basketball";
                            infrastructure = "Terrain de basket";
                            break;
                        case 5:
                            sport2 = "Natation";
                            infrastructure = "Piscine olymptique";
                            break;
                        case 6:
                            sport2 = "Athlétisme";
                            infrastructure = "Piste d'athlétisme";
                            break;
                        case 7:
                            sport2 = "Crossfit";
                            infrastructure = "Salle de musculation";
                            break;
                        default:
                            Console.WriteLine("Votre réponse n'est pas valide, veuillez entrer un numéro valide");
                            break;
                    }

                    Console.WriteLine("Sport 2 : " + sport2 + " Infrastructure : " + infrastructure); //A supprimer
                }

                bool construction = false;
                foreach (Building building in _village.Buildings)
                {
                    Console.WriteLine("On rentre dans la boucle, le nom de mon building est : " + building.Name);
                    if (building is SportsInfrastructure)
                    {
                        Console.WriteLine("On rentre dans la  2eme boucle, le nom de mon building est : " + building.Name);
                        Console.WriteLine(building.Name); //Pour tester les noms de mes infrastructures créée voir si le pblm est la
                        if (building.Name == infrastructure)
                            construction = true;
                    }
                }

                if (construction == true)
                {
                    Athletic athletic = new Athletic(nationality2, sport2);
                    _village.AddSettler(athletic);
                    Console.WriteLine("Vous avez recruté un nouveau sportif : ");
                    Console.WriteLine(athletic);
                }
                else
                {
                    Console.WriteLine("Vous ne pouvez pas recruter ce sportif car vous n'avez pas l'infrastructure spotive adapté à sa discipline");
                    CreateAthletics();
                }
            }

            return createAthletics;

            
        }
    }
}