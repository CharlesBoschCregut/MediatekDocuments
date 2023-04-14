using System.Web;

namespace MediaTekDocuments.view
{
    partial class FormAjoutLivre
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
            this.txtAuteur = new System.Windows.Forms.TextBox();
            this.txtCollection = new System.Windows.Forms.TextBox();
            this.AjouterLivre = new System.Windows.Forms.Button();
            this.labeltxtAuteur = new System.Windows.Forms.Label();
            this.labeltxtCollection = new System.Windows.Forms.Label();
            this.labeltxtIsbn = new System.Windows.Forms.Label();
            this.txtIsbn = new System.Windows.Forms.TextBox();
            this.textBoxTitre = new System.Windows.Forms.TextBox();
            this.comboBoxPublic = new System.Windows.Forms.ComboBox();
            this.comboBoxRayon = new System.Windows.Forms.ComboBox();
            this.comboBoxGenre = new System.Windows.Forms.ComboBox();
            this.labeltxtTitre = new System.Windows.Forms.Label();
            this.labeltxtPublic = new System.Windows.Forms.Label();
            this.labeltxtRayon = new System.Windows.Forms.Label();
            this.labeltxtGenre = new System.Windows.Forms.Label();
            this.ErrorMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtAuteur
            // 
            this.txtAuteur.Location = new System.Drawing.Point(341, 38);
            this.txtAuteur.MaxLength = 20;
            this.txtAuteur.Name = "txtAuteur";
            this.txtAuteur.Size = new System.Drawing.Size(153, 20);
            this.txtAuteur.TabIndex = 1;
            // 
            // txtCollection
            // 
            this.txtCollection.Location = new System.Drawing.Point(341, 101);
            this.txtCollection.MaxLength = 50;
            this.txtCollection.Name = "txtCollection";
            this.txtCollection.Size = new System.Drawing.Size(153, 20);
            this.txtCollection.TabIndex = 3;
            // 
            // AjouterLivre
            // 
            this.AjouterLivre.Location = new System.Drawing.Point(534, 227);
            this.AjouterLivre.Name = "AjouterLivre";
            this.AjouterLivre.Size = new System.Drawing.Size(75, 23);
            this.AjouterLivre.TabIndex = 7;
            this.AjouterLivre.Text = "Enregistrer";
            this.AjouterLivre.UseVisualStyleBackColor = true;
            this.AjouterLivre.Click += new System.EventHandler(this.AjouterLivre_Click);
            // 
            // labeltxtAuteur
            // 
            this.labeltxtAuteur.AutoSize = true;
            this.labeltxtAuteur.Location = new System.Drawing.Point(338, 19);
            this.labeltxtAuteur.Name = "labeltxtAuteur";
            this.labeltxtAuteur.Size = new System.Drawing.Size(38, 13);
            this.labeltxtAuteur.TabIndex = 5;
            this.labeltxtAuteur.Text = "Auteur";
            // 
            // labeltxtCollection
            // 
            this.labeltxtCollection.AutoSize = true;
            this.labeltxtCollection.Location = new System.Drawing.Point(338, 82);
            this.labeltxtCollection.Name = "labeltxtCollection";
            this.labeltxtCollection.Size = new System.Drawing.Size(53, 13);
            this.labeltxtCollection.TabIndex = 6;
            this.labeltxtCollection.Text = "Collection";
            // 
            // labeltxtIsbn
            // 
            this.labeltxtIsbn.AutoSize = true;
            this.labeltxtIsbn.Location = new System.Drawing.Point(43, 82);
            this.labeltxtIsbn.Name = "labeltxtIsbn";
            this.labeltxtIsbn.Size = new System.Drawing.Size(27, 13);
            this.labeltxtIsbn.TabIndex = 4;
            this.labeltxtIsbn.Text = "Isbn";
            // 
            // txtIsbn
            // 
            this.txtIsbn.Location = new System.Drawing.Point(46, 101);
            this.txtIsbn.MaxLength = 13;
            this.txtIsbn.Name = "txtIsbn";
            this.txtIsbn.Size = new System.Drawing.Size(176, 20);
            this.txtIsbn.TabIndex = 2;
            // 
            // textBoxTitre
            // 
            this.textBoxTitre.Location = new System.Drawing.Point(46, 39);
            this.textBoxTitre.MaxLength = 60;
            this.textBoxTitre.Name = "textBoxTitre";
            this.textBoxTitre.Size = new System.Drawing.Size(176, 20);
            this.textBoxTitre.TabIndex = 0;
            // 
            // comboBoxPublic
            // 
            this.comboBoxPublic.FormattingEnabled = true;
            this.comboBoxPublic.Location = new System.Drawing.Point(410, 167);
            this.comboBoxPublic.Name = "comboBoxPublic";
            this.comboBoxPublic.Size = new System.Drawing.Size(124, 21);
            this.comboBoxPublic.TabIndex = 6;
            // 
            // comboBoxRayon
            // 
            this.comboBoxRayon.FormattingEnabled = true;
            this.comboBoxRayon.Location = new System.Drawing.Point(238, 167);
            this.comboBoxRayon.Name = "comboBoxRayon";
            this.comboBoxRayon.Size = new System.Drawing.Size(138, 21);
            this.comboBoxRayon.TabIndex = 5;
            // 
            // comboBoxGenre
            // 
            this.comboBoxGenre.FormattingEnabled = true;
            this.comboBoxGenre.Location = new System.Drawing.Point(52, 164);
            this.comboBoxGenre.Name = "comboBoxGenre";
            this.comboBoxGenre.Size = new System.Drawing.Size(137, 21);
            this.comboBoxGenre.TabIndex = 4;
            // 
            // labeltxtTitre
            // 
            this.labeltxtTitre.AutoSize = true;
            this.labeltxtTitre.Location = new System.Drawing.Point(43, 19);
            this.labeltxtTitre.Name = "labeltxtTitre";
            this.labeltxtTitre.Size = new System.Drawing.Size(28, 13);
            this.labeltxtTitre.TabIndex = 11;
            this.labeltxtTitre.Text = "Titre";
            // 
            // labeltxtPublic
            // 
            this.labeltxtPublic.AutoSize = true;
            this.labeltxtPublic.Location = new System.Drawing.Point(410, 148);
            this.labeltxtPublic.Name = "labeltxtPublic";
            this.labeltxtPublic.Size = new System.Drawing.Size(36, 13);
            this.labeltxtPublic.TabIndex = 12;
            this.labeltxtPublic.Text = "Public";
            // 
            // labeltxtRayon
            // 
            this.labeltxtRayon.AutoSize = true;
            this.labeltxtRayon.Location = new System.Drawing.Point(235, 148);
            this.labeltxtRayon.Name = "labeltxtRayon";
            this.labeltxtRayon.Size = new System.Drawing.Size(38, 13);
            this.labeltxtRayon.TabIndex = 13;
            this.labeltxtRayon.Text = "Rayon";
            // 
            // labeltxtGenre
            // 
            this.labeltxtGenre.AutoSize = true;
            this.labeltxtGenre.Location = new System.Drawing.Point(49, 148);
            this.labeltxtGenre.Name = "labeltxtGenre";
            this.labeltxtGenre.Size = new System.Drawing.Size(36, 13);
            this.labeltxtGenre.TabIndex = 14;
            this.labeltxtGenre.Text = "Genre";
            // 
            // ErrorMsg
            // 
            this.ErrorMsg.AutoSize = true;
            this.ErrorMsg.Location = new System.Drawing.Point(222, 227);
            this.ErrorMsg.Name = "ErrorMsg";
            this.ErrorMsg.Size = new System.Drawing.Size(0, 13);
            this.ErrorMsg.TabIndex = 15;
            // 
            // FormAjoutLivre
            // 
            this.AcceptButton = this.AjouterLivre;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 262);
            this.Controls.Add(this.ErrorMsg);
            this.Controls.Add(this.labeltxtGenre);
            this.Controls.Add(this.labeltxtRayon);
            this.Controls.Add(this.labeltxtPublic);
            this.Controls.Add(this.labeltxtTitre);
            this.Controls.Add(this.comboBoxGenre);
            this.Controls.Add(this.comboBoxRayon);
            this.Controls.Add(this.comboBoxPublic);
            this.Controls.Add(this.textBoxTitre);
            this.Controls.Add(this.labeltxtCollection);
            this.Controls.Add(this.labeltxtAuteur);
            this.Controls.Add(this.labeltxtIsbn);
            this.Controls.Add(this.AjouterLivre);
            this.Controls.Add(this.txtCollection);
            this.Controls.Add(this.txtAuteur);
            this.Controls.Add(this.txtIsbn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormAjoutLivre";
            this.Text = "Ajouter un Livre";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtAuteur;
        private System.Windows.Forms.TextBox txtCollection;
        private System.Windows.Forms.Button AjouterLivre;
        private System.Windows.Forms.Label labeltxtAuteur;
        private System.Windows.Forms.Label labeltxtCollection;
        private System.Windows.Forms.Label labeltxtIsbn;
        private System.Windows.Forms.TextBox txtIsbn;
        private System.Windows.Forms.TextBox textBoxTitre;
        private System.Windows.Forms.ComboBox comboBoxPublic;
        private System.Windows.Forms.ComboBox comboBoxRayon;
        private System.Windows.Forms.ComboBox comboBoxGenre;
        private System.Windows.Forms.Label labeltxtTitre;
        private System.Windows.Forms.Label labeltxtPublic;
        private System.Windows.Forms.Label labeltxtRayon;
        private System.Windows.Forms.Label labeltxtGenre;
        private System.Windows.Forms.Label ErrorMsg;
    }
}