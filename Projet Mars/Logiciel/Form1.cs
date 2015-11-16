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
<<<<<<< HEAD
            jour42.BackgroundImage = sortie;           
            c.Jour = 12;
=======
            jour42.BackgroundImage = sortie;
>>>>>>> origin/master
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            c.Horloge();
            heureMars.Text = c.Heure.ToString() + " h " + c.Minute.ToString() + " min " + c.Seconde.ToString() + " s";
            nbrJour.Text = c.Jour.ToString();
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
                        if (int.Parse(groupBox1.Controls[i].Text) < c.Jour)
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
                if (int.Parse(test.Text) == c.Jour)
                {
                    test.BackColor = Color.RoyalBlue;
                }
                else
                {
                    if (int.Parse(test.Text) < c.Jour)
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
        }
     
        private void button3_Click(object sender, EventArgs e)
        {
            Niveau3.Visible = true;
            comboBox1.Text = "Sleeping";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Niveau3.Visible = false;
        }

        private void CreerActivite_Click(object sender, EventArgs e)
        {
            Niveau3.Visible = true;
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
        }

        private void JourSuivant_Click(object sender, EventArgs e)
        {
            if (int.Parse(NumeroJour.Text) < 500)
            {
                NumeroJour.Text = Convert.ToString(int.Parse(NumeroJour.Text) + 1);
            }
        }





       
       

       

       


    }
}

