using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public Calendrier_Martien()
        {
            _debut = System.DateTime.Now;
            System.TimeSpan duration = new System.TimeSpan(0, 500 * 1480, 0);
            _fin = System.DateTime.Now + duration;
            _jour = 1;
            _minute = 0;
            _heure = 0;
            _seconde = 0;
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
    }
}
