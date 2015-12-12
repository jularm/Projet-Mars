using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Logiciel
{
    class CalendrierMartien 
    {
        private int _jour;
        private DateTime _debut;
        private DateTime _fin;
        private int _minute;
        private int _heure;
        private int _seconde;
        private List<Jour> _Jours = new List<Jour>();

        public CalendrierMartien()
        {
            _debut = System.DateTime.Now;
            System.TimeSpan duration = new System.TimeSpan(0, 500 * 1480, 0);
            _fin = System.DateTime.Now + duration;
            _jour = 1;
            _minute = 0;
            _heure = 0;
            _seconde = 0;
        }

        public CalendrierMartien(DateTime debut, DateTime fin, int jour, int heure, int minute, int seconde)
        {
            _debut = debut;
            _fin = fin;
            _jour = jour;
            _minute = minute;
            _heure = heure;
            _seconde = seconde;
        }


        public int Day
        {
            get { return _jour; }
            set { _jour = value; }
        }

        public int Minute
        {
            get { return _minute; }
            set { _minute = value; }
        }

        public int Heure
        {
            get { return _heure; }
            set { _heure = value; }
        }

        public int Seconde
        {
            get { return _seconde; }
            set { _seconde = value; }
        }

        public List<Jour> Jours
        {
            get { return _Jours; }
        }

        public void AddJours(Jour J)
        {
            _Jours.Add(J);
        }

        public DateTime Fin
        {
            get { return _fin; }
            set { _fin = value; }
        }

        public DateTime Debut
        {
            get { return _debut; }
            set { _debut = value; }
        }

        public void Horloge()
        {
            if (this.Seconde < 59)
            {
                this.Seconde++;
            }
            else
            {
                this.Seconde = 0;
                if (this.Heure != 24)
                {
                    if (this.Minute < 59)
                    {
                        this.Minute++;
                    }
                    else
                    {
                        this.Heure++;
                        this.Minute = 0;
                    }
                }
                else
                {
                    if (this.Minute < 39)
                    {
                        this.Minute++;
                    }
                    else
                    {
                        this.Heure = 0;
                        this.Minute = 0;
                        this.Day++;
                    }
                }
            }
        }

        public void MiseAJour()
        {              
            TimeSpan Ts = System.DateTime.Now - this._debut;
            int j = 0;
            int h = 0;
            int m = 0;
            int s = 0;
            h = this._debut.Hour;
            m = this._debut.Minute;
            s = this._debut.Second;
            double ecart = Math.Round(Ts.TotalSeconds);

            for (int i = 0; i < ecart; i++)
            {
                if ( h != 24)
                {
                    if (m < 59)
                    {
                        if (s < 59)
                        {
                            s++;
                        }
                        else
                        {
                            m++;
                            s = 0;
                        }
                    }
                    else
                    {
                        h++;
                        m = 0;
                    }
                }
                else
                {
                    if (m < 39)
                    {
                        if (s < 59)
                        {
                            s++;
                        }
                        else
                        {
                            m++;
                            s = 0;
                        }
                    }

                    else
                    {
                        h = 0;
                        m = 0;
                        j++;
                    }
                }
            }

            this.Day = j;
            this.Heure = h;
            this.Minute = m;
            this.Seconde = s;


          /*  for (int i = 0; i < ecart; i++)
            {
                if (this.Heure != 24)
                {
                    if (this.Minute < 59)
                    {
                        if (this.Seconde < 59)
                        {
                            this.Seconde++;
                        }
                        else
                        {
                            this.Minute++;
                            this.Seconde = 0;
                        }
                    }
                    else
                    {
                        this.Heure++;
                        this.Minute = 0;
                    }
                }
                else
                {
                    if (this.Minute < 39)
                    {
                        if (this.Seconde < 59)
                        {
                            this.Seconde++;
                        }
                        else
                        {
                            this.Minute++;
                            this.Seconde = 0;
                        }
                    }

                    else
                    {
                        this.Heure = 0;
                        this.Minute = 0;
                        this.Day++;
                    }
                }
            }     */
        }



        // Generation Xml
        public void genereXml(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode NodeCalendrier = xmlDoc.CreateElement("Calendrier_Martien");

            XmlNode NodeDebut = xmlDoc.CreateElement("Début");
            NodeDebut.InnerText = Debut.ToString();
            NodeCalendrier.AppendChild(NodeDebut);

            XmlNode NodeFin = xmlDoc.CreateElement("Fin");
            NodeFin.InnerText = Fin.ToString();
            NodeCalendrier.AppendChild(NodeFin);

            XmlNode NodeJour = xmlDoc.CreateElement("Jour");
            NodeJour.InnerText = Day.ToString();
            NodeCalendrier.AppendChild(NodeJour);

            XmlNode NodeHeure = xmlDoc.CreateElement("Heure");
            NodeHeure.InnerText = Heure.ToString();
            NodeCalendrier.AppendChild(NodeHeure);

            XmlNode NodeMinute = xmlDoc.CreateElement("Minute");
            NodeMinute.InnerText = Minute.ToString();
            NodeCalendrier.AppendChild(NodeMinute);

            XmlNode NodeSeconde = xmlDoc.CreateElement("Seconde");
            NodeSeconde.InnerText = Seconde.ToString();
            NodeCalendrier.AppendChild(NodeSeconde);

            XmlNode NodeListeJour = xmlDoc.CreateElement("Liste_Jour");
            foreach (Jour j in _Jours)
            {
                j.genereXml(xmlDoc, NodeListeJour);
            }
            NodeCalendrier.AppendChild(NodeListeJour);

            rootNode.AppendChild(NodeCalendrier);
        }

        // lecture xml et generation objets    
        public void chargerXml(XmlDocument xmlDoc, Mission M)
        {
            XmlNodeList nodelistCalendrier = xmlDoc.GetElementsByTagName("Calendrier_Martien");

            CalendrierMartien c = new CalendrierMartien();

            foreach (XmlNode nodeCalendrier in nodelistCalendrier)
            {
                c.Debut = DateTime.Parse(nodeCalendrier.SelectSingleNode("Début").InnerText);
                c.Fin = DateTime.Parse(nodeCalendrier.SelectSingleNode("Fin").InnerText);
                c.Day = int.Parse(nodeCalendrier.SelectSingleNode("Jour").InnerText);
                c.Heure = int.Parse(nodeCalendrier.SelectSingleNode("Heure").InnerText);
                c.Minute = int.Parse(nodeCalendrier.SelectSingleNode("Minute").InnerText);
                c.Seconde = int.Parse(nodeCalendrier.SelectSingleNode("Seconde").InnerText);

                XmlNodeList nodelistJours = nodeCalendrier.SelectNodes("Liste_Jour");
                foreach (XmlNode nodeJour in nodelistJours)
                {
                    XmlNodeList nodelistJour = nodeJour.SelectNodes("Jour");
                    foreach (XmlNode jour in nodelistJour)
                    {
                        string CompteRendu = "";
                        int num = 0;
                        bool[] tabHoraires = new bool[147];

                        Jour j = new Jour(CompteRendu, num, tabHoraires);


                        j.CompteRendu = jour.SelectSingleNode("Compte_Rendu").InnerText;
                        j.Numero = int.Parse(jour.SelectSingleNode("Numéro").InnerText);
                        j.Sortie = bool.Parse(jour.SelectSingleNode("Sortie").InnerText);

                        List<Activite> listeActivites = new List<Activite>();


                        XmlNodeList nodelistActivite = jour.SelectNodes("Liste_Activité");
                        foreach (XmlNode nodeActivite in nodelistActivite)
                        {
                            XmlNodeList nodelisteActivite = nodeActivite.SelectNodes("Activité");

                            foreach (XmlNode Activite in nodelisteActivite)
                            {
                                Heure debut = new Heure(0, 0);
                                Heure fin = new Heure(0, 0);
                                Lieu gps = new Lieu();
                                List<Astronaute> listAstronaute = new List<Astronaute>();

                                Activite a = new Activite("");

                                a.Nom = Activite.SelectSingleNode("Nom").InnerText;
                                a.CompteRendu = Activite.SelectSingleNode("Compte_Rendu").InnerText;
                                debut.Heures = int.Parse(Activite.SelectSingleNode("Heure_Debut").InnerText);
                                fin.Heures = int.Parse(Activite.SelectSingleNode("Heure_Fin").InnerText);
                                debut.Minutes = int.Parse(Activite.SelectSingleNode("Minute_Debut").InnerText);
                                fin.Minutes = int.Parse(Activite.SelectSingleNode("Minute_Fin").InnerText);

                                a.Debut = new Heure(debut.Heures, debut.Minutes);
                                a.Fin = new Heure(fin.Heures, fin.Minutes);

                                gps = Lieu.Parse(Activite.SelectSingleNode("Lieu"));

                                List<Astronaute> listAstr = new List<Astronaute>();

                                XmlNodeList nodelistAstronaute = nodeActivite.SelectNodes("Liste_Astronaute");
                                foreach (XmlNode nodeAstronaute in nodelistAstronaute)
                                {
                                    string nomAst = "";
                                    int id = 0;
                                    XmlNodeList nodeAstronautee = nodeAstronaute.SelectNodes("Astronaute");
                                    foreach (XmlNode nodeAstro in nodeAstronautee)
                                    {
                                        id = int.Parse(nodeAstro.SelectSingleNode("Id").InnerText);
                                        nomAst = nodeAstro.SelectSingleNode("Nom").InnerText;
                                        Astronaute ast = new Astronaute(id, nomAst);
                                        listAstr.Add(ast);
                                    }
                                }
                                a.ListAstronaute = listAstr;
                                a.TexteDescriptif = Activite.SelectSingleNode("Texte_Descriptif").InnerText;
                                listeActivites.Add(a);
                            }
                        }

                        j.ListeActivites = listeActivites;

                        XmlNodeList nodelistTabHoraire = jour.SelectNodes("Tableau_Horaire");

                        foreach (XmlNode nodeListeLibre in nodelistTabHoraire)
                        {

                            XmlNodeList nodelistLibre = nodeListeLibre.SelectNodes("Libre");
                            foreach (XmlNode nodeLibre in nodelistLibre)
                            {
                                for (int i = 0; i < 147; i++)
                                {
                                    tabHoraires[i] = bool.Parse(nodeLibre.InnerText);
                                }
                            }
                        }

                        j.TabHoraires = tabHoraires;
                        c.Jours.Add(j);
                    }
                }
            }

            M.Calendar = c;
        }
    }
}