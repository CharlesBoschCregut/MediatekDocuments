namespace MediaTekDocuments.view
{
    partial class Confirm
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
            this.NotConfirm = new System.Windows.Forms.Button();
            this.YesConfirm = new System.Windows.Forms.Button();
            this.message = new System.Windows.Forms.Label();
            this.Ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NotConfirm
            // 
            this.NotConfirm.Location = new System.Drawing.Point(31, 91);
            this.NotConfirm.Name = "NotConfirm";
            this.NotConfirm.Size = new System.Drawing.Size(75, 23);
            this.NotConfirm.TabIndex = 1;
            this.NotConfirm.Text = "Annuler";
            this.NotConfirm.UseVisualStyleBackColor = true;
            this.NotConfirm.Click += new System.EventHandler(this.NotConfirm_Click);
            // 
            // YesConfirm
            // 
            this.YesConfirm.Location = new System.Drawing.Point(128, 91);
            this.YesConfirm.Name = "YesConfirm";
            this.YesConfirm.Size = new System.Drawing.Size(75, 23);
            this.YesConfirm.TabIndex = 2;
            this.YesConfirm.Text = "Supprimer";
            this.YesConfirm.UseVisualStyleBackColor = true;
            this.YesConfirm.Click += new System.EventHandler(this.YesConfirm_Click);
            // 
            // message
            // 
            this.message.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.message.AutoSize = true;
            this.message.Location = new System.Drawing.Point(13, 24);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(201, 13);
            this.message.TabIndex = 3;
            this.message.Text = "Etes vous sur de vouloir supprimer 00019";
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(75, 91);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 4;
            this.Ok.Text = "OK";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Visible = false;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Confirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 134);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.message);
            this.Controls.Add(this.YesConfirm);
            this.Controls.Add(this.NotConfirm);
            this.Name = "Confirm";
            this.Text = "Message";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button NotConfirm;
        private System.Windows.Forms.Button YesConfirm;
        private System.Windows.Forms.Label message;
        private System.Windows.Forms.Button Ok;
    }
}