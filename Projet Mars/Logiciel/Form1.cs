using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;


namespace Logiciel
{
    public partial class Form1 : Form
    {
        Calendrier_Martien c = new Calendrier_Martien(); // à charger pour ne pas perdre les infos
        int jourSelec;
        Image sortie = Image.FromFile("..\\..\\..\\..\\astronaut.png");

        Button test;
        bool premClick = false;

        public Form1()
        {
            InitializeComponent();
            // c.MiseAJour();      Pour remettre les pendules à l'heure
            timer1.Start();
            dureMission.Maximum = 500;
            trackBar1.Maximum = 9;
            jour42.BackgroundImage = sortie;           
            c.Day = 12;
            jour42.BackgroundImage = sortie;
        }

        public void CreerBoutons(int n) //n : numéro du jour
        {
            boutonsMatin.Controls.Clear();
            boutonsApresMidi.Controls.Clear();
            Point pointDeBase=new Point(0,0);
            int largeurActivite = 262;
            Jour jourJ = c.Jours.ElementAt(n);

            //recherche du nombre d'activités du jour n dans le calendrier
            for (int i=0; i < jourJ.ListeActivites.Count; i++)
            {
                Button bouton = new System.Windows.Forms.Button();
                Activite Act = jourJ.ListeActivites.ElementAt(i);
                switch (Act.Nom)
                {
                    case "Eating":
                        bouton.BackColor = Color.Bisque;
                        break;
                    case "Sleeping":
                        bouton.BackColor = Color.BurlyWood;
                        break;
                    default :
                        bouton.BackColor = Color.LightCyan;
                        break;
                }//à compléter

                //bouton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                bouton.Cursor = System.Windows.Forms.Cursors.Hand;
                bouton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

                Point localisation=new Point(pointDeBase.X,pointDeBase.Y);

                //On scinde le calendrier selon les cas
                if (Act.Debut.HeuresMinutes < 1220 && Act.Fin.HeuresMinutes<=1220) //Si l'activité commence et se termine avant 12h20 (ou se termine à 12h20)
                {
                    this.boutonsMatin.Controls.Add(bouton);

                    localisation.Y += (Act.Debut.Heures * 6 + Act.Debut.Minutes / 10) * 5; //calcul du nb de tranches de 10min * 5px le matin                  
                    bouton.Size = new System.Drawing.Size(largeurActivite, ((Act.Fin.Heures * 6 + Act.Fin.Minutes / 10) * 5) - localisation.Y -1);
                }
                else //Si l'activité commence et se termine après 12h20
                {
                    this.boutonsApresMidi.Controls.Add(bouton);

                    if ((Act.Debut.Heures * 6 + Act.Debut.Minutes / 10 - 74) * 5 < 0)
                    {
                        localisation.Y += -1;
                    }
                    else
                    {
                        localisation.Y += (Act.Debut.Heures * 6 + Act.Debut.Minutes / 10 - 74) * 5;                 
                    }                    

                    bouton.Size = new System.Drawing.Size(largeurActivite, ((Act.Fin.Heures * 6 + Act.Fin.Minutes / 10-74) * 5) - localisation.Y -1);
                    if (Act.Debut.HeuresMinutes < 1220)
                    {
                        Button bouttonBis = new Button();
                        this.boutonsMatin.Controls.Add(bouttonBis);
                        bouttonBis.Location = new Point(0, (Act.Debut.Heures * 6 + Act.Debut.Minutes / 10) * 5);                   
                        bouttonBis.Size = new System.Drawing.Size(largeurActivite, 370-((Act.Debut.Heures * 6 + Act.Debut.Minutes / 10) * 5) - localisation.Y );
                        bouttonBis.Margin = new System.Windows.Forms.Padding(0);
                        bouttonBis.Name = "ActiviteBis" + i;
                        bouttonBis.TabIndex = i;
                        bouttonBis.Text = Act.Nom;
                        bouttonBis.UseVisualStyleBackColor = false;
                        bouttonBis.Click += new System.EventHandler(this.ClickNiveau3);
                        bouttonBis.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                        bouttonBis.Cursor = System.Windows.Forms.Cursors.Hand;
                        bouttonBis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        bouttonBis.BackColor = bouton.BackColor;
                    }
                }

                bouton.Location = localisation;
                bouton.Margin = new System.Windows.Forms.Padding(0);
                bouton.Name = "Activite" + i;
                bouton.TabIndex = i;
                bouton.Text = Act.Nom;
                bouton.UseVisualStyleBackColor = false;

                bouton.Click += new System.EventHandler(this.ClickNiveau3);
            }
            
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            c.Horloge();
            heureMars.Text = c.Heure.ToString() + " h " + c.Minute.ToString() + " min " + c.Seconde.ToString() + " s";
            nbrJour.Text = c.Day.ToString();
            DateTerrestre.Text = Convert.ToString(DateTime.Now);
            timer1.Start();
        }

