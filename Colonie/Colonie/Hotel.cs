using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Hotel : ReceptionBuilding
    {
        public Hotel(int x, int y):base(x, y)
        {
            _x = x;
            _y = y;
            _nbBuilder = 2;
            _nbTurn = 2;
            _nbBuilder = 3;
            _nbColonnes = 3;
            _totalPlace = 10;
        }


    }
}
