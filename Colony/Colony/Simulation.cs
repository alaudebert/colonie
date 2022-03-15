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

        /*
        public override string ToString()
        {

            string retour = "\nMA SIMULATION : \nMon village est le suivant : \n" + _village.ToString();
                retour += "\nIl y a : " + _buildings.Count() + " batiments\n";
            if (_buildings.Count() != 0)
                retour += "Les batiments existants sont les suivants : \n";
            foreach(Building b in _buildings)
                retour += b.ToString();
            if (_village.getSettlers().Count() != 0)
                retour += "Les colons existants sont les suivants : \n";
            foreach (Settler s in _village.getSettlers())
                retour += s.ToString();
            retour += "\n";
            return retour;
        }*/

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

        public void addBuilding() {
            Console.WriteLine("Entrez la ligne de l'angle en haut à gauche de votre batiment");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Entrez la colonne de l'angle en haut à gauche de votre batiment");
            int y = int.Parse(Console.ReadLine());
            Console.WriteLine("Entrez 1 pour créer un Hotel");
            Console.WriteLine("Entrez 2 pour créer un Restaurant");
            Console.WriteLine("Entrez 3 pour créer une Infrastructure Sportive");
            int create = int.Parse(Console.ReadLine());
            switch (create)
            {
                case 1:
                    Hotel hotel = new Hotel(x,y);
                    _village.addBuildings(hotel);
                    break;
                case 2:
                    Restaurant restaurant = new Restaurant(x, y);
                    _village.addBuildings(restaurant);
                    break;
                case 3:
                    SportsInfrastructure sportInfrastructure = new SportsInfrastructure(x, y);
                    _village.addBuildings(sportInfrastructure);
                    break;
            }

        }
        private void addSettler() {
            Console.WriteLine("Entrez 1 pour créer un Batisseur");
            Console.WriteLine("Entrez 2 pour créer un Sportif");
            Console.WriteLine("Entrez 3 pour créer un Coach");
            int create = int.Parse(Console.ReadLine());
            switch (create)
            {
                case 1:
                    Builder builder = new Builder();
                    _village.addSettler(builder);
                    break;
                case 2:
                    createAthletics();
                    break;
                case 3:
                    Coach coach = new Coach();
                    _village.addSettler(coach);
                    break;
            }
        }

        private void createAthletics()
        {
            Console.WriteLine("Entrez votre nationnalité");
            string nationality = Console.ReadLine();
            Athletic athletic = new Athletic("Française", "natation"); //TO DO choisir le sport
            _village.addSettler(athletic);
        }
    }
}
