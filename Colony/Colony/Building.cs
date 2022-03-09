using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    abstract class Building
    {
        protected int _nbTurn;  //Je pense quil faut plutot mettre _turnNb
        protected int _nbBuilder;  //_builderNb etc..
        protected int _nbLines;  //it's not in english
        protected int _nbColonnes;
        protected int _x, _y;

        //Il faudrait mettre un name, id, number ou quo aux batiments pour les identifier non?
        //parce que les restaurants par exemple leur  c'est 2, mais commnt on l es differencie entre eux? 



        public Building(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void Build()
        {

        }

        public override string ToString()
        {
            return "C'est le batiment : " + _nbBuilder + "\nIl sera réalisé en : "
                + _nbTurn + " tours \nIl a comme dimensions : " + _nbLines + " lignes et "
                + _nbColonnes + " colonnes \nIl a comme coodronnées : " + _x + " , " + _y + "\n";
        }
    }
}
