using System;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Data;

namespace MediaTekDocuments.view

{
    /// <summary>
    /// Classe d'affichage
    /// </summary>
    public partial class FrmMediatek : Form
    {
        #region Commun
        private readonly FrmMediatekController controller;
        private readonly BindingSource bdgGenres = new BindingSource();
        private readonly BindingSource bdgPublics = new BindingSource();
        private readonly BindingSource bdgRayons = new BindingSource();
        private readonly string service;

        /// <summary>
        /// Constructeur : création du contrôleur lié à ce formulaire
        /// </summary>
        public FrmMediatek(string service)
        {
            this.service = service;
            InitializeComponent();
            this.controller = new FrmMediatekController();
        }

        /// <summary>
        /// Rempli un des 3 combo (genre, public, rayon)
        /// </summary>
        /// <param name="lesCategories">liste des objets de type Genre ou Public ou Rayon</param>
        /// <param name="bdg">bindingsource contenant les informations</param>
        /// <param name="cbx">combobox à remplir</param>
        public void RemplirComboCategorie(List<Categorie> lesCategories, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesCategories;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
            {
                cbx.SelectedIndex = -1;
            }
        }
        #endregion

        #region Onglet Livres
        private readonly BindingSource bdgLivresListe = new BindingSource();
        private readonly BindingSource bdgExemplaireLivre = new BindingSource();
        private readonly BindingSource bdgEtats = new BindingSource();
        private List<Livre> lesLivres = new List<Livre>();
        private List<Exemplaire> lesExemplairesLivres = new List<Exemplaire>();

        /// <summary>
        /// Ouverture de l'onglet Livres : 
        /// appel des méthodes pour remplir le datagrid des livres et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabLivres_Enter(object sender, EventArgs e)
        {
            if (this.service == "001")
            {
                ajouterLivre.Visible = false;
                ajouterLivre.Enabled = false;
                editerLivre.Visible = false;
                editerLivre.Enabled = false;
                supprimerLivre.Visible = false;
                supprimerLivre.Enabled = false;
                groupBox6.Enabled = false;
                groupBox6.Visible = false;
                groupBox7.Enabled = false;
                groupBox7.Visible = false;
            }
            RechargerLivres();
        }

        public void RechargerLivres()
        {
            lesLivres = controller.GetAllLivres();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxLivresGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxLivresPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxLivresRayons);
            bdgEtats.DataSource = controller.GetAllEtats();
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="livres">liste de livres</param>
        private void RemplirLivresListe(List<Livre> livres)
        {
            bdgLivresListe.DataSource = livres;
            dgvLivresListe.DataSource = bdgLivresListe;
            dgvLivresListe.Columns["isbn"].Visible = false;
            dgvLivresListe.Columns["idRayon"].Visible = false;
            dgvLivresListe.Columns["idGenre"].Visible = false;
            dgvLivresListe.Columns["idPublic"].Visible = false;
            dgvLivresListe.Columns["image"].Visible = false;
            dgvLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLivresListe.Columns["id"].DisplayIndex = 0;
            dgvLivresListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du livre dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbLivresNumRecherche.Text.Equals(""))
            {
                txbLivresTitreRecherche.Text = "";
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbLivresNumRecherche.Text));
                if (livre != null)
                {
                    List<Livre> livres = new List<Livre>() { livre };
                    RemplirLivresListe(livres);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirLivresListeComplete();
                }
            }
            else
            {
                RemplirLivresListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des livres dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbLivresTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbLivresTitreRecherche.Text.Equals(""))
            {
                Console.WriteLine(txbLivresTitreRecherche.Text);
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                txbLivresNumRecherche.Text = "";
                List<Livre> lesLivresParTitre;
                lesLivresParTitre = lesLivres.FindAll(x => x.Titre.ToLower().Contains(txbLivresTitreRecherche.Text.ToLower()));
                Console.WriteLine(lesLivresParTitre.Count);
                RemplirLivresListe(lesLivresParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxLivresGenres.SelectedIndex < 0 && cbxLivresPublics.SelectedIndex < 0 && cbxLivresRayons.SelectedIndex < 0
                    && txbLivresNumRecherche.Text.Equals(""))
                {
                    RemplirLivresListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du livre sélectionné
        /// </summary>
        /// <param name="livre">le livre</param>
        private void AfficheLivresInfos(Livre livre)
        {
            txbLivresAuteur.Text = livre.Auteur;
            txbLivresCollection.Text = livre.Collection;
            txbLivresImage.Text = livre.Image;
            txbLivresIsbn.Text = livre.Isbn;
            txbLivresNumero.Text = livre.Id;
            txbLivresGenre.Text = livre.Genre;
            txbLivresPublic.Text = livre.Public;
            txbLivresRayon.Text = livre.Rayon;
            txbLivresTitre.Text = livre.Titre;
            string image = livre.Image;
            try
            {
                pcbLivresImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbLivresImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du livre
        /// </summary>
        private void VideLivresInfos()
        {
            txbLivresAuteur.Text = "";
            txbLivresCollection.Text = "";
            txbLivresImage.Text = "";
            txbLivresIsbn.Text = "";
            txbLivresNumero.Text = "";
            txbLivresGenre.Text = "";
            txbLivresPublic.Text = "";
            txbLivresRayon.Text = "";
            txbLivresTitre.Text = "";
            pcbLivresImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresGenres.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Genre genre = (Genre)cbxLivresGenres.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresPublics.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Public lePublic = (Public)cbxLivresPublics.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresRayons.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxLivresRayons.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirLivresListe(livres);
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            ToggleEditionLivre(false);
            if (dgvLivresListe.CurrentCell != null)
            {
                try
                {
                    Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
                    AfficheLivresInfos(livre);
                    RemplirExemplairesLivres(livre.Id);
                }
                catch
                {
                    VideLivresZones();
                }
            }
            else
            {
                VideLivresInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des livres
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirLivresListeComplete()
        {
            RemplirLivresListe(lesLivres);
            VideLivresZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideLivresZones()
        {
            cbxLivresGenres.SelectedIndex = -1;
            cbxLivresRayons.SelectedIndex = -1;
            cbxLivresPublics.SelectedIndex = -1;
            txbLivresNumRecherche.Text = "";
            txbLivresTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideLivresZones();
            string titreColonne = dgvLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<Livre> sortedList = new List<Livre>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesLivres.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesLivres.OrderBy(o => o.Titre).ToList();
                    break;
                case "Collection":
                    sortedList = lesLivres.OrderBy(o => o.Collection).ToList();
                    break;
                case "Auteur":
                    sortedList = lesLivres.OrderBy(o => o.Auteur).ToList();
                    break;
                case "Genre":
                    sortedList = lesLivres.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesLivres.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesLivres.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirLivresListe(sortedList);
        }

        private void RemplirExemplairesLivres(string id)
        {
            dgvExemplairesLivres.DataSource = null;
            lesExemplairesLivres = controller.GetExemplairesRevue(id);

            if (lesExemplairesLivres.Count > 0)
            {
                bdgExemplaireLivre.DataSource = lesExemplairesLivres;
                dgvExemplairesLivres.DataSource = lesExemplairesLivres;
                dgvExemplairesLivres.Columns["Id"].Visible = false;
                dgvExemplairesLivres.Columns["IdEtat"].Visible = false;
                dgvExemplairesLivres.Columns["photo"].Visible = false;
                dgvExemplairesLivres.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                ViderInfosExemplaireLivre();
                dgvExemplairesLivres.ClearSelection();
            }
        }

        private void RemplirInfosExemplaireLivre(Exemplaire exemplaire)
        {
            txbIdExemplaireLivre.Text = exemplaire.Numero.ToString();
            txbIdExemplaireLivre2.Text = exemplaire.Numero.ToString();
            txbEtatExemplaireLivre.Text = exemplaire.Etat;

            cbxSetEtatLivre.DataSource = bdgEtats;
            cbxSetEtatLivre.DisplayMember = "Libelle";
            cbxSetEtatLivre.ValueMember = "Id";
            cbxSetEtatLivre.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxSetEtatLivre.FlatStyle = FlatStyle.Flat;
        }

        private void ViderInfosExemplaireLivre()
        {
            txbIdExemplaireLivre.Text = "";
            txbIdExemplaireLivre2.Text = "";
            txbEtatExemplaireLivre.Text = "";

            cbxSetEtatLivre.DataSource = null;
        }

        private void dgvExemplairesLivres_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvExemplairesLivres.Rows[e.RowIndex];
                if (selectedRow != null)
                {
                    string numero = selectedRow.Cells["Numero"].Value.ToString();
                    var exemplaireList = bdgExemplaireLivre.DataSource as List<Exemplaire>;
                    Exemplaire exemplaire = exemplaireList.FirstOrDefault(ex => ex.Numero == int.Parse(numero));
                    RemplirInfosExemplaireLivre(exemplaire);
                }
            }
        }

        private void dgvExemplairesLivres_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            string titreColonne = dgvExemplairesLivres.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sortedList;
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = lesExemplairesLivres.OrderBy(o => o.Numero).ToList();
                    break;
                case "DateAchat":
                    sortedList = lesExemplairesLivres.OrderByDescending(o => o.DateAchat).ToList();
                    break;
                case "Etat":
                    sortedList = lesExemplairesLivres.OrderBy(o => o.Etat).ToList();
                    break;
                default:
                    sortedList = lesExemplairesLivres.OrderByDescending(o => o.DateAchat).ToList();
                    break;
            }

            lesExemplairesLivres = sortedList;
            bdgExemplaireLivre.DataSource = lesExemplairesLivres;
            dgvExemplairesLivres.DataSource = lesExemplairesLivres;
            dgvExemplairesLivres.Columns["Id"].Visible = false;
            dgvExemplairesLivres.Columns["IdEtat"].Visible = false;
            dgvExemplairesLivres.Columns["photo"].Visible = false;
            dgvExemplairesLivres.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            ViderInfosExemplaireLivre();
            dgvExemplairesLivres.ClearSelection();
        }

        private void btnEditExemplaireLivre_Click(object sender, EventArgs e)
        {
            string id = txbIdExemplaireLivre.Text;
            if (id != "" && id != null)
            {
                DataGridViewRow selectedRow = dgvExemplairesLivres.SelectedRows[0];
                if (selectedRow != null)
                {
                    string numero = selectedRow.Cells["Numero"].Value.ToString();
                    var exemplaireList = bdgExemplaireLivre.DataSource as List<Exemplaire>;
                    Exemplaire exemplaire = exemplaireList.FirstOrDefault(ex => ex.Numero == int.Parse(numero));
                    if (exemplaire != null)
                    {
                        exemplaire.IdEtat = cbxSetEtatLivre.SelectedValue.ToString();
                        if (controller.EditerEtatExemplaire(exemplaire))
                        {
                            RefreshExemplaireLivre();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez séléctionner un exemplaire");
            }
        }

        private void RefreshExemplaireLivre()
        {
            DataGridViewRow selectedRow = dgvExemplairesLivres.SelectedRows[0];
            if (selectedRow != null)
            {
                string id = selectedRow.Cells["Id"].Value.ToString();
                RemplirExemplairesLivres(id);
            }
        }


        private void btnSupprExemplaireLivre_Click(object sender, EventArgs e)
        {
            string id = txbIdExemplaireLivre2.Text;
            if(id !=  "" && id != null)
            {
                Confirm f = new Confirm("Êtes-vous sûr de vouloir supprimer " + id, "exemplaire", id, controller.Suppr);
                f.Show();
                f.FormClosed += (s, args) =>
                {
                    RefreshExemplaireLivre();
                };
                f.Show();
            }
            else
            {
                MessageBox.Show("Veuillez séléctionner un exemplaire");
            }
        }

        #endregion

        #region Onglet Dvd
        private readonly BindingSource bdgDvdListe = new BindingSource();
        private List<Dvd> lesDvd = new List<Dvd>();

        /// <summary>
        /// Ouverture de l'onglet Dvds : 
        /// appel des méthodes pour remplir le datagrid des dvd et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabDvd_Enter(object sender, EventArgs e)
        {
            if (this.service == "001")
            {
                ajouterDvd.Visible = false;
                ajouterDvd.Enabled = false;
                editerDvd.Visible = false;
                editerDvd.Enabled = false;
                supprimerDvd.Visible = false;
                supprimerDvd.Enabled = false;
            }
            RechargerDvd();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="Dvds">liste de dvd</param>
        private void RemplirDvdListe(List<Dvd> Dvds)
        {
            bdgDvdListe.DataSource = Dvds;
            dgvDvdListe.DataSource = bdgDvdListe;
            dgvDvdListe.Columns["idRayon"].Visible = false;
            dgvDvdListe.Columns["idGenre"].Visible = false;
            dgvDvdListe.Columns["idPublic"].Visible = false;
            dgvDvdListe.Columns["image"].Visible = false;
            dgvDvdListe.Columns["synopsis"].Visible = false;
            dgvDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDvdListe.Columns["id"].DisplayIndex = 0;
            dgvDvdListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du Dvd dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbDvdNumRecherche.Text.Equals(""))
            {
                txbDvdTitreRecherche.Text = "";
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txbDvdNumRecherche.Text));
                if (dvd != null)
                {
                    List<Dvd> Dvd = new List<Dvd>() { dvd };
                    RemplirDvdListe(Dvd);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirDvdListeComplete();
                }
            }
            else
            {
                RemplirDvdListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des Dvd dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbDvdTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbDvdTitreRecherche.Text.Equals(""))
            {
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                txbDvdNumRecherche.Text = "";
                List<Dvd> lesDvdParTitre;
                lesDvdParTitre = lesDvd.FindAll(x => x.Titre.ToLower().Contains(txbDvdTitreRecherche.Text.ToLower()));
                RemplirDvdListe(lesDvdParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxDvdGenres.SelectedIndex < 0 && cbxDvdPublics.SelectedIndex < 0 && cbxDvdRayons.SelectedIndex < 0
                    && txbDvdNumRecherche.Text.Equals(""))
                {
                    RemplirDvdListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du dvd sélectionné
        /// </summary>
        /// <param name="dvd">le dvd</param>
        private void AfficheDvdInfos(Dvd dvd)
        {
            txbDvdRealisateur.Text = dvd.Realisateur;
            txbDvdSynopsis.Text = dvd.Synopsis;
            txbDvdImage.Text = dvd.Image;
            txbDvdDuree.Value = dvd.Duree;
            txbDvdNumero.Text = dvd.Id;
            txbDvdGenre.Text = dvd.Genre;
            txbDvdPublic.Text = dvd.Public;
            txbDvdRayon.Text = dvd.Rayon;
            txbDvdTitre.Text = dvd.Titre;
            string image = dvd.Image;
            try
            {
                pcbDvdImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbDvdImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du dvd
        /// </summary>
        private void VideDvdInfos()
        {
            txbDvdRealisateur.Text = "";
            txbDvdSynopsis.Text = "";
            txbDvdImage.Text = "";
            txbDvdDuree.Text = "";
            txbDvdNumero.Text = "";
            txbDvdGenre.Text = "";
            txbDvdPublic.Text = "";
            txbDvdRayon.Text = "";
            txbDvdTitre.Text = "";
            pcbDvdImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdGenres.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Genre genre = (Genre)cbxDvdGenres.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdPublics.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Public lePublic = (Public)cbxDvdPublics.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdRayons.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxDvdRayons.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_SelectionChanged(object sender, EventArgs e)
        {
            ToggleEditionDvd(false);
            if (dgvDvdListe.CurrentCell != null)
            {
                try
                {
                    Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
                    AfficheDvdInfos(dvd);
                }
                catch
                {
                    VideDvdZones();
                }
            }
            else
            {
                VideDvdInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des Dvd
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirDvdListeComplete()
        {
            RemplirDvdListe(lesDvd);
            VideDvdZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideDvdZones()
        {
            cbxDvdGenres.SelectedIndex = -1;
            cbxDvdRayons.SelectedIndex = -1;
            cbxDvdPublics.SelectedIndex = -1;
            txbDvdNumRecherche.Text = "";
            txbDvdTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideDvdZones();
            string titreColonne = dgvDvdListe.Columns[e.ColumnIndex].HeaderText;
            List<Dvd> sortedList = new List<Dvd>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesDvd.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesDvd.OrderBy(o => o.Titre).ToList();
                    break;
                case "Duree":
                    sortedList = lesDvd.OrderBy(o => o.Duree).ToList();
                    break;
                case "Realisateur":
                    sortedList = lesDvd.OrderBy(o => o.Realisateur).ToList();
                    break;
                case "Genre":
                    sortedList = lesDvd.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesDvd.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesDvd.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirDvdListe(sortedList);
        }
        #endregion

        #region Onglet Revues
        private readonly BindingSource bdgRevuesListe = new BindingSource();
        private List<Revue> lesRevues = new List<Revue>();

        /// <summary>
        /// Ouverture de l'onglet Revues : 
        /// appel des méthodes pour remplir le datagrid des revues et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabRevues_Enter(object sender, EventArgs e)
        {
            if (this.service == "001")
            {
                ajouterRevue.Visible = false;
                ajouterRevue.Enabled = false;
                editerRevue.Visible = false;
                editerRevue.Enabled = false;
                supprimerRevue.Visible = false;
                supprimerRevue.Enabled = false;
            }
            RechargerRevues();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="revues"></param>
        private void RemplirRevuesListe(List<Revue> revues)
        {
            bdgRevuesListe.DataSource = revues;
            dgvRevuesListe.DataSource = bdgRevuesListe;
            dgvRevuesListe.Columns["idRayon"].Visible = false;
            dgvRevuesListe.Columns["idGenre"].Visible = false;
            dgvRevuesListe.Columns["idPublic"].Visible = false;
            dgvRevuesListe.Columns["image"].Visible = false;
            dgvRevuesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvRevuesListe.Columns["id"].DisplayIndex = 0;
            dgvRevuesListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage de la revue dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbRevuesNumRecherche.Text.Equals(""))
            {
                txbRevuesTitreRecherche.Text = "";
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbRevuesNumRecherche.Text));
                if (revue != null)
                {
                    List<Revue> revues = new List<Revue>() { revue };
                    RemplirRevuesListe(revues);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirRevuesListeComplete();
                }
            }
            else
            {
                RemplirRevuesListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des revues dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbRevuesTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbRevuesTitreRecherche.Text.Equals(""))
            {
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                txbRevuesNumRecherche.Text = "";
                List<Revue> lesRevuesParTitre;
                lesRevuesParTitre = lesRevues.FindAll(x => x.Titre.ToLower().Contains(txbRevuesTitreRecherche.Text.ToLower()));
                RemplirRevuesListe(lesRevuesParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxRevuesGenres.SelectedIndex < 0 && cbxRevuesPublics.SelectedIndex < 0 && cbxRevuesRayons.SelectedIndex < 0
                    && txbRevuesNumRecherche.Text.Equals(""))
                {
                    RemplirRevuesListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionné
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheRevuesInfos(Revue revue)
        {
            txbRevuesPeriodicite.Text = revue.Periodicite;
            txbRevuesImage.Text = revue.Image;
            txbRevuesDateMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbRevuesNumero.Text = revue.Id;
            txbRevuesGenre.Text = revue.Genre;
            txbRevuesPublic.Text = revue.Public;
            txbRevuesRayon.Text = revue.Rayon;
            txbRevuesTitre.Text = revue.Titre;
            txbRevuesDMAD.Value = revue.DelaiMiseADispo;
            string image = revue.Image;
            try
            {
                pcbRevuesImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbRevuesImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de la reuve
        /// </summary>
        private void VideRevuesInfos()
        {
            txbRevuesPeriodicite.Text = "";
            txbRevuesImage.Text = "";
            txbRevuesDateMiseADispo.Text = "";
            txbRevuesNumero.Text = "";
            txbRevuesGenre.Text = "";
            txbRevuesPublic.Text = "";
            txbRevuesRayon.Text = "";
            txbRevuesTitre.Text = "";
            pcbRevuesImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesGenres.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Genre genre = (Genre)cbxRevuesGenres.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesPublics.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Public lePublic = (Public)cbxRevuesPublics.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesRayons.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxRevuesRayons.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations de la revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_SelectionChanged(object sender, EventArgs e)
        {
            ToggleEditionRevue(false);
            if (dgvRevuesListe.CurrentCell != null)
            {
                try
                {
                    Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
                    AfficheRevuesInfos(revue);
                }
                catch
                {
                    VideRevuesZones();
                }
            }
            else
            {
                VideRevuesInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des revues
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirRevuesListeComplete()
        {
            RemplirRevuesListe(lesRevues);
            VideRevuesZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideRevuesZones()
        {
            cbxRevuesGenres.SelectedIndex = -1;
            cbxRevuesRayons.SelectedIndex = -1;
            cbxRevuesPublics.SelectedIndex = -1;
            txbRevuesNumRecherche.Text = "";
            txbRevuesTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideRevuesZones();
            string titreColonne = dgvRevuesListe.Columns[e.ColumnIndex].HeaderText;
            List<Revue> sortedList = new List<Revue>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesRevues.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesRevues.OrderBy(o => o.Titre).ToList();
                    break;
                case "Periodicite":
                    sortedList = lesRevues.OrderBy(o => o.Periodicite).ToList();
                    break;
                case "DelaiMiseADispo":
                    sortedList = lesRevues.OrderBy(o => o.DelaiMiseADispo).ToList();
                    break;
                case "Genre":
                    sortedList = lesRevues.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesRevues.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesRevues.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirRevuesListe(sortedList);
        }
        #endregion

        #region Onglet Parutions
        private readonly BindingSource bdgExemplairesListe = new BindingSource();
        private List<Exemplaire> lesExemplaires = new List<Exemplaire>();
        const string ETATNEUF = "00001";

        /// <summary>
        /// Ouverture de l'onglet : récupère le revues et vide tous les champs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabReceptionRevue_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            txbReceptionRevueNumero.Text = "";
        }

        /// <summary>
        /// Remplit le dategrid des exemplaires avec la liste reçue en paramètre
        /// </summary>
        /// <param name="exemplaires">liste d'exemplaires</param>
        private void RemplirReceptionExemplairesListe(List<Exemplaire> exemplaires)
        {
            if (exemplaires != null)
            {
                bdgExemplairesListe.DataSource = exemplaires;
                dgvReceptionExemplairesListe.DataSource = bdgExemplairesListe;
                dgvReceptionExemplairesListe.Columns["idEtat"].Visible = false;
                dgvReceptionExemplairesListe.Columns["id"].Visible = false;
                dgvReceptionExemplairesListe.Columns["photo"].Visible = false;
                dgvReceptionExemplairesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvReceptionExemplairesListe.Columns["numero"].DisplayIndex = 0;
                dgvReceptionExemplairesListe.Columns["dateAchat"].DisplayIndex = 1;
            }
            else
            {
                bdgExemplairesListe.DataSource = null;
            }
        }

        /// <summary>
        /// Recherche d'un numéro de revue et affiche ses informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionRechercher_Click(object sender, EventArgs e)
        {
            if (!txbReceptionRevueNumero.Text.Equals(""))
            {
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbReceptionRevueNumero.Text));
                if (revue != null)
                {
                    AfficheReceptionRevueInfos(revue);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                }
            }
        }

        /// <summary>
        /// Si le numéro de revue est modifié, la zone de l'exemplaire est vidée et inactive
        /// les informations de la revue son aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbReceptionRevueNumero_TextChanged(object sender, EventArgs e)
        {
            txbReceptionRevuePeriodicite.Text = "";
            txbReceptionRevueImage.Text = "";
            txbReceptionRevueDelaiMiseADispo.Text = "";
            txbReceptionRevueGenre.Text = "";
            txbReceptionRevuePublic.Text = "";
            txbReceptionRevueRayon.Text = "";
            txbReceptionRevueTitre.Text = "";
            pcbReceptionRevueImage.Image = null;
            RemplirReceptionExemplairesListe(null);
            AccesReceptionExemplaireGroupBox(false);
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionnée et les exemplaires
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheReceptionRevueInfos(Revue revue)
        {
            // informations sur la revue
            txbReceptionRevuePeriodicite.Text = revue.Periodicite;
            txbReceptionRevueImage.Text = revue.Image;
            txbReceptionRevueDelaiMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbReceptionRevueNumero.Text = revue.Id;
            txbReceptionRevueGenre.Text = revue.Genre;
            txbReceptionRevuePublic.Text = revue.Public;
            txbReceptionRevueRayon.Text = revue.Rayon;
            txbReceptionRevueTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbReceptionRevueImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbReceptionRevueImage.Image = null;
            }
            // affiche la liste des exemplaires de la revue
            AfficheReceptionExemplairesRevue();
        }

        /// <summary>
        /// Récupère et affiche les exemplaires d'une revue
        /// </summary>
        private void AfficheReceptionExemplairesRevue()
        {
            string idDocuement = txbReceptionRevueNumero.Text;
            lesExemplaires = controller.GetExemplairesRevue(idDocuement);
            RemplirReceptionExemplairesListe(lesExemplaires);
            AccesReceptionExemplaireGroupBox(true);
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la réception d'un exemplaire
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces">true ou false</param>
        private void AccesReceptionExemplaireGroupBox(bool acces)
        {
            grpReceptionExemplaire.Enabled = acces;
            txbReceptionExemplaireImage.Text = "";
            txbReceptionExemplaireNumero.Text = "";
            pcbReceptionExemplaireImage.Image = null;
            dtpReceptionExemplaireDate.Value = DateTime.Now;
        }

        /// <summary>
        /// Recherche image sur disque (pour l'exemplaire à insérer)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplaireImage_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                // positionnement à la racine du disque où se trouve le dossier actuel
                InitialDirectory = Path.GetPathRoot(Environment.CurrentDirectory),
                Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbReceptionExemplaireImage.Text = filePath;
            try
            {
                pcbReceptionExemplaireImage.Image = Image.FromFile(filePath);
            }
            catch
            {
                pcbReceptionExemplaireImage.Image = null;
            }
        }

        /// <summary>
        /// Enregistrement du nouvel exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplairevaliderLivre_Click(object sender, EventArgs e)
        {
            if (!txbReceptionExemplaireNumero.Text.Equals(""))
            {
                try
                {
                    int numero = int.Parse(txbReceptionExemplaireNumero.Text);
                    DateTime dateAchat = dtpReceptionExemplaireDate.Value;
                    string photo = txbReceptionExemplaireImage.Text;
                    string idEtat = ETATNEUF;
                    string idDocument = txbReceptionRevueNumero.Text;
                    Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, "neuf", idDocument);
                    if (controller.CreerExemplaire(exemplaire))
                    {
                        AfficheReceptionExemplairesRevue();
                    }
                    else
                    {
                        MessageBox.Show("numéro de publication déjà existant", "Erreur");
                    }
                }
                catch
                {
                    MessageBox.Show("le numéro de parution doit être numérique", "Information");
                    txbReceptionExemplaireNumero.Text = "";
                    txbReceptionExemplaireNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("numéro de parution obligatoire", "Information");
            }
        }

        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvExemplairesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvReceptionExemplairesListe.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sortedList = new List<Exemplaire>();
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = lesExemplaires.OrderBy(o => o.Numero).Reverse().ToList();
                    break;
                case "DateAchat":
                    sortedList = lesExemplaires.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Photo":
                    sortedList = lesExemplaires.OrderBy(o => o.Photo).ToList();
                    break;
            }
            RemplirReceptionExemplairesListe(sortedList);
        }

        /// <summary>
        /// affichage de l'image de l'exemplaire suite à la sélection d'un exemplaire dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceptionExemplairesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReceptionExemplairesListe.CurrentCell != null)
            {
                Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
                string image = exemplaire.Photo;
                try
                {
                    pcbReceptionExemplaireRevueImage.Image = Image.FromFile(image);
                }
                catch
                {
                    pcbReceptionExemplaireRevueImage.Image = null;
                }
            }
            else
            {
                pcbReceptionExemplaireRevueImage.Image = null;
            }
        }
        #endregion

        #region Gestion Livre
        private void ajouterLivre_Click(object sender, EventArgs e)
        {
            List<BindingSource> data = new List<BindingSource>
            {
                bdgGenres, 
                bdgPublics, 
                bdgRayons
            };
            FormAjoutLivre f = new FormAjoutLivre(data);
            f.Show();
        }

        private void supprimerLivre_Click(object sender, EventArgs e)
        {
            string id = txbLivresNumero.Text;
            //check si exemplaire ou commande
            if (controller.PeutSuppr("commande", id) && controller.PeutSuppr("exemplaire", id))
            {
                Confirm f = new Confirm("Êtes-vous sûr de vouloir supprimer " + id, "livre", id, controller.Suppr);
                f.Show();
            }
            else
            {
                Confirm f = new Confirm("Impossible de supprimer : \n" + txbLivresTitre.Text + "\n Il y'a encore une commande ou \n un exmplaire rattaché");
                f.Show();
            }
        }

        private void editerLivre_Click(object sender, EventArgs e)
        {
            cbxEditRayonLivre.DataSource = bdgRayons.DataSource;
            cbxEditRayonLivre.DisplayMember = "Libelle";
            cbxEditRayonLivre.ValueMember = "Id";
            cbxEditRayonLivre.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxEditGenreLivre.FlatStyle = FlatStyle.Flat;

            cbxEditGenreLivre.DataSource = bdgGenres.DataSource;
            cbxEditGenreLivre.DisplayMember = "Libelle";
            cbxEditGenreLivre.ValueMember = "Id";
            cbxEditGenreLivre.DropDownStyle = ComboBoxStyle.DropDownList;

            cbxEditPublicLivre.DataSource = bdgPublics.DataSource;
            cbxEditPublicLivre.DisplayMember = "Libelle";
            cbxEditPublicLivre.ValueMember = "Id";
            cbxEditPublicLivre.DropDownStyle = ComboBoxStyle.DropDownList;

            ToggleEditionLivre(true);
        }

        private void validerLivre_Click(object sender, EventArgs e)
        {
            Document document = new Document(
                txbLivresNumero.Text,
                txbLivresTitre.Text,
                txbLivresImage.Text,
                cbxEditGenreLivre.SelectedValue.ToString(),
                "LeGenre",
                cbxEditPublicLivre.SelectedValue.ToString(),
                "Lepublic",
                cbxEditRayonLivre.SelectedValue.ToString(),
                "Lerayon"

            );
            Livre livre = new Livre(document.Id, document.Titre, document.Image, txbLivresIsbn.Text, txbLivresAuteur.Text, txbLivresCollection.Text, document.IdGenre, document.Genre, document.IdPublic, document.Public, document.IdRayon, document.Rayon);
            if (!EditerLivre(livre, document))
            {
                Console.WriteLine("Erreur lors de l'édition d'un livre");
            }
        }

        private void annulerLivre_Click(object sender, EventArgs e)
        {
            ToggleEditionLivre(false);
        }

        private bool EditerLivre(Livre livre, Document document)
        {
            if (controller.EditerLivre(livre))
            {
                if (controller.EditerDocument(document))
                {
                    RechargerLivres();
                    ToggleEditionLivre(false);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void refreshLivres_Click(object sender, EventArgs e)
        {
            RechargerLivres();
        }

        private void ToggleEditionLivre(bool status)
        {
            if (status)
            {
                grpLivresInfos.BackColor = Color.AliceBlue;
                cbxEditRayonLivre.Visible = true;
                cbxEditRayonLivre.Enabled = true;
                cbxEditGenreLivre.Visible = true;
                cbxEditGenreLivre.Enabled = true;
                cbxEditPublicLivre.Visible = true;
                cbxEditPublicLivre.Enabled = true;

                validerLivre.Enabled = true;
                validerLivre.Visible = true;
                annulerLivre.Enabled = true;
                annulerLivre.Visible = true;

                txbLivresTitre.BackColor = Color.White;
                txbLivresTitre.ReadOnly = false;

                txbLivresAuteur.BackColor = Color.White;
                txbLivresAuteur.ReadOnly = false;

                txbLivresCollection.BackColor = Color.White;
                txbLivresCollection.ReadOnly = false;

                txbLivresImage.BackColor = Color.White;
                txbLivresImage.ReadOnly = false;

                txbLivresIsbn.BackColor = Color.White;
                txbLivresIsbn.ReadOnly = false;

                var value = cbxEditGenreLivre.Items.Cast<object>().FirstOrDefault(item => item.ToString() == txbLivresGenre.Text);
                if (value != null)
                {
                    cbxEditGenreLivre.SelectedItem = value;
                }

                value = cbxEditRayonLivre.Items.Cast<object>().FirstOrDefault(item => item.ToString() == txbLivresRayon.Text);
                if (value != null)
                {
                    cbxEditRayonLivre.SelectedItem = value;
                }

                value = cbxEditPublicLivre.Items.Cast<object>().FirstOrDefault(item => item.ToString() == txbLivresPublic.Text);
                if (value != null)
                {
                    cbxEditPublicLivre.SelectedItem = value;
                }
            } 
            else 
            {
                if (cbxEditRayonLivre.Visible)
                {
                    //annule les changements d'interface
                    grpLivresInfos.BackColor = Color.White;
                    cbxEditRayonLivre.Visible = false;
                    cbxEditRayonLivre.Enabled = false;
                    cbxEditGenreLivre.Visible = false;
                    cbxEditGenreLivre.Enabled = false;
                    cbxEditPublicLivre.Visible = false;
                    cbxEditPublicLivre.Enabled = false;

                    validerLivre.Enabled = false;
                    validerLivre.Visible = false;
                    annulerLivre.Enabled = false;
                    annulerLivre.Visible = false;

                    txbLivresTitre.BackColor = txbLivresNumero.BackColor;
                    txbLivresTitre.ReadOnly = true;

                    txbLivresAuteur.BackColor = txbLivresNumero.BackColor;
                    txbLivresAuteur.ReadOnly = true;

                    txbLivresCollection.BackColor = txbLivresNumero.BackColor;
                    txbLivresCollection.ReadOnly = true;

                    txbLivresImage.BackColor = txbLivresNumero.BackColor;
                    txbLivresImage.ReadOnly = true;

                    txbLivresIsbn.BackColor = txbLivresNumero.BackColor;
                    txbLivresIsbn.ReadOnly = true;

                    VideLivresZones();
                    Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
                    AfficheLivresInfos(livre);
                }
            }
        }

        public bool AjouterLivre(List<string> data)
        {
            Document document = new Document(data[7], data[0], "leimage", data[1], "LeGenre", data[2], "Lepublic", data[3], "Lerayon");
            Livre livre = new Livre(document.Id, document.Titre, document.Image, data[4], data[5], data[6], document.IdGenre, document.Genre, document.IdPublic, document.Public, document.IdRayon, document.Rayon);
            if (controller.CreerDocument(document))
            {
                if (controller.CreerLivreDvd(livre))
                {
                    if (controller.CreerLivre(livre))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        #endregion

        #region Gestion Dvd
        private void ajouterDvd_Click(object sender, EventArgs e)
        {
            List<BindingSource> data = new List<BindingSource>
            {
                bdgGenres,
                bdgPublics,
                bdgRayons
            };
            FormAjoutDvd f = new FormAjoutDvd(data);
            f.Show();
        }

        private void RechargerDvd()
        {
            lesDvd = controller.GetAllDvd();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxDvdGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxDvdPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxDvdRayons);
            RemplirDvdListeComplete();
        }

        private void refreshDvd_Click(object sender, EventArgs e)
        {
            RechargerDvd();
        }

        private void editerDvd_Click(object sender, EventArgs e)
        {
            cbxEditRayonDvd.DataSource = bdgRayons.DataSource;
            cbxEditRayonDvd.DisplayMember = "Libelle";
            cbxEditRayonDvd.ValueMember = "Id";

            cbxEditGenreDvd.DataSource = bdgGenres.DataSource;
            cbxEditGenreDvd.DisplayMember = "Libelle";
            cbxEditGenreDvd.ValueMember = "Id";

            cbxEditPublicDvd.DataSource = bdgPublics.DataSource;
            cbxEditPublicDvd.DisplayMember = "Libelle";
            cbxEditPublicDvd.ValueMember = "Id";

            ToggleEditionDvd(true);
        }

        private void supprimerDvd_Click(object sender, EventArgs e)
        {
            string id = txbDvdNumero.Text;
            //check si exemplaire ou commande
            if (controller.PeutSuppr("commande", id) && controller.PeutSuppr("exemplaire", id))
            {
                Confirm f = new Confirm("Êtes-vous sûr de vouloir supprimer " + id, "dvd", id, controller.Suppr);
                f.Show();
            }
            else
            {
                Confirm f = new Confirm("Impossible de supprimer : \n" + txbDvdTitre.Text + "\n Il y'a encore une commande ou \n un exmplaire rattaché");
                f.Show();
            }
        }

        private void annulerDvd_Click(object sender, EventArgs e)
        {
            ToggleEditionDvd(false);
        }

        private void validerDvd_Click(object sender, EventArgs e)
        {
            Document document = new Document(
                txbDvdNumero.Text,
                txbDvdTitre.Text,
                txbDvdImage.Text,
                cbxEditGenreDvd.SelectedValue.ToString(),
                "LeGenre",
                cbxEditPublicDvd.SelectedValue.ToString(),
                "Lepublic",
                cbxEditRayonDvd.SelectedValue.ToString(),
                "Lerayon"

            );
            Dvd dvd = new Dvd(document.Id, document.Titre, document.Image, Convert.ToInt32(txbDvdDuree.Text), txbDvdRealisateur.Text, txbDvdSynopsis.Text, document.IdGenre, document.Genre, document.IdPublic, document.Public, document.IdRayon, document.Rayon);
            if (!EditerDvd(document, dvd))
            {
                Console.WriteLine("Erreur lors de l'édition d'un dvd");
            }
        }
        public bool EditerDvd(Document document, Dvd dvd)
        {
            if (controller.EditerDvd(dvd))
            {
                if (controller.EditerDocument(document))
                {
                    RechargerDvd();
                    ToggleEditionDvd(false);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }   
        }
        public bool AjouterDvd(List<string> data)
        {
            Document document = new Document(data[7], data[0], "leimage", data[1], "LeGenre", data[2], "Lepublic", data[3], "Lerayon");
            Dvd dvd = new Dvd(document.Id, document.Titre, document.Image, Convert.ToInt32(data[4]), data[5], data[6], document.IdGenre, document.Genre, document.IdPublic, document.Public, document.IdRayon, document.Rayon);
            if (controller.CreerDocument(document))
            {
                if (controller.CreerLivreDvd(dvd))
                {
                    if (controller.CreerDvd(dvd))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private void ToggleEditionDvd(bool status)
        {
            if (status)
            {
                grpDvdInfos.BackColor = Color.AliceBlue;
                cbxEditRayonDvd.Visible = true;
                cbxEditRayonDvd.Enabled = true;
                cbxEditGenreDvd.Visible = true;
                cbxEditGenreDvd.Enabled = true;
                cbxEditPublicDvd.Visible = true;
                cbxEditPublicDvd.Enabled = true;

                validerDvd.Enabled = true;
                validerDvd.Visible = true;
                annulerDvd.Enabled = true;
                annulerDvd.Visible = true;

                txbDvdTitre.BackColor = Color.White;
                txbDvdTitre.ReadOnly = false;

                txbDvdRealisateur.BackColor = Color.White;
                txbDvdRealisateur.ReadOnly = false;

                txbDvdSynopsis.BackColor = Color.White;
                txbDvdSynopsis.ReadOnly = false;

                txbDvdImage.BackColor = Color.White;
                txbDvdImage.ReadOnly = false;

                txbDvdDuree.BackColor = Color.White;
                txbDvdDuree.ReadOnly = false;

                var value = cbxEditGenreDvd.Items.Cast<object>().FirstOrDefault(item => item.ToString() == txbDvdGenre.Text);
                if (value != null)
                {
                    cbxEditGenreDvd.SelectedItem = value;
                }

                value = cbxEditRayonDvd.Items.Cast<object>().FirstOrDefault(item => item.ToString() == txbDvdRayon.Text);
                if (value != null)
                {
                    cbxEditRayonDvd.SelectedItem = value;
                }

                value = cbxEditPublicDvd.Items.Cast<object>().FirstOrDefault(item => item.ToString() == txbDvdPublic.Text);
                if (value != null)
                {
                    cbxEditPublicDvd.SelectedItem = value;
                }
            }
            else
            {
                grpDvdInfos.BackColor = Color.White;
                cbxEditRayonDvd.Visible = false;
                cbxEditRayonDvd.Enabled = false;
                cbxEditGenreDvd.Visible = false;
                cbxEditGenreDvd.Enabled = false;
                cbxEditPublicDvd.Visible = false;
                cbxEditPublicDvd.Enabled = false;

                validerDvd.Enabled = false;
                validerDvd.Visible = false;
                annulerDvd.Enabled = false;
                annulerDvd.Visible = false;

                txbDvdTitre.BackColor = txbDvdNumero.BackColor;
                txbDvdTitre.ReadOnly = true;

                txbDvdRealisateur.BackColor = txbDvdNumero.BackColor;
                txbDvdRealisateur.ReadOnly = true;

                txbDvdSynopsis.BackColor = txbDvdNumero.BackColor;
                txbDvdSynopsis.ReadOnly = true;

                txbDvdImage.BackColor = txbDvdNumero.BackColor;
                txbDvdImage.ReadOnly = true;

                txbDvdDuree.BackColor = txbDvdNumero.BackColor;
                txbDvdDuree.ReadOnly = true;
            }
        }
        #endregion

        #region Gestion Revues
        private void ajouterRevue_Click(object sender, EventArgs e)
        {
            List<BindingSource> data = new List<BindingSource>
            {
                bdgGenres,
                bdgPublics,
                bdgRayons
            };
            FormAjoutRevue f = new FormAjoutRevue(data);
            f.Show();
        }

        private void refreshRevue_Click(object sender, EventArgs e)
        {
            RechargerRevues();
        }

        private void validerRevue_Click(object sender, EventArgs e)
        {
            Document document = new Document(
                txbRevuesNumero.Text,
                txbRevuesTitre.Text,
                txbRevuesImage.Text,
                cbxEditGenreRevue.SelectedValue.ToString(),
                "LeGenre",
                cbxEditPublicRevue.SelectedValue.ToString(),
                "Lepublic",
                cbxEditRayonRevue.SelectedValue.ToString(),
                "Lerayon"

            );
            Revue revue = new Revue(document.Id, document.Titre, document.Image, document.IdGenre, document.Genre, document.IdPublic, document.Public, document.IdRayon, document.Rayon, txbRevuesPeriodicite.Text, Convert.ToInt32(txbRevuesDMAD.Value));

            if (!EditerRevue(revue, document))
            {
                Console.WriteLine("Erreur lors de l'édition d'une revue");
            }
        }
        
        private bool EditerRevue(Revue revue, Document document)
        {
            if (controller.EditerRevue(revue))
            {
                if (controller.EditerDocument(document))
                {
                    RechargerRevues();
                    ToggleEditionRevue(false);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void AnnulerRevue_Click(object sender, EventArgs e)
        {
            ToggleEditionRevue(false);
        }

        private void editerRevue_Click(object sender, EventArgs e)
        {
            cbxEditRayonRevue.DataSource = bdgRayons.DataSource;
            cbxEditRayonRevue.DisplayMember = "Libelle";
            cbxEditRayonRevue.ValueMember = "Id";

            cbxEditGenreRevue.DataSource = bdgGenres.DataSource;
            cbxEditGenreRevue.DisplayMember = "Libelle";
            cbxEditGenreRevue.ValueMember = "Id";

            cbxEditPublicRevue.DataSource = bdgPublics.DataSource;
            cbxEditPublicRevue.DisplayMember = "Libelle";
            cbxEditPublicRevue.ValueMember = "Id";

            ToggleEditionRevue(true);
        }

        private void supprimerRevue_Click(object sender, EventArgs e)
        {
            string id = txbRevuesNumero.Text;
            //check si exemplaire ou commande
            if (controller.PeutSuppr("commande", id) && controller.PeutSuppr("exemplaire", id))
            {
                Confirm f = new Confirm("Êtes-vous sûr de vouloir supprimer " + id, "revue", id, controller.Suppr);
                f.Show();
            }
            else
            {
                Confirm f = new Confirm("Impossible de supprimer : \n" + txbDvdTitre.Text + "\n Il y'a encore une commande ou \n un exmplaire rattaché");
                f.Show();
            }
        }

        public bool AjouterRevue(List<string> data)
        {
            Document document = new Document(data[6], data[0], "leimage", data[1], "LeGenre", data[2], "Lepublic", data[3], "Lerayon");
            Revue revue = new Revue(document.Id, document.Titre, document.Image, document.IdGenre, document.Genre, document.IdPublic, document.Public, document.IdRayon, document.Rayon, data[4], Convert.ToInt32(data[5]));
            if (controller.CreerDocument(document))
            {
                if (controller.CreerRevue(revue))
                {
                    RechargerRevues();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void RechargerRevues()
        {
            lesRevues = controller.GetAllRevues();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxRevuesGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxRevuesPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxRevuesRayons);
            RemplirRevuesListeComplete();
        }

        private void ToggleEditionRevue(bool status)
        {
            if (status)
            {
                grpRevuesInfos.BackColor = Color.AliceBlue;
                cbxEditRayonRevue.Visible = true;
                cbxEditRayonRevue.Enabled = true;
                cbxEditGenreRevue.Visible = true;
                cbxEditGenreRevue.Enabled = true;
                cbxEditPublicRevue.Visible = true;
                cbxEditPublicRevue.Enabled = true;

                txbRevuesDMAD.Visible = true;
                txbRevuesDMAD.Enabled = true;

                validerRevue.Enabled = true;
                validerRevue.Visible = true;
                annulerRevue.Enabled = true;
                annulerRevue.Visible = true;

                txbRevuesTitre.BackColor = Color.White;
                txbRevuesTitre.ReadOnly = false;

                txbRevuesPeriodicite.BackColor = Color.White;
                txbRevuesPeriodicite.ReadOnly = false;

                txbRevuesImage.BackColor = Color.White;
                txbRevuesImage.ReadOnly = false;

                var value = cbxEditGenreRevue.Items.Cast<object>().FirstOrDefault(item => item.ToString() == txbRevuesGenre.Text);
                if (value != null)
                {
                    cbxEditGenreRevue.SelectedItem = value;
                }

                value = cbxEditRayonRevue.Items.Cast<object>().FirstOrDefault(item => item.ToString() == txbRevuesRayon.Text);
                if (value != null)
                {
                    cbxEditRayonRevue.SelectedItem = value;
                }

                value = cbxEditPublicRevue.Items.Cast<object>().FirstOrDefault(item => item.ToString() == txbRevuesPublic.Text);
                if (value != null)
                {
                    cbxEditPublicRevue.SelectedItem = value;
                }

            }
            else
            {
                grpRevuesInfos.BackColor = Color.White;
                cbxEditRayonRevue.Visible = false;
                cbxEditRayonRevue.Enabled = false;
                cbxEditGenreRevue.Visible = false;
                cbxEditGenreRevue.Enabled = false;
                cbxEditPublicRevue.Visible = false;
                cbxEditPublicRevue.Enabled = false;

                txbRevuesDMAD.Visible = false;
                txbRevuesDMAD.Enabled = false;

                validerRevue.Enabled = false;
                validerRevue.Visible = false;
                annulerRevue.Enabled = false;
                annulerRevue.Visible = false;

                txbRevuesTitre.BackColor = txbRevuesNumero.BackColor;
                txbRevuesTitre.ReadOnly = true;

                txbRevuesPeriodicite.BackColor = txbRevuesNumero.BackColor;
                txbRevuesPeriodicite.ReadOnly = true;

                txbRevuesImage.BackColor = txbRevuesNumero.BackColor;
                txbRevuesImage.ReadOnly = true;
            }
        }

        #endregion

        #region Commandes livres

        private readonly BindingSource bdgSuivis = new BindingSource();
        private void rechercherCommandeLivre_Click(object sender, EventArgs e)
        {
            RechercherLivre(txbIdLivre.Text);
        }

        private void RechercherLivre(string id)
        {
            Livre livre = controller.GetLivre(id);
            List<CommandeDocument> commandes = controller.GetCommande(id);
            if (livre != null)
            {
                if (commandes.Count > 0)
                {
                    RemplirInfosLivre(livre);
                    RemplirTableauLivre(commandes);
                    error.Text = "";
                }
                else
                {
                    RemplirInfosLivre(livre);
                    ViderTableauLivre();
                    error.ForeColor = Color.Red;
                    error.Font = new Font("Arial", 12);
                    error.Text = "Aucune commande de ce livre, vous pouvez en ajouter !";
                }
            }
            else
            {
                error.ForeColor = Color.Red;
                error.Font = new Font("Arial", 12);
                error.Text = "Numéro de livre introuvable";
            }
        }

        private void RemplirInfosLivre(Livre livre)
        {
            txbInfosLivreTitre.Text = livre.Titre;
            txbInfosLivreAuteur.Text = livre.Auteur;
            txbInfosLivreCollection.Text = livre.Collection;
            txbInfosLivreGenre.Text = livre.Genre;
            txbInfosLivrePublic.Text = livre.Public;
            txbInfosLivreRayon.Text = livre.Rayon;
            txbInfosLivreImage.Text = livre.Image;

            txbSelectedLivre.Text = livre.Id;
        }

        private void RemplirTableauLivre(List<CommandeDocument> commandes)
        {
            dgvCommandeLivre.DataSource = commandes;
            dgvCommandeLivre.Columns["IdSuivi"].Visible = false;
            dgvCommandeLivre.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvCommandeLivre.Columns["id"].DisplayIndex = 0;
        }

        private void ViderTableauLivre()
        {
            dgvCommandeLivre.DataSource = null;
        }

        private void dgvCommandeLivre_SelectionChanged(object sender, EventArgs e)
        {
            bdgSuivis.DataSource = controller.GetAllSuivis();
            if (dgvCommandeLivre.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvCommandeLivre.SelectedRows[0];
                string commandeId = selectedRow.Cells["id"].Value.ToString();
                string commandeSuivi = selectedRow.Cells["suivi"].Value.ToString();
                string commandeidSuivi = selectedRow.Cells["idSuivi"].Value.ToString();
                RemplirInfosCommande(commandeId, commandeidSuivi, commandeSuivi);
            }
        }

        private void RemplirInfosCommande(string id, string idSuivi, string commandeSuivi)
        {
            txbSelectedCommande.Text = id;
            txbSelectedCommande2.Text = id;
            txbSelectedCommandeSuivi.Text = commandeSuivi;

            RemplircbxSetSuivi(idSuivi);
        }

        private void RemplircbxSetSuivi(string id)
        {
            int suiviId = int.Parse(id);
            switch (id)
            {
                case "00001":
                    bdgSuivis.Remove(bdgSuivis.List.OfType<Suivi>().FirstOrDefault(item => item.Id == "00004"));
                    btnSupprCommande.Enabled = false;
                    cbxSetSuivi.Enabled = true;
                    break;
                case "00002":
                    bdgSuivis.Remove(bdgSuivis.List.OfType<Suivi>().FirstOrDefault(item => item.Id == "00001"));
                    bdgSuivis.Remove(bdgSuivis.List.OfType<Suivi>().FirstOrDefault(item => item.Id == "00004"));
                    btnSupprCommande.Enabled = false;
                    cbxSetSuivi.Enabled = true;
                    break;
                case "00003":
                    bdgSuivis.Remove(bdgSuivis.List.OfType<Suivi>().FirstOrDefault(item => item.Id == "00001"));
                    bdgSuivis.Remove(bdgSuivis.List.OfType<Suivi>().FirstOrDefault(item => item.Id == "00002"));
                    btnSupprCommande.Enabled = true;
                    cbxSetSuivi.Enabled = true;
                    break;
                case "00004":
                    bdgSuivis.Remove(bdgSuivis.List.OfType<Suivi>().FirstOrDefault(item => item.Id == "00001"));
                    bdgSuivis.Remove(bdgSuivis.List.OfType<Suivi>().FirstOrDefault(item => item.Id == "00002"));
                    bdgSuivis.Remove(bdgSuivis.List.OfType<Suivi>().FirstOrDefault(item => item.Id == "00003"));
                    btnSupprCommande.Enabled = true;
                    cbxSetSuivi.Enabled = false;
                    break;
            }

            cbxSetSuivi.DataSource = bdgSuivis;
            cbxSetSuivi.DisplayMember = "Libelle";
            cbxSetSuivi.ValueMember = "Id";
            cbxSetSuivi.SelectedItem = suiviId;
        }

        private void RechargerCommandeLivre(Livre livre)
        {
            RemplirInfosLivre(livre);
            RemplirTableauLivre(controller.GetCommande(livre.Id));
        }

        private void btnAjoutCommande_Click(object sender, EventArgs e)
        {
            if (txbSelectedLivre.Text != "" && txbSelectedLivre.Text != null)
            {
                Commande commande = new Commande(controller.GenererId("commande"), DateTime.Now, (double)txbCommandeMontantLivre.Value);
                CommandeDocument commandeDocument = new CommandeDocument(commande.Id, commande.DateCommande, commande.Montant, (int)txbCommandeNbLivre.Value, txbSelectedLivre.Text, "00001", "En cours");
               
                if (!AjouterCommande(commande, commandeDocument))
                {
                    Console.WriteLine("Erreur lors de l'édition de l'ajout d'une commande");
                }
            }
            else
            {
                error.ForeColor = Color.Red;
                error.Font = new Font("Arial", 12);
                error.Text = "Vous devez sélectionner un livre";
            }
        }

        private bool AjouterCommande(Commande commande, CommandeDocument commandeDocument)
        {
            if (controller.CreerCommande(commande))
            {
                if (controller.CreerCommandeDocument(commandeDocument))
                {
                    error.ForeColor = Color.Blue;
                    error.Font = new Font("Arial", 12);
                    error.Text = "Commande enregistrée";
                    RechargerCommandeLivre(controller.GetLivre(commandeDocument.IdLivreDvd));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void btnEditSuivi_Click(object sender, EventArgs e)
        {
            if (txbSelectedCommande.Text != "" && txbSelectedCommande.Text != null)
            {
                Commande commande = new Commande(txbSelectedCommande.Text, DateTime.Now, (double)txbCommandeMontantLivre.Value);
                CommandeDocument commandeDocument = new CommandeDocument(commande.Id, commande.DateCommande, commande.Montant, (int)txbCommandeNbLivre.Value, txbSelectedLivre.Text, cbxSetSuivi.SelectedValue.ToString(), cbxSetSuivi.Text);

                if (controller.EditerCommande(commande))
                {
                    if (controller.EditerCommandeDocument(commandeDocument))
                    {
                        error.ForeColor = Color.Blue;
                        error.Font = new Font("Arial", 12);
                        error.Text = "Suivi édité";
                        RechargerCommandeLivre(controller.GetLivre(commandeDocument.IdLivreDvd));
                    }
                }
                else
                {
                    error.ForeColor = Color.Red;
                    error.Font = new Font("Arial", 12);
                    error.Text = "Vous devez sélectionner une commande";
                }
            }
        }

        private void btnSupprCommande_Click(object sender, EventArgs e)
        {
            string id = txbSelectedCommande2.Text;
            Confirm f = new Confirm("Êtes-vous sûr de vouloir supprimer " + id, "commande", id, controller.Suppr);
            f.FormClosed += (s, args) =>
            {
                RechargerCommandeLivre(controller.GetLivre(txbSelectedLivre.Text));
            };
            f.Show();
        }


        #endregion
    }
}
