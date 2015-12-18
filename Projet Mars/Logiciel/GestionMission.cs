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
    /// <summary>
    /// Form principal de l'application contenant le calendrier, le planning des activités, les fenêtres de modification des activités et la fiche d'exploration
    /// </summary>

    public partial class GestionMission : Form 
    {
        private Mission M = new Mission(); //Déclaration de la mission avec son calendrier, ses astronautes et ses activités
        
        //On crée deux documents pour l'écriture XML  
        XmlDocument xmlDoc = new XmlDocument();
        XmlDocument xmlDoc2 = new XmlDocument();

        //Icônes pour les activités d'exploration
        Image sortie = Image.FromFile("..\\..\\..\\..\\astronaut.png");
        Image scaphandrePasse = Image.FromFile("..\\..\\..\\..\\astronautGray.png");
        Image scaphandreFutur = Image.FromFile("..\\..\\..\\..\\astronautGreen.png");
        Image scaphandreActuel = Image.FromFile("..\\..\\..\\..\\astronautBlue.png");
        Image vehiculePasse = Image.FromFile("..\\..\\..\\..\\MarsVehiculeGray.png");
        Image vehiculeActuel = Image.FromFile("..\\..\\..\\..\\MarsVehiculeBlue.png");
        Image vehiculeFutur = Image.FromFile("..\\..\\..\\..\\MarsVehiculeGreen.png");
        Image experiencePasse = Image.FromFile("..\\..\\..\\..\\experimentGray.png");
        Image experienceFutur = Image.FromFile("..\\..\\..\\..\\experimentGreen.png");
        Image experienceActuelle = Image.FromFile("..\\..\\..\\..\\experimentBlue.png");

        //Carte niveau 3 :
        static double echCarteNiv3 = 37.9; //Facteur d'échelle pour la carte de niveau 3 par rapport à la carte fournie
        Point coordBase = new Point(Convert.ToInt32(Math.Round(90*echCarteNiv3)), Convert.ToInt32(Math.Round(129*echCarteNiv3))); //Origine du repère carte niveau 3
        Graphics croix; //Pour dessiner une croix formée de 2 traits sur la carte (niveau 3)

        //Carte fiche exploration :
        Point coordBaseExpl = new Point(175, 250); //origine du repère carte d'exploration
        static double echCarteExplo = 1.94; //Facteur d'échelle pour la carte de la fiche exploration par rapport à la carte au niveau 3

        int jourSelec; // permet de retenir le numéro jour sélectionner 


        //INITIALISATION DE LA MISSION//
        
        /// <summary>
        /// Ici sont chargés les fichiers XML et instanciés les objets de la classe "Mission"
        /// </summary>
        public GestionMission()
        {
            InitializeComponent();
            try
            {               
                xmlDoc.Load(@"..\\..\\..\\sauvegarde1.xml");                
                M.chargerXml(xmlDoc, M);

                xmlDoc2.Load(@"..\\..\\..\\sauvegarde2.xml");
                M.chargerXml2(xmlDoc2, M);

                M.Calendar.MiseAJour();//Pour remettre les pendules à l'heure 
            }
            catch
            {
                MessageBox.Show("Initialisation Mission", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Initialisation des paramètres de base pour une mission

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

                //Journée type par défaut :                                      
                for (int i = 1; i < 501; i++)
                {
                    Jour j = new Jour(i);
                     
                    j.AddAct(new Activite("Sleeping", new Heure(0, 0), new Heure(7, 0), "Un repos bien mérité !"));
                    j.AddAct(new Activite("Eating", new Heure(7, 0), new Heure(8, 0), "Manger c'est important"));
                    j.AddAct(new Activite("Private", new Heure(8, 0), new Heure(12, 0), ""));
                    j.AddAct(new Activite("Eating", new Heure(12, 0), new Heure(14, 0), "Manger c'est important"));
                    j.AddAct(new Activite("Private", new Heure(14, 0), new Heure(19, 0), ""));
                    j.AddAct(new Activite("Eating", new Heure(19, 0), new Heure(21, 0), "Manger c'est important"));
                    j.AddAct(new Activite("Private", new Heure(21, 0), new Heure(23, 0), ""));
                    j.AddAct(new Activite("Sleeping", new Heure(23, 0), new Heure(24, 40), "Un repos bien mérité !"));

                    c.AddJours(j);
                }
                
                //Ajout du calendrier et des activités à la mission :

                M.Calendar = c;

                M.AddCategorie(VieCourante);
                M.AddCategorie(Science);
                M.AddCategorie(Maintenance);
                M.AddCategorie(Communication);
                M.AddCategorie(Reparation);
                M.AddCategorie(Urgence);

                // Initialisation des astronautes :

                List<Astronaute> ListAtr = new List<Astronaute>(); 
                Astronautes InitAstr = new Astronautes();

                InitAstr.ShowDialog();
           
                M.ListAstr = InitAstr.Astro();
                InitAstr.BringToFront();                
            }                
            timer1.Start();
            dureMission.Maximum = 500;
            trackBar1.Maximum = 9; //Permet l'affichage des jours de 50 en 50
            trackBar1_Scroll(new Object(), new EventArgs()); //Pour différencier les jours d'un scroll à l'autre           
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
        /// Indique quels jours du calendrier sont concernés par une activité en extérieur
        /// </summary>
        public void Sortie()
        {
            int test = 0;            
            for (int i = 0; i < M.Calendar.Jours.Count; i++)  //Pour chaque jour du calendrier
            {
                bool ext = false;
                test = M.Calendar.Jours[i].ListeActivites.Count;
                for (int j = 0; j < test; j++) //Pour chaque activité d'un jour donné
                {
                    if (M.Calendar.Jours[i].ListeActivites[j].Nom == "Exploration Vehicule" || M.Calendar.Jours[i].ListeActivites[j].Nom == "Exploration Space suit" || M.Calendar.Jours[i].ListeActivites[j].Nom == "Outside experiment")
                    {
                        ext = true;
                    }

                    if (ext)
                    {
                        M.Calendar.Jours[i].Sortie = true;
                    }
                    else
                    {
                        M.Calendar.Jours[i].Sortie = false;
                    }
                }
            }
        }


        //NIVEAU 1//
        //Contient le calendrier des jours de 1 à 500

        /// <summary>
        /// Donne l'heure qu'il est pour le jour martien de la mission
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            M.Calendar.Horloge();            
            heureMars.Text = M.Calendar.Heure.ToString() + " h " + M.Calendar.Minute.ToString() + " min " + M.Calendar.Seconde.ToString() + " s";
            JourCourantMission.Text = M.Calendar.Day.ToString();
            DateTerrestre.Text = Convert.ToString(DateTime.Now);
            timer1.Start();
        }

        /// <summary>
        /// Met à jour la barre de progression
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JourCourantMission_TextChanged(object sender, EventArgs e)
        {
            dureMission.Increment(1);  //On augmente de 1 pas la barre de progression de la mission           
            trackBar1.Value = int.Parse(JourCourantMission.Text) / 50;
            trackBar1_Scroll(sender, e); 
        }

        /// <summary>
        /// Permet de changer l'affichage au niveau des jours du calendrier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Scroll(object sender, EventArgs e)  
        {
            Sortie(); //Affiche un astronaute sur les jours concernés par la sortie

            for (int i = 0; i < Niveau1.Controls.Count; i++)  //Pour chaque élément du groupBox 
            {
                if (Niveau1.Controls[i].Name.Contains("jour"))   //Ceux dont le nom contient jour
                {
                    Niveau1.Controls[i].Text = Convert.ToString((50 * trackBar1.Value) + i +1);  //On modifie le texte des bouton selon la valeur du trackBar

                    if (int.Parse(JourCourantMission.Text) == (50 * trackBar1.Value) + i +1)
                    {
                        Niveau1.Controls[i].BackColor = Color.RoyalBlue; //Jour courant en bleu
                    }
                    else
                    {
                        if (int.Parse(Niveau1.Controls[i].Text) < M.Calendar.Day)
                        {
                            Niveau1.Controls[i].BackColor = Color.DimGray; //Jour passé en gris
                        }
                        else
                        {
                            Niveau1.Controls[i].BackColor = Color.DarkGreen; //Jour futur en vert
                        }
                    }
                }                
            }

            // Boucle qui permet d'afficher les astronautes pour les jours avec sortie 
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

        /// <summary>
        /// Permet d'accéder aux 50 prochains jours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value < 9)
            {
                trackBar1.Value = trackBar1.Value + 1;
            }
            trackBar1_Scroll(sender, e);
        }

        /// <summary>
        /// Permer d'accéder aux 50 jours précédents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value > 0)
            {
                trackBar1.Value = trackBar1.Value - 1;
            }
            trackBar1_Scroll(sender, e);
        }

        /// <summary>
        /// Permet d'accéder au niveau 2 par le jour cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jour_Click(object sender, EventArgs e)
        {          
            Button clickedButton = (Button)sender; //On conserve le bouton qui sert à entrer dans la fonction (sender)
            jourSelec = int.Parse(clickedButton.Text); //On conserve le numéro du jour cliqué
               
            Niveau2.Show(); //On montre le deuxième niveau du programme
            Niveau1.Hide(); //On cache le premier niveau du programme

            NumeroJour.Text = clickedButton.Text; 
            NduJNiv3.Text = NumeroJour.Text;
            if (int.Parse(NumeroJour.Text) < int.Parse(JourCourantMission.Text))
            {
                CreerActivite.Enabled = false; //On ne peut pas créer d'activité pour un jour passé
            }
            else
            {
                CreerActivite.Enabled = true;
            }
            CreerBoutons(jourSelec);
        }


        //NIVEAU 2//
        //Correspond au planning des activités

        /// <summary>
        /// Création d'un bouton par activité pour chaque jour
        /// </summary>
        /// <param name="n">n : numéro du jour</param>
        public void CreerBoutons(int n)
        {
            boutonsMatin.Controls.Clear();
            boutonsApresMidi.Controls.Clear();
            Point pointDeBase = new Point(0, 0); //Point servant à la localisation des boutons
            int largeurActivite = 262; //Largeur fixée
            Jour jourJ = M.Calendar.Jours.ElementAt(n - 1); //n-1 : emplacement du jour dans la liste de jours
            

            //Recherche du nombre d'activités du jour n dans le calendrier
            for (int i = 0; i < jourJ.ListeActivites.Count; i++)
            {
                Button bouton = new System.Windows.Forms.Button();
                Activite Act = jourJ.ListeActivites.ElementAt(i);

                CoordX.Enabled = false;
                CoordY.Enabled = false;
                NomLieu.Enabled = false;
                pictureBox1.Enabled = false;

                //Définition d'une couleur par activité pour une meilleure lisibilité
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
                        CoordX.Enabled = true;
                        CoordY.Enabled = true;
                        NomLieu.Enabled = true;
                        pictureBox1.Enabled = true;
                        break;
                    case "Exploration Vehicule":
                        bouton.BackColor = Color.LightSteelBlue;
                        CoordX.Enabled = true;
                        CoordY.Enabled = true;
                        NomLieu.Enabled = true;
                        pictureBox1.Enabled = true;
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
                        CoordX.Enabled = true;
                        CoordY.Enabled = true;
                        NomLieu.Enabled = true;
                        pictureBox1.Enabled = true;
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

                Point localisation = new Point(pointDeBase.X, pointDeBase.Y); //Rq : 5px pour 10min

                //On scinde le planning selon les cas avant 12h20 et après 12h20 car il comporte 2 colonnes
                if (Act.Debut.HeuresMinutes < 1220 && Act.Fin.HeuresMinutes <= 1220) //Si l'activité commence et se termine avant 12h20 (ou se termine à 12h20)
                {
                    this.boutonsMatin.Controls.Add(bouton);
                    localisation.Y += (Act.Debut.Heures * 6 + Act.Debut.Minutes / 10) * 5; //Calcul du nb de tranches de 10min * 5px le matin                  
                    bouton.Size = new System.Drawing.Size(largeurActivite, ((Act.Fin.Heures * 6 + Act.Fin.Minutes / 10) * 5) - localisation.Y - 1);
                }
                else //Si l'activité commence avant 12h20 et se termine après 12h20, ou si elle commence et se termine après 12h20
                {
                    this.boutonsApresMidi.Controls.Add(bouton);

                    if ((Act.Debut.Heures * 6 + Act.Debut.Minutes / 10 - 74) * 5 < 0)
                    {
                        localisation.Y += -1; //pour une activité à cheval sur les 2 colonnes, permet d'éliminer le trait noir supérieur ou inférieur du bouton
                    }
                    else
                    {
                        localisation.Y += (Act.Debut.Heures * 6 + Act.Debut.Minutes / 10 - 74) * 5;
                    }
                    bouton.Size = new System.Drawing.Size(largeurActivite, ((Act.Fin.Heures * 6 + Act.Fin.Minutes / 10 - 74) * 5) - localisation.Y - 1);

                    if (Act.Debut.HeuresMinutes < 1220) //Si l'activité commence avant 12h20, il faut créer un 2eme bouton pour scinder l'activité en deux
                    {
                        Button bouttonBis = new Button();
                        this.boutonsMatin.Controls.Add(bouttonBis);
                        bouttonBis.Location = new Point(0, (Act.Debut.Heures * 6 + Act.Debut.Minutes / 10) * 5);
                        bouttonBis.Size = new System.Drawing.Size(largeurActivite, 370 - ((Act.Debut.Heures * 6 + Act.Debut.Minutes / 10) * 5) - localisation.Y);
                        bouttonBis.Margin = new System.Windows.Forms.Padding(0);
                        bouttonBis.Name = "ActiviteBis" + i;
                        bouttonBis.Tag = jourJ.ListeActivites[i];
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
                bouton.Tag = jourJ.ListeActivites[i];
                bouton.Text = Act.Nom;
                bouton.UseVisualStyleBackColor = false;
                bouton.Click += new System.EventHandler(this.ClickNiveau3);
            }
        }

        /// <summary>
        /// Permet de d'accéder au jour précédent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JourPrecedent_Click(object sender, EventArgs e)
        {
            if (int.Parse(NumeroJour.Text) > 1)
            {
                NumeroJour.Text = Convert.ToString(int.Parse(NumeroJour.Text) - 1);
            }
            NduJNiv3.Text = NumeroJour.Text;
            CreerBoutons(int.Parse(NumeroJour.Text)); //Génération des boutons des activités de ce jour
            if (int.Parse(NumeroJour.Text) < int.Parse(JourCourantMission.Text))
            {
                CreerActivite.Enabled = false; //On ne peut pas créer d'activités pour un jour passé
            }
            else
            {
                CreerActivite.Enabled = true;
            }
        }

        /// <summary>
        /// Permet de d'accéder au jour suivant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Accès au compte-rendu de la journée (dans un autre form)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompteRendu_Click(object sender, EventArgs e)
        {
            CompteRendu cr = new CompteRendu(M.Calendar.Jours[int.Parse(NumeroJour.Text)].CompteRendu);
            cr.CR += new CompteRendu.AjouterEventHandler(this.MiseAJourCompteRendu); //On ajoute le nouveau compte-rendu
            cr.ShowDialog();
        }

        /// <summary>
        /// Pour retourner au calendrier du niveau 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RetourCalendrier_Click_1(object sender, EventArgs e)
        {
            JourCourantMission_TextChanged(new Object(), new EventArgs());
            Niveau2.Hide();
            Niveau1.Show();
            trackBar1_Scroll(sender,e);
        }



        //NIVEAU 3//
        //On y accède de deux façons : en cliquant sur une activité pour la modifier ou en créant une nouvelle activité (Bouton "Créer une activité")
       
        /// <summary>
        /// Ici on accède au niveau 3 en cliquant sur un bouton représentant une activité
        /// (modification seulement, la création d'activité est gérée en cliquant sur un autre bouton)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickNiveau3(object sender, EventArgs e)
        {
            //Dans le cas d'une modification, le bouton "Supprimer l'activité" est accessible (ce n'est pas le cas pour une création)
            SupprimerNiv3.Visible = true;

            if (!CreerActivite.Enabled)
            {
                HDebut.Enabled = false;
                HFin.Enabled = false;
                MinDebut.Enabled = false;
                MinFin.Enabled = false;
                listeActivites.Enabled = false;
                listeAstronautes.Enabled = false;
                texteDescriptif.Enabled = false;
                SupprimerNiv3.Visible = false;
            }
            else
            {
                HDebut.Enabled = true;
                HFin.Enabled = true;
                MinDebut.Enabled = true;
                MinFin.Enabled = true;
                listeActivites.Enabled = true;
                listeAstronautes.Enabled = true;
                texteDescriptif.Enabled = true;
            }

            listeAstronautes.Clear();
            foreach (Astronaute a in M.ListAstr)
            {
                listeAstronautes.Items.Add(a.ToString());
            }

            //Cette fonction est appelée à chaque clique sur un bouton du planning, c'est le sender qui fait référence au bouton sur lequel on a cliqué
            Button clickedButton = (Button)sender;            
            Activite act = (Activite)clickedButton.Tag; 
            
            ConfirmerNiv3.Tag = act;

            Niveau3.Show();

            TitreNiv3.Text = "Modifier une activité";
            texteDescriptif.Text = act.TexteDescriptif;
            for (int i = 0; i < listeAstronautes.Items.Count; i++) //On décoche tous les astronautes par sécurité
            {
                listeAstronautes.Items[i].Checked = false;
            }
            for (int i = 0; i < act.ListAstronaute.Count; i++) //On coche les astronautes pour l'activité concernée
            {
                for (int j = 0; j < listeAstronautes.Items.Count; j++)
                {
                    if (listeAstronautes.Items[j].Text == act.ListAstronaute[i].Nom)
                    {
                        listeAstronautes.Items[j].Checked = true;
                    }
                }
            }
            ItemSelect.Text = act.Nom; //Dans le treeview des activités
            HDebut.SelectedIndex = act.Debut.Heures;
            MinDebut.SelectedIndex = act.Debut.Minutes / 10; //On divise par 10 pour avoir le numéro de l'index (ex : 10min -> index 1)
            HFin.SelectedIndex = act.Fin.Heures;
            MinFin.SelectedIndex = act.Fin.Minutes / 10;
            CoordX.Text = Convert.ToString(act.Gps.Coords.X);
            CoordY.Text = Convert.ToString(act.Gps.Coords.Y);
            NomLieu.Text = act.Gps.Nom;

            //On gère les champs en relation avec la carte en fonction de l'activité sélectionnée
            if (ItemSelect.Text == "Exploration Space suit" || ItemSelect.Text == "Exploration Vehicule" || ItemSelect.Text == "Outside experiment")
            {
                if (!CreerActivite.Enabled)
                {
                    pictureBox1.Enabled = false;
                    CoordX.Enabled = false;
                    CoordY.Enabled = false;
                    NomLieu.Enabled = false;
                }
                else
                {
                    //Si c'est une activité d'exploration, on a accès à la modification des coordonnées, du lieu et on peut cliquer sur la carte
                    CoordX.Enabled = true;
                    CoordY.Enabled = true;
                    NomLieu.Enabled = true;
                    pictureBox1.Enabled = true;
                }
                
            }
            else
            {
                //Pour toute autre activité les champs sont vérouillés et pré-remplis car les astronautes restent à la base
                CoordX.Enabled = false;
                CoordY.Enabled = false;
                NomLieu.Enabled = false;
                pictureBox1.Enabled = false;
                CoordX.Text = "0";
                CoordY.Text = "0";
                NomLieu.Text = "Base";
            }
             
        }

        /// <summary>
        /// Accès au niveau 3 pour créer une activité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreerActivite_Click(object sender, EventArgs e)
        {
            listeAstronautes.Clear();
            foreach (Astronaute a in M.ListAstr)
            {
                listeAstronautes.Items.Add(a.ToString());
            }

            TitreNiv3.Text = "Créer une activité";
            SupprimerNiv3.Visible = false; //Le bouton de suppression d'activité n'apparaît pas par soucis de cohérence

            HDebut.SelectedIndex = -1;
            MinDebut.SelectedIndex = -1;
            HFin.SelectedIndex = -1;
            MinFin.SelectedIndex = -1;

            HDebut.Enabled = true; //On déverrouille les champs au fur et à mesure
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
        }

        /// <summary>
        /// Conrôle des valeurs autorisées pour les heures et les minutes de début de l'activité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HDebut_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!CreerActivite.Enabled)
            {
                MinDebut.Enabled = false;
                listeActivites.Enabled = false;
                listeAstronautes.Enabled = false;
                texteDescriptif.Enabled = false;
                SupprimerNiv3.Visible = false;
            }
            else
            {
                if (HDebut.SelectedIndex == 24)
                {
                    MinDebut.Items.Remove("40"); //Une activité ne peut pas commencer à 24h40
                    MinDebut.Items.Remove("50"); //24h50 n'existe pas
                    MinDebut.SelectedText = "";
                }
                //Si l'heure sélectionnée n'est pas 24, il faut remettre toutes les possibilités pour les minutes
                else if (MinDebut.Items.Count == 4)
                {
                    MinDebut.Items.Add("40");
                    MinDebut.Items.Add("50");
                }
                MinDebut.Enabled = true;
            }
        }

        /// <summary>
        /// Déverrouille le champ concernant l'heure de fin une fois les minutes de l'heure de début remplies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinDebut_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CreerActivite.Enabled)
            {
                HFin.Enabled = false;
                listeActivites.Enabled = false;
                listeAstronautes.Enabled = false;
                texteDescriptif.Enabled = false;
                SupprimerNiv3.Visible = false;               
            }
            else
            {
                HFin.Enabled = true;
            } 
        }

        /// <summary>
        /// Conrôle des valeurs autorisées pour les heures et les minutes de fin de l'activité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HFin_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!CreerActivite.Enabled)
            {
                MinFin.Enabled = false;
                listeActivites.Enabled = false;
                listeAstronautes.Enabled = false;
                texteDescriptif.Enabled = false;
                SupprimerNiv3.Visible = false;
            }
            else
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
        }

        /// <summary>
        /// Affiche le nom de l'activité sélectionnée dans le treeview et gère l'accès aux champs de la carte en fonction de l'activité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listeActivites_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (listeActivites.SelectedNode.Level == 1)
            {
                ItemSelect.Text = listeActivites.SelectedNode.Text;
                if (ItemSelect.Text == "Exploration Space suit" || ItemSelect.Text == "Exploration Vehicule" || ItemSelect.Text == "Outside experiment")
                {
                    if (!CreerActivite.Enabled)
                    {
                        pictureBox1.Enabled = false;
                        CoordX.Enabled = false;
                        CoordY.Enabled = false;
                        NomLieu.Enabled = false;
                    }
                    else
                    {
                        //Les champs de la carte sont accessibles
                        CoordX.Enabled = true;
                        CoordY.Enabled = true;
                        NomLieu.Enabled = true;
                        pictureBox1.Enabled = true;
                    }                   
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
                //On n'affiche pas les activités correspondant aux noeuds ("Living", "Science"...)
                ItemSelect.Text = "";
            }
        }

        /// <summary>
        /// Bouton de retour au niveau 2 sans enregistrer les modifications
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnnulerNiv3_Click(object sender, EventArgs e)
        {
            Niveau3.Hide();
            Niveau2.Show();
            NomLieu.Text = "";
            CoordX.Value = 0;
            CoordY.Value = 0;
        }

        /// <summary>
        /// Parcours l'ensemble des activités du jour et attribue la disponibilité des crénaux (true ou false)
        /// Cette fonction permet aux activités de ne pas se chevaucher
        /// </summary>
        /// <param name="act"></param>
        /// <param name="tab">tableau contenant les disponibilités des crénaux</param>
        /// <param name="disponible"></param>
        private void changerUnePlageHoraire(Activite act, bool[] tab, bool disponible)
        {
            for (int i = act.Debut.Heures * 6 + act.Debut.Minutes / 10; i < act.Fin.Heures * 6 + act.Fin.Minutes / 10; i++)
            {
                tab[i] = disponible;
            }
        }

        /// <summary>
        /// Vérifie la cohérence des informations rentrées dans les champs au niveau 3 
        /// Enregistre les modifications s'il n'y a pas d'erreur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmerNiv3_Click(object sender, EventArgs e)
        {
            bool[] tab = M.Calendar.Jours[int.Parse(NduJNiv3.Text) - 1].TabHoraires; //Tableau de disponibilité des activités du jour
            bool[] erreurs = { true, false, false, false,true }; //Tableau d'erreurs
            Jour jourj = M.Calendar.Jours[int.Parse(NduJNiv3.Text) - 1];
            Activite act = new Activite("");

            if (TitreNiv3.Text == "Modifier une activité")
            {
                act = (Activite)ConfirmerNiv3.Tag;
                changerUnePlageHoraire(act, jourj.TabHoraires, true);
            }
            //Vérification de la sélection d'un astronautes
            if (listeAstronautes.CheckedItems.Count == 0)
            {
                erreurs[4] = false;
            }

            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i])
                {
                    erreurs[1] = true;
                }
            }

            //Vérification de la sélection d'une activité
            if (ItemSelect.Text != "")
            {
                erreurs[3] = true;
            }

            //Si l'heure (minutes comprises) de fin est antérieure à l'heure (minutes comprises) de début
            if (HDebut.Text != "" && MinDebut.Text != "" && HFin.Text != "" && MinFin.Text != "" && (int.Parse(HDebut.Text) * 6 + int.Parse(MinDebut.Text) / 10 <= int.Parse(HFin.Text) * 6 + int.Parse(MinFin.Text) / 10 - 1))
            {
                erreurs[2] = true;

                for (int i = int.Parse(HDebut.Text) * 6 + int.Parse(MinDebut.Text) / 10; i < int.Parse(HFin.Text) * 6 + int.Parse(MinFin.Text) / 10 ; i++)
                {
                    if (!tab[i])//Si tab[i] est false, alors au moins un des emplacements demandés n'est pas disponible
                    {
                        erreurs[0] = false;
                    }
                }
            }

            if (erreurs[0] && erreurs[1] && erreurs[2] && erreurs[3] && erreurs[4]) //Si pas d'erreurs
            {
                List<Astronaute> liA = new List<Astronaute>();

                for (int i = 0; i < listeAstronautes.Items.Count; i++)
                {
                    if (listeAstronautes.Items[i].Checked == true)
                    {
                        for (int j = 0; j < M.ListAstr.Count; j++)
                        {
                            if (M.ListAstr[j].Nom == listeAstronautes.Items[i].Text)
                            {
                                if (i == j)
                                {
                                    liA.Add(M.ListAstr[j]);
                                }
                            }

                        }
                    }
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
                else //Création d'une activité et ajout à la liste pour le jour concerné
                {
                    act = new Activite(ItemSelect.Text, new Heure(int.Parse(HDebut.Text), int.Parse(MinDebut.Text)), new Heure(int.Parse(HFin.Text), int.Parse(MinFin.Text)), texteDescriptif.Text, liA, new Lieu(NomLieu.Text, new Point(int.Parse(CoordX.Text), int.Parse(CoordY.Text))));
                    jourj.ListeActivites.Add(act);
                }

                changerUnePlageHoraire(act, jourj.TabHoraires, false); //L'activité occupe maintenant la plage horaire
                CreerBoutons(int.Parse(NduJNiv3.Text)); //On crée le bouton associé

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
            //En cas d'erreur, on affiche des messages spécifiques et il est impossible d'enregistrer l'activité
            else if (!erreurs[4])
            {
                MessageBox.Show("Veuillez sélectionner au moins un astronaute", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!erreurs[3])
            {
                MessageBox.Show("Veuillez sélectionner une activité", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        /// <summary>
        /// Supprime l'activité sélectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerNiv3_Click(object sender, EventArgs e)
        {
            Activite act = (Activite)ConfirmerNiv3.Tag;
            M.Calendar.Jours[int.Parse(NduJNiv3.Text) - 1].ListeActivites.Remove(act);

            changerUnePlageHoraire(act, M.Calendar.Jours[int.Parse(NduJNiv3.Text) - 1].TabHoraires, true); //Libère la plage horaire de l'activité supprimée

            CreerBoutons(int.Parse(NduJNiv3.Text)); //On réactualise les boutons du niveau 2 et donc les indices de la liste d'activités

            Niveau2.Show();
            Niveau3.Hide();
        }

        /// <summary>
        /// On positionne la croix en fonction des coordonnées rentrées dans les champs X et Y
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CoordX_ValueChanged(object sender, EventArgs e)
        {
            if (CoordY.Text != "" && CoordX.Text != "" && CoordX.Text != "-" && CoordY.Text != "-")
            {
                if (NomLieu.Text == "Base")
                {
                    NomLieu.Text = "";
                }

                pictureBox1.Refresh(); //Pour n'avoir qu'une seule croix à la fois sur la carte

                croix = pictureBox1.CreateGraphics();

                //On dessine les 2 branches de la croix selon les coordonnées indiquées pour X et Y
                if (int.Parse(CoordY.Text) >= 0)
                {
                    croix.DrawLine(new Pen(Color.Black), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / echCarteNiv3)) - 10, Convert.ToInt32(Math.Round((coordBase.Y - double.Parse(CoordY.Text)) / echCarteNiv3))), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / echCarteNiv3)) + 10, Convert.ToInt32(Math.Round((coordBase.Y - double.Parse(CoordY.Text)) / echCarteNiv3))));
                    croix.DrawLine(new Pen(Color.Black), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / echCarteNiv3)), Convert.ToInt32(Math.Round((coordBase.Y - double.Parse(CoordY.Text)) / echCarteNiv3)) - 10), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / echCarteNiv3)), Convert.ToInt32(Math.Round((coordBase.Y - double.Parse(CoordY.Text)) / echCarteNiv3)) + 10));

                }
                else
                {
                    croix.DrawLine(new Pen(Color.Black), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / echCarteNiv3)) - 10, -1 * (Convert.ToInt32(Math.Round((double.Parse(CoordY.Text) - coordBase.Y) / echCarteNiv3)))), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / echCarteNiv3)) + 10, -1 * (Convert.ToInt32(Math.Round((double.Parse(CoordY.Text) - coordBase.Y) / echCarteNiv3)))));
                    croix.DrawLine(new Pen(Color.Black), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / echCarteNiv3)), -1 * (Convert.ToInt32(Math.Round((double.Parse(CoordY.Text) - coordBase.Y) / echCarteNiv3))) - 10), new Point(Convert.ToInt32(Math.Round((double.Parse(CoordX.Text) + coordBase.X) / echCarteNiv3)), -1 * (Convert.ToInt32(Math.Round((double.Parse(CoordY.Text) - coordBase.Y) / echCarteNiv3))) + 10));
                }
            }
        }

        /// <summary>
        /// Même traitement pour le champ de la coordonnée en Y
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CoordY_ValueChanged(object sender, EventArgs e)
        {
            CoordX_ValueChanged(new object(), new EventArgs());
        }

        /// <summary>
        /// Affiche les coordonnées en X et en Y d'après le clic sur la carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Point positionSouris = new Point(Convert.ToInt32(Math.Round(e.X * echCarteNiv3)), Convert.ToInt32(Math.Round(e.Y * echCarteNiv3))); //echCarteNiv3=facteur d'échelle
            if (pictureBox1.Enabled) //Si l'activité sélectionnée est une activité d'exploration
            {            
                if (NomLieu.Text == "Base")
                {
                    NomLieu.Text = "";
                }
                
                //Calcul de la position de la croix en fonction de la position du clic par rapport au cadran dans lequel le clic se situe
                CoordX.Text = Convert.ToString((positionSouris.X - coordBase.X)); //La valeur du champ a changé, on actualise la croix avec les nouvelles coordonnées
                if (e.Y < coordBase.Y) //Sur les machines un peu lentes, on constate l'apparition éclair d'une croix fantome calculée sur l'ancienne valeur de Y 
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


        //ACTIVITE D'EXPLORATION//
        //Ce groupbox contient la carte d'exploration avec icônes cliquables

        /// <summary>
        /// Permet d'afficher la fiche d'exploration avec les icônes placés sur la carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
       
            //On affiche l'icône en fonction du numéro du jour (passé ou futur) et du type d'activité :

            for (int i = Convert.ToInt32(PeriodeDebut.Value) - 1; i < Convert.ToInt32(PeriodeFin.Value); i++) //On parcourt les jours
            {
                for (int j = 0; j < M.Calendar.Jours[i].ListeActivites.Count; j++) //On parcourt l'ensemble des activités du jour i
                {
                    if (M.Calendar.Jours[i].ListeActivites[j].Nom == "Exploration Space suit" || M.Calendar.Jours[i].ListeActivites[j].Nom == "Exploration Vehicule" || M.Calendar.Jours[i].ListeActivites[j].Nom == "Outside experiment")
                    {
                        PictureBox icone = new PictureBox();
                        icone.BackColor = Color.Transparent;
                        icone.Size = new Size(24, 24);
                        Carte.Controls.Add(icone);
                        icone.Cursor = System.Windows.Forms.Cursors.Hand;
                        icone.Tag = M.Calendar.Jours[i].ListeActivites[j]; //On associe l'activité à la picture box qui sert d'icône pour la récupérer après clic
                        icone.Controls.Add(new Control(Convert.ToString(i))); //On ajoute un contrôle pour récupérer le numéro du jour
                        icone.Click += new System.EventHandler(this.ClickIconeCarte);

                        //On place et on centre l'icône (-12 en X et en Y pour centrer l'icone de 24*24px)                       
                        if (M.Calendar.Jours[i].ListeActivites[j].Gps.Coords.Y >= 0)
                        {
                            icone.Location = new Point(Convert.ToInt32(Math.Round((M.Calendar.Jours[i].ListeActivites[j].Gps.Coords.X + coordBase.X) / echCarteNiv3 * echCarteExplo)) - 12, Convert.ToInt32(Math.Round((coordBase.Y - M.Calendar.Jours[i].ListeActivites[j].Gps.Coords.Y) / echCarteNiv3 * echCarteExplo)) - 12);
                        }
                        else
                        {
                            icone.Location = new Point(Convert.ToInt32(Math.Round((M.Calendar.Jours[i].ListeActivites[j].Gps.Coords.X + coordBase.X) / echCarteNiv3 * echCarteExplo)) - 12, -1 * (Convert.ToInt32(Math.Round((M.Calendar.Jours[i].ListeActivites[j].Gps.Coords.Y - coordBase.Y) / echCarteNiv3 * echCarteExplo))) - 12);
                        }

                        //Affichage de l'icône correspondant au type d'activité et distinction entre passé/futur
                        if (M.Calendar.Jours[i].ListeActivites[j].Nom == "Exploration Space suit")
                        {
                            if (i < int.Parse(JourCourantMission.Text)-1)
                            {
                                icone.Image = scaphandrePasse;
                            }
                            else
                            {
                                if (i == int.Parse(JourCourantMission.Text)-1)
                                {
                                    icone.Image = scaphandreActuel;
                                }
                                else
                                {
                                    icone.Image = scaphandreFutur;
                                }
                            }
                        }
                        if (M.Calendar.Jours[i].ListeActivites[j].Nom == "Exploration Vehicule")
                        {
                            if (i < int.Parse(JourCourantMission.Text)-1)
                            {
                                icone.Image = vehiculePasse;
                            }
                            else
                            {
                                if (i == int.Parse(JourCourantMission.Text)-1)
                                {
                                    icone.Image = vehiculeActuel;
                                }
                                else
                                {
                                    icone.Image = vehiculeFutur;
                                }
                            }
                        }
                        if (M.Calendar.Jours[i].ListeActivites[j].Nom == "Outside experiment")
                        {
                            if (i < int.Parse(JourCourantMission.Text)-1)
                            {
                                icone.Image = experiencePasse;
                            }
                            else
                            {
                                if (i == int.Parse(JourCourantMission.Text)-1)
                                {
                                    icone.Image = experienceActuelle;
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
        }


       
        /// <summary>
        /// Affiche les informations relatives à l'activité (icône cliqué)
        /// </summary>
        /// <param name="sender">Picturebox utilisée pour afficher les informations</param>
        /// <param name="e"></param>
        private void ClickIconeCarte(object sender, EventArgs e)
        {
            ActExplo_Click(new object(), new EventArgs());
            PictureBox p = (PictureBox)sender;
            Activite activiteClickee = (Activite)p.Tag;
            InfoLieu.Text = activiteClickee.Gps.Nom;
            InfoActivite.Text = activiteClickee.Nom;
            InfoNumJour.Text = Convert.ToString(Convert.ToInt32(p.Controls[0].Text) + 1);
            InfoHDebut.Text = Convert.ToString(activiteClickee.Debut.Heures);
            InfoMDebut.Text = Convert.ToString(activiteClickee.Debut.Minutes);
            InfoHFin.Text = Convert.ToString(activiteClickee.Fin.Heures);
            InfoMFin.Text = Convert.ToString(activiteClickee.Fin.Minutes);
            for (int i = 0; i < activiteClickee.ListAstronaute.Count; i++)
            {
                InfoAstronautes.Items.Add(activiteClickee.ListAstronaute[i]);
            }
            InfoDescriptif.Text = activiteClickee.TexteDescriptif;
        }

        /// <summary>
        /// Affichage des icônes compris entre deux jours (les icônes des jours mentionnés sont affichés également)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValiderPeriode_Click(object sender, EventArgs e)
        {
            ActExplo_Click(new object(), new EventArgs());
        }
            

        /// <summary>
        /// Retour au calendrier ou au planning (selon le niveau où on a cliqué)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Retour_Click(object sender, EventArgs e)
        {
            ActiviteExploration.Visible = false;
        }



        //PARAMETRES

        /// <summary>
        /// Permet d'accéder à la fiche de gestion des astronautes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Parametres_Click(object sender, EventArgs e)
        {
            Astronautes modif = new Astronautes(M.ListAstr);
            modif.Show();
            M.ListAstr = modif.Astro();
        }



        //RECHERCHE

        /// <summary>
        /// Permet d'accéder au form Recherche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, EventArgs e)
        {
            Recherche R = new Recherche(M.ListAstr, M.Listcat, M.Calendar);
            R.Show();
        }


        //RETOUR ACCUEIL

        /// <summary>
        /// Permet de retourner au calendrier (niveau 1) depuis n'importe quel niveau de GestionMission 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Home_Click(object sender, EventArgs e)
        {
            Niveau1.Show();
            Niveau2.Hide();
            Niveau3.Hide();
            ActiviteExploration.Hide();
            trackBar1_Scroll(sender, e);
        }

        //FERMETURE DE L'APPLICATION

        /// <summary>
        /// A la fermeture de l'application, 2 fichiers XML sont générés pour sauvegarder l'ensemble des données
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GestionMission_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Génération XML 
            xmlDoc = new XmlDocument();
            xmlDoc2 = new XmlDocument();
            M.Calendar.Last = System.DateTime.Now;
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
    }
}

