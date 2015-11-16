using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;


namespace Logiciel
{
    class Activite
    {
        private string _nom;
        private string _compteRendu;
        private DateTime _debut;
        private DateTime _fin;
        private Lieu _gps;
        private List<Astronaute> _listAstronaute = new List<Astronaute>();
        private string _texteDescriptif;


        public Activite(string nom)
        {
            _nom = nom;
        }

        public string CompteRendu
        {
            get { return _compteRendu;}
            set { _compteRendu = value; }
        }

        public string TexteDescriptif
        {
            get { return _texteDescriptif; }
            set { _texteDescriptif = value; }
        }

        public Lieu Gps
        {
            get { return _gps; }
            set { _gps = value; }
        }

        public DateTime Debut
        {
            get { return _debut; }
            set { _debut = value; }
        }

        public DateTime Fin
        {
            get { return _fin; }
            set { _fin = value; }
        }

        public void AddAstronaute(Astronaute a)
        {
            _listAstronaute.Add(a);
        }

        public void RemoveAstronaute(Astronaute a)
        {
            _listAstronaute.Remove(a);
        }        

    }
}
