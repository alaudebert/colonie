using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    abstract class Settler
    {
        private static int _settlersNb = 0;
        private int _id;
        protected int _energyState;
        protected int _hungerState;
        private int _x, _y;
        private bool _available;

        //FINALEMENT je suis plutot chaud de mettre un id aux athlètes, aux coachs et sportifs mais pas comme ca,
        //en mode s'il y a 4 athlètes ils sont numérotés de 1 à 4 et donc on prends pas en compte les autres Settlers
        //dans les numérotations

        public Settler()
        {
            _settlersNb++;
            _id = _settlersNb;
            _energyState = 15;
            _hungerState = 10;
            _x = 0;
            _y = 0;
            _available = true;
        }

        public void Play()
        {
            _energyState--;
            _hungerState--;
        }

        public override string ToString()
        {
            return "Colon n° : " + _id + "\nNiveau d'énergie : " + _energyState + "\nNiveau de faim : " + _hungerState
                + "\nCoodronnées : " + _x + " , " + _y + "\nDisponibilité : " + _available + "\n";
        }

    }
}
