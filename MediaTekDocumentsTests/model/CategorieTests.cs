using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaTekDocuments.model.Tests
{
    [TestClass()]
    public class CategorieTests
    {
        private const string id = "00042";
        private const string libelle = "LE LIBELLE";
        private static readonly Categorie categorie = new Categorie(id, libelle);

        [TestMethod()]
        public void CategorieTest()
        {
            Assert.AreEqual(id, categorie.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(libelle, categorie.Libelle, "devrait réussir : libelle valorisé");
        }
    }
}