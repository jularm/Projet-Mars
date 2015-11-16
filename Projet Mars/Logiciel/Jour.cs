using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logiciel
{
    class Jour
    {
        private int _num;
        private bool _sortie;

        public Jour(int num, bool sortie)
        {
            _num = num;
            _sortie = sortie;
        }

        public int Num
        {
            get { return _num; }
            set { _num = value; }
        }

        public bool Sortie
        {
            get { return _sortie; }
            set { _sortie = value; }
        }

    }
}