        private void nbrJour_TextChanged(object sender, EventArgs e)
        {
            dureMission.Increment(1);
            for (int i = 0; i < groupBox1.Controls.Count; i++)
            {              
                if (groupBox1.Controls[i].Name.Contains("jour"))
                {
                    groupBox1.Controls[i].Text = Convert.ToString((50 * trackBar1.Value) + i - 1);


                    if (int.Parse(groupBox1.Controls[i].Text) < int.Parse(nbrJour.Text))
                    {
                        groupBox1.Controls[i].BackColor = Color.DimGray;
                    }  
                    if (int.Parse(groupBox1.Controls[i].Text) == int.Parse(nbrJour.Text))
                    {
                        groupBox1.Controls[i].BackColor = Color.RoyalBlue;
                    }                    
                }
            }      
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            for (int i = 0; i < groupBox1.Controls.Count; i++)
            {
                if (groupBox1.Controls[i].Name.Contains("jour"))
                {
                    groupBox1.Controls[i].Text = Convert.ToString((50 * trackBar1.Value) + i-1);

                    if (int.Parse(nbrJour.Text) == (50 * trackBar1.Value) + i - 1)
                    {
                        groupBox1.Controls[i].BackColor = Color.RoyalBlue;
                    }
                    else
                    {
                        if (int.Parse(groupBox1.Controls[i].Text) < c.Day)
                        {
                            groupBox1.Controls[i].BackColor = Color.DimGray;
                        }
                        else
                        {
                            groupBox1.Controls[i].BackColor = Color.DarkGreen;
                        }

                    }
                }
            }      
        }

        private void jour_Click(object sender, EventArgs e)
        {           
            if (premClick) 
            {                
                if (int.Parse(test.Text) == c.Day)
                {
                    test.BackColor = Color.RoyalBlue;
                }
                else
                {
                    if (int.Parse(test.Text) < c.Day)
                    {
                        test.BackColor = Color.DimGray;
                    }
                    else
                    {
                        test.BackColor = Color.DarkGreen;
                    }
                }
            }
            Button clickedButton = (Button)sender;
            jourSelec=int.Parse(clickedButton.Text);
            test = clickedButton;           
            premClick = true;

            //modifié par léo
            Niveau2.Visible = true;
            //groupBox1.Visible = false;
            NumeroJour.Text = clickedButton.Text;
            NduJNiv3.Text = NumeroJour.Text;
            CreerBoutons(jourSelec);
        }
     
