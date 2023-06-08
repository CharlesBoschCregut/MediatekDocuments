
using MediaTekDocuments.model;
using System;
using System.Collections.ObjectModel;

namespace MediaTekDocuments.model
{
    public class CommandeDocument : Commande
    {
        public int NbExemplaire { get; }
        public string IdLivreDvd { get; }
        public string IdSuivi { get; set; }

        public string Suivi { get; }

        public CommandeDocument(string id, DateTime dateCommande, double Montant, int NbExemplaire, string idLivreDvd, string idSuivi, string suivi) : base(id, dateCommande, Montant)
        {
            this.NbExemplaire = NbExemplaire;
            this.IdLivreDvd = idLivreDvd;
            this.IdSuivi = idSuivi;
            this.Suivi = suivi;
        }
    }
}