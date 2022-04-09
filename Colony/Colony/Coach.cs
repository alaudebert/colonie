﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Coach : Settler
    {
        private static int _coachNb = 0;
        private static string _type;
        public Coach() : base()
        {
            _type = "C";
            SettlerType = _type;
            _coachNb++;
            _id = _type + _coachNb.ToString();
            _decreasingHunger = 5;
            _decreasingEnergy = 5;
        }

        public string Type
        {
            get { return _type; }
        }
    }
}
