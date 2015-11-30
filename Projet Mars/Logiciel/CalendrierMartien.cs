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

            for (int i = 0; i < 500; i++)
            {
                _Jours.Add(new Jour(i)) ;
            }
            _Jours.ElementAt(20).ListeActivites.RemoveAt(1);
        }

        public CalendrierMartien(DateTime debut, DateTime fin, int jour, int heure, int minute, int seconde)
        {
            _debut = debut;
            _fin = fin;
            _jour = jour;
            _minute = minute;
            _heure = heure;
            _seconde = seconde;
           
            _Jours.ElementAt(20).ListeActivites.RemoveAt(1);
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
            for (int i = 0; i < Ts.Minutes; i++)
            {
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


        // Generation Xml
            public void genereXml(XmlDocument xmlDoc, XmlNode rootNode)
           {
               XmlNode NodeCalendrier = xmlDoc.CreateElement("Calendrier_Martien");
               
               XmlNode NodeDebut = xmlDoc.CreateElement("Debut");
               NodeDebut.InnerText = NodeDebut.ToString();
               NodeCalendrier.AppendChild(NodeDebut);

               XmlNode NodeFin = xmlDoc.CreateElement("Fin");
               NodeFin.InnerText = NodeFin.ToString();
               NodeCalendrier.AppendChild(NodeFin);

               XmlNode NodeJour = xmlDoc.CreateElement("Jour");
               NodeJour.InnerText = NodeJour.ToString();
               NodeCalendrier.AppendChild(NodeJour);

               XmlNode NodeHeure = xmlDoc.CreateElement("Heure");
               NodeHeure.InnerText = NodeHeure.ToString();
               NodeCalendrier.AppendChild(NodeHeure);

               XmlNode NodeMinute = xmlDoc.CreateElement("Minute");
               NodeMinute.InnerText = NodeMinute.ToString();
               NodeCalendrier.AppendChild(NodeMinute);

               XmlNode NodeSeconde = xmlDoc.CreateElement("Seconde");
               NodeSeconde.InnerText = NodeSeconde.ToString();
               NodeCalendrier.AppendChild(NodeSeconde);

               XmlNode NodeListeJour = xmlDoc.CreateElement("ListeJour");  
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
                XmlNodeList nodelistCalendrier = xmlDoc.GetElementsByTagName("Calendrier_Martien"); // Je récupère une liste des noeuds ayant pour nom Calendrier_MArtien

                DateTime debut = new DateTime();
                DateTime fin = new DateTime();
                int jour = 0;
                int heure = 0;
                int minute = 0;
                int seconde = 0;
                List<Jour> Days = new List<Jour>();

                foreach (XmlNode nodeCalendrier in nodelistCalendrier) // Je boucle dessus ( mais il n'y en a qu'un ici) 
                {
                    // le récupere les données du Calendrier
                    debut = DateTime.Parse(nodeCalendrier.SelectSingleNode("Debut").InnerText);
                    fin = DateTime.Parse(nodeCalendrier.SelectSingleNode("Fin").InnerText);
                    jour = int.Parse(nodeCalendrier.SelectSingleNode("Day").InnerText);
                    heure = int.Parse(nodeCalendrier.SelectSingleNode("Heure").InnerText);
                    minute = int.Parse(nodeCalendrier.SelectSingleNode("Minute").InnerText);
                    seconde = int.Parse(nodeCalendrier.SelectSingleNode("Seconde").InnerText);
                                        
                }

                CalendrierMartien c = new CalendrierMartien(debut, fin, jour, heure, minute, seconde);

               // Jour.chargerXml(xmlDoc, M);
            }



    }
}
