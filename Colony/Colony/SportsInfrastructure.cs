using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class SportsInfrastructure : Building
    {
        protected static int _sportsInfrastructureNb = 0;
        public SportsInfrastructure(int x, int y) : base(x, y)
        {
            _linesNb = 5;
            _columnsNb = 4;
            _builderNb = 6;
            _turnNb = 12;
            _sportsInfrastructureNb++;
            Type = "S";
            _id = Type + _sportsInfrastructureNb.ToString();
        }

        public override string ToString()
        {
            return base.ToString() + "C'est une infrastructure sportive \n";
        }
    }
}
