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
        public static int _turnNb = 1;
        public int _linesNb;
        public int _columnsNb;
        public SportsInfrastructure(int x, int y, string name) : base(x, y)
        {
            _sportsInfrastructureNb++;
            type = "S";
            _type = type;
            _id = _type + _sportsInfrastructureNb.ToString();
            _name = name;
        }

        public int LinesNb
        {
            get { return Building._buildingSize.FirstOrDefault(x => x.Key == "S").Value[0]; }
        }
        public int ColumnsNb
        {
            get { return Building._buildingSize.FirstOrDefault(x => x.Key == "S").Value[1]; }
        }


        public override string ToString()
        {
            return base.ToString() + "C'est une infrastructure sportive \n";
        }
    }
}
