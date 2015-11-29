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
    public partial class CompteRendu : Form
    {

        public event AjouterJourEventHandler CR;
        
        public delegate void AjouterJourEventHandler(string CR);

        public CompteRendu(string CR)
        {
            InitializeComponent();
            texteCR.Text = CR;
            
        }

        private void ConfirmerCR_Click(object sender, EventArgs e)
        {
            
            this.CR(texteCR.Text);
            this.Close();
            
        }

        private void AnnulerCR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
