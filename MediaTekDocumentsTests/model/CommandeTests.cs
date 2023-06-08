using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaTekDocuments.model.Tests
{
    [TestClass()]
    public class CommandeTests
    {
        private const string id = "00005";
        private readonly static DateTime DateCommande = DateTime.Now;
        private const double Montant = 5.55;
        private static readonly Commande commande = new Commande(id, DateCommande, Montant);

        [TestMethod()]
        public void CommandeTest()
        {
            Assert.AreEqual(id, commande.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(DateCommande, commande.DateCommande, "devrait réussir : DateCommande valorisé");
            Assert.AreEqual(Montant, commande.Montant, "devrait réussir : Montant valorisé");
        }
    }
}