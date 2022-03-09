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
        private string[,] _gameBoard;
        //J'aurai donné un nom aussi non?

        public Village(int maxNbSettlers, string[,] gameBoard)
        {
            _maxNbSettlers = maxNbSettlers;
            _gameBoard = gameBoard;//Jsp si on le def par defaut au debut ou si on le def nous en jouant 
        }

        public override string ToString()
        {
            return "Le village peut contenir jusqu'à : " + _maxNbSettlers + "\nEt le plateau de jeu contient : " + _gameBoard + "\n";
        }
    }
}
