using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;



namespace Logiciel
{
    public class Astronaute
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

        public override string ToString()
        {
            return _nom;
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
    }
}
