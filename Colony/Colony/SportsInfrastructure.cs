﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class SportsInfrastructure : Building
    {
        protected static int _sportsInfrastructureNb = 0;
        public static int _builderNb = 6; 
        public static int _turnNb = 3;
        public SportsInfrastructure(int x, int y) : base(x, y)
        {
            _linesNb = 5;
            _columnsNb = 4;
            _sportsInfrastructureNb++;
            _type = "S";
            type = _type;
            _id = _type + _sportsInfrastructureNb.ToString();
        }

        public override string ToString()
        {
            return base.ToString() + "C'est une infrastructure sportive \n";
        }
    }
}
