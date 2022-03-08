using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colonie
{
    class ReceptionBuilding : Building
    {
        //liste de colon dans le building
        //protected List<Settlers> settlers;
        protected int _totalPlace;
        protected int _x, _y;

        public ReceptionBuilding(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}
