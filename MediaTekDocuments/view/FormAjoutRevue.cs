﻿using MediaTekDocuments.controller;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MediaTekDocuments.view
{
    public partial class FormAjoutRevue : Form
    {
        private readonly FrmMediatek view;
        private readonly FrmMediatekController controller;

        private readonly BindingSource Genres = new BindingSource();
        private readonly BindingSource Publics = new BindingSource();
        private readonly BindingSource Rayons = new BindingSource();
        public FormAjoutRevue(List<BindingSource> data)
        {
            InitializeComponent();
            this.controller = new FrmMediatekController();
            this.view = new FrmMediatek("111");

            this.Genres.DataSource = data[0].DataSource;
            this.Publics.DataSource = data[1].DataSource;
            this.Rayons.DataSource = data[2].DataSource;
            FormAjoutRevue_Load();
        }

        private void FormAjoutRevue_Load()
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

        private void AjouterRevue_Click(object sender, EventArgs e)
        {
            if (ValidateForm().success)
            {
                List<string> list = new List<string>
                {
                    textBoxTitre.Text,
                    comboBoxGenre.SelectedValue.ToString(),
                    comboBoxPublic.SelectedValue.ToString(),
                    comboBoxRayon.SelectedValue.ToString(),
                    txtPeriodicite.Text,
                    numDMAD.Value.ToString(),
                    controller.GenererId("revue")
                };
                if (view.AjouterRevue(list))
                {
                    this.Close();
                }
            }
            else
            {
                ErrorMsg.Text = "/!\\ " + ValidateForm().message;
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

            if (string.IsNullOrEmpty(txtPeriodicite.Text))
            {
                return (false, "Veuillez ajouter un Réalisateur");
            }

            if ((Convert.ToInt32(numDMAD.Value) < 0))
            {
                return (false, "Veuillez ajouter un Synopsis");
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