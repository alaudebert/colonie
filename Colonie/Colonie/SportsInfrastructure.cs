using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class SportsInfrastructure : Building
    {
        SportsInfrastructure(int x, int y): base(x, y)
        {
            _x = x;
            _y = y;
            _nbLines = 5;
            _nbColonnes = 4;
            _nbBuilder = 6;
            _nbTurn = 12;
        }
    }
}
