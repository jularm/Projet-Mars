using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;



namespace Logiciel
{
    public class CategorieActivite 
    {
        private string _nom;
        private List<Activite> _listActivite = new List<Activite>(); //Pour créer une liste d'activités par jour

        public CategorieActivite(string nom)
        {
            _nom = nom;
            _listActivite.Add(new Activite(nom));
        }

        public CategorieActivite(string nom, List<Activite> listActivite)
        {
            _nom = nom;
            _listActivite = listActivite;
        }

        public string Nom
        {
            get { return _nom; }           
        }

        public List<Activite> ListActivite
        {
            get { return _listActivite; }
        }

        

        // Generation Xml
        public void genereXml(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode NodeCategorieActivite = xmlDoc.CreateElement("Catégorie_Activité");

            XmlNode NodeNom = xmlDoc.CreateElement("NomCatégorie");
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
    }
}
