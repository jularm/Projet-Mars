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
    public struct Heure //La structure permet la factorisation de code de la même manière qu'une classe mais en plus léger donc plus utilisable pour des petits éléments
    {
        int _heures;
        int _minutes;
        int _heuresMinutes;

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

        public Heure(int h, int m)
        {
            _heures = h;
            _minutes = m;
            if (m == 0)// pour les besoins de cet attribut, on double le zéro pour que 8h00 devienne 800 et non 80 
            {
                _heuresMinutes = int.Parse(Convert.ToString(h) + "00");
            }
            else
            {
                _heuresMinutes = int.Parse(Convert.ToString(h) + Convert.ToString(m));
            }
        }

        public static Heure Parse(string test)
        {
            int heures = 0;
            int minutes = 0;
            List<string> prout = new List<string>();

            foreach (char c in test)
            {
                prout.Add(c.ToString());
            }

            for (int i = 0; i < prout.Count; i++)
            {
                //
                //
            }
            Heure h = new Heure(heures, minutes);
            return h;
        }

    }
}
