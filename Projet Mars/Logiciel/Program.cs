using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Logiciel
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {             
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GestionMission());            
        }    
    }
             
    //On crée une structure Heure pour faciliter la gestion de la création des boutons d'activité
    public struct Heure 
    {
        int _heures;
        int _minutes;
        int _heuresMinutes;

        public Heure(int h, int m)
        {
            _heures = h;
            _minutes = m;
            if (m == 0)//Pour les besoins de cet attribut, on double le zéro pour que 8h00 devienne 800 et non 80 
            {
                _heuresMinutes = int.Parse(Convert.ToString(h) + "00");
            }
            else
            {
                _heuresMinutes = int.Parse(Convert.ToString(h) + Convert.ToString(m));
            }
        }


        public int HeuresMinutes
        {
            get { return _heuresMinutes; }
            set { _heuresMinutes = value; }
        }

        public int Heures
        {
            get { return _heures; }
            set { _heures = value; }
        }
        public int Minutes
        {
            get { return _minutes; }
            set { _minutes = value; }
        }
        public override string ToString()
        {
            string res = "" + this.Heures + " H " + this.Minutes + "";
            return res;
        }     
    }
}
