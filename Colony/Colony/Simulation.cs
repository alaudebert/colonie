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
        private static int _turn = 0;
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
            foreach(Building b in _village.Buildings)
                retour += b.ToString();
            if (_village.getSettlers().Count() != 0)
                retour += "Les colons existants sont les suivants : \n";
            foreach (Settler s in _village.getSettlers())
                retour += s.ToString();
            retour += "\n";
            return retour;
            //Prochaine etape : dessiner le plateau de jeu
        }

        public void Play()
        {
            Console.WriteLine("Vous êtes au tour {0} \n Vous pouvez : ", _turnNb);
            Console.WriteLine("Créer un Batiment (entrez 1)");
            string createSettler = _village.freePlaces()?"Créer un Colon (entrez 2)":"Créer un Colon (vous n'avez pas assez d'infrastructures pour accueillir de nouveaux colons)";
            Console.WriteLine(createSettler);
            int play = int.Parse(Console.ReadLine());
            switch (play)
            {
                case 1:
                    addBuilding();
                    break;
                case 2:
                    if (_village.freePlaces())
                    {
                        addSettler();
                    }
                    else
                    {
                        Console.WriteLine("vous n'avez pas assez d'infrastructures pour accueillir de nouveaux colons");
                        Play();
                    }
                    break;
                default:
                    Console.WriteLine("Votre réponse n'est pas valide");
                    Play();
                    break;
            }
            

        }

        public bool FreeSpace(Building building, int x, int y)  // Pas encore verifié si ça marche
        {
            for (int i = x; i <= x + building.X - 1; i++)
            {
                for (int j = y; j<= y + building.Y - 1; j++)
                {
                    if (_village.GameBoard[i, j] == "")
                        return false;
                }
            }
            return true;
        }

        public void addBuilding() 
        {
            Console.WriteLine("Entrez 1 pour créer un Hotel");
            Console.WriteLine("Entrez 2 pour créer un Restaurant");
            Console.WriteLine("Entrez 3 pour créer une Infrastructure Sportive");
            int create = int.Parse(Console.ReadLine());

            Console.WriteLine("Entrez la ligne de l'angle en haut à gauche de votre batiment");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Entrez la colonne de l'angle en haut à gauche de votre batiment");
            int y = int.Parse(Console.ReadLine());

            switch (create)
            {
                case 1:
                    Hotel hotel = new Hotel(x, y);
                    if (FreeSpace(hotel, x, y))//Trouver autre solution qui verifie que l'hotel c'est ok avant de le créér,
                                               //ou alors le supprimer après. Même chose pour Restau eu infrastructure sportive
                        _village.addBuildings(hotel);
                    break;
                case 2:
                    Restaurant restaurant = new Restaurant(x, y);
                    if (FreeSpace(restaurant, x, y))
                        _village.addBuildings(restaurant);
                    break;
                case 3:
                    SportsInfrastructure sportInfrastructure = new SportsInfrastructure(x, y);
                    if (FreeSpace(sportInfrastructure, x, y))
                        _village.addBuildings(sportInfrastructure);
                    break;
            }

        }
        public void addSettler() //Je l'ai mis en public temporairement pour les tests
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

            while (nationality2=="")
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

            while (sport2=="")
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
