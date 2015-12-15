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
        

        public static Lieu Parse(XmlNode test)    // on utilise le contenue d'un noeud
        {
            Lieu l = new Lieu();
            Point coords = new Point(0, 0);
            string nomLieu = "";

            nomLieu = test.SelectSingleNode("Nom").InnerText; // on donne le contenu du noeud nom à la variable
            string Coord = test.SelectSingleNode("Coordonnées").InnerText;// on prend le texte du noeud coordonnées

            int i = 0;
            int j = 0;
            string numberX = "";
            string numberY = "";

            foreach (char c in Coord)   // pour chaque caractère 
            {
                if (c == 'X')     // quand on trouve le caractère X
                {
                    j = i + 3;     //on se place sur le premier nombre
                    do          //on boucle à l'infini
                    {
                        if (Coord[j - 1] == ',') // quand un trouve le caractère spécifié
                        {
                            break; // on sort le la boucle 
                        }
                        else
                        {
                            numberX = numberX + Coord[j - 1];    // un ajoute les numéro comme caractère les uns derrière les autres
                            j++;
                        }
                    } while (true);
                    coords.X = int.Parse(numberX); // on convertie la chaine de caractère en entier
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
