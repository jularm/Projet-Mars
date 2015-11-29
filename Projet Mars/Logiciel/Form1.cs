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
        private Calendrier_Martien c = new Calendrier_Martien(); // à charger pour ne pas perdre les infos
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
            c.Day = 12;
            
        }
        public void MiseAJourCompteRendu(string compteRendu)
        {
            c.Jours[int.Parse(nbrJour.Text)].CompteRendu = compteRendu;
        }

        public void CreerBoutons(int n) //n : numéro du jour, n-1 : emplacement du jour dans la liste
        {
            boutonsMatin.Controls.Clear();
            boutonsApresMidi.Controls.Clear();
            Point pointDeBase=new Point(0,0);
            int largeurActivite = 262;
            Jour jourJ = c.Jours.ElementAt(n-1);

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
                bouton.Margin = new System.Windows.Forms.Padding(0);

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
            for (int i = 0; i < Niveau1.Controls.Count; i++)
            {              
                if (Niveau1.Controls[i].Name.Contains("jour"))
                {
                    Niveau1.Controls[i].Text = Convert.ToString((50 * trackBar1.Value) + i - 1);
                    bool check=false;
                    for (int j = 0; j < c.Jours[(50 * trackBar1.Value) + i-1].ListeActivites.Count; j++)
                    {

                        string nomActivite = c.Jours[(50 * trackBar1.Value) + i-1].ListeActivites[j].Nom;
                        if (nomActivite == "Exploration Space suit" || nomActivite == "Exploration Vehicule" || nomActivite == "Outside experiment")
                        {
                            check=true;
                        }
                    }
                    if(check)
                    {
                        Niveau1.Controls[i+1].BackgroundImage = sortie;
                    }
                    else
                    {
                        Niveau1.Controls[i+1].BackgroundImage = null;
                    }

                    if (int.Parse(Niveau1.Controls[i].Text) < int.Parse(nbrJour.Text))
                    {
                        Niveau1.Controls[i].BackColor = Color.DimGray;
                    }  
                    if (int.Parse(Niveau1.Controls[i].Text) == int.Parse(nbrJour.Text))
                    {
                        Niveau1.Controls[i].BackColor = Color.RoyalBlue;
                    }                    
                }
            }      
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            nbrJour_TextChanged(new Object(), new EventArgs());
            for (int i = 0; i < Niveau1.Controls.Count; i++)
            {
                if (Niveau1.Controls[i].Name.Contains("jour"))
                {
                    Niveau1.Controls[i].Text = Convert.ToString((50 * trackBar1.Value) + i-1);

                    if (int.Parse(nbrJour.Text) == (50 * trackBar1.Value) + i - 1)
                    {
                        Niveau1.Controls[i].BackColor = Color.RoyalBlue;
                    }
                    else
                    {
                        if (int.Parse(Niveau1.Controls[i].Text) < c.Day)
                        {
                            Niveau1.Controls[i].BackColor = Color.DimGray;
                        }
                        else
                        {
                            Niveau1.Controls[i].BackColor = Color.DarkGreen;
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

            
            Niveau2.Show();
            Niveau1.Hide();
            
            
            NumeroJour.Text = clickedButton.Text;
            NduJNiv3.Text = NumeroJour.Text;
            if (int.Parse(NumeroJour.Text) < int.Parse(nbrJour.Text))
            {
                CreerActivite.Enabled = false;
            }
            else
            {
                CreerActivite.Enabled = true;
            }
            CreerBoutons(jourSelec);
        }
     
        private void ClickNiveau3(object sender, EventArgs e)
        {
            //treeView1.Controls[0].Controls.Add(new CheckBox());
            Button clickedButton = (Button)sender;
            
            Activite act = c.Jours.ElementAt(int.Parse(NduJNiv3.Text)-1).ListeActivites.ElementAt(clickedButton.TabIndex); //activité à l'index i du jour concerné
            labelInvisible.Text = Convert.ToString(clickedButton.TabIndex);

            Niveau2.Hide();

            Niveau3.Show();
            
            

            TitreNiv3.Text = "Modifier une activité";
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
           
            HDebut.SelectedIndex = act.Debut.Heures;
            MinDebut.SelectedIndex = act.Debut.Minutes/10;
            HFin.SelectedIndex = act.Fin.Heures;
            MinFin.SelectedIndex = act.Fin.Minutes/10;
            CoordX.Text = Convert.ToString(act.Gps.Coords.X);
            CoordY.Text = Convert.ToString(act.Gps.Coords.Y);
            NomLieu.Text = act.Gps.Nom;

            if (ItemSelect.Text == "Exploration Space suit" || ItemSelect.Text == "Exploration Vehicule" ||ItemSelect.Text == "Outside experiment")
            {
                CoordX.Enabled = true;
                CoordY.Enabled = true;
                NomLieu.Enabled = true;
            }
            else
            {
                CoordX.Enabled = false;
                CoordY.Enabled = false;
                NomLieu.Enabled = false;
                CoordX.Text = "0";
                CoordY.Text = "0";
                NomLieu.Text = "Base";
            }



            SupprimerNiv3.Visible = true;
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Niveau3.Hide();
            Niveau2.Show();
        }

        private void CreerActivite_Click(object sender, EventArgs e)
        {
            TitreNiv3.Text = "Créer une activité";
            SupprimerNiv3.Visible = false;

            HDebut.SelectedIndex = -1;
            MinDebut.SelectedIndex = -1;
            HFin.SelectedIndex = -1;
            MinFin.SelectedIndex = -1;

            HDebut.Enabled = true;
            MinDebut.Enabled = false;
            HFin.Enabled = false;
            MinFin.Enabled = false;
            
            ItemSelect.Text = "";
            for(int i=0;i<listView1.Items.Count;i++)
            {
                listView1.Items[i].Checked = false;
            }
            CoordX.Text = "";
            CoordY.Text = "";
            texteDescriptif.Text = "";


            Niveau3.Show();
            Niveau2.Hide();

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
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
            nbrJour_TextChanged(new Object(),new EventArgs());
            Niveau2.Hide();
            Niveau1.Show();
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
            if (int.Parse(NumeroJour.Text) < int.Parse(nbrJour.Text))
            {
                CreerActivite.Enabled = false;
            }
            else
            {
                CreerActivite.Enabled = true;
            }
        }

        private void JourSuivant_Click(object sender, EventArgs e)
        {
            if (int.Parse(NumeroJour.Text) < 500)
            {
                NumeroJour.Text = Convert.ToString(int.Parse(NumeroJour.Text) + 1);
            }
            NduJNiv3.Text = NumeroJour.Text;
            CreerBoutons(int.Parse(NumeroJour.Text));
            if (int.Parse(NumeroJour.Text) < int.Parse(nbrJour.Text))
            {
                CreerActivite.Enabled = false;
            }
            else
            {
                CreerActivite.Enabled = true;
            }
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
                if (ItemSelect.Text == "Exploration Space suit" || ItemSelect.Text == "Exploration Vehicule")
                {
                    CoordX.Enabled = true;
                    CoordY.Enabled = true;
                    NomLieu.Enabled = true;
                }
                else
                {
                    CoordX.Enabled = false;
                    CoordY.Enabled = false;
                    NomLieu.Enabled = false;
                    CoordX.Text = "0";
                    CoordY.Text = "0";
                    NomLieu.Text = "Base";
                }
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

      

        private void ConfirmerNiv3_Click(object sender, EventArgs e)
        {
            bool[] tab=c.Jours[int.Parse(NduJNiv3.Text)-1].TabHoraires;
            bool[] erreurs = {true, false, false, false, false};
            Jour jourj = c.Jours[int.Parse(NduJNiv3.Text) - 1];
            Activite act= new Activite();


            if (TitreNiv3.Text == "Modifier une activité")
            {
                act = jourj.ListeActivites[int.Parse(labelInvisible.Text)];
                changerUnePlageHoraire(act, jourj.TabHoraires, true);
            }
            

            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i])
                {
                    erreurs[1] = true;
                }
            }

            if (ItemSelect.Text != "")
            {
                erreurs[4] = true;
            }

            if (HDebut.Text!="" && MinDebut.Text!="" && HFin.Text!="" && MinFin.Text!="" && (int.Parse(HDebut.Text) * 6 + int.Parse(MinDebut.Text)/10 <= int.Parse(HFin.Text) * 6 + int.Parse(MinFin.Text) / 10 - 1))
            {
                erreurs[2] = true;

                for (int i = int.Parse(HDebut.Text) * 6 + int.Parse(MinDebut.Text) / 10; i < int.Parse(HFin.Text) * 6 + int.Parse(MinFin.Text) / 10 - 1; i++)
                {
                    if (!tab[i])// si tab[i] est false, alors au moins un des emplacements demandés n'est pas disponible
                    {
                        erreurs[0] = false;
                    }
                }
            }

            if (CoordX.Text != "" && CoordY.Text != "")
            {
                erreurs[3] = true;
            }
            if (erreurs[0] && erreurs[1] && erreurs[2] && erreurs[3] && erreurs[4])
            {


                List<Astronaute> liA = new List<Astronaute>();
                for (int i = 0; i < listView1.CheckedItems.Count; i++)
                {
                    liA.Add(new Astronaute(i, listView1.CheckedItems[i].Text));
                }
                if (TitreNiv3.Text == "Modifier une activité")
                {
                    
                    act.Nom = ItemSelect.Text;
                    act.Debut = new Heure(int.Parse(HDebut.Text), int.Parse(MinDebut.Text));
                    act.Fin = new Heure(int.Parse(HFin.Text), int.Parse(MinFin.Text));

                    act.TexteDescriptif = texteDescriptif.Text;
                    act.ListAstronaute = liA;
                    act.Gps = new Lieu(NomLieu.Text, new Point(int.Parse(CoordX.Text), int.Parse(CoordY.Text)));
                    
                }
                else
                {
                    act = new Activite(ItemSelect.Text, new Heure(int.Parse(HDebut.Text), int.Parse(MinDebut.Text)), new Heure(int.Parse(HFin.Text), int.Parse(MinFin.Text)), texteDescriptif.Text, liA, new Lieu(NomLieu.Text, new Point(int.Parse(CoordX.Text), int.Parse(CoordY.Text))));
                    jourj.ListeActivites.Add(act);
                    

                }

                changerUnePlageHoraire(act, jourj.TabHoraires, false);
                CreerBoutons(int.Parse(NduJNiv3.Text));
                Niveau3.Hide();
                Niveau2.Show();
                if (int.Parse(NumeroJour.Text) < int.Parse(nbrJour.Text))
                {
                    CreerActivite.Enabled = false;
                }
                else
                {
                    CreerActivite.Enabled = true;
                }

            }
            else if (!erreurs[4])
            {

                MessageBox.Show("Veuillez sélectionner une activité", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!erreurs[3])
            {
                
                MessageBox.Show("Les coordonnées rentrées ne sont pas valides", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!erreurs[2])
            {
                
                MessageBox.Show("La plage horaire selectionnée n'est pas valide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!erreurs[1])
            {
                
                MessageBox.Show("Aucune plage horaire disponible dans la journée, supprimez d'abord des activités", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!erreurs[0])
            {
                
                MessageBox.Show("La plage horaire selectionnée n'est pas disponible", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            
        }

        private void changerUnePlageHoraire(Activite act, bool[] tab, bool disponible) //à commenter
        {
            for (int i = act.Debut.Heures * 6 + act.Debut.Minutes / 10; i < act.Fin.Heures * 6 + act.Fin.Minutes / 10-1; i++)
                {
                    tab[i] = disponible;
                }
        }

        private void SupprimerNiv3_Click(object sender, EventArgs e)
        {
            Activite act = c.Jours[int.Parse(NduJNiv3.Text)-1].ListeActivites[int.Parse(labelInvisible.Text)];
            c.Jours[int.Parse(NduJNiv3.Text)-1].ListeActivites.RemoveAt(int.Parse(labelInvisible.Text));

            changerUnePlageHoraire(act, c.Jours[int.Parse(NduJNiv3.Text) - 1].TabHoraires, true);

            CreerBoutons(int.Parse(NduJNiv3.Text)); //on réactualise les boutons du niveau 2 et donc les indices de la liste d'activités

            Niveau2.Show();
           
            Niveau3.Hide();
        }

        private void CompteRenduJour_Click(object sender, EventArgs e)
        {
            CompteRendu cr = new CompteRendu(c.Jours[int.Parse(nbrJour.Text)].CompteRendu);
            cr.CR += new CompteRendu.AjouterJourEventHandler(this.MiseAJourCompteRendu);
            cr.ShowDialog();
        }

        private void CreerActivite_EnabledChanged(object sender, EventArgs e)
        {
            //if (!CreerActivite.Enabled)
            //{
            //    HDebut.Enabled = false;
            //    MinDebut.Enabled = false;
            //    HFin.Enabled = false;
            //    MinFin.Enabled = false;
            //    texteDescriptif.Enabled = false;
            //    treeView1.Enabled = false;
            //    CoordX.Enabled = false;
            //    CoordY.Enabled = false;
            //    listView1.Enabled = false;
            //    NomLieu.Enabled = false;
            //}
            //else
            //{
            //    HDebut.Enabled = true;
            //    MinDebut.Enabled = true;
            //    HFin.Enabled = true;
            //    MinFin.Enabled = true;
            //    texteDescriptif.Enabled = true;
            //    treeView1.Enabled = true;
            //    CoordX.Enabled = true;
            //    CoordY.Enabled = true;
            //    listView1.Enabled = true;
            //    NomLieu.Enabled = true;
            //}
        }

        

        


        

        


       

        
        
        





       
       

       

       


    }
}

