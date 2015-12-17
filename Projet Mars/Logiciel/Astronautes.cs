﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Logiciel
{
    public partial class Astronautes : Form      // à revoir
    {
        private Astronaute s;
        static int Id=0;
        private List<Astronaute> listAstro = new List<Astronaute>();

        public Astronautes()
        {
            InitializeComponent();
            SupprimerAstronaute.Enabled = false;
            AjouterAstronaute.Enabled = false;
            ConfirmerAstronaute.Enabled = false;
        }                    

        public List<Astronaute> Astro()
        {   
            return listAstro;  
        }    

        private void AjouterAstronaute_Click(object sender, EventArgs e)
        {
            Id++;
            Astronaute A = new Astronaute(Id, NomAstronaute.Text);
            listAstro.Add(A);
            AstronautesMission.Items.Add(A);
            NomAstronaute.Text = ""; 
            ConfirmerAstronaute.Enabled = true;
        }

        private void AstronautesMission_SelectedIndexChanged(object sender, EventArgs e)
        {
            SupprimerAstronaute.Enabled = true;
            s = (Astronaute)AstronautesMission.SelectedItem;           
        }

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

        
        private void ConfirmerAstronaute_Click(object sender, EventArgs e)
        {     
            this.Visible = false;
            this.Close();
        }  
    }
}
