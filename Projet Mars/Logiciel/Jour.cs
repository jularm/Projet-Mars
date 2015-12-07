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
        private bool[] _tabHoraires = new bool[148]; // (avant 149) ca change pas et on compte 148 cran donc de 0 à 147
        private List<Activite> _listeActivites = new List<Activite>();


        public Jour(int numero)
        {
            _numero = numero;
            //Journée type par défaut : 
            _listeActivites.Add(new Activite("Sleeping", new Heure(0, 0), new Heure(7, 0), "Dormir c'est important"));
            _listeActivites.Add(new Activite("Eating", new Heure(7, 0), new Heure(8, 0), "Manger c'est important"));
            _listeActivites.Add(new Activite("Private", new Heure(8, 0), new Heure(12, 0), ""));
            _listeActivites.Add(new Activite("Eating", new Heure(12, 0), new Heure(14, 0), "Manger c'est important"));
            _listeActivites.Add(new Activite("Private", new Heure(14, 0), new Heure(19, 0), ""));
            _listeActivites.Add(new Activite("Eating", new Heure(19, 0), new Heure(21, 0), "Manger c'est important"));
            _listeActivites.Add(new Activite("Private", new Heure(21, 0), new Heure(23, 0), ""));
            _listeActivites.Add(new Activite("Sleeping", new Heure(23, 0), new Heure(24, 40), "Dormir c'est important"));
            _compteRendu = "test";
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
            // journée type : 
            _listeActivites.Add(new Activite("Sleeping", new Heure(0, 0), new Heure(7, 0), "Dormir c'est important"));
            _listeActivites.Add(new Activite("Eating", new Heure(10, 30), new Heure(13, 40), "Manger c'est important"));
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
            NodeJour.AppendChild(NodeListeActivite);

            rootNode.AppendChild(NodeJour);

        }


        // lecture xml et generation objets    
        public void chargerXml(XmlDocument xmlDoc2, Mission M)
        {
            XmlNodeList nodelistJour = xmlDoc2.GetElementsByTagName("Jour");

            string CompteRendu = "";
            int num = 0;
            bool sortie = false;
            bool[] tabHoraires = new bool[147];

            foreach (XmlNode nodeJour in nodelistJour)
            {
                CompteRendu = nodeJour.SelectSingleNode("Compte_rendu").InnerText;
                num = int.Parse(nodeJour.SelectSingleNode("Numéro").InnerText);
                sortie = bool.Parse(nodeJour.SelectSingleNode("Sortie").InnerText);

                List<Activite> listeActivites = new List<Activite>();
                Jour j = new Jour(CompteRendu, num, tabHoraires);

                XmlNodeList nodelistActivite = nodeJour.SelectNodes("Liste_Activité");
                foreach (XmlNode nodeActivite in nodelistActivite)
                {
                    XmlNodeList nodelisteActivite = nodeActivite.SelectNodes("Activité");
                    foreach (XmlNode Activite in nodelisteActivite)
                    {
                        Activite a = new Activite("");
                        a.chargerXml2(xmlDoc2, M);
                        listeActivites.Add(a);
                    }

                }
                j.ListeActivites = listeActivites;

                XmlNodeList nodelistTabHoraire = nodeJour.SelectNodes("Tableau_Horaire");
                foreach (XmlNode nodeListeLibre in nodelistTabHoraire)
                {
                    int i = 0;
                    XmlNodeList nodelistLibre = nodeListeLibre.SelectNodes("Libre");
                    foreach (XmlNode nodeLibre in nodelistLibre)
                    {
                        tabHoraires[i] = bool.Parse(nodeLibre.InnerText);
                        i++;
                    }
                }
                j.TabHoraires = tabHoraires;

                M.Calendar.Jours.Add(j);
            }

        }
    }
}       
           
            

            
