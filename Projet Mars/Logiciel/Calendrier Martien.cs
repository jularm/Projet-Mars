using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;



namespace Logiciel
{
    class Calendrier_Martien
    {
        private int _jour;
        private DateTime _debut;
        private DateTime _fin;
        private int _minute;
        private int _heure;
        private int _seconde;
        private List<Jour> _Jours = new List<Jour>();

        public Calendrier_Martien()
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
                

        public int Jour
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
                        this.Jour++;
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
                        this.Jour++;
                    }
                }
            } 
        }


        // Generation Xml
        /*    public override void genereXml(XmlDocument xmlDoc, XmlNode rootNode)
           {
               XmlNode NodeChef = xmlDoc.CreateElement("Chef");

               base.genereXml(xmlDoc, NodeChef);

               XmlNode NodeNbrCommis = xmlDoc.CreateElement("nbrCommis");
               NodeNbrCommis.InnerText = NbrCommis.ToString();
               NodeChef.AppendChild(NodeNbrCommis);

               XmlNode NodeNbrCouverts = xmlDoc.CreateElement("nbrCouverts");
               NodeNbrCouverts.InnerText = NbrCouverts.ToString();
               NodeChef.AppendChild(NodeNbrCouverts);


               rootNode.AppendChild(NodeChef);

           }

          // lecture xml et generation objets
           static
           public void chargerXml(XmlDocument xmlDoc, Restaurant R)
           {
               XmlNodeList nodelistListePersonnel = xmlDoc.GetElementsByTagName("ListePersonnel");

               foreach (XmlNode nodelist in nodelistListePersonnel)
               {
                   // les chefs

                   XmlNodeList nodelistChef = nodelist.SelectNodes("Chef");

                   foreach (XmlNode nodeChef in nodelistChef)
                   {
                       int id_chef = int.Parse(nodeChef.SelectSingleNode("ID").InnerText);
                       string nom_chef = nodeChef.SelectSingleNode("Nom").InnerText;
                       string prenom_chef = nodeChef.SelectSingleNode("Prenom").InnerText;
                       int nbrCommis_chef = int.Parse(nodeChef.SelectSingleNode("nbrCommis").InnerText);
                       int nbrCouverts_chef = int.Parse(nodeChef.SelectSingleNode("nbrCouverts").InnerText);


                       int[] _jHTrav_chef = new int[7];


                       XmlNodeList nodelistListeHorraire = nodeChef.SelectNodes("ListeHorraire");
                       foreach (XmlNode nodeListeHorraire in nodelistListeHorraire)
                       {
                           int i = 0;
                           XmlNodeList nodelistHorraire = nodeListeHorraire.SelectNodes("Horraire");
                           foreach (XmlNode nodeHorraire in nodelistHorraire)
                           {
                               _jHTrav_chef[i] = int.Parse(nodeHorraire.InnerText);
                               i++;
                           }

                       }

                       Chef c = new Chef(id_chef, nom_chef, prenom_chef, nbrCommis_chef, nbrCouverts_chef);
                       c._jHTrav = _jHTrav_chef;
                       R.ajout(c);
                   }
               }

           }*/



    }
}
