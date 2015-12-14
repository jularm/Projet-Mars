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

        public List<CategorieActivite> Listcat
        {
            get { return _listCategorieActivite; }
        }

        private List<CategorieActivite> Listcate
        {
            set { _listCategorieActivite = value; }
        }

        public List<Astronaute> ListAstr
        {
            get { return _listAstronautes; }
            set { _listAstronautes = value; }
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

            XmlNode NodelistAstronautre = xmlDoc.CreateElement("Liste_Astronaute");
            foreach (Astronaute a in _listAstronautes)
            {
                a.genereXml(xmlDoc, NodelistAstronautre);
            }
            NodeMission.AppendChild(NodelistAstronautre);

            XmlNode NodelistCategorieActivite = xmlDoc.CreateElement("Liste_Catégorie_Activité");
            foreach (CategorieActivite a in _listCategorieActivite)
            {
                a.genereXml(xmlDoc, NodelistCategorieActivite);
            }
            NodeMission.AppendChild(NodelistCategorieActivite);        
           
            xmlDoc.AppendChild(NodeMission);
        }


        public void genereXml2(XmlDocument xmlDoc)
        {
            XmlNode NodeMission = xmlDoc.CreateElement("Mission");           

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

            foreach (XmlNode nodeMission in nodelistMission)
            {

                XmlNodeList nodelistAstronautes = nodeMission.SelectNodes("Liste_Astronaute");
                foreach (XmlNode nodeAstronautes in nodelistAstronautes)
                {
                    XmlNodeList nodelistAstronaute = xmlDoc.GetElementsByTagName("Astronaute");

                    string nom = "";
                    int id = 0;

                    foreach (XmlNode nodeAstronaute in nodelistAstronaute)
                    {
                        id = int.Parse(nodeAstronaute.SelectSingleNode("Id").InnerText);
                        nom = nodeAstronaute.SelectSingleNode("Nom").InnerText;
                        Astronaute a = new Astronaute(id, nom);
                        M._listAstronautes.Add(a);
                    }
                }


                XmlNodeList nodelistCategorieActivite = nodeMission.SelectNodes("Liste_Catégorie_Activité");
                foreach (XmlNode nodeCategorieActivite in nodelistCategorieActivite)
                {
                    XmlNodeList nodelisteCategorieActivite = nodeCategorieActivite.SelectNodes("Catégorie_Activité");
                    
                    foreach (XmlNode nodeCategoriedActivite in nodelisteCategorieActivite)
                    {
                        string nom = "";

                        List<Activite> listActivite = new List<Activite>();

                        nom = nodeCategoriedActivite.SelectSingleNode("NomCatégorie").InnerText;

                        XmlNodeList nodelistActivite = nodeCategoriedActivite.SelectNodes("Liste_Activité");
                        foreach (XmlNode nodeActivite in nodelistActivite)
                        {
                            string nome = "";

                            XmlNodeList nodelisteActivite = nodeActivite.SelectNodes("Activité"); 
                            foreach (XmlNode nodeActivitee in nodelisteActivite)
                            {
                                nome = nodeActivitee.SelectSingleNode("NomActivité").InnerText;
                                Activite act = new Activite(nome);
                                listActivite.Add(act);
                            }                            
                            
                        }

                        CategorieActivite c = new CategorieActivite(nom, listActivite);
                        M.AddCategorie(c);
                    }
                }
            }

           
            M.Listcate = Listcat;
            
        }


        public void chargerXml2(XmlDocument xmlDoc, Mission M)
        {
            XmlNodeList nodelistMission = xmlDoc.GetElementsByTagName("Mission");

            CalendrierMartien calendar = new CalendrierMartien();

            foreach (XmlNode nodeMission in nodelistMission)
            {   
                XmlNodeList nodelistCalendrierMartien = nodeMission.SelectNodes("Calendrier_Mission");
                foreach (XmlNode nodeCalendrierMartien in nodelistCalendrierMartien)
                {
                    calendar.chargerXml(xmlDoc, this);                   
                }
            }
        }
    }
}
