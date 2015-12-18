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
    /// <summary>
    /// Fiche de gestion des astronautes de la mission
    /// </summary>

    public partial class Astronautes : Form 
    {
        private Astronaute s;
        private int Id=0;
        private List<Astronaute> listAstro = new List<Astronaute>();
       
        public Astronautes()
        {
            InitializeComponent();
            SupprimerAstronaute.Enabled = false;
            AjouterAstronaute.Enabled = false;
            ConfirmerAstronaute.Enabled = false;
        }

        public Astronautes(List<Astronaute> listeAstro)
        {
            InitializeComponent();
            listAstro = listeAstro;
            foreach (Astronaute a in listeAstro)
            {
                AstronautesMission.Items.Add(a);
                Id = a.Id;
            }            
            SupprimerAstronaute.Enabled = false;
            AjouterAstronaute.Enabled = false;           
        } 

        public List<Astronaute> Astro()
        {   
            return listAstro;  
        }    

        /// <summary>
        /// Ajoute le nom de l'astronaute à la liste et lui attribue un identifiant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterAstronaute_Click(object sender, EventArgs e)
        {
            Id++;
            Astronaute A = new Astronaute(Id, NomAstronaute.Text);
            listAstro.Add(A);
            AstronautesMission.Items.Add(A);
            NomAstronaute.Text = ""; 
            ConfirmerAstronaute.Enabled = true;
        }

        /// <summary>
        /// Active le bouton de suppression à la sélection d'un astronaute
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AstronautesMission_SelectedIndexChanged(object sender, EventArgs e)
        {
            SupprimerAstronaute.Enabled = true;
            s = (Astronaute)AstronautesMission.SelectedItem;           
        }

        /// <summary>
        /// Active le bouton d'ajout d'un astronaute
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NomAstronaute_TextChanged(object sender, EventArgs e)
        {
            if (NomAstronaute.Text != "")
            {
                AjouterAstronaute.Enabled = true;
            }
            else
            {
                AjouterAstronaute.Enabled = false;
            }
        }

        /// <summary>
        /// Supprime l'astronaute de la liste (avec son identifiant)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerAstronaute_Click(object sender, EventArgs e)
        {
            s = (Astronaute)AstronautesMission.SelectedItem;
            listAstro.Remove(s);
            AstronautesMission.Items.Remove(s);
            if (AstronautesMission.Items.Count == 0)
            {
                SupprimerAstronaute.Enabled = false;
                ConfirmerAstronaute.Enabled = false;
            }
        }

        /// <summary>
        /// Ferme la fenêtre de gestion des astronautes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmerAstronaute_Click(object sender, EventArgs e)
        {     
            this.Visible = false;
            this.Close();            
        }
    }
}
