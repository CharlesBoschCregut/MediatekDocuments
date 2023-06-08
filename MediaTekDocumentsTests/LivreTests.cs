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
    public class LivreTests
    {
        private const string Id = "00005";
        private const string Titre = "LE TITRE";
        private const string Image = "LE IMAGE";
        private const string IdGenre = "00001";
        private const string Genre = "LE GENRE";
        private const string IdPublic = "00002";
        private const string Public = "LE PUBLIC";
        private const string IdRayon = "00003";
        private const string Rayon = "LE RAYON";

        private const string Isbn = "50514515";
        private const string Auteur = "LE REAL";
        private const string Collection = "LE SYNOP";

        private static readonly Livre livre = new Livre(Id, Titre, Image, Isbn, Auteur, Collection, IdGenre, Genre, IdPublic, Public, IdRayon, Rayon);
        
        [TestMethod()]
        public void LivreTest()
        {
            Assert.AreEqual(Id, livre.Id, "devrait réussir : Id valorisé");
            Assert.AreEqual(Titre, livre.Titre, "devrait réussir : Titre valorisé");
            Assert.AreEqual(Image, livre.Image, "devrait réussir : Image valorisé");
            Assert.AreEqual(IdGenre, livre.IdGenre, "devrait réussir : IdGenre valorisé");
            Assert.AreEqual(Genre, livre.Genre, "devrait réussir : Genre valorisé");
            Assert.AreEqual(IdPublic, livre.IdPublic, "devrait réussir : IdPublic valorisé");
            Assert.AreEqual(Public, livre.Public, "devrait réussir : Public valorisé");
            Assert.AreEqual(IdRayon, livre.IdRayon, "devrait réussir : IdRayon valorisé");
            Assert.AreEqual(Rayon, livre.Rayon, "devrait réussir : Rayon valorisé");

            Assert.AreEqual(Isbn, livre.Isbn, "devrait réussir : Isbn valorisé");
            Assert.AreEqual(Auteur, livre.Auteur, "devrait réussir : Auteur valorisé");
            Assert.AreEqual(Collection, livre.Collection, "devrait réussir : Collection valorisé");
        }
    }
}