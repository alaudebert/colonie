using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Program
    {
        static void Main(string[] args)
        {

            Athletic a1 = new Athletic("Belge", "Golfeur");
            Console.WriteLine(a1);

            Builder b1 = new Builder();
            Console.WriteLine(b1);

            Coach c1 = new Coach();
            Console.WriteLine(c1);

            SportsInfrastructure sp1 = new SportsInfrastructure(7,8);
            Console.WriteLine(sp1);
            //Il y a un message d'erreur chelou

            Restaurant r1 = new Restaurant(2, 2);
            Console.WriteLine(r1);

            Hotel h1 = new Hotel(12, 13);
            Console.WriteLine(h1);

            string[,] plateau = new string[20, 30];
            Village v1 = new Village(4, plateau);
            Console.WriteLine(v1);

            Simulation s1 = new Simulation();
            Console.WriteLine(s1);


            Console.ReadLine();

        }
    }
}
