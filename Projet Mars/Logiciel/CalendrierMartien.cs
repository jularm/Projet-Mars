using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Logiciel
{
    public class CalendrierMartien 
    {
        private int _jour;
        private DateTime _debut;
        private DateTime _fin;
        private DateTime _last;
        private int _minute;
        private int _heure;
        private int _seconde;
        private List<Jour> _Jours = new List<Jour>();

        public CalendrierMartien()
        {
            _debut = System.DateTime.Now;
            System.TimeSpan duration = new System.TimeSpan(0, 500 * 1480, 0);  // calcul le temps que dure 500 jours martien soit 500*1480 minutes
            _fin = System.DateTime.Now + duration;
            _jour = 1;
            _minute = 0;
            _heure = 0;
            _seconde = 0;
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

        public DateTime Fin
        {
            get { return _fin; }
            set { _fin = value; }
        }

        public DateTime Last
        {
            get { return _last; }
            set { _last = value; }
        }

        public DateTime Debut
        {
            get { return _debut; }
            set { _debut = value; }
        }


        public void AddJours(Jour J)
        {
            _Jours.Add(J);
        }


        public void Horloge()// Fonction qui permet d'avancer dans le temps martien de secondes en secondes
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

        public void MiseAJour()// Fonction de mise à niveau de l'horloge martienne 
        {
            TimeSpan Ts = System.DateTime.Now - this._last;   // on calcule l'écart entre le temps du système et le temps où on a arrêter le système          
            double ecart = Math.Round(Ts.TotalSeconds); //  on convertit l'écart en seconde

            for (int i = 0; i < ecart; i++)          // pour chaque seconde d'écart
            {
                Horloge();     // on appel
            }
        }



        // Generation Xml
        public void genereXml(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode NodeCalendrier = xmlDoc.CreateElement("Calendrier_Martien"); // création d'un noeud XML pour le calendrier

            XmlNode NodeDebut = xmlDoc.CreateElement("Début");// création d'un noeud XML pour la date du début du calendrier
            NodeDebut.InnerText = Debut.ToString();// on écrit dans le noeud ce que contient la variable début 
            NodeCalendrier.AppendChild(NodeDebut);// on conclut le noeud début et on l'attache au noeud calendrier

            XmlNode NodeFin = xmlDoc.CreateElement("Fin");
            NodeFin.InnerText = Fin.ToString();
            NodeCalendrier.AppendChild(NodeFin);

            XmlNode NodeLast = xmlDoc.CreateElement("Last");
            NodeLast.InnerText = Last.ToString();
            NodeCalendrier.AppendChild(NodeLast);

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
            XmlNodeList nodelistCalendrier = xmlDoc.GetElementsByTagName("Calendrier_Martien");// on créait une liste de tout les éléments dans tout le document qui porte le nom de Calendrier_Martien

            CalendrierMartien c = new CalendrierMartien();

            foreach (XmlNode nodeCalendrier in nodelistCalendrier)// Pour tout les noeuds présent dans notre liste d'élement (ici on a juste un élément)
            {
                c.Debut = DateTime.Parse(nodeCalendrier.SelectSingleNode("Début").InnerText);// la date de début du calendrier est égal à ce qui est contenu dans le seul noeud portant le nom début
                c.Fin = DateTime.Parse(nodeCalendrier.SelectSingleNode("Fin").InnerText);
                c.Last= DateTime.Parse(nodeCalendrier.SelectSingleNode("Last").InnerText);
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
                        bool[] tabHoraires = new bool[148];

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
                                debut.Heures = int.Parse(Activite.SelectSingleNode("Heure_Debut").InnerText);
                                fin.Heures = int.Parse(Activite.SelectSingleNode("Heure_Fin").InnerText);
                                debut.Minutes = int.Parse(Activite.SelectSingleNode("Minute_Debut").InnerText);
                                fin.Minutes = int.Parse(Activite.SelectSingleNode("Minute_Fin").InnerText);

                                a.Debut = new Heure(debut.Heures, debut.Minutes);
                                a.Fin = new Heure(fin.Heures, fin.Minutes);

                                gps = Lieu.Parse(Activite.SelectSingleNode("Lieu"));
                                a.Gps = gps;

                                List<Astronaute> listAstr = new List<Astronaute>();

                                XmlNodeList nodelistAstronaute = Activite.SelectNodes("Liste_Astronaute");
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
                                int i = 0;
                                foreach (XmlNode nodeLibre in nodelistLibre)
                                {
                                    tabHoraires[i] = bool.Parse(nodeLibre.InnerText);
                                    i++;
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