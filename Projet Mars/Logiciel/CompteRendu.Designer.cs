namespace Logiciel
{
    partial class CompteRendu
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
            this.texteCR = new System.Windows.Forms.RichTextBox();
            this.ConfirmerCR = new System.Windows.Forms.Button();
            this.AnnulerCR = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // texteCR
            // 
            this.texteCR.Location = new System.Drawing.Point(13, 13);
            this.texteCR.MaxLength = 1000;
            this.texteCR.Name = "texteCR";
            this.texteCR.Size = new System.Drawing.Size(440, 346);
            this.texteCR.TabIndex = 0;
            this.texteCR.Text = "";
            // 
            // ConfirmerCR
            // 
            this.ConfirmerCR.Location = new System.Drawing.Point(148, 366);
            this.ConfirmerCR.Name = "ConfirmerCR";
            this.ConfirmerCR.Size = new System.Drawing.Size(75, 23);
            this.ConfirmerCR.TabIndex = 1;
            this.ConfirmerCR.Text = "Confirmer";
            this.ConfirmerCR.UseVisualStyleBackColor = true;
            this.ConfirmerCR.Click += new System.EventHandler(this.ConfirmerCR_Click);
            // 
            // AnnulerCR
            // 
            this.AnnulerCR.Location = new System.Drawing.Point(229, 366);
            this.AnnulerCR.Name = "AnnulerCR";
            this.AnnulerCR.Size = new System.Drawing.Size(75, 23);
            this.AnnulerCR.TabIndex = 2;
            this.AnnulerCR.Text = "Annuler";
            this.AnnulerCR.UseVisualStyleBackColor = true;
            this.AnnulerCR.Click += new System.EventHandler(this.AnnulerCR_Click);
            // 
            // CompteRendu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 400);
            this.Controls.Add(this.AnnulerCR);
            this.Controls.Add(this.ConfirmerCR);
            this.Controls.Add(this.texteCR);
            this.Name = "CompteRendu";
            this.Text = "Compte-rendu de la journée";
            this.ResumeLayout(false);

        }

        #endregion

        //Le form contient une zone de texte, un bouton pour confirmer et un autre pour annuler
        private System.Windows.Forms.RichTextBox texteCR;
        private System.Windows.Forms.Button ConfirmerCR;
        private System.Windows.Forms.Button AnnulerCR;
    }
}