using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Drawing;


namespace Logiciel
{
    
    class Activite
    {
        private string _nom;
        private string _compteRendu;
        private Heure _debut;
        private Heure _fin;
        private Lieu _gps;
        private List<Astronaute> _listAstronaute = new List<Astronaute>();
        private string _texteDescriptif;

        public Activite()
        {

        }
        public Activite(string nom)
        {
            _nom = nom;
        }

        public Activite(string nom, Heure debut, Heure fin, string texteDescriptif,List<Astronaute> listAst):this (nom)
        {
            _debut = debut;
            _fin = fin;
            _texteDescriptif = texteDescriptif;
            _listAstronaute = listAst;
            _gps = new Lieu();
        }
        public Activite(string nom, Heure debut, Heure fin, string texteDescriptif, List<Astronaute> listAst, Lieu lieu)
            : this(nom, debut, fin, texteDescriptif, listAst)
        {
            _gps = lieu;
        }

        internal List<Astronaute> ListAstronaute
        {
            get { return _listAstronaute; }
            set { _listAstronaute = value; }
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

        public Heure Debut
        {
            get { return _debut; }
            set { _debut = value; }
        }

        public Heure Fin
        {
            get { return _fin; }
            set { _fin = value; }
        }
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }     

    }
}
