
using MediaTekDocuments.model;
using System;
using System.Collections.ObjectModel;

namespace MediaTekDocuments.model
{
    public class CommandeDocument : Commande
    {
        public int nbExemplaire { get; }
        public string IdLivreDvd { get; }
        public string IdSuivi { get; set; }

        public string Suivi { get; }

        public CommandeDocument(string id, DateTime dateCommande, double Montant, int NbExemplaire, string idLivreDvd, string idSuivi, string suivi) : base(id, dateCommande, Montant)
        {
            nbExemplaire = NbExemplaire;
            IdLivreDvd = idLivreDvd;
            IdSuivi = idSuivi;
            Suivi = suivi;
        }
    }
}