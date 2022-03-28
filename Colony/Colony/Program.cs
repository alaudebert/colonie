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
            /*Village v = new Village();
            
            Athletic a1 = new Athletic("Belge", "Golfeur");
                

            Builder b1 = new Builder();
            Console.WriteLine(b1);

            Coach c1 = new Coach();
            Console.WriteLine(c1);

            SportsInfrastructure sp1 = new SportsInfrastructure(7,8);
            Console.WriteLine(sp1);

            Restaurant r1 = new Restaurant(2, 2);
            Console.WriteLine(r1);

            Hotel h1 = new Hotel(12, 13);
            Console.WriteLine(h1);

            string[,] plateau = new string[20, 30];
            Village v1 = new Village();
            Console.WriteLine(v1);

            a1.Play();
            Console.WriteLine(a1);

            a1.Play();
            Console.WriteLine(a1);

            a1.Play();
            Console.WriteLine(a1);

            b1.Play();
            Console.WriteLine(b1);

            c1.Play();
            Console.WriteLine(c1);

            //s1.AddSettler();
            */

            Simulation s1 = new Simulation();
            s1.Play();


        }
    }
}
