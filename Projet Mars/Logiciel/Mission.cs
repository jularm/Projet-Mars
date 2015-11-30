using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace Logiciel
{
    class Mission
    {
        private List<Astronaute> _listAstronautes;
        private List<CategorieActivite> _listCategorieActivite;
        private Calendrier_Martien _calendar;

        public Mission(Calendrier_Martien calendar, List<Astronaute> listAstronautes, List<CategorieActivite> listCategorieActivite)
        {
            _calendar = calendar;
            _listAstronautes = listAstronautes;
            _listCategorieActivite = listCategorieActivite;
        }

        public Calendrier_Martien Calendar
        {
            get { return _calendar; }
        }

        public void AddAstronaute(Astronaute a)
        {
            _listAstronautes.Add(a);
        }

        public void RemoveAstronaute(Astronaute a)
        {
            _listAstronautes.Remove(a);
        }

        public void AddCategorie(CategorieActivite a)
        {
            _listCategorieActivite.Add(a);
        }

        public void RemoveCategorie(CategorieActivite a)
        {
            _listCategorieActivite.Remove(a);
        }


        // Generation Xml
        public void genereXml(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode NodeMission = xmlDoc.CreateElement("Mission");

            XmlNode NodeCalendar = xmlDoc.CreateElement("Calendrier_Martien");
            NodeCalendar.InnerText = NodeCalendar.ToString();
            NodeMission.AppendChild(NodeCalendar);
           
            foreach (CategorieActivite c in _listCategorieActivite)
            {
                c.genereXml(xmlDoc, rootNode);
            }

            foreach (Astronaute a in _listAstronautes)
            {
                a.genereXml(xmlDoc, rootNode);
            }

            rootNode.AppendChild(NodeMission);

        }

        // lecture xml et generation objets
        public void chargerXml(XmlDocument xmlDoc, Mission M)
        {
            XmlNodeList nodelistMission = xmlDoc.GetElementsByTagName("Mission");

            List<Astronaute> listAstronautes = new List<Astronaute>();
            List<CategorieActivite> listCategorieActivite = new List<CategorieActivite>();
            Calendrier_Martien calendar = new Calendrier_Martien();

            foreach (XmlNode nodeMission in nodelistMission)
            {
                XmlNodeList nodelistCategorieActivite = nodeMission.SelectNodes("Categorie_Activite");
                foreach (XmlNode nodeCategorieActivite in nodelistCategorieActivite)
                {
                    CategorieActivite cat = new CategorieActivite("");
                    cat.chargerXml(xmlDoc, this);
                    listCategorieActivite.Add(cat);
                }

                XmlNodeList nodelistAstronautes = nodeMission.SelectNodes("Astronautes");
                foreach (XmlNode nodeAstronautes in nodelistAstronautes)
                {
                    Astronaute a = new Astronaute(0,"");
                    a.chargerXml(xmlDoc, this);
                    listAstronautes.Add(a);

                }
                XmlNodeList nodelistCalendrierMartien = nodeMission.SelectNodes("Calendrier_Martien");
                foreach (XmlNode nodeCalendrierMartien in nodelistCalendrierMartien)
                {
                    Calendrier_Martien c = new Calendrier_Martien();
                    c.chargerXml(xmlDoc, this); 
                    calendar=c;
                }

            }

            Mission Mission = new Mission(calendar, listAstronautes, listCategorieActivite);
        }
    }
}
