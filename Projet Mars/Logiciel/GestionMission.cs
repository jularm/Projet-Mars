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
    public partial class GestionMission : Form
    {

        private Mission M = new Mission();

        XmlDocument xmlDoc = new XmlDocument();
        XmlDocument xmlDoc2 = new XmlDocument();

        int jourSelec;

        Image sortie = Image.FromFile("..\\..\\..\\..\\astronaut.png");
        Image scaphandrePasse = Image.FromFile("..\\..\\..\\..\\astronautGray.png");
        Image scaphandreFutur = Image.FromFile("..\\..\\..\\..\\astronautGreen.png");
        Image vehiculePasse = Image.FromFile("..\\..\\..\\..\\MarsVehiculeGray.png");
        Image vehiculeFutur = Image.FromFile("..\\..\\..\\..\\MarsVehiculeGreen.png");
        Image experiencePasse = Image.FromFile("..\\..\\..\\..\\chemicalGray.png");
        Image experienceFutur = Image.FromFile("..\\..\\..\\..\\chemicalGreen.png");

        Point coordBase = new Point(Convert.ToInt32(Math.Round(90*37.9)), Convert.ToInt32(Math.Round(129*37.9))); //origine du repère carte niveau 3
        //Point coordBase = new Point(90, 129);
        Graphics croix;

        Button test;
        bool premClick = false;


        public GestionMission()
        {
            InitializeComponent();
            try
            {
                // ici lecture de l'xml et generation des objets 
                xmlDoc.Load(@"..\\..\\..\\sauvegarde1.xml");                
                M.chargerXml(xmlDoc, M);

                xmlDoc2.Load(@"..\\..\\..\\sauvegarde2.xml");
                M.chargerXml2(xmlDoc2, M);

                M.Calendar.MiseAJour();      //Pour remettre les pendules à l'heure 
            }
            catch
            {
                MessageBox.Show("Initialisation Mission", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CalendrierMartien c = new CalendrierMartien();

                List<Activite> VieCourante_act = new List<Activite>();
                Activite manger = new Activite("Eating");
                Activite dormir = new Activite("Sleeping");
                Activite divertissement = new Activite("Entertainment");
                Activite privé = new Activite("Private");
                Activite ctrlSante = new Activite("Health control");
                Activite actmedical = new Activite("Medical act");
                VieCourante_act.Add(manger);
                VieCourante_act.Add(dormir);
                VieCourante_act.Add(divertissement);
                VieCourante_act.Add(privé);
                VieCourante_act.Add(ctrlSante);
                VieCourante_act.Add(actmedical);
                CategorieActivite VieCourante = new CategorieActivite("Living", VieCourante_act);

                List<Activite> Science_act = new List<Activite>();
                Activite exploCost = new Activite("Exploration Space suit");
                Activite exploVeh = new Activite("Exploration Vehicule");
                Activite briefing = new Activite("Briefing");
                Activite debriefing = new Activite("Debriefing");
                Activite Expint = new Activite("Inside experiment");
                Activite Expext = new Activite("Outside experiment");
                Science_act.Add(exploCost);
                Science_act.Add(exploVeh);
                Science_act.Add(briefing);
                Science_act.Add(debriefing);
                Science_act.Add(Expint);
                Science_act.Add(Expext);
                CategorieActivite Science = new CategorieActivite("Science", Science_act);

                List<Activite> Maintenance_act = new List<Activite>();
                Activite nettoyage = new Activite("Cleaning");
                Activite LSSair = new Activite("LSS air system");
                Activite LSSeau = new Activite("LSS water system");
                Activite LSSnour = new Activite("LSS food system");
                Activite powsyst = new Activite("Power systems");
                Activite comb = new Activite("Space suit");
                Activite autre = new Activite("Other");
                Maintenance_act.Add(nettoyage);
                Maintenance_act.Add(LSSair);
                Maintenance_act.Add(LSSeau);
                Maintenance_act.Add(LSSnour);
                Maintenance_act.Add(powsyst);
                Maintenance_act.Add(comb);
                Maintenance_act.Add(autre);
                CategorieActivite Maintenance = new CategorieActivite("Maintenance", Maintenance_act);

                List<Activite> Communication_act = new List<Activite>();
                Activite RecMess = new Activite("Sending message");
                Activite EncMess = new Activite("Receiving message");
                Communication_act.Add(RecMess);
                Communication_act.Add(EncMess);
                CategorieActivite Communication = new CategorieActivite("Communication", Communication_act);

                List<Activite> Reparation_act = new List<Activite>();
                Activite LSS = new Activite("LSS");
                Activite communication = new Activite("Communication");
                Activite propSyst = new Activite("Propulsion systems");
                Activite habitation = new Activite("Habitat");
                Activite vehicule = new Activite("Vehicule");
                Reparation_act.Add(LSS);
                Reparation_act.Add(powsyst);
                Reparation_act.Add(communication);
                Reparation_act.Add(propSyst);
                Reparation_act.Add(habitation);
                Reparation_act.Add(comb);
                Reparation_act.Add(vehicule);
                CategorieActivite Reparation = new CategorieActivite("Reparation", Reparation_act);

                List<Activite> Emergency_act = new List<Activite>();
                Activite urgence = new Activite("Emergency");
                Emergency_act.Add(urgence);
                CategorieActivite Urgence = new CategorieActivite("Emergency");

                //à automatiser
                Astronaute A = new Astronaute(1, "Pierre");
                Astronaute B = new Astronaute(2, "Paul");
                Astronaute C = new Astronaute(3, "Jack");
                Astronaute D = new Astronaute(4, "Phoebé");

                //Astronautes.Show();

                for (int i = 1; i < 501; i++)
                {
                    Jour j = new Jour(i);
                    //Journée type par défaut :  
                    j.AddAct(new Activite("Sleeping", new Heure(0, 0), new Heure(7, 0), "Dormir c'est important"));
                    j.AddAct(new Activite("Eating", new Heure(7, 0), new Heure(8, 0), "Manger c'est important"));
                    j.AddAct(new Activite("Private", new Heure(8, 0), new Heure(12, 0), ""));
                    j.AddAct(new Activite("Eating", new Heure(12, 0), new Heure(14, 0), "Manger c'est important"));
                    j.AddAct(new Activite("Private", new Heure(14, 0), new Heure(19, 0), ""));
                    j.AddAct(new Activite("Eating", new Heure(19, 0), new Heure(21, 0), "Manger c'est important"));
                    j.AddAct(new Activite("Private", new Heure(21, 0), new Heure(23, 0), ""));
                    j.AddAct(new Activite("Sleeping", new Heure(23, 0), new Heure(24, 40), "Dormir c'est important"));

                    c.AddJours(j);
                }
                


                M.Calendar = c;
                M.AddCategorie(VieCourante);
                M.AddCategorie(Science);
                M.AddCategorie(Maintenance);
                M.AddCategorie(Communication);
                M.AddCategorie(Reparation);
                M.AddCategorie(Urgence);                               

                M.AddAstronaute(A);
                M.AddAstronaute(B);
                M.AddAstronaute(C);
                M.AddAstronaute(D);               
            }           

            //création 
            timer1.Start();
            dureMission.Maximum = 500;
            trackBar1.Maximum = 9;
            trackBar1_Scroll(new Object(), new EventArgs());
        }


        private void GestionMission_FormClosing(object sender, FormClosingEventArgs e)
        {
            // generation xml 
            xmlDoc = new XmlDocument();
            xmlDoc2 = new XmlDocument();            

            try
            {                
                M.genereXml(xmlDoc);
                xmlDoc.Save(@"..\\..\\..\\sauvegarde1.xml");                     
            }
            catch
            {
                MessageBox.Show("Echec sauvegarde 1 !", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            try
            {
                M.genereXml2(xmlDoc2);
                xmlDoc2.Save(@"..\\..\\..\\sauvegarde2.xml");
            }
            catch
            {
                MessageBox.Show("Echec sauvegarde 2 !", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

                       
        }

        /// <summary>
        /// Permet de remplacer le compte rendu précédent par le nouveau inscrit dans le Form "CompteRendu"
        /// </summary>
        /// <param name="compteRendu"></param>
        public void MiseAJourCompteRendu(string compteRendu)
        {
            M.Calendar.Jours[int.Parse( NumeroJour.Text)].CompteRendu = compteRendu;
        }

        /// <summary>
        /// Création d'un bouton par activité pour chaque jour
        /// </summary>
        /// <param name="n"></param>
        public void CreerBoutons(int n) //n : numéro du jour, n-1 : emplacement du jour dans la liste
        {
            boutonsMatin.Controls.Clear();
            boutonsApresMidi.Controls.Clear();
            Point pointDeBase = new Point(0, 0); //Point servant à la localisation des boutons
            int largeurActivite = 262; //Largeur fixée
            Jour jourJ = M.Calendar.Jours.ElementAt(n - 1);

            //Recherche du nombre d'activités du jour n dans le calendrier
            for (int i = 0; i < jourJ.ListeActivites.Count; i++)
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
                    case "Entertainment":
                        bouton.BackColor = Color.Peru;
                        break;
                    case "Private":
                        bouton.BackColor = Color.SandyBrown;
                        break;
                    case "Health control":
                        bouton.BackColor = Color.Chocolate;
                        break;
                    case "Medical act":
                        bouton.BackColor = Color.NavajoWhite;
                        break;
                    case "Exploration Space suit":
                        bouton.BackColor = Color.CornflowerBlue;
                        break;
                    case "Exploration Vehicule":
                        bouton.BackColor = Color.LightSteelBlue;
                        break;
                    case "Briefing":
                        bouton.BackColor = Color.RoyalBlue;
                        break;
                    case "Debriefing":
                        bouton.BackColor = Color.SteelBlue;
                        break;
                    case "Inside experiment":
                        bouton.BackColor = Color.SkyBlue;
                        break;
                    case "Outside experiment":
                        bouton.BackColor = Color.LightSteelBlue;
                        break;
                    case "Cleaning":
                        bouton.BackColor = Color.LightGray;
                        break;
                    case "LSS air system":
                        bouton.BackColor = Color.Silver;
                        break;
                    case "LSS water system":
                        bouton.BackColor = Color.DarkGray;
                        break;
                    case "LSS food system":
                        bouton.BackColor = Color.Gainsboro;
                        break;
                    case "Power systems":
                        bouton.BackColor = Color.LightSlateGray;
                        break;
                    case "Space suit":
                        bouton.BackColor = Color.Gray;
                        break;
                    case "Other":
                        bouton.BackColor = Color.WhiteSmoke;
                        break;
                    case "Sending message":
                        bouton.BackColor = Color.DarkKhaki;
                        break;
                    case "Receiving message":
                        bouton.BackColor = Color.Khaki;
                        break;
                    case "LSS":
                        bouton.BackColor = Color.Thistle;
                        break;
                    case "Communication systems":
                        bouton.BackColor = Color.Plum;
                        break;
                    case "Propulsion systems":
                        bouton.BackColor = Color.Pink;
                        break;
                    case "Habitat":
                        bouton.BackColor = Color.LightPink;
                        break;
                    case "Vehicule":
                        bouton.BackColor = Color.PaleVioletRed;
                        break;
                    case "Emergency":
                        bouton.BackColor = Color.Firebrick;
                        break;
                    default:
                        bouton.BackColor = Color.Snow;
                        break;
                }
                bouton.Cursor = System.Windows.Forms.Cursors.Hand;
                bouton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                bouton.Margin = new System.Windows.Forms.Padding(0);

                Point localisation = new Point(pointDeBase.X, pointDeBase.Y);

                //On scinde le calendrier selon les cas avant 12h20 et après 12h20
                if (Act.Debut.HeuresMinutes < 1220 && Act.Fin.HeuresMinutes <= 1220) //Si l'activité commence et se termine avant 12h20 (ou se termine à 12h20)
                {
                    this.boutonsMatin.Controls.Add(bouton);

                    localisation.Y += (Act.Debut.Heures * 6 + Act.Debut.Minutes / 10) * 5; //calcul du nb de tranches de 10min * 5px le matin                  
                    bouton.Size = new System.Drawing.Size(largeurActivite, ((Act.Fin.Heures * 6 + Act.Fin.Minutes / 10) * 5) - localisation.Y - 1);
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

                    bouton.Size = new System.Drawing.Size(largeurActivite, ((Act.Fin.Heures * 6 + Act.Fin.Minutes / 10 - 74) * 5) - localisation.Y - 1);
                    if (Act.Debut.HeuresMinutes < 1220)
                    {
                        Button bouttonBis = new Button();
                        this.boutonsMatin.Controls.Add(bouttonBis);
                        bouttonBis.Location = new Point(0, (Act.Debut.Heures * 6 + Act.Debut.Minutes / 10) * 5);
                        bouttonBis.Size = new System.Drawing.Size(largeurActivite, 370 - ((Act.Debut.Heures * 6 + Act.Debut.Minutes / 10) * 5) - localisation.Y);
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
            M.Calendar.Horloge();
            heureMars.Text = M.Calendar.Heure.ToString() + " h " + M.Calendar.Minute.ToString() + " min " + M.Calendar.Seconde.ToString() + " s";
            JourCourantMission.Text = M.Calendar.Day.ToString();
            DateTerrestre.Text = Convert.ToString(DateTime.Now);
            timer1.Start();
        }


        private void JourCourantMission_TextChanged(object sender, EventArgs e)
        {
            dureMission.Increment(1);
            for (int i = 0; i < Niveau1.Controls.Count; i++)
            {
                if (Niveau1.Controls[i].Name.Contains("jour"))
                {
                    Niveau1.Controls[i].Text = Convert.ToString((50 * trackBar1.Value) + i +1);
                    /*bool check = false;
                    for (int j = 0; j < M.Calendar.Jours[(50 * trackBar1.Value) + i +1].ListeActivites.Count; j++)
                    {
                        string nomActivite = M.Calendar.Jours[(50 * trackBar1.Value) + i +1].ListeActivites[j].Nom;
                        if (nomActivite == "Exploration Space suit" || nomActivite == "Exploration Vehicule" || nomActivite == "Outside experiment")
                        {
                            check = true;
                        }
                    }
                    if (check)
                    {
                        Niveau1.Controls[i + 1].BackgroundImage = sortie;
                    }
                    else
                    {
                        Niveau1.Controls[i + 1].BackgroundImage = null;
                    }*/

                    if (int.Parse(Niveau1.Controls[i].Text) < int.Parse(JourCourantMission.Text))
                    {
                        Niveau1.Controls[i].BackColor = Color.DimGray;
                    }
                    if (int.Parse(Niveau1.Controls[i].Text) == int.Parse(JourCourantMission.Text))
                    {
                        Niveau1.Controls[i].BackColor = Color.RoyalBlue;
                    }
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
          //  JourCourantMission_TextChanged(new Object(), new EventArgs());           
            for (int i = 0; i < Niveau1.Controls.Count; i++)
            {
                if (Niveau1.Controls[i].Name.Contains("jour"))
                {
                    Niveau1.Controls[i].Text = Convert.ToString((50 * trackBar1.Value) + i +1);

                    if (int.Parse(JourCourantMission.Text) == (50 * trackBar1.Value) + i +1)
                    {
                        Niveau1.Controls[i].BackColor = Color.RoyalBlue;
                    }
                    else
                    {
                        if (int.Parse(Niveau1.Controls[i].Text) < M.Calendar.Day)
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
           for (int i = 50 * trackBar1.Value; i < 50 * (trackBar1.Value + 1); i++)
            {
                if (Niveau1.Controls[i - 50 * trackBar1.Value].Name.Contains("jour"))
                {
                    if (M.Calendar.Jours[i].Sortie == true)
                    {
                        Niveau1.Controls[i - 50 * trackBar1.Value].BackgroundImage = sortie;
                    }
                    else
                    {
                        Niveau1.Controls[i - 50 * trackBar1.Value].BackgroundImage = null;
                    }

                }
            }
        }

        private void jour_Click(object sender, EventArgs e)
        {
            if (premClick)
            {
                if (int.Parse(test.Text) == M.Calendar.Day)
                {
                    test.BackColor = Color.RoyalBlue;
                }
                else
                {
                    if (int.Parse(test.Text) < M.Calendar.Day)
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
            jourSelec = int.Parse(clickedButton.Text);
            test = clickedButton;
            premClick = true;

            Niveau2.Show();
            Niveau1.Hide();

            NumeroJour.Text = clickedButton.Text;
            NduJNiv3.Text = NumeroJour.Text;
            if (int.Parse(NumeroJour.Text) < int.Parse(JourCourantMission.Text))
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
           
            Button clickedButton = (Button)sender; //cette fonction est appelée à chaque clique sur un bouton du planning, le sender fait référence au bouton sur lequel on a cliqué

            Activite act = M.Calendar.Jours.ElementAt(int.Parse(NduJNiv3.Text) - 1).ListeActivites.ElementAt(clickedButton.TabIndex); //activité à l'index i du jour concerné
            labelInvisible.Text = Convert.ToString(clickedButton.TabIndex);

            //Niveau2.Hide();
            Niveau3.Show();

            TitreNiv3.Text = "Modifier une activité";
            texteDescriptif.Text = act.TexteDescriptif;
            for (int i = 0; i < listeAstronautes.Items.Count; i++) //on décoche tous les astronautes par sécurité
            {
                listeAstronautes.Items[i].Checked = false;
            }
            for (int i = 0; i < act.ListAstronaute.Count; i++) //on coche les astronautes pour une activité donnée
            {
                for (int j = 0; j < listeAstronautes.Items.Count; j++)
                {
                    if (listeAstronautes.Items[j].Text == act.ListAstronaute[i].Nom)
                    {
                        listeAstronautes.Items[j].Checked = true;
                    }
                }
            }

            ItemSelect.Text = act.Nom;

            HDebut.SelectedIndex = act.Debut.Heures;
            MinDebut.SelectedIndex = act.Debut.Minutes / 10;
            HFin.SelectedIndex = act.Fin.Heures;
            MinFin.SelectedIndex = act.Fin.Minutes / 10;
            CoordX.Text = Convert.ToString(act.Gps.Coords.X);
            CoordY.Text = Convert.ToString(act.Gps.Coords.Y);
            NomLieu.Text = act.Gps.Nom;

            if (ItemSelect.Text == "Exploration Space suit" || ItemSelect.Text == "Exploration Vehicule" || ItemSelect.Text == "Outside experiment")
            {
                CoordX.Enabled = true;
                CoordY.Enabled = true;
                NomLieu.Enabled = true;
                pictureBox1.Enabled = true;
            }
            else
            {
                CoordX.Enabled = false;
                CoordY.Enabled = false;
                NomLieu.Enabled = false;
                pictureBox1.Enabled = false;
                CoordX.Text = "0";
                CoordY.Text = "0";
                NomLieu.Text = "Base";
            }

            SupprimerNiv3.Visible = true;
        }


        private void AnnulerNiv3_Click(object sender, EventArgs e)
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
            for (int i = 0; i < listeAstronautes.Items.Count; i++)
            {
                listeAstronautes.Items[i].Checked = false;
            }
            CoordX.Text = "";
            CoordY.Text = "";
            texteDescriptif.Text = "";

            Niveau3.Show();
            //Niveau2.Hide();
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
            JourCourantMission_TextChanged(new Object(), new EventArgs());
            Niveau2.Hide();
            Niveau1.Show();
        }


        private void JourPrecedent_Click(object sender, EventArgs e)
        {
            if (int.Parse(NumeroJour.Text) > 1)
            {
                NumeroJour.Text = Convert.ToString(int.Parse(NumeroJour.Text) - 1);
            }
            NduJNiv3.Text = NumeroJour.Text;
            CreerBoutons(int.Parse(NumeroJour.Text));
            if (int.Parse(NumeroJour.Text) < int.Parse(JourCourantMission.Text))
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
            if (int.Parse(NumeroJour.Text) < int.Parse(JourCourantMission.Text))
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
            else if (MinDebut.Items.Count == 4)
            {
                MinDebut.Items.Add("40");
                MinDebut.Items.Add("50");
            }
            MinDebut.Enabled = true;
        }


        private void MinDebut_SelectedIndexChanged(object sender, EventArgs e)
        {
            HFin.Enabled = true;
        }


        private void HFin_SelectedValueChanged(object sender, EventArgs e)
        {
            if (HFin.SelectedIndex == 24)
            {
                MinFin.Items.Remove("50");
                MinFin.SelectedText = "";
            }
            else if (MinFin.Items.Count == 5)
            {
                MinFin.Items.Add("50");
            }
            MinFin.Enabled = true;
        }

        private void listeActivites_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (listeActivites.SelectedNode.Level == 1)
            {
                ItemSelect.Text = listeActivites.SelectedNode.Text;
                if (ItemSelect.Text == "Exploration Space suit" || ItemSelect.Text == "Exploration Vehicule" || ItemSelect.Text == "Outside experiment")
                {
                    CoordX.Enabled = true;
                    CoordY.Enabled = true;
                    NomLieu.Enabled = true;
                    pictureBox1.Enabled = true;
                }
                else
                {
                    CoordX.Enabled = false;
                    CoordY.Enabled = false;
                    NomLieu.Enabled = false;
                    pictureBox1.Enabled = false;
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


        private void ConfirmerNiv3_Click(object sender, EventArgs e)
        {
            bool[] tab = M.Calendar.Jours[int.Parse(NduJNiv3.Text) - 1].TabHoraires;
            bool[] erreurs = { true, false, false, false, false };
            Jour jourj = M.Calendar.Jours[int.Parse(NduJNiv3.Text) - 1];
            Activite act = new Activite("");

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

            if (HDebut.Text != "" && MinDebut.Text != "" && HFin.Text != "" && MinFin.Text != "" && (int.Parse(HDebut.Text) * 6 + int.Parse(MinDebut.Text) / 10 <= int.Parse(HFin.Text) * 6 + int.Parse(MinFin.Text) / 10 - 1))
            {
                erreurs[2] = true;

                for (int i = int.Parse(HDebut.Text) * 6 + int.Parse(MinDebut.Text) / 10; i < int.Parse(HFin.Text) * 6 + int.Parse(MinFin.Text) / 10 ; i++)
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
                for (int i = 0; i < listeAstronautes.CheckedItems.Count; i++)
                {
                    liA.Add(new Astronaute(i, listeAstronautes.CheckedItems[i].Text));
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
                if (int.Parse(NumeroJour.Text) < int.Parse(JourCourantMission.Text))
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
            for (int i = act.Debut.Heures * 6 + act.Debut.Minutes / 10; i < act.Fin.Heures * 6 + act.Fin.Minutes / 10 ; i++)
            {
                tab[i] = disponible;
            }
        }


        private void SupprimerNiv3_Click(object sender, EventArgs e)
        {
            Activite act = M.Calendar.Jours[int.Parse(NduJNiv3.Text) - 1].ListeActivites[int.Parse(labelInvisible.Text)];
            M.Calendar.Jours[int.Parse(NduJNiv3.Text) - 1].ListeActivites.RemoveAt(int.Parse(labelInvisible.Text));

            changerUnePlageHoraire(act, M.Calendar.Jours[int.Parse(NduJNiv3.Text) - 1].TabHoraires, true);

            CreerBoutons(int.Parse(NduJNiv3.Text)); //on réactualise les boutons du niveau 2 et donc les indices de la liste d'activités

            Niveau2.Show();
            Niveau3.Hide();
        }


 


        private void CreerActivite_EnabledChanged(object sender, EventArgs e)
        {
            if (!CreerActivite.Enabled)
            {
                HDebut.Enabled = false;
                MinDebut.Enabled = false;
                HFin.Enabled = false;
                MinFin.Enabled = false;
                texteDescriptif.Enabled = false;
                listeActivites.Enabled = false;
                CoordX.Enabled = false;
                CoordY.Enabled = false;
                listeAstronautes.Enabled = false;
                NomLieu.Enabled = false;
            }
            else
            {
                HDebut.Enabled = true;
                MinDebut.Enabled = true;
                HFin.Enabled = true;
                MinFin.Enabled = true;
                texteDescriptif.Enabled = true;
                listeActivites.Enabled = true;
                CoordX.Enabled = true;
                CoordY.Enabled = true;
                listeAstronautes.Enabled = true;
                NomLieu.Enabled = true;
            }
        }


        private void CompteRendu_Click(object sender, EventArgs e)
        {
            CompteRendu cr = new CompteRendu(M.Calendar.Jours[int.Parse(NumeroJour.Text)].CompteRendu);
            cr.CR += new CompteRendu.AjouterJourEventHandler(this.MiseAJourCompteRendu);
            cr.ShowDialog();
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Point positionSouris = new Point(Convert.ToInt32(Math.Round(e.X * 37.9)), Convert.ToInt32(Math.Round(e.Y * 37.9))); //37.9=facteur d'échelle
            if (pictureBox1.Enabled)
            {
                
                if (NomLieu.Text == "Base")
                {
                    NomLieu.Text = "";
                }
                
                CoordX.Text = Convert.ToString((positionSouris.X - coordBase.X)); // la valeur du textbox a changé, on actualise la croix avec les nouvelles coordonnées
                if (e.Y < coordBase.Y) // sur les machines un peu lentes, on constate l'apparition éclair d'une croix fantome calculée sur l'ancienne valeur de Y 
                {
                    CoordY.Text = Convert.ToString(-1 * (positionSouris.Y - coordBase.Y));
                }
                else
                {
                    CoordY.Text = Convert.ToString((coordBase.Y - positionSouris.Y));
                }
            }
            else
            {

            }
            
        }


        private void ActExplo_Click(object sender, EventArgs e)
        {         
            //Referesh
            InfoLieu.Text = "";
            InfoActivite.Text = "";
            InfoNumJour.Text = "";
            InfoHDebut.Text = "";
            InfoMDebut.Text = "";
            InfoHFin.Text = "";
            InfoMFin.Text = "";

            InfoAstronautes.Items.Clear();
            

            InfoDescriptif.Text = "";
            Carte.Controls.Clear();
            Carte.Refresh();

            ActiviteExploration.Visible = true;

            //List<Activite> listActExploration;
            //List<int> listJoursExploration;
            //Point repere = new Point(Carte.Location.X,Carte.Location.Y);

            Point coordBaseExpl = new Point(175, 250); //origine du repère carte d'exploration

            for (int i = int.Parse(PeriodeDebut.Text)-1; i < int.Parse(PeriodeFin.Text)-1; i++) //on parcourt les jours
            {
                for (int j = 0; j < M.Calendar.Jours[i].ListeActivites.Count; j++) //on parcourt l'ensemble des activités du jour i
                {
                    if (M.Calendar.Jours[i].ListeActivites[j].Nom == "Exploration Space suit" || M.Calendar.Jours[i].ListeActivites[j].Nom == "Exploration Vehicule" || M.Calendar.Jours[i].ListeActivites[j].Nom == "Outside experiment")
                    {
                        PictureBox icone = new PictureBox();
                        icone.BackColor = Color.Transparent;
                        icone.Size = new Size(24, 24);
                        Carte.Controls.Add(icone);

                        icone.Cursor = System.Windows.Forms.Cursors.Hand;
                        icone.Tag = M.Calendar.Jours[i].ListeActivites[j]; // on associe l'activité à la picture box qui sert d'icône pour la récupérer après clic
                        icone.Controls.Add(new Control(Convert.ToString(i))); //on ajoute un contrôle pour récupérer le numéro du jour
                        icone.Click += new System.EventHandler(this.ClickIconeCarte);


                        //on place et on centre l'icône (- 12 pour centrer l'icone de 24*24px)
                        
                        if (M.Calendar.Jours[i].ListeActivites[j].Gps.Coords.Y >= 0)
                        {
                            icone.Location = new Point(Convert.ToInt32(Math.Round((M.Calendar.Jours[i].ListeActivites[j].Gps.Coords.X + coordBase.X) / 37.9 * 1.94)) - 12, Convert.ToInt32(Math.Round((coordBase.Y - M.Calendar.Jours[i].ListeActivites[j].Gps.Coords.Y) / 37.9 * 1.94)) - 12);

                        }
                        else
                        {
                            icone.Location = new Point(Convert.ToInt32(Math.Round((M.Calendar.Jours[i].ListeActivites[j].Gps.Coords.X + coordBase.X) / 37.9 * 1.94)) - 12, -1 * (Convert.ToInt32(Math.Round((M.Calendar.Jours[i].ListeActivites[j].Gps.Coords.Y - coordBase.Y) / 37.9 * 1.94))) - 12);
                        }

                        if (M.Calendar.Jours[i].ListeActivites[j].Nom == "Exploration Space suit")
                        {
                            if (i <= int.Parse(JourCourantMission.Text))
                            {
                                icone.Image = scaphandrePasse;
                            }
                            else
                            {
                                icone.Image = scaphandreFutur;
                            }
                        }

                        if (M.Calendar.Jours[i].ListeActivites[j].Nom == "Exploration Vehicule")
                        {
                            if (i <= int.Parse(JourCourantMission.Text))
                            {
                                icone.Image = vehiculePasse;
                            }
                            else
                            {
                                icone.Image = vehiculeFutur;
                            }
                        }

                        if (M.Calendar.Jours[i].ListeActivites[j].Nom == "Outside experiment")
                        {
                            if (i <= int.Parse(JourCourantMission.Text))
                            {
                                icone.Image = experiencePasse;
                            }
                            else
                            {
                                icone.Image = experienceFutur;
                            }
                        }
                    }
                }
            }
        }

        private void ClickIconeCarte(object sender, EventArgs e)
        {
            ActExplo_Click(new object(), new EventArgs());
            PictureBox p = (PictureBox)sender;
            Activite activiteClickee = (Activite)p.Tag;
            InfoLieu.Text = activiteClickee.Gps.Nom;
            InfoActivite.Text = activiteClickee.Nom;
            InfoNumJour.Text = p.Controls[0].Text;
            InfoHDebut.Text = Convert.ToString(activiteClickee.Debut.Heures);
            InfoMDebut.Text = Convert.ToString(activiteClickee.Debut.Minutes);
            InfoHFin.Text = Convert.ToString(activiteClickee.Fin.Heures);
            InfoMFin.Text = Convert.ToString(activiteClickee.Fin.Minutes);
            for (int i = 0; i < activiteClickee.ListAstronaute.Count; i++)
            {
                InfoAstronautes.Items.Add(activiteClickee.ListAstronaute[i].Nom);
            }
            
            InfoDescriptif.Text = activiteClickee.TexteDescriptif;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ActiviteExploration.Visible = false;
        }

        private void PeriodeDebut_TextChanged(object sender, EventArgs e)
        {
            if (PeriodeDebut.Text != "" && PeriodeFin.Text != "" && int.Parse(PeriodeDebut.Text) > 0 && int.Parse(PeriodeDebut.Text)<=500)
            {
                ActExplo_Click(new object(), new EventArgs());
            }           
        }

        private void PeriodeFin_TextChanged(object sender, EventArgs e)
        {
            if (PeriodeDebut.Text != "" && PeriodeFin.Text != "" && int.Parse(PeriodeDebut.Text) > 0 && int.Parse(PeriodeDebut.Text) <= 500)
            {
                ActExplo_Click(new object(), new EventArgs());
            }
        }

        private void CoordX_ValueChanged(object sender, EventArgs e)
        {
            if (CoordY.Text != "" && CoordX.Text != "" && CoordX.Text != "-" && CoordY.Text != "-")
            {
                if (NomLieu.Text == "Base")
                {
                    NomLieu.Text = "";
                }
                pictureBox1.Refresh();

                croix = pictureBox1.CreateGraphics();

                if (int.Parse(CoordY.Text) >= 0)
                {
                    croix.DrawLine(new Pen(Color.Black), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / 37.9)) - 10, Convert.ToInt32(Math.Round((coordBase.Y - double.Parse(CoordY.Text)) / 37.9))), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / 37.9)) + 10, Convert.ToInt32(Math.Round((coordBase.Y - double.Parse(CoordY.Text)) / 37.9))));
                    croix.DrawLine(new Pen(Color.Black), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / 37.9)), Convert.ToInt32(Math.Round((coordBase.Y - double.Parse(CoordY.Text)) / 37.9)) - 10), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / 37.9)), Convert.ToInt32(Math.Round((coordBase.Y - double.Parse(CoordY.Text)) / 37.9)) + 10));

                }
                else
                {
                    croix.DrawLine(new Pen(Color.Black), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / 37.9)) - 10, -1 * (Convert.ToInt32(Math.Round((double.Parse(CoordY.Text) - coordBase.Y) / 37.9)))), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / 37.9)) + 10, -1 * (Convert.ToInt32(Math.Round((double.Parse(CoordY.Text) - coordBase.Y) / 37.9)))));
                    croix.DrawLine(new Pen(Color.Black), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / 37.9)), -1 * (Convert.ToInt32(Math.Round((double.Parse(CoordY.Text) - coordBase.Y) / 37.9))) - 10), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / 37.9)), -1 * (Convert.ToInt32(Math.Round((double.Parse(CoordY.Text) - coordBase.Y) / 37.9))) + 10));

                }


            }
        }

        private void CoordY_ValueChanged(object sender, EventArgs e)
        {
            CoordX_ValueChanged(new object(), new EventArgs());
        }
    }
}

