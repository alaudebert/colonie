﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Builder : Settler
    {
        private static int _builderNb;
        private string _type;
        public Builder() : base() 
        {
            _builderNb++;
            _type = "B";
            SettlerType = _type;
            _id = _type + _builderNb.ToString();
            _decreasingEnergy = 1;
            _decreasingHunger = 1;
        }
        public string Type
        {
            get { return _type; }
        }
        }
}
