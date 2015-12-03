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
        private List<Astronaute> _listAstronautes = new List<Astronaute>();
        private List<CategorieActivite> _listCategorieActivite = new List<CategorieActivite>();
        private CalendrierMartien _calendar;

        public Mission()
        {            
        }

        public Mission(CalendrierMartien calendar, List<Astronaute> listAstronautes, List<CategorieActivite> listCategorieActivite)
        {
            _calendar = calendar;
            _listAstronautes = listAstronautes;
            _listCategorieActivite = listCategorieActivite;
        }

        public CalendrierMartien Calendar
        {
            get { return _calendar; }
            set { _calendar = value; }
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
        public void genereXml(XmlDocument xmlDoc)
        {
            XmlNode NodeMission = xmlDoc.CreateElement("Mission");            

            XmlNode NodeListeAstronautre = xmlDoc.CreateElement("List_Astronaute");
            foreach (Astronaute a in _listAstronautes)
            {
                a.genereXml(xmlDoc, NodeListeAstronautre);
            }
            NodeMission.AppendChild(NodeListeAstronautre);

            XmlNode NodelistCategorieActivite = xmlDoc.CreateElement("List_Categorie_Activite");
            foreach (CategorieActivite a in _listCategorieActivite)
            {
                a.genereXml(xmlDoc, NodelistCategorieActivite);
            }
            NodeMission.AppendChild(NodelistCategorieActivite);


            XmlNode NodeListeCalendar = xmlDoc.CreateElement("Calendrier_Mission");
            _calendar.genereXml(xmlDoc, NodeListeCalendar);
            NodeMission.AppendChild(NodeListeCalendar);

            xmlDoc.AppendChild(NodeMission);
        }

        // lecture xml et generation objets
        public void chargerXml(XmlDocument xmlDoc, Mission M)
        {
            XmlNodeList nodelistMission = xmlDoc.GetElementsByTagName("Mission");

            List<Astronaute> listAstronautes = new List<Astronaute>();
            List<CategorieActivite> listCategorieActivite = new List<CategorieActivite>();
            CalendrierMartien calendar = new CalendrierMartien();

            foreach (XmlNode nodeMission in nodelistMission)
            {
                XmlNodeList nodelistAstronautes = nodeMission.SelectNodes("List_Astronaute");
                foreach (XmlNode nodeAstronautes in nodelistAstronautes)
                {
                    Astronaute a = new Astronaute(0, "");
                    a.chargerXml(xmlDoc, this);
                    listAstronautes.Add(a);

                }

                XmlNodeList nodelistCategorieActivite = nodeMission.SelectNodes("List_Categorie_Activite");
                foreach (XmlNode nodeCategorieActivite in nodelistCategorieActivite)
                {
                    CategorieActivite cat = new CategorieActivite("");
                    cat.chargerXml(xmlDoc, this);
                    listCategorieActivite.Add(cat);
                }

               
                XmlNodeList nodelistCalendrierMartien = nodeMission.SelectNodes("Calendrier_Mission");
                foreach (XmlNode nodeCalendrierMartien in nodelistCalendrierMartien)
                {
                    CalendrierMartien c = new CalendrierMartien();
                    c.chargerXml(xmlDoc, this); 
                    calendar=c;
                }

            }

            Mission Mission = new Mission(calendar, listAstronautes, listCategorieActivite);
        }
    }
}
