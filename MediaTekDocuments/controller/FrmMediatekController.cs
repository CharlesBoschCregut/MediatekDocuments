using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;
using System;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmMediatek
    /// </summary>
    class FrmMediatekController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmMediatekController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// getter sur la liste des genres
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return access.GetAllGenres();
        }

        /// <summary>
        /// getter sur la liste des livres
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return access.GetAllLivres();
        }

        /// <summary>
        /// getter sur la liste des Dvd
        /// </summary>
        /// <returns>Liste d'objets dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            return access.GetAllDvd();
        }

        /// <summary>
        /// getter sur la liste des revues
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return access.GetAllRevues();
        }

        /// <summary>
        /// getter sur les rayons
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return access.GetAllRayons();
        }

        /// <summary>
        /// getter sur les publics
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return access.GetAllPublics();
        }

        public List<Etat> GetAllEtats()
        { 
            return access.GetAllEtats();
        }


        /// <summary>
        /// récupère les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocuement">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocuement)
        {
            return access.GetExemplairesRevue(idDocuement);
        }

        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            return access.CreerExemplaire(exemplaire);
        }

        public bool CreerDocument(Document document)
        {
            return access.CreerDocument(document);
        }

        public bool CreerLivreDvd(LivreDvd ldvd)
        {
            return access.CreerLivreDvd(ldvd);
        }

        public bool CreerLivre(Livre livre)
        {
            return access.CreerLivre(livre);
        }

        public bool PeutSuppr(string table, string id)
        {
            return access.PeutSuppr(table, id);
        }

        public void Suppr(string table, string id)
        {
            access.Suppr(table, id);
        }

        public int GetPlusGrandId(string table)
        {
            return access.GetPlusGrandId(table);
        }

        public string GenererId(string table)
        {
            return access.GenererId(table);
        }

        public bool EditerDocument(Document document)
        {
            return access.EditerDocument(document);
        }

        public bool EditerLivre(Livre livre)
        {
            return access.EditerLivre(livre);
        }

        public bool CreerDvd(Dvd dvd)
        {
            return access.CreerDvd(dvd);
        }

        public bool EditerDvd(Dvd dvd)
        {
            return access.EditerDvd(dvd);
        }

        public bool CreerRevue(Revue revue)
        {
            return access.CreerRevue(revue);
        }

        public bool EditerRevue(Revue revue)
        {
            return access.EditerRevue(revue);
        }

        public Livre GetLivre(string id)
        {
            return access.GetLivre(id);
        }

        public List<CommandeDocument> GetCommande(string id)
        {
            return access.GetCommande(id);
        }

        public List<Suivi> GetAllSuivis()
        {
            return access.GetAllSuivis();
        }

        public bool CreerCommande(Commande commande)
        {
            return access.CreerCommande(commande);
        }

        public bool CreerCommandeDocument(CommandeDocument commandeDocument)
        {
            return access.CreerCommandeDocument(commandeDocument);
        }

        public bool EditerCommandeDocument(CommandeDocument commandeDocument)
        {
            return access.EditerCommandeDocument(commandeDocument);
        }

        public bool EditerCommande(Commande commande)
        {
            return access.EditerCommande(commande);
        }

        public bool EditerEtatExemplaire(Exemplaire exemplaire)
        {
            return access.EditerEtatExemplaire(exemplaire);
        }

        public string GetUserLogin(string username, string pwd)
        {
            return access.GetUserLogin(username, pwd);
        }
    }
}
