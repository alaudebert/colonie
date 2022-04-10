using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Builder : Settler
    {
        private static int _builderNb;

        /// <summary>
        /// Builder that allows you to create a builder
        /// </summary>
        public Builder() : base() 
        {
            _builderNb++;
            _type = "B";
            _id = _type + _builderNb.ToString();
            _decreasingEnergy = 15;
            _decreasingHunger = 4;
        }

    }
}
