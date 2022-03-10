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
        private int _builderId;
        public Builder() : base() 
        {
            _builderNb++;
            _builderId = _builderNb;
        }


        public override string ToString()
        {
            return base.ToString() + "C'est le Batisseur n° : " + _builderId + "\n";
        }
    }
}
