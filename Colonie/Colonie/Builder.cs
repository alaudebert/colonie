using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Builder : Settler
    {
        public Builder() : base() { }


        public override string ToString()
        {
            return base.ToString() + "C'est un Batisseur \n";
        }
    }
}
