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
    public class DvdTests
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

        private const int Duree = 500;
        private const string Realisateur = "LE REAL";
        private const string Synopsis = "LE SYNOP";

        private static readonly Dvd dvd = new Dvd(Id, Titre, Image, Duree, Realisateur, Synopsis, IdGenre, Genre, IdPublic, Public, IdRayon, Rayon);

        [TestMethod()]
        public void DvdTest()
        {
            Assert.AreEqual(Id, dvd.Id, "devrait réussir : Id valorisé");
            Assert.AreEqual(Titre, dvd.Titre, "devrait réussir : Titre valorisé");
            Assert.AreEqual(Image, dvd.Image, "devrait réussir : Image valorisé");
            Assert.AreEqual(IdGenre, dvd.IdGenre, "devrait réussir : IdGenre valorisé");
            Assert.AreEqual(Genre, dvd.Genre, "devrait réussir : Genre valorisé");
            Assert.AreEqual(IdPublic, dvd.IdPublic, "devrait réussir : IdPublic valorisé");
            Assert.AreEqual(Public, dvd.Public, "devrait réussir : Public valorisé");
            Assert.AreEqual(IdRayon, dvd.IdRayon, "devrait réussir : IdRayon valorisé");
            Assert.AreEqual(Rayon, dvd.Rayon, "devrait réussir : Rayon valorisé");

            Assert.AreEqual(Duree, dvd.Duree, "devrait réussir : Duree valorisé");
            Assert.AreEqual(Realisateur, dvd.Realisateur, "devrait réussir : Realisateur valorisé");
            Assert.AreEqual(Synopsis, dvd.Synopsis, "devrait réussir : Synopsis valorisé");
        }
    }
}