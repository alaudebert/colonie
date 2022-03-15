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
        public Builder() : base() 
        {
            _builderNb++;
            _type = "batisseur_";
            _id = _type + _builderNb.ToString();
        }


        public override void Play()
        {
            _energyState -= 3;
            if (_energyState < 0)
                _energyState = 0;
            _hungerState -= 2;
            if (_hungerState < 0)
                _hungerState = 0;
        }
    }
}
