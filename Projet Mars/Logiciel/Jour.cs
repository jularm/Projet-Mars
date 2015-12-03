﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;


namespace Logiciel
{
    class Jour
    {
        private string _compteRendu;
        private int _numero;
        private bool _sortie;
        private bool[] _tabHoraires= new bool[147]; // (avant 149) ca change pas et on compte 148 cran donc de 0 à 147
        private List<Activite> _listeActivites;


        public Jour(int numero)
        {
            _numero = numero;           
            //Journée type par défaut :
            List<Astronaute>li =new List<Astronaute>();
            li.Add(new Astronaute(1, "Paul"));
            li.Add(new Astronaute(2, "Pierre"));
            _listeActivites=new List<Activite>();
            _listeActivites.Add(new Activite("Sleeping", new Heure (0,0),new Heure(7,0),"Dormir c'est important",li));
            _listeActivites.Add(new Activite("Eating", new Heure(7, 0), new Heure(8, 0), "Manger c'est important", li));
            _listeActivites.Add(new Activite("Private", new Heure(8,0), new Heure(12,0), "", li));
            _listeActivites.Add(new Activite("Eating", new Heure(12,0), new Heure(14,0), "Manger c'est important", li));
            _listeActivites.Add(new Activite("Private", new Heure(14,0), new Heure(19,0), "", li));
            _listeActivites.Add(new Activite("Eating", new Heure(19,0), new Heure(21,0), "Manger c'est important", li));
            _listeActivites.Add(new Activite("Private", new Heure(21,0), new Heure(23,0), "", li));
            _listeActivites.Add(new Activite("Sleeping", new Heure(23,0), new Heure(24,40), "Dormir c'est important", li));
            _compteRendu = "";
        }

        public Jour(string compteRendu, int numero, bool sortie, bool[] tabHoraires)
        {
            _numero = numero;
            _compteRendu = compteRendu;
            _sortie = sortie;
            _tabHoraires = tabHoraires;
            // journée type :
            List<Astronaute> li = new List<Astronaute>();
            li.Add(new Astronaute(1, "Paul"));
            li.Add(new Astronaute(2, "Pierre"));
            _listeActivites = new List<Activite>();
            _listeActivites.Add(new Activite("Sleeping", new Heure(0, 0), new Heure(7, 0), "Dormir c'est important", li));
            _listeActivites.Add(new Activite("Eating", new Heure(10, 30), new Heure(13, 40), "Manger c'est important", li));

        }

        public string CompteRendu
        {
            get { return _compteRendu; }
            set { _compteRendu = value; }
        }

        public int Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public bool Sortie
        {
            get { return _sortie; }
            set { _sortie = value; }
        }
        public bool[] TabHoraires
        {
            get { return _tabHoraires; }
            set { _tabHoraires = value; }
        }
        public List<Activite> ListeActivites
        {
            get { return _listeActivites; }
            set { _listeActivites = value; }
        }

        public void Parse(string test)
        {

        }


        
        public void genereXml(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode NodeJour = xmlDoc.CreateElement("Jour");            

            XmlNode NodeCompteRendu = xmlDoc.CreateElement("CompteRendu");
            NodeCompteRendu.InnerText = CompteRendu.ToString();
            NodeJour.AppendChild(NodeCompteRendu);

            XmlNode NodeNumero = xmlDoc.CreateElement("Numero");
            NodeNumero.InnerText = Numero.ToString();
            NodeJour.AppendChild(NodeNumero);

            XmlNode NodeSortie = xmlDoc.CreateElement("Sortie");
            NodeSortie.InnerText = Sortie.ToString();
            NodeJour.AppendChild(NodeSortie);

            XmlNode NodeListeTabHoraire = xmlDoc.CreateElement("list_Activite");
            foreach (Activite a in ListeActivites)
            {
                a.genereXml(xmlDoc, rootNode);
            }        

            rootNode.AppendChild(NodeJour);

        }
       

        // lecture xml et generation objets    
        public void chargerXml(XmlDocument xmlDoc, Mission M)
        {
            XmlNodeList nodelistJour = xmlDoc.GetElementsByTagName("Jour"); // Je récupère une liste des noeuds ayant pour nom Jour

            string compteRendu = "";
            int numero = 0;
            bool sortie = false;
            bool[] tabHoraires = new bool[147];

            foreach (XmlNode nodeJour in nodelistJour)
            {
                compteRendu = nodeJour.SelectSingleNode("Debut").InnerText;
                numero = int.Parse(nodeJour.SelectSingleNode("Numero").InnerText);
                sortie = bool.Parse(nodeJour.SelectSingleNode("Jour").InnerText);


                XmlNodeList nodelistTabHoraire = nodeJour.SelectNodes("TabHoraire");
                foreach (XmlNode nodeListeLibre in nodelistTabHoraire)
                {
                    int i = 0;
                    XmlNodeList nodelistLibre = nodeListeLibre.SelectNodes("Libre");
                    foreach (XmlNode nodeLibre in nodelistLibre)
                    {
                        tabHoraires[i] = bool.Parse(nodeLibre.InnerText);
                        i++;
                    }
                }
            }

            Jour j = new Jour(compteRendu, numero, sortie, tabHoraires);
            
        }
    }
    
}
