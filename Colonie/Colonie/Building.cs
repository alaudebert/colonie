using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colonie
{
    abstract class Building
    {
        protected int _nbTurn;
        protected int _nbBuilder;
        protected int _nbLines;
        protected int _nbColonnes;
        protected int _x, _y;

        public Building(int x,int y)
        {
            _x = x;
            _y = y;
        }

        public void Build() {
            
        }
    }
}
