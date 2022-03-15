using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Simulation
    {
        private List<Settler> _settlers; 
        private List<Building> _buildings = new List<Building> { };
        private Village _village;

        public Simulation()
        {
            Builder s1 = new Builder();
            Builder s2 = new Builder();
            Builder s3 = new Builder();
            Builder s4 = new Builder();
            _settlers = new List<Settler> { s1, s2, s3, s4 };
            _village = new Village(4);
        }

        public override string ToString()
        {

            string retour = "\nMA SIMULATION : \nMon village est le suivant : \n" + _village.ToString();
                retour += "\nIl y a : " + _buildings.Count() + " batiments\n";
            if (_buildings.Count() != 0)
                retour += "Les batiments existants sont les suivants : \n";
            foreach(Building b in _buildings)
                retour += b.ToString();
            if (_settlers.Count() != 0)
                retour += "Les colons existants sont les suivants : \n";
            foreach (Settler s in _settlers)
                retour += s.ToString();
            retour += "\n";
            return retour;
        }
    }
}
