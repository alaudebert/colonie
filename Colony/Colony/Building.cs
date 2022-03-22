using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    abstract class Building
    {
        protected int _turnNb;  
        protected int _builderNb;
        public static int _linesNb;
        public static int _columnsNb;
        protected int _x, _y;
        protected string _id;
        public static string Type; 

        public string Id
        {
            get { return _id; }
        }

        public int LinesNb
        {
            get { return _linesNb; }
        }

        public int ColumnsNb
        {
            get { return _columnsNb; }
        }

        public Building(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X
        {
            get { return _x; }
        }


        public int Y
        {
            get { return _y; }
        }

        public override string ToString()
        {
            return "C'est le batiment : " + _builderNb + "\nIl sera réalisé en : "
                + _turnNb + " tours \nIl a comme dimensions : " + LinesNb + " lignes et "
                + ColumnsNb + " colonnes \nIl a comme coodronnées : " + _x + " , " + _y + "\n";
        }
    }
}
