using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class ReceptionBuilding : Building //Inutile je crois Alex
    {
        protected List<Settler> settlers; //Inutile je crois Alex
        protected int _totalPlace; //Inutile je crois Alex

        public ReceptionBuilding(int x, int y) : base(x, y) //Inutile je crois Alex
        {
            _x = x;
            _y = y;
        }
    }
}
