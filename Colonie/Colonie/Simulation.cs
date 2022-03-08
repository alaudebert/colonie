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
        private int _maxNbSettlers; //C'est déjà dans village, mais je pense qu'on devrait l'enlever et le mttre ici, à moins qu'ils ne représentent pas la même chose
        private List<Building> _buildings = new List<Building>;

        public Simulation()
        {
            Settler s1 = new Settler();
            Settler s2 = new Settler(); 
            Settler s3 = new Settler(); 
            Settler s4 = new Settler();
            _settlers = new List<Settler> { s1, s2, s3, s4 };
            _maxNbSettlers = 4;
            Sett
        }
    }
}
