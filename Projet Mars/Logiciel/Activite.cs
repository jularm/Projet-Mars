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
        }

        public Activite(string nom, Heure debut, Heure fin, string texteDescriptif, List<Astronaute> listAst)
            : this(nom)
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
            XmlNode NodeActivite = xmlDoc.CreateElement("Activite");

            XmlNode NodeNom = xmlDoc.CreateElement("Nom");
            NodeNom.InnerText = NodeNom.ToString();
            NodeActivite.AppendChild(NodeNom);

            XmlNode NodeCompteRendu = xmlDoc.CreateElement("Compte_Rendu");
            NodeCompteRendu.InnerText = NodeCompteRendu.ToString();
            NodeActivite.AppendChild(NodeCompteRendu);

            XmlNode NodeDebut = xmlDoc.CreateElement("Debut");
            NodeDebut.InnerText = NodeDebut.ToString();
            NodeActivite.AppendChild(NodeDebut);

            XmlNode NodeFin = xmlDoc.CreateElement("Fin");
            NodeFin.InnerText = NodeFin.ToString();
            NodeActivite.AppendChild(NodeFin);

            XmlNode NodeGps = xmlDoc.CreateElement("GPS");
            Lieu l = _gps;
            l.genereXml(xmlDoc, NodeActivite);

            NodeActivite.AppendChild(NodeGps);

            foreach (Astronaute a in _listAstronaute)
            {
                a.genereXml(xmlDoc, rootNode);
            }

            XmlNode NodeTexteDescriptif = xmlDoc.CreateElement("Fin");
            NodeTexteDescriptif.InnerText = NodeTexteDescriptif.ToString();
            NodeActivite.AppendChild(NodeTexteDescriptif);

            rootNode.AppendChild(NodeActivite);

        }

        // lecture xml et generation objets
        public void chargerXml(XmlDocument xmlDoc, Mission M)
        {
            XmlNodeList nodelistActivite = xmlDoc.GetElementsByTagName("Activite");

            string nom="";
            string compteRendu="";
            Heure debut=new Heure (0,0);
            Heure fin = new Heure(0, 0);
            Lieu gps= new Lieu();
            List<Astronaute> listAstronaute = new List<Astronaute>();
            string texteDescriptif="";

            foreach (XmlNode nodeActivite in nodelistActivite)
            {
                nom = nodeActivite.SelectSingleNode("Nom").InnerText;
                compteRendu = nodeActivite.SelectSingleNode("Compte_Rendu").InnerText;
                debut = Heure.Parse(nodeActivite.SelectSingleNode("Debut").InnerText);
                fin = Heure.Parse(nodeActivite.SelectSingleNode("Fin").InnerText);
               // gps = Lieu.Parse(nodeActivite.SelectSingleNode("GPS").InnerText);

                XmlNodeList nodelistAstronaute = nodeActivite.SelectNodes("Astronaute");
                foreach (XmlNode nodeAstronaute in nodelistAstronaute)
                {
                    Astronaute a = new Astronaute(0,"");
                    a.chargerXml(xmlDoc, M);
                    listAstronaute.Add(a);
                }
            }
            Activite act = new Activite(nom, debut, fin, texteDescriptif, listAstronaute);            
        }


    }
}
