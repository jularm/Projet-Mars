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
    public struct Heure //La structure permet la factorisation de code de la même manière qu'une classe mais en plus léger donc plus utilisable pour des petits éléments
    {
        int _heures;
        int _minutes;
        int _heuresMinutes;

        public int HeuresMinutes
        {
            get { return _heuresMinutes; }
            set { _heuresMinutes = value; }
        }

        public int Heures
        {
            get { return _heures; }
            set { _heures = value; }
        }
        public int Minutes
        {
            get { return _minutes; }
            set { _minutes = value; }
        }

        public Heure(int h, int m)
        {
            _heures = h;
            _minutes = m;
            if (m == 0)// pour les besoins de cet attribut, on double le zéro pour que 8h00 devienne 800 et non 80 
            {
                _heuresMinutes = int.Parse(Convert.ToString(h) + "00");
            }
            else
            {
                _heuresMinutes = int.Parse(Convert.ToString(h) + Convert.ToString(m));
            }

        }
    }

    class Activite
    {
        private string _nom;
        private string _compteRendu;
        private Heure _debut;
        private Heure _fin;
        private Lieu _gps;
        private List<Astronaute> _listAstronaute = new List<Astronaute>();
        private string _texteDescriptif; 

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
