namespace MediaTekDocuments.view
{
    partial class FormAjoutRevue
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
            this.ErrorMsg = new System.Windows.Forms.Label();
            this.labeltxtGenre = new System.Windows.Forms.Label();
            this.labeltxtRayon = new System.Windows.Forms.Label();
            this.labeltxtPublic = new System.Windows.Forms.Label();
            this.labeltxtTitre = new System.Windows.Forms.Label();
            this.comboBoxGenre = new System.Windows.Forms.ComboBox();
            this.comboBoxRayon = new System.Windows.Forms.ComboBox();
            this.comboBoxPublic = new System.Windows.Forms.ComboBox();
            this.textBoxTitre = new System.Windows.Forms.TextBox();
            this.labeltxtCollection = new System.Windows.Forms.Label();
            this.labeltxtAuteur = new System.Windows.Forms.Label();
            this.AjouterRevue = new System.Windows.Forms.Button();
            this.txtPeriodicite = new System.Windows.Forms.TextBox();
            this.numDMAD = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numDMAD)).BeginInit();
            this.SuspendLayout();
            // 
            // ErrorMsg
            // 
            this.ErrorMsg.AutoSize = true;
            this.ErrorMsg.Location = new System.Drawing.Point(201, 216);
            this.ErrorMsg.Name = "ErrorMsg";
            this.ErrorMsg.Size = new System.Drawing.Size(0, 13);
            this.ErrorMsg.TabIndex = 31;
            // 
            // labeltxtGenre
            // 
            this.labeltxtGenre.AutoSize = true;
            this.labeltxtGenre.Location = new System.Drawing.Point(15, 137);
            this.labeltxtGenre.Name = "labeltxtGenre";
            this.labeltxtGenre.Size = new System.Drawing.Size(36, 13);
            this.labeltxtGenre.TabIndex = 30;
            this.labeltxtGenre.Text = "Genre";
            // 
            // labeltxtRayon
            // 
            this.labeltxtRayon.AutoSize = true;
            this.labeltxtRayon.Location = new System.Drawing.Point(201, 137);
            this.labeltxtRayon.Name = "labeltxtRayon";
            this.labeltxtRayon.Size = new System.Drawing.Size(38, 13);
            this.labeltxtRayon.TabIndex = 29;
            this.labeltxtRayon.Text = "Rayon";
            // 
            // labeltxtPublic
            // 
            this.labeltxtPublic.AutoSize = true;
            this.labeltxtPublic.Location = new System.Drawing.Point(376, 137);
            this.labeltxtPublic.Name = "labeltxtPublic";
            this.labeltxtPublic.Size = new System.Drawing.Size(36, 13);
            this.labeltxtPublic.TabIndex = 28;
            this.labeltxtPublic.Text = "Public";
            // 
            // labeltxtTitre
            // 
            this.labeltxtTitre.AutoSize = true;
            this.labeltxtTitre.Location = new System.Drawing.Point(9, 10);
            this.labeltxtTitre.Name = "labeltxtTitre";
            this.labeltxtTitre.Size = new System.Drawing.Size(28, 13);
            this.labeltxtTitre.TabIndex = 27;
            this.labeltxtTitre.Text = "Titre";
            // 
            // comboBoxGenre
            // 
            this.comboBoxGenre.FormattingEnabled = true;
            this.comboBoxGenre.Location = new System.Drawing.Point(18, 153);
            this.comboBoxGenre.Name = "comboBoxGenre";
            this.comboBoxGenre.Size = new System.Drawing.Size(153, 21);
            this.comboBoxGenre.TabIndex = 20;
            // 
            // comboBoxRayon
            // 
            this.comboBoxRayon.FormattingEnabled = true;
            this.comboBoxRayon.Location = new System.Drawing.Point(204, 156);
            this.comboBoxRayon.Name = "comboBoxRayon";
            this.comboBoxRayon.Size = new System.Drawing.Size(153, 21);
            this.comboBoxRayon.TabIndex = 22;
            // 
            // comboBoxPublic
            // 
            this.comboBoxPublic.FormattingEnabled = true;
            this.comboBoxPublic.Location = new System.Drawing.Point(379, 156);
            this.comboBoxPublic.Name = "comboBoxPublic";
            this.comboBoxPublic.Size = new System.Drawing.Size(153, 21);
            this.comboBoxPublic.TabIndex = 24;
            // 
            // textBoxTitre
            // 
            this.textBoxTitre.Location = new System.Drawing.Point(12, 30);
            this.textBoxTitre.MaxLength = 60;
            this.textBoxTitre.Name = "textBoxTitre";
            this.textBoxTitre.Size = new System.Drawing.Size(189, 20);
            this.textBoxTitre.TabIndex = 16;
            // 
            // labeltxtCollection
            // 
            this.labeltxtCollection.AutoSize = true;
            this.labeltxtCollection.Location = new System.Drawing.Point(15, 70);
            this.labeltxtCollection.Name = "labeltxtCollection";
            this.labeltxtCollection.Size = new System.Drawing.Size(131, 13);
            this.labeltxtCollection.TabIndex = 25;
            this.labeltxtCollection.Text = "Délai de mise à disposition";
            // 
            // labeltxtAuteur
            // 
            this.labeltxtAuteur.AutoSize = true;
            this.labeltxtAuteur.Location = new System.Drawing.Point(201, 70);
            this.labeltxtAuteur.Name = "labeltxtAuteur";
            this.labeltxtAuteur.Size = new System.Drawing.Size(56, 13);
            this.labeltxtAuteur.TabIndex = 23;
            this.labeltxtAuteur.Text = "Periodicite";
            // 
            // AjouterRevue
            // 
            this.AjouterRevue.Location = new System.Drawing.Point(500, 216);
            this.AjouterRevue.Name = "AjouterRevue";
            this.AjouterRevue.Size = new System.Drawing.Size(75, 23);
            this.AjouterRevue.TabIndex = 26;
            this.AjouterRevue.Text = "Enregistrer";
            this.AjouterRevue.UseVisualStyleBackColor = true;
            this.AjouterRevue.Click += new System.EventHandler(this.AjouterRevue_Click);
            // 
            // txtPeriodicite
            // 
            this.txtPeriodicite.Location = new System.Drawing.Point(204, 89);
            this.txtPeriodicite.MaxLength = 20;
            this.txtPeriodicite.Name = "txtPeriodicite";
            this.txtPeriodicite.Size = new System.Drawing.Size(153, 20);
            this.txtPeriodicite.TabIndex = 17;
            // 
            // numDMAD
            // 
            this.numDMAD.Location = new System.Drawing.Point(18, 90);
            this.numDMAD.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numDMAD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDMAD.Name = "numDMAD";
            this.numDMAD.Size = new System.Drawing.Size(120, 20);
            this.numDMAD.TabIndex = 32;
            this.numDMAD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // FormAjoutRevue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 261);
            this.Controls.Add(this.numDMAD);
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
            this.Controls.Add(this.AjouterRevue);
            this.Controls.Add(this.txtPeriodicite);
            this.Name = "FormAjoutRevue";
            this.Text = "Ajouter une revue";
            ((System.ComponentModel.ISupportInitialize)(this.numDMAD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ErrorMsg;
        private System.Windows.Forms.Label labeltxtGenre;
        private System.Windows.Forms.Label labeltxtRayon;
        private System.Windows.Forms.Label labeltxtPublic;
        private System.Windows.Forms.Label labeltxtTitre;
        private System.Windows.Forms.ComboBox comboBoxGenre;
        private System.Windows.Forms.ComboBox comboBoxRayon;
        private System.Windows.Forms.ComboBox comboBoxPublic;
        private System.Windows.Forms.TextBox textBoxTitre;
        private System.Windows.Forms.Label labeltxtCollection;
        private System.Windows.Forms.Label labeltxtAuteur;
        private System.Windows.Forms.Button AjouterRevue;
        private System.Windows.Forms.TextBox txtPeriodicite;
        private System.Windows.Forms.NumericUpDown numDMAD;
    }
}