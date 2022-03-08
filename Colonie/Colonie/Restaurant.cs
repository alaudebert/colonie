using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colonie
{
    class Restaurant : ReceptionBuilding
    {
        public Restaurant(int x, int y):base(x,y)
        {
            _totalPlace = 10; 
            _x = x;
            _y = y;
            _nbBuilder = 2;
            _nbTurn = 2;
            _nbLines = 4;
            _nbColonnes = 3;
        }
    }
}
