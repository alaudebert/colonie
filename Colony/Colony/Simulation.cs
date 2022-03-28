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
            if (_village.getSettlers().Count() != 0)
                retour += "Les colons existants sont les suivants : \n";
            foreach (Settler s in _village.getSettlers())
                retour += s.ToString();
            retour += "\n";

            retour += "-------------Plateau de jeu-------------";

            for (int i = 0; i < _village.GameBoard.GetLength(0); i++)
            {
                retour += "\n";
                for (int j = 0; j < _village.GameBoard.GetLength(1); j++)
                {
                    if (_village.GameBoard[i, j] is null)
                    {
                        retour += "__";
                    }
                    else
                        retour += _village.GameBoard[i, j];
                }
            }
            retour += "\n";
            return retour;
        }


        public void Play()
        {
            bool end = false;
            foreach (Building building in _village.Buildings)
            {
                _village.LocationOccupiedBuilding(building);
                end = true;
            }
            while (!end || _turnNb < 20)
            {
                PendingBuildingCreation();
                Console.WriteLine("Vous êtes au tour {0} \n --------- ", _turnNb);
                //DisplayGameBoard();

                //Debug
                Console.WriteLine("nb colon : " + _village.NbSettlerAvailable("B"));
                Console.WriteLine("nombre de constructeur necessaire :" + Math.Max(Math.Max(Hotel._builderNb, Restaurant._builderNb), SportsInfrastructure._builderNb));
                Console.WriteLine("Colon dans le village : "); 
                foreach (Settler settler in _village.getSettlers())
                {
                    Console.WriteLine(settler.isAvailable());
                }

                Console.WriteLine("nb place resto : " + _village.freeRestaurantPlaces());
                Console.WriteLine("nb place hotel : " + _village.freeHotelPlaces());

                //fin debug

                if (_village.NbSettlerAvailable("B") >= Math.Min(Math.Min(Hotel._builderNb, Restaurant._builderNb), SportsInfrastructure._builderNb))
                {
                    Console.WriteLine("Entrez 1 pour créer un batiment");
                }
                else { 
                    Console.WriteLine("Vous n'avez pas assez de Colon pour construire un batiment");
                }
                if (_village.canRecruit())
                {
                    Console.WriteLine("Entrez 2 pour recruter un colon");
                }
                else
                {
                    Console.WriteLine("Vous n'avez pas assez d'infrastructure pour accueillir des colons, ");
                    if (!_village.freeHotelPlaces()) 
                    {
                        Console.WriteLine("Il vous faut un hotel");
                    }
                    if (!_village.freeRestaurantPlaces()) 
                    {
                        Console.WriteLine("Il vous faut un restaurant");
                    }
                }

                int create = int.Parse(Console.ReadLine());
                switch (create)
                {
                    case 1:
                        createBuilding();
                        break;
                    case 2:
                        addSettler();
                        break;
                    default:
                        Console.WriteLine("Votre réponse n'est pas valide");
                        break;
                }
                _turnNb++;
            }

        }

        public void PendingBuildingCreation()
        {
            foreach(Tuple<string, int, int, int, List<Settler>> building in _village.InConstruction)
            {
                
                if (building.Item2 == _turnNb) 
                { 
                    if(building.Item1 == "H")
                    {
                        Hotel hotel = new Hotel(building.Item3, building.Item4);
                        _village.addBuildings(hotel); 
                        _village.LocationOccupiedBuilding(hotel);
                    }
                    else if(building.Item1 == "R")
                    {
                        Restaurant restaurant = new Restaurant(building.Item3, building.Item4);
                        _village.addBuildings(restaurant); 
                        _village.LocationOccupiedBuilding(restaurant);
                    }
                    else
                    {
                        SportsInfrastructure sportsInfrastructurel = new SportsInfrastructure(building.Item3, building.Item4);
                        _village.addBuildings(sportsInfrastructurel); 
                        _village.LocationOccupiedBuilding(sportsInfrastructurel);
                    }
                    foreach (Settler builder in building.Item5)
                    {
                        builder.Available = true;
                    }
                }
            }
        }

        public void DisplayGameBoard()
        {
            string retour = "";
            for (int i = 0; i < _village.GameBoard.GetLength(0); i++)
            {
                retour += "\n";
                for (int j = 0; j < _village.GameBoard.GetLength(1); j++)
                {
                    if (_village.GameBoard[i, j] is null)
                        retour += "__";
                    else
                        retour += _village.GameBoard[i, j];
                }
            }
            retour += "\n";
            Console.WriteLine(retour);
        }

        public bool FreeSpaceBuilding(string building, int x, int y) //Verifie si l 'espace est pas déjà occupé ou si ca sort pas du plateau
                                                                     // Ca a l'aire de marcher
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
            if (x + lines >= _village.GameBoard.GetLength(0) || y + columns >= _village.GameBoard.GetLength(1))
            {
                Console.WriteLine("Vous ne pouvez pas construire ici, vous sortirez du plateau de jeu");
                return false;
            }
            for (int i = x; i <= x + lines - 1; i++)
            {
                for (int j = y; j <= y + columns - 1; j++)
                {
                    if (_village.GameBoard[i, j] != null)//O=Place  occupé par batiment, j'ai pas toruvé mieux, dans l'idéal juste colorier case
                    {
                        Console.WriteLine("Tu ne peux pas construire sur un batiment qui existe déjà, choisi un autre emplacement!");
                        return false;
                    }
                }
            }
            return true;
        }

        public void createBuilding()
        {
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

            Console.WriteLine("Entrez la ligne de l'angle en haut à gauche de votre batiment");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Entrez la colonne de l'angle en haut à gauche de votre batiment");
            int y = int.Parse(Console.ReadLine());
            int nbBuilders = 0;
            if (create == 1 && FreeSpaceBuilding(Hotel.type, x, y))
            {
                nbBuilders = Hotel._builderNb;
                _village.InConstruction.Add(new Tuple<string, int, int, int, List<Settler>>(Hotel.type, _turnNb + Hotel._turnNb, x, y, busyBulderList(nbBuilders)));
                
            }
            else if (create == 2 && FreeSpaceBuilding(Restaurant.type, x, y))
            {
                nbBuilders = Restaurant._builderNb;
                _village.InConstruction.Add(new Tuple<string, int, int, int, List<Settler>>(Restaurant.type, _turnNb + Restaurant._turnNb, x, y, busyBulderList(nbBuilders)));
            }
            else if (create == 3 && FreeSpaceBuilding(SportsInfrastructure.type, x, y))
            {
                nbBuilders = SportsInfrastructure._builderNb;
                _village.InConstruction.Add(new Tuple<string, int, int, int, List<Settler>>(SportsInfrastructure.type, _turnNb + SportsInfrastructure._turnNb, x, y, busyBulderList(nbBuilders)));
            }
        }

        /// <summary>
        /// Available bulder assigment at the building
        /// </summary>
        /// <param name="nbBuilders">The number of builder assigments</param>
        /// <returns>A list of builder assigment's</returns>
        public List<Settler> busyBulderList(int nbBuilders)
        {
            List<Settler> busyBuilder = new List<Settler>();
            for (int i = 0; i < nbBuilders; i++)
            {
                Console.WriteLine(_village.findAvailable("B").Count());
                busyBuilder.Add(_village.findAvailable("B")[0]);
                _village.findAvailable("B")[0].Available = false;
            }

            return busyBuilder;

        }


        public void addSettler() //Je l'ai mis en public temporairement pour les tests
                                 //Faut ajouter que quand on crée bonhomme ça remplie son emplacement dans le plateau
                                 //Jsp si le mieux c'est de remplir de tableau, et modifier à chaque fois que le personnage bouge, ou si c'est dans
                                 //l'affichage qu'on cherche les position x et y  de chaque sellter (risquer car peut sortir du plateau)
        {
            Console.WriteLine("Entrez 1 pour recruter un Batisseur");
            Console.WriteLine("Entrez 2 pour recruter un Sportif");
            Console.WriteLine("Entrez 3 pour recruter un Coach");
            int create = int.Parse(Console.ReadLine());
            switch (create)
            {
                case 1:
                    Builder builder = new Builder();
                    _village.addSettler(builder);
                    Console.WriteLine("Vous avez recruté un nouveau batisseur : ");
                    Console.WriteLine(builder);
                    break;
                case 2:
                    createAthletics();
                    break;
                case 3:
                    Coach coach = new Coach();
                    _village.addSettler(coach);
                    Console.WriteLine("Vous avez recruté un nouveau coach : ");
                    Console.WriteLine(coach);
                    break;
                default:
                    Console.WriteLine("Votre réponse n'est pas valide");
                    addSettler();
                    break;
            }
        }


        private void createAthletics()
        {
            string nationality2 = "";

            while (nationality2 == "")
            {
                Console.WriteLine("Choisissez sa nationnalité \nEntrez 1 pour que le sportif soit Français \nEntrez 1 pour que le sportif soit Anglais\nEntrez 1 pour que le sportif soit Américain\nEntrez 1 pour que le sportif soit Japonais");//A en rajouter
                int nationality = int.Parse(Console.ReadLine());

                switch (nationality)
                {
                    case 1:
                        nationality2 = "Francais";
                        break;
                    case 2:
                        nationality2 = "Anglais";
                        break;
                    case 3:
                        nationality2 = "Americain";
                        break;
                    case 4:
                        nationality2 = "Japonais";
                        break;
                    default:
                        Console.WriteLine("Votre réponse n'est pas valide, veuillez entrer un numéro valide");
                        break;
                }
            }

            string sport2 = "";

            while (sport2 == "")
            {
                Console.WriteLine("Choisissez son sport : \nEntrez 1 pour du football \nEntrez 2 pour du volley \nEntrez 3 pour du tennis \nEntrez 4 pour du basketball \nEntrez 5 pour de la natation \nEntrez 6 pour de l'athlétisme \nEntrez 7 pour du crossfit");//A en rajouter 
                int sport = int.Parse(Console.ReadLine());

                switch (sport)
                {
                    case 1:
                        sport2 = "Football";
                        break;
                    case 2:
                        sport2 = "Volley";
                        break;
                    case 3:
                        sport2 = "Tennis";
                        break;
                    case 4:
                        sport2 = "Basketball";
                        break;
                    case 5:
                        sport2 = "Natation";
                        break;
                    case 6:
                        sport2 = "Athlétisme";
                        break;
                    case 7:
                        sport2 = "Crossfit";
                        break;
                    default:
                        Console.WriteLine("Votre réponse n'est pas valide, veuillez entrer un numéro valide");
                        break;
                }
            }

            Athletic athletic = new Athletic(nationality2, sport2);
            _village.addSettler(athletic);
            Console.WriteLine("Vous avez recruté un nouveau sportif : ");
            Console.WriteLine(athletic);
        }
    }
}