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


        public override string ToString()
        {
            return base.ToString() + _id + "\n";
        }
    }
}
