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

    public class Activite 
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

        public Activite(string nom, string texteDescriptif)
            : this(nom)
        {
            _texteDescriptif = texteDescriptif;
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

        public List<Astronaute> ListAstronaute
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


        public void AddAstronaute(Astronaute a)
        {
            _listAstronaute.Add(a);
        }

        public void RemoveAstronaute(Astronaute a)
        {
            _listAstronaute.Remove(a);
        }

        public override string ToString()
        {
            return this.Nom;
        }



        // Generation Xml
        public void genereXml(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode NodeActivite = xmlDoc.CreateElement("Activité");

            XmlNode NodeNom = xmlDoc.CreateElement("NomActivité");
            NodeNom.InnerText = Nom.ToString();
            NodeActivite.AppendChild(NodeNom);            

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
    
    }
}
