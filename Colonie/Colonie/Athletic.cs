using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Athletic : Settler
    {
        private int _level;
        private string _sport;
        private string _nationality;

        public Athletic(string nationality) : base()
        {
            _nationality = nationality;
        }


        public override void Play()
        {
            
        }
    }
}
