using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Drawing; //pour pouvoir utiliser la structure point



namespace Logiciel
{
    class Lieu
    {
        private string _nom;
        private Point _coords;

        public Point Coords
        {
            get { return _coords; }
            set { _coords = value; }
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public Lieu(string nom, Point point)
        {
            _nom = nom;
            _coords = point;
        }
        public Lieu()
        {
            _nom = "Base";
            _coords = new Point(0, 0);
        }


        public static Lieu Parse(string test)
        {
            Lieu l = new Lieu();           
            Point coords = new Point(0, 0);
            List<string> nomLieu = new List<string>();

            foreach (char c in test)
            {
                nomLieu.Add(c.ToString());
            }

            for (int i = 0; i < nomLieu.Count; i++)
            {
                if (nomLieu[i] == "n" && nomLieu[i + 2] == "m")
                {
                    int j=i+3;
                    string elnom = "";
                    do
                    {
                        elnom = elnom + nomLieu[j];
                        j++;
                    } while (nomLieu[j] == " ");
                    l.Nom = elnom;
                }
            }
            return l;
        }

        // Generation Xml
        public void genereXml(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode NodeLieu = xmlDoc.CreateElement("Lieu");

            XmlNode NodeNom = xmlDoc.CreateElement("Nom");
            NodeNom.InnerText = Nom.ToString();
            NodeLieu.AppendChild(NodeNom);

            XmlNode NodeCoords = xmlDoc.CreateElement("Coordonnees");
            NodeCoords.InnerText = Coords.ToString();
            NodeLieu.AppendChild(NodeCoords);

            rootNode.AppendChild(NodeLieu);

        }

        // lecture xml et generation objets
        public void chargerXml(XmlDocument xmlDoc, Mission M)
        {
            XmlNodeList nodelistLieu = xmlDoc.GetElementsByTagName("Lieu");

            string nom="";
            Point coords = new Point (0,0);

            foreach (XmlNode nodeLieu in nodelistLieu)
            {
                nom = nodeLieu.SelectSingleNode("Nom").InnerText;
                coords.X = int.Parse(nodeLieu.SelectSingleNode("Coordonnees").InnerText);
                coords.Y = int.Parse(nodeLieu.SelectSingleNode("Coordonnees").InnerText);
                
            }
            Lieu L = new Lieu(nom, coords);        
        }


    }
}
