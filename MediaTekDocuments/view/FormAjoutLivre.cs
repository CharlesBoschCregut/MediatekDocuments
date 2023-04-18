using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MediaTekDocuments.controller;

namespace MediaTekDocuments.view
{
    public partial class FormAjoutLivre : Form
    {
        private readonly FrmMediatek view;
        private readonly FrmMediatekController controller;

        private readonly BindingSource Genres = new BindingSource();
        private readonly BindingSource Publics = new BindingSource();
        private readonly BindingSource Rayons = new BindingSource();

        public FormAjoutLivre(List<BindingSource> data)
        {
            InitializeComponent();
            this.view = new FrmMediatek();
            this.controller = new FrmMediatekController();
             
            this.Genres.DataSource = data[0].DataSource;
            this.Publics.DataSource = data[1].DataSource;
            this.Rayons.DataSource = data[2].DataSource;
            FormAjoutLivre_Load();
        }

        private void FormAjoutLivre_Load()
        {
            comboBoxGenre.DataSource = Genres;
            comboBoxGenre.DisplayMember = "Libelle";
            comboBoxGenre.ValueMember = "Id";
            comboBoxGenre.SelectedIndex = -1;

            comboBoxPublic.DataSource = Publics;
            comboBoxPublic.DisplayMember = "Libelle";
            comboBoxPublic.ValueMember = "Id";
            comboBoxPublic.SelectedIndex = -1;

            comboBoxRayon.DataSource = Rayons;
            comboBoxRayon.DisplayMember = "Libelle";
            comboBoxRayon.ValueMember = "Id";
            comboBoxRayon.SelectedIndex = -1;
        }

        private void AjouterLivre_Click(object sender, EventArgs e)
        {
            var valid = ValidateForm();
            if (valid.success)
            {
                List<string> list = new List<string>
                {
                    textBoxTitre.Text,
                    comboBoxGenre.SelectedValue.ToString(),
                    comboBoxPublic.SelectedValue.ToString(),
                    comboBoxRayon.SelectedValue.ToString(),
                    txtIsbn.Text,
                    txtAuteur.Text,
                    txtCollection.Text,
                    controller.GenererId("livre")
                };
                if (view.AjouterLivre(list))
                {
                    this.Close();
                }
            }
            else
            {
                ErrorMsg.Text = "/!\\ " + valid.message;
                ErrorMsg.ForeColor = Color.Red;
                ErrorMsg.Font = new Font("Arial", 12);
            }

        }

        private (bool success, string message) ValidateForm()
        {
            if (string.IsNullOrEmpty(textBoxTitre.Text))
            {
                return (false, "Veuillez ajouter un titre");
            }

            if (string.IsNullOrEmpty(txtIsbn.Text))
            {
                return (false, "Veuillez ajouter un ISBN");
            }

            if (string.IsNullOrEmpty(txtAuteur.Text))
            {
                return (false, "Veuillez ajouter un Auteur");
            }

            if (string.IsNullOrEmpty(txtCollection.Text))
            {
                return (false, "Veuillez ajouter une Collection");
            }

            if (comboBoxGenre.SelectedIndex == -1)
            {
                return (false, "Veuillez choisir un Genre");
            }

            if (comboBoxPublic.SelectedIndex == -1)
            {
                return (false, "Veuillez choisir un Public");
            }

            if (comboBoxRayon.SelectedIndex == -1)
            {
                return (false, "Veuillez choisir un Rayon");
            }

            return (true, "Le formulaire est validé");
        }

    }
}
