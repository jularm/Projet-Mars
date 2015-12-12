using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;



namespace Logiciel
{
    //Contient les instances et les méthodes relative au nom des activités
    class CategorieActivite 
    {
        private string _nom;
        private List<Activite> _listActivite = new List<Activite>(); //Pour créer une liste d'activités par jour

        public CategorieActivite(string nom)
        {
            _nom = nom;
            _listActivite.Add(new Activite(nom));
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public CategorieActivite(string nom, List<Activite> listActivite)
        {
            _nom = nom;
            _listActivite = listActivite;
        }

        public void AddActivite(Activite A)
        {
            _listActivite.Add(A);
        }

        public void RemoveActivite(Activite A)
        {
            _listActivite.Remove(A);
        }

        public List<Activite> ListActivite
        {
            get { return _listActivite; }
            set { _listActivite = value; }
        }

        // Generation Xml
        public void genereXml(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode NodeCategorieActivite = xmlDoc.CreateElement("Catégorie_Activité");

            XmlNode NodeNom = xmlDoc.CreateElement("Nom");
            NodeNom.InnerText = Nom.ToString();
            NodeCategorieActivite.AppendChild(NodeNom);

            XmlNode NodeListeAct = xmlDoc.CreateElement("Liste_Activité");
            foreach (Activite i in _listActivite)
            {
                i.genereXml(xmlDoc, NodeListeAct);
            }
            NodeCategorieActivite.AppendChild(NodeListeAct);


            rootNode.AppendChild(NodeCategorieActivite);

        }

        // lecture xml et generation objets
        public void chargerXml(XmlDocument xmlDoc, Mission M)
        {
            XmlNodeList nodelistCategorieActivite = xmlDoc.GetElementsByTagName("Catégorie_Activité");

            string nom = "";

            foreach (XmlNode nodeCategorieActivite in nodelistCategorieActivite)
            {
                List<Activite> listActivite = new List<Activite>();

                nom = nodeCategorieActivite.SelectSingleNode("Nom").InnerText;

                XmlNodeList nodelistActivite = nodeCategorieActivite.SelectNodes("Liste_Activité");
                foreach (XmlNode nodeActivite in nodelistActivite)
                {
                    Activite a = new Activite("");
                    a.chargerXml(xmlDoc, M);
                    listActivite.Add(a);
                }

                CategorieActivite c = new CategorieActivite(nom, listActivite);
                M.AddCategorie(c);
            }

        }
    }
}
