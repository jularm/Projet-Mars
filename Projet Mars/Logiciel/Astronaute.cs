using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;



namespace Logiciel
{
    class Astronaute
    {
        private int _id;
        private string _nom;

        public Astronaute(int id, string nom)
        {
            _id = id;
            _nom = nom;
        }

        public int Id
        {
            get { return _id; }
        }

        public string Nom
        {
            get { return _nom; }
        }

        // Generation Xml
        public void genereXml(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode NodeAstronaute = xmlDoc.CreateElement("Astronaute");

            XmlNode NodeId = xmlDoc.CreateElement("Id");
            NodeId.InnerText = Id.ToString();
            NodeAstronaute.AppendChild(NodeId);

            XmlNode NodeNom = xmlDoc.CreateElement("Nom");
            NodeNom.InnerText = Nom.ToString();
            NodeAstronaute.AppendChild(NodeNom);

            if (Id != 0 && Nom != "")
            {
                rootNode.AppendChild(NodeAstronaute);
            }
        }

        // lecture xml et generation objets
        public void chargerXml(XmlDocument xmlDoc, Mission M)
        { 
            XmlNodeList nodelistAstronaute = xmlDoc.GetElementsByTagName("Astronaute");
            
            string nom = "";
            int id = 0;

            foreach (XmlNode nodeAstronaute in nodelistAstronaute)
            {
                id = int.Parse(nodeAstronaute.SelectSingleNode("Id").InnerText);
                nom = nodeAstronaute.SelectSingleNode("Nom").InnerText;
                Astronaute a = new Astronaute(id, nom);                      
            }                       
        }


        public void chargerXml2(XmlDocument xmlDoc, XmlNodeList truc)
        {
            //XmlNodeList nodelistAstronaute = xmlDoc.GetElementsByTagName("Astronaute");

            string nom = "";
            int id = 0;

            foreach (XmlNode nodeAstronaute in truc)
            {
                id = int.Parse(nodeAstronaute.SelectSingleNode("Id").InnerText);
                nom = nodeAstronaute.SelectSingleNode("Nom").InnerText;
                Astronaute a = new Astronaute(id, nom);                
            }
        }
    }
}
