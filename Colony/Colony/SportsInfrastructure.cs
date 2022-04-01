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
        public static int _builderNb = 2; 
        public static int _turnNb = 3;
        protected string _name;
        public SportsInfrastructure(int x, int y, string name) : base(x, y)
        {
            _linesNb = 5;
            _columnsNb = 4;
            _sportsInfrastructureNb++;
            type = "S";
            _type = type;
            _id = _type + _sportsInfrastructureNb.ToString();
        }

        public string Name
        {
            get { return _name; }
        }

        public override string ToString()
        {
            return base.ToString() + "C'est une infrastructure sportive \n";
        }
    }
}
