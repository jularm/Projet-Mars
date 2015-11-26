using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Drawing; //pour pouvoir utiliser la structure point



namespace Logiciel
{
    class Lieu
    {
        private string _nom;
        private Point _coords;

        public Point Coords
        {
            get { return _coords; }
            set { _coords = value; }
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public Lieu(string nom, Point point)
        {
            _nom = nom;
            _coords = point;
        }
        public Lieu()
        {
            _nom = "Base";
            _coords = new Point(0,0);
        }

    }
}
