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


        public Activite(string nom)

        {
            _nom = nom;
            _debut = new Heure (0,0);
            _fin = new Heure(0, 0);
            _texteDescriptif = "";
            _gps = new Lieu();
            _texteDescriptif = "";
            _listAstronaute.Add (new Astronaute(0,""));
            _compteRendu = "";
        }

        public Activite(string nom, Heure debut, Heure fin, string texteDescriptif)
            : this(nom)
        {
            _debut = debut;
            _fin = fin;
            _texteDescriptif = texteDescriptif;
        }
        public Activite(string nom, Heure debut, Heure fin, string texteDescriptif, List<Astronaute> listAst, Lieu lieu)
            : this(nom, debut, fin, texteDescriptif)
        {
            _listAstronaute = listAst;
            _gps = lieu;
        }

        internal List<Astronaute> ListAstronaute
        {
            get { return _listAstronaute; }
            set { _listAstronaute = value; }
        }

        public void AddAstronaute(Astronaute a)
        {
            _listAstronaute.Add(a);
        }

        public void RemoveAstronaute(Astronaute a)
        {
            _listAstronaute.Remove(a);
        }

        public string CompteRendu
        {
            get { return _compteRendu; }
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


        // Generation Xml
        public void genereXml(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode NodeActivite = xmlDoc.CreateElement("Activité");

            XmlNode NodeNom = xmlDoc.CreateElement("Nom");
            NodeNom.InnerText = Nom.ToString();
            NodeActivite.AppendChild(NodeNom); 

            XmlNode NodeTexteDescriptif = xmlDoc.CreateElement("Texte_Descriptif");
            NodeTexteDescriptif.InnerText = TexteDescriptif.ToString();
            NodeActivite.AppendChild(NodeTexteDescriptif);

            rootNode.AppendChild(NodeActivite);

        }

        public void genereXml2(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode NodeActivite = xmlDoc.CreateElement("Activité");

            XmlNode NodeNom = xmlDoc.CreateElement("Nom");
            NodeNom.InnerText = Nom.ToString();
            NodeActivite.AppendChild(NodeNom);

            XmlNode NodeCompteRendu = xmlDoc.CreateElement("Compte_Rendu");
            NodeCompteRendu.InnerText = CompteRendu.ToString();
            NodeActivite.AppendChild(NodeCompteRendu);

            XmlNode NodeHDebut = xmlDoc.CreateElement("Heure_Debut");
            NodeHDebut.InnerText = Debut.Heures.ToString();
            NodeActivite.AppendChild(NodeHDebut);

            XmlNode NodeMDebut = xmlDoc.CreateElement("Minute_Debut");
            NodeMDebut.InnerText = Debut.Minutes.ToString();
            NodeActivite.AppendChild(NodeMDebut);

            XmlNode NodeHFin = xmlDoc.CreateElement("Heure_Fin");
            NodeHFin.InnerText = Fin.Heures.ToString();
            NodeActivite.AppendChild(NodeHFin);

            XmlNode NodeMFin = xmlDoc.CreateElement("Minute_Fin");
            NodeMFin.InnerText = Fin.Minutes.ToString();
            NodeActivite.AppendChild(NodeMFin);

            Lieu l = _gps;
            l.genereXml(xmlDoc, NodeActivite);

            XmlNode NodeListeAstr = xmlDoc.CreateElement("Liste_Astronaute");
            foreach (Astronaute a in _listAstronaute)
            {
                a.genereXml(xmlDoc, NodeListeAstr);
            }
            NodeActivite.AppendChild(NodeListeAstr);

            XmlNode NodeTexteDescriptif = xmlDoc.CreateElement("Texte_Descriptif");
            NodeTexteDescriptif.InnerText = TexteDescriptif.ToString();
            NodeActivite.AppendChild(NodeTexteDescriptif);

            rootNode.AppendChild(NodeActivite);

        }

        // lecture xml et generation objets
        public void chargerXml(XmlDocument xmlDoc, Mission M)
        {
            XmlNodeList nodelistActivite = xmlDoc.GetElementsByTagName("Activité");

            string nom="";
            string compteRendu="";
            Heure debut=new Heure (0,0);
            Heure fin=new Heure (0,0);
            Lieu gps= new Lieu();
            List<Astronaute> listAstronaute = new List<Astronaute>();
            string texteDescriptif="";

            foreach (XmlNode nodeActivite in nodelistActivite)
            {
                nom = nodeActivite.SelectSingleNode("Nom").InnerText;
                compteRendu = nodeActivite.SelectSingleNode("Compte_Rendu").InnerText;
                debut.Heures = int.Parse(nodeActivite.SelectSingleNode("Heure_Debut").InnerText);
                fin.Heures = int.Parse(nodeActivite.SelectSingleNode("Heure_Fin").InnerText);
                debut.Minutes = int.Parse(nodeActivite.SelectSingleNode("Minute_Debut").InnerText);
                fin.Minutes = int.Parse(nodeActivite.SelectSingleNode("Minute_Fin").InnerText);

                gps = Lieu.Parse(nodeActivite.SelectSingleNode("Lieu"),xmlDoc, M);

                XmlNodeList nodelistAstronaute = nodeActivite.SelectNodes("Liste_Astronaute");
                foreach (XmlNode nodeAstronaute in nodelistAstronaute)
                {
                    Astronaute a = new Astronaute(0,"");
                    a.chargerXml2(xmlDoc, nodelistAstronaute);
                    listAstronaute.Add(a);
                }
                texteDescriptif = nodeActivite.SelectSingleNode("Texte_Descriptif").InnerText;
            }
            Activite act = new Activite(nom, debut, fin, texteDescriptif);            
        }

        public void chargerXml2(XmlDocument xmlDoc, Mission M)
        {
            XmlNodeList nodelistActivite = xmlDoc.GetElementsByTagName("Activité");

            string nom = "";
            string compteRendu = "";
            Heure debut = new Heure(0, 0);
            Heure fin = new Heure(0, 0);
            Lieu gps = new Lieu();
            List<Astronaute> listAstronaute = new List<Astronaute>();
            string texteDescriptif = "";

            foreach (XmlNode nodeActivite in nodelistActivite)
            {
                nom = nodeActivite.SelectSingleNode("Nom").InnerText;
                compteRendu = nodeActivite.SelectSingleNode("Compte_Rendu").InnerText;
                debut.Heures = int.Parse(nodeActivite.SelectSingleNode("Heure_Debut").InnerText);
                fin.Heures = int.Parse(nodeActivite.SelectSingleNode("Heure_Fin").InnerText);
                debut.Minutes = int.Parse(nodeActivite.SelectSingleNode("Minute_Debut").InnerText);
                fin.Minutes = int.Parse(nodeActivite.SelectSingleNode("Minute_Fin").InnerText);

                gps = Lieu.Parse(nodeActivite.SelectSingleNode("Lieu"), xmlDoc, M);

                XmlNodeList nodelistAstronaute = nodeActivite.SelectNodes("Liste_Astronaute");
                foreach (XmlNode nodeAstronaute in nodelistAstronaute)
                {
                    Astronaute a = new Astronaute(0, "");
                    a.chargerXml2(xmlDoc, nodelistAstronaute);
                    listAstronaute.Add(a);
                }
                texteDescriptif = nodeActivite.SelectSingleNode("Texte_Descriptif").InnerText;
            }
            Activite act = new Activite(nom, debut, fin, texteDescriptif);
        }


    }
}