        private void ClickNiveau3(object sender, EventArgs e)
        {
            //treeView1.Controls[0].Controls.Add(new CheckBox());
            Button clickedButton = (Button)sender;
            
            Activite act = c.Jours.ElementAt(int.Parse(NduJNiv3.Text)).ListeActivites.ElementAt(clickedButton.TabIndex); //activité à l'index i du jour concerné

            

            Niveau3.Visible = true;
            Niveau2.Visible = false;
            TitreNiv3.Text = "Mofifier une activité";
            texteDescriptif.Text = act.TexteDescriptif;
            for (int i = 0; i < listView1.Items.Count; i++) //on décoche tous les astronautes par sécurité
            {
                listView1.Items[i].Checked = false;
            }
            for (int i = 0; i < act.ListAstronaute.Count; i++) //on coche les astronautes pour une activité donnée
            {
                for (int j = 0; j < listView1.Items.Count; j++)
                {
                    if (listView1.Items[j].Text == act.ListAstronaute[i].Nom)
                    {
                        listView1.Items[j].Checked = true;
                    }

                }
                
                        
            }

            ItemSelect.Text= act.Nom;
            HDebut.Enabled = true;
            MinDebut.Enabled = true;
            HFin.Enabled = true;
            MinFin.Enabled = true;
            HDebut.SelectedIndex = act.Debut.Heures;
            MinDebut.SelectedIndex = act.Debut.Minutes/10;
            HFin.SelectedIndex = act.Fin.Heures;
            MinFin.SelectedIndex = act.Fin.Minutes/10;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Niveau3.Visible = false;
            Niveau2.Visible = true;
        }

        private void CreerActivite_Click(object sender, EventArgs e)
        {
            TitreNiv3.Text = "Créer une activité";
            Niveau3.Visible = true;
            Niveau2.Visible = false;

            HDebut.Enabled = true;
            MinDebut.Enabled = false;
            HFin.Enabled = false;
            MinFin.Enabled = false;
            HDebut.SelectedIndex = -1;
            MinDebut.SelectedIndex = -1;
            HFin.SelectedIndex = -1;
            MinFin.SelectedIndex = -1;

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value > 0)
            {
                trackBar1.Value = trackBar1.Value - 1;
            }
            trackBar1_Scroll(sender, e);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value < 9)
            {
                trackBar1.Value = trackBar1.Value + 1;
            }
            trackBar1_Scroll(sender, e);
        }

        private void RetourCalendrier_Click_1(object sender, EventArgs e)
        {
            Niveau2.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void JourPrecedent_Click(object sender, EventArgs e)
        {
            if (int.Parse(NumeroJour.Text) > 1)
            {
                NumeroJour.Text = Convert.ToString(int.Parse(NumeroJour.Text) - 1);
            }
            NduJNiv3.Text = NumeroJour.Text;
            CreerBoutons(int.Parse(NumeroJour.Text));
        }

        private void JourSuivant_Click(object sender, EventArgs e)
        {
            if (int.Parse(NumeroJour.Text) < 500)
            {
                NumeroJour.Text = Convert.ToString(int.Parse(NumeroJour.Text) + 1);
            }
            NduJNiv3.Text = NumeroJour.Text;
            CreerBoutons(int.Parse(NumeroJour.Text));
        }

        private void HDebut_SelectedValueChanged(object sender, EventArgs e)
        {
            if (HDebut.SelectedIndex == 24)
            {
                MinDebut.Items.Remove("40");
                MinDebut.Items.Remove("50");
                MinDebut.SelectedText = "";
            }
            else if(MinDebut.Items.Count==4)
            {
                MinDebut.Items.Add("40");
                MinDebut.Items.Add("50");
            }

            //code à revoir
            //int heureFinMin = 0;
            //if (MinDebut.SelectedIndex == 50)
            //{
            //    heureFinMin = HDebut.SelectedIndex + 1;
            //}
            //else
            //{
            //    heureFinMin = HDebut.SelectedIndex;
            //}
            //for (int i = 0; i < heureFinMin; i++)
            //{
            //    HFin.Items.RemoveAt(i);
            //}
            MinDebut.Enabled = true;
        }

        private void HFin_SelectedValueChanged(object sender, EventArgs e)
        {
            MinFin.Enabled = true;
        }

        private void MinDebut_SelectedIndexChanged(object sender, EventArgs e)
        {
            HFin.Enabled = true;
        }

    

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Level == 1)
            {
                ItemSelect.Text = treeView1.SelectedNode.Text;
            }
            else
            {
                ItemSelect.Text = "";
            }
        }

        private void DateTerrestre_Click(object sender, EventArgs e)
        {

        }

        private void titreJour_Click(object sender, EventArgs e)
        {

        }

        private void texteDescriptif_TextChanged(object sender, EventArgs e)
        {

        }

        

        


       

        
        
        





       
       

       

       


    }
}

