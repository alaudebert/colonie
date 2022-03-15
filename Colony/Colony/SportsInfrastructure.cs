using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class SportsInfrastructure : Building
    {

        protected string _type = "infrastructure_sportive_";
        protected static int _sportsInfrastructureNb = 0;
        public SportsInfrastructure(int x, int y) : base(x, y)
        {
            _linesNb = 5;
            _columnsNb = 4;
            _builderNb = 6;
            _turnNb = 12;
            _sportsInfrastructureNb++;
            _id = _type + _sportsInfrastructureNb.ToString();
        }

        public override string ToString()
        {
            return base.ToString() + "C'est une infrastructure sportive \n";
        }
    }
}
