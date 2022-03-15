using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Village
    {
        private int _maxNbSettlers;
        private string[,] _gameBoard = new string[20, 40];

        public Village(int maxNbSettlers)
        {
            _maxNbSettlers = maxNbSettlers;
        }

        public override string ToString()
        {
            return "Le village peut contenir jusqu'à : " + _maxNbSettlers + " colons \nEt le plateau de jeu contient : " + _gameBoard.GetLength(0) + " lignes et " + _gameBoard.GetLength(1) + " colonnes \n";
        }
    }
}
