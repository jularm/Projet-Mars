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
    public partial class Astronautes : Form
    {
        private Astronaute s;
        static int Id=0;
        private List<Astronaute> list = new List<Astronaute>();

        public Astronautes()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            InitializeComponent();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            Id++;
            Astronaute A = new Astronaute(Id, textBox1.Text);            
            list.Add(A);
            listBox1.Items.Add(A);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            s = (Astronaute)listBox1.SelectedItem;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            s = (Astronaute)listBox1.SelectedItem;
            list.Remove(s);
            listBox1.Items.Remove(s);
        }
        
    }
}
