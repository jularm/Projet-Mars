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
    public partial class FenetreParametres : Form // à revoir ça ne marche pas du tout...
    {
        private TreeNode Living;
        private List<TreeNode> LivingNode = new List<TreeNode>();
        private int cptLivingNode = 0; //compte les noeuds enfants ajoutés à LivingNode

        public FenetreParametres()
        {
            InitializeComponent();
        }

        private void FenetreParametres_Load(object sender, EventArgs e)
        {
            LivingNode.Add(new System.Windows.Forms.TreeNode("Eating"));
            cptLivingNode++;
            LivingNode.Add(new System.Windows.Forms.TreeNode("Sleeping"));
            cptLivingNode++;
            LivingNode.Add(new System.Windows.Forms.TreeNode("Entertainment"));
            cptLivingNode++;
            LivingNode.Add(new System.Windows.Forms.TreeNode("Private"));
            cptLivingNode++;
            LivingNode.Add(new System.Windows.Forms.TreeNode("Health control"));
            cptLivingNode++;
            LivingNode.Add(new System.Windows.Forms.TreeNode("Medical act"));
            cptLivingNode++;
            /*for (int i = 1; i <= cptLivingNode++; i++)
            {
                Living = new System.Windows.Forms.TreeNode("Living", new System.Windows.Forms.TreeNode[i]);
                this.VueActivites.Nodes.AddRange(new System.Windows.Forms.TreeNode[i]);
            }  */                 
        }     
    }
}
