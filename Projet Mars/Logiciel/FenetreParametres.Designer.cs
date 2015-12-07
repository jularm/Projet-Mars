namespace Logiciel
{
    partial class FenetreParametres
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Eating");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Living", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.VueActivites = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modifier la liste des activités";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(437, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Modifier la liste des astronautes";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(442, 100);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(279, 205);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // VueActivites
            // 
            this.VueActivites.Location = new System.Drawing.Point(48, 100);
            this.VueActivites.Name = "VueActivites";
            treeNode1.Name = "Nœud1";
            treeNode1.Text = "Eating";
            treeNode2.Name = "Nœud0";
            treeNode2.Text = "Living";
            this.VueActivites.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.VueActivites.Size = new System.Drawing.Size(257, 326);
            this.VueActivites.TabIndex = 3;
            // 
            // FenetreParametres
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 579);
            this.Controls.Add(this.VueActivites);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FenetreParametres";
            this.Text = "FenetreParametres";
            this.Load += new System.EventHandler(this.FenetreParametres_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TreeView VueActivites;
    }
}