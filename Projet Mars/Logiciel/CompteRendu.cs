using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Logiciel
{
    //Form contenant le compte-rendu accessible depuis le niveau 2 de GestionMission
    public partial class CompteRendu : Form
    {
        public event AjouterEventHandler CR;
        
        public delegate void AjouterEventHandler(string CR);

        public CompteRendu(string CR)
        {
            InitializeComponent();
            texteCR.Text = CR;            
        }

        /// <summary>
        ///Enregistrement des modifications 
        ///Fermeture de la fenêtre et retour au planning de la journée (niveau 2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmerCR_Click(object sender, EventArgs e)
        {          
            this.CR(texteCR.Text);
            this.Close();            
        }

        /// <summary>
        /// Fermeture de la fenêtre sans enregistrement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnnulerCR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
