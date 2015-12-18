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
    public partial class recherche : Form
    {
        Mission refer = new Mission();
        int choixJ = 0;

        public int choixj()
        {
            return choixJ;
        }
        
        public recherche(List<Astronaute> LAstr, List<CategorieActivite> LCat, CalendrierMartien C)
        {
            InitializeComponent();
            refer.ListAstr = LAstr;
            refer.Listcat = LCat;
            refer.Calendar = C;

            foreach (CategorieActivite c in refer.Listcat)
            {
                for (int a=0;a<c.ListActivite.Count;a++)
                {
                    comboBox1.Items.Add(c.ListActivite[a]);
                }
            }

            foreach (Astronaute a in refer.ListAstr)
            {  
                comboBox2.Items.Add(a);                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Enabled == true && comboBox2.Enabled == true)
            {
                MessageBox.Show("Veuillez entrer un champ de recherche", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {   
                if (comboBox1.Enabled == true)
                {

                    for (int i = 0; i < refer.Calendar.Jours.Count(); i++)
                    {
                        for (int j = 0; j < refer.Calendar.Jours[i].ListeActivites.Count; j++)
                        {
                            if (refer.Calendar.Jours[i].ListeActivites[j].Nom == comboBox1.SelectedItem.ToString())
                            {
                                string aff = "Jour " + refer.Calendar.Jours[i].Numero + ", Début :" + refer.Calendar.Jours[i].ListeActivites[j].Debut.ToString() + ", Fin :" + refer.Calendar.Jours[i].ListeActivites[j].Fin.ToString() + "";
                                aff = aff + " Astronautes Concernés : ";
                                for (int k = 0; k < refer.Calendar.Jours[i].ListeActivites[j].ListAstronaute.Count(); k++)
                                {
                                    aff = aff + refer.Calendar.Jours[i].ListeActivites[j].ListAstronaute[k].Nom + ", ";
                                }
                                listBox1.Items.Add(aff);
                            }
                        }
                    }                    
                }

                if (comboBox2.Enabled == true)
                {
                    for (int i = 0; i < refer.Calendar.Jours.Count(); i++)
                    {
                        for (int j = 0; j < refer.Calendar.Jours[i].ListeActivites.Count; j++)
                        {
                            for (int k = 0; k < refer.Calendar.Jours[i].ListeActivites[j].ListAstronaute.Count(); k++)
                            {
                                if (refer.Calendar.Jours[i].ListeActivites[j].ListAstronaute[k].Nom == comboBox2.SelectedItem.ToString())
                                {
                                    string aff = "Jour " + refer.Calendar.Jours[i].Numero + " Activitée :"+refer.Calendar.Jours[i].ListeActivites[j].Nom +", Début :" + refer.Calendar.Jours[i].ListeActivites[j].Debut.ToString() + ", Fin :" + refer.Calendar.Jours[i].ListeActivites[j].Fin.ToString() + "";
                                    listBox1.Items.Add(aff);
                                }
                            }
                        }
                    }
                }
            }
        }
                

        private void button2_Click(object sender, EventArgs e)
        {                    
            comboBox1.Enabled = true;
            comboBox1.SelectedItem = null;
            comboBox2.Enabled = true;
            comboBox2.SelectedItem = null;
            listBox1.Items.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {                
                comboBox2.Enabled = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {               
                comboBox1.Enabled = false;
            }
        }

        private void recherche_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

       
    }
}
        



               