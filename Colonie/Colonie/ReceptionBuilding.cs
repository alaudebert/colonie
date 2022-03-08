using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class ReceptionBuilding : Building
    {
        //liste de colon dans le building
        //protected List<Settlers> settlers;
        protected int _totalPlace;

        public ReceptionBuilding(int x, int y): base(x,y)
        {
            _x = x;
            _y = y;
        }
    }
}
