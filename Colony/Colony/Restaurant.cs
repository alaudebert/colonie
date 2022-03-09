using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Restaurant : ReceptionBuilding
    {
        public Restaurant(int x, int y) : base(x, y)
        {
            _totalPlace = 10;
            _nbBuilder = 2;
            _nbTurn = 2;
            _nbLines = 4;
            _nbColonnes = 3;
        }

        public override string ToString()
        {
            return base.ToString() + "C'est un restaurant\n";
        }
    }
}
