using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;


namespace Logiciel
{
    public class Jour
    {
        private string _compteRendu;
        private int _numero;
        private bool _sortie;
        private bool[] _tabHoraires = new bool[148]; //Il y a 148 crénaux de 10min dans une journée de 24h40
        private List<Activite> _listeActivites = new List<Activite>();

        public Jour(int numero)
        {
            _numero = numero;            
            _compteRendu = "La journée s'est bien passée";
            _sortie = false;
            for (int i = 0; i < 147; i++)
            {
                _tabHoraires[i] = false;
            }
        }

        public Jour(string compteRendu, int numero, bool[] tabHoraires)
        {
            _numero = numero;
            _compteRendu = compteRendu;
            _tabHoraires = tabHoraires;            
            _sortie = false;
            for (int i = 0; i < 147; i++)
            {
                _tabHoraires[i] = false;
            }
        }

        public string CompteRendu
        {
            get { return _compteRendu; }
            set { _compteRendu = value; }
        }

        public int Numero
        {
            get { return _numero; }
            set { _numero = value; }
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

        public List<Activite> ListeActivites
        {
            get { return _listeActivites; }
            set { _listeActivites = value; }
        }


        public void AddAct(Activite A)
        {
            _listeActivites.Add(A);
        }

        public void RemoveAct(Activite A)
        {
            _listeActivites.Remove(A);
        }



        public void genereXml(XmlDocument xmlDoc2, XmlNode rootNode)
        {
            XmlNode NodeJour = xmlDoc2.CreateElement("Jour");

            XmlNode NodeCompteRendu = xmlDoc2.CreateElement("Compte_Rendu");
            NodeCompteRendu.InnerText = CompteRendu.ToString();
            NodeJour.AppendChild(NodeCompteRendu);

            XmlNode NodeNumero = xmlDoc2.CreateElement("Numéro");
            NodeNumero.InnerText = Numero.ToString();
            NodeJour.AppendChild(NodeNumero);

            XmlNode NodeSortie = xmlDoc2.CreateElement("Sortie");
            NodeSortie.InnerText = Sortie.ToString();
            NodeJour.AppendChild(NodeSortie);

            XmlNode NodeListeActivite = xmlDoc2.CreateElement("Liste_Activité");
            foreach (Activite a in ListeActivites)
            {
                a.genereXml2(xmlDoc2, NodeListeActivite);
            }
            NodeJour.AppendChild(NodeListeActivite);

            XmlNode NodeTabHoraire = xmlDoc2.CreateElement("Tableau_Horaire");
            foreach (bool b in _tabHoraires)
            {
                XmlNode NodeLibre = xmlDoc2.CreateElement("Libre");
                NodeLibre.InnerText = b.ToString();
                NodeTabHoraire.AppendChild(NodeLibre);
            }
            NodeJour.AppendChild(NodeTabHoraire);

            rootNode.AppendChild(NodeJour);

        }       
    }
}       
           
            

            
