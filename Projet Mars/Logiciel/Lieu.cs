using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Drawing; //Pour pouvoir utiliser la structure Point



namespace Logiciel
{
    class Lieu
    {
        private string _nom;
        private Point _coords;

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
        

        public static Lieu Parse(XmlNode test)
        {
            Lieu l = new Lieu();
            Point coords = new Point(0, 0);
            string nomLieu = "";

            nomLieu = test.SelectSingleNode("Nom").InnerText;
            string Coord = test.SelectSingleNode("Coordonnées").InnerText;

            int i = 0;
            int j = 0;
            string numberX = "";
            string numberY = "";

            foreach (char c in Coord)
            {
                if (c == 'X')
                {
                    j = i + 3;
                    do
                    {
                        if (Coord[j - 1] == ',')
                        {
                            break;
                        }
                        else
                        {
                            numberX = numberX + Coord[j - 1];
                            j++;
                        }
                    } while (true);
                    coords.X = int.Parse(numberX);
                }

                if (c == 'Y')
                {
                    j = i + 3;
                    do
                    {
                        if (Coord[j - 1] == '}')
                        {
                            break;
                        }
                        else
                        {
                            numberY = numberY + Coord[j - 1];
                            j++;
                        }
                    } while (true);
                    coords.Y = int.Parse(numberY);
                }
                i++;
            }
            l.Nom = nomLieu;
            l.Coords = coords;
            return l;
        }

        // Generation Xml
        public void genereXml(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode NodeLieu = xmlDoc.CreateElement("Lieu");

            XmlNode NodeNom = xmlDoc.CreateElement("Nom");
            NodeNom.InnerText = Nom.ToString();
            NodeLieu.AppendChild(NodeNom);

            XmlNode NodeCoords = xmlDoc.CreateElement("Coordonnées");
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
                coords.X = int.Parse(nodeLieu.SelectSingleNode("Coordonnées").InnerText);
                coords.Y = int.Parse(nodeLieu.SelectSingleNode("Coordonnées").InnerText);                
            }
            Lieu L = new Lieu(nom, coords);        
        }
    }
}
