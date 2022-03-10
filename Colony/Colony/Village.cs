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

        public Village(int maxNbSettlers, string[,] gameBoard)//Je pense que c'est pas à nous d'entree au debut 4
                                                              //joueurs mais que c'est automatique lors de la ceation
                                                              //d'une simulation (pq pas ne pas avoir besoin de créer
                                                              //de village mais il y a création automatique lors de la
                                                              //simulation)
        {
            _maxNbSettlers = maxNbSettlers;
            _gameBoard = gameBoard;//Jsp si on le def par defaut au debut ou si on le def nous en jouant 
        }

        public override string ToString()
        {
            return "Le village peut contenir jusqu'à : " + _maxNbSettlers + " colons \nEt le plateau de jeu contient : " + _gameBoard.GetLength(0) + " lignes et " + _gameBoard.GetLength(1) + " colonnes \n";
        }
    }
}
