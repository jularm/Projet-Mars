namespace Logiciel
{
    partial class Astronautes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Astronautes));
            this.label1 = new System.Windows.Forms.Label();
            this.AstronautesMission = new System.Windows.Forms.ListBox();
            this.Nom = new System.Windows.Forms.Label();
            this.NomAstronaute = new System.Windows.Forms.TextBox();
            this.AjouterAstronaute = new System.Windows.Forms.Button();
            this.SupprimerAstronaute = new System.Windows.Forms.Button();
            this.ConfirmerAstronaute = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(359, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Initialisation des Astronautes de la mission";
            // 
            // AstronautesMission
            // 
            this.AstronautesMission.FormattingEnabled = true;
            this.AstronautesMission.Location = new System.Drawing.Point(12, 130);
            this.AstronautesMission.Name = "AstronautesMission";
            this.AstronautesMission.Size = new System.Drawing.Size(232, 264);
            this.AstronautesMission.TabIndex = 1;
            this.AstronautesMission.SelectedIndexChanged += new System.EventHandler(this.AstronautesMission_SelectedIndexChanged);
            // 
            // Nom
            // 
            this.Nom.AutoSize = true;
            this.Nom.Location = new System.Drawing.Point(12, 59);
            this.Nom.Name = "Nom";
            this.Nom.Size = new System.Drawing.Size(35, 13);
            this.Nom.TabIndex = 11;
            this.Nom.Text = "Nom :";
            // 
            // NomAstronaute
            // 
            this.NomAstronaute.Location = new System.Drawing.Point(47, 57);
            this.NomAstronaute.Name = "NomAstronaute";
            this.NomAstronaute.Size = new System.Drawing.Size(197, 20);
            this.NomAstronaute.TabIndex = 10;
            this.NomAstronaute.TextChanged += new System.EventHandler(this.NomAstronaute_TextChanged);
            // 
            // AjouterAstronaute
            // 
            this.AjouterAstronaute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AjouterAstronaute.Location = new System.Drawing.Point(250, 51);
            this.AjouterAstronaute.Name = "AjouterAstronaute";
            this.AjouterAstronaute.Size = new System.Drawing.Size(120, 33);
            this.AjouterAstronaute.TabIndex = 12;
            this.AjouterAstronaute.Text = "Ajouter";
            this.AjouterAstronaute.UseVisualStyleBackColor = true;
            this.AjouterAstronaute.Click += new System.EventHandler(this.AjouterAstronaute_Click);
            // 
            // SupprimerAstronaute
            // 
            this.SupprimerAstronaute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SupprimerAstronaute.Location = new System.Drawing.Point(250, 360);
            this.SupprimerAstronaute.Name = "SupprimerAstronaute";
            this.SupprimerAstronaute.Size = new System.Drawing.Size(120, 35);
            this.SupprimerAstronaute.TabIndex = 13;
            this.SupprimerAstronaute.Text = "Supprimer";
            this.SupprimerAstronaute.UseVisualStyleBackColor = true;
            this.SupprimerAstronaute.Click += new System.EventHandler(this.SupprimerAstronaute_Click);
            // 
            // ConfirmerAstronaute
            // 
            this.ConfirmerAstronaute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConfirmerAstronaute.Location = new System.Drawing.Point(148, 426);
            this.ConfirmerAstronaute.Name = "ConfirmerAstronaute";
            this.ConfirmerAstronaute.Size = new System.Drawing.Size(146, 66);
            this.ConfirmerAstronaute.TabIndex = 14;
            this.ConfirmerAstronaute.Text = "Confirmer";
            this.ConfirmerAstronaute.UseVisualStyleBackColor = true;
            this.ConfirmerAstronaute.Click += new System.EventHandler(this.ConfirmerAstronaute_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Astronautes affectés à la mission :";
            // 
            // Astronautes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 516);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ConfirmerAstronaute);
            this.Controls.Add(this.SupprimerAstronaute);
            this.Controls.Add(this.AjouterAstronaute);
            this.Controls.Add(this.Nom);
            this.Controls.Add(this.NomAstronaute);
            this.Controls.Add(this.AstronautesMission);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Astronautes";
            this.Text = "Astronautes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox AstronautesMission;
        private System.Windows.Forms.Label Nom;
        private System.Windows.Forms.TextBox NomAstronaute;
        private System.Windows.Forms.Button AjouterAstronaute;
        private System.Windows.Forms.Button SupprimerAstronaute;
        private System.Windows.Forms.Button ConfirmerAstronaute;
        private System.Windows.Forms.Label label3;
    }
}