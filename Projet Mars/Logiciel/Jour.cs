using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;


namespace Logiciel
{
    class Jour
    {
        private string _compteRendu;
        private int _numero;
        private bool _sortie;
        private bool[] _tabHoraires;
        private List<Activite> _listeActivites;

        public List<Activite> ListeActivites
        {
            get { return _listeActivites; }
            set { _listeActivites = value; }
        }
        //private bool _passee;

        public Jour(int numero)
        {
            _numero = numero;
            _tabHoraires= new bool[149]; //nb de tranches de 10min dans une journée
            // journée type :
            List<Astronaute>li =new List<Astronaute>();
            li.Add(new Astronaute(1, "Paul"));
            li.Add(new Astronaute(2, "Pierre"));
            _listeActivites=new List<Activite>();
            _listeActivites.Add(new Activite("Sleeping", new Heure (0,0),new Heure(7,0),"Dormir c'est important",li));
            _listeActivites.Add(new Activite("Eating", new Heure(10, 30), new Heure(13, 40), "Manger c'est important", li));
            
        }

        public string CompteRendu
        {
            get { return _compteRendu; }
            set { _compteRendu = value; }
        }

        public int Numero
        {
            get { return _numero; }
        }

        public bool Sortie
        {
            get { return _sortie; }
            set { _sortie = value; }
        }
        public bool[] TabHoraires
        {
            get { return _tabHoraires; }
            set { _tabHoraires = value; }
        }

    }
    
}
