using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model.Tests
{
    [TestClass()]
    public class DocumentTests
    {
        public const string Id = "00005";
        public const string Titre = "LE TITRE";
        public const string Image = "LE IMAGE";
        public const string IdGenre = "00001";
        public const string Genre = "LE GENRE";
        public const string IdPublic = "00002";
        public const string Public = "LE PUBLIC";
        public const string IdRayon = "00003";
        public const string Rayon = "LE RAYON";

        private static readonly Document document = new Document(Id, Titre, Image, IdGenre, Genre, IdPublic, Public, IdRayon, Rayon);

        [TestMethod()]
        public void DocumentTest()
        {
            Assert.AreEqual(Id, document.Id, "devrait réussir : Id valorisé");
            Assert.AreEqual(Titre, document.Titre, "devrait réussir : Titre valorisé");
            Assert.AreEqual(Image, document.Image, "devrait réussir : Image valorisé");
            Assert.AreEqual(IdGenre, document.IdGenre, "devrait réussir : IdGenre valorisé");
            Assert.AreEqual(Genre, document.Genre, "devrait réussir : Genre valorisé");
            Assert.AreEqual(IdPublic, document.IdPublic, "devrait réussir : IdPublic valorisé");
            Assert.AreEqual(Public, document.Public, "devrait réussir : Public valorisé");
            Assert.AreEqual(IdRayon, document.IdRayon, "devrait réussir : IdRayon valorisé");
            Assert.AreEqual(Rayon, document.Rayon, "devrait réussir : Rayon valorisé");
        }
    }
}