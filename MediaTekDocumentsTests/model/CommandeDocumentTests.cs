using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaTekDocuments.model.Tests
{
    [TestClass()]
    public class CommandeDocumentTests
    {
        private const string id = "00005";
        private readonly static DateTime DateCommande = DateTime.Now;
        private const double Montant = 5.55;
        private const int nbExemplaire = 80;
        private const string IdLivreDvd = "00015";
        private const string IdSuivi = "00001";
        private const string Suivi = "neuf";
        private static readonly CommandeDocument commandeDocument = new CommandeDocument(id, DateCommande, Montant, nbExemplaire, IdLivreDvd, IdSuivi, Suivi);

        [TestMethod()]
        public void CommandeDocumentTest()
        {
            Assert.AreEqual(id, commandeDocument.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(DateCommande, commandeDocument.DateCommande, "devrait réussir : DateCommande valorisé");
            Assert.AreEqual(Montant, commandeDocument.Montant, "devrait réussir : Montant valorisé");
            Assert.AreEqual(nbExemplaire, commandeDocument.NbExemplaire, "devrait réussir : nbExemplaire valorisé");
            Assert.AreEqual(IdLivreDvd, commandeDocument.IdLivreDvd, "devrait réussir : IdLivreDvd valorisé");
            Assert.AreEqual(IdSuivi, commandeDocument.IdSuivi, "devrait réussir : IdSuivi valorisé");
            Assert.AreEqual(Suivi, commandeDocument.Suivi, "devrait réussir : Suivi valorisé");
        }
    }
}