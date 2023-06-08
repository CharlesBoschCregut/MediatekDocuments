using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MediaTekDocuments.model.Tests
{
    [TestClass()]
    public class ExemplaireTests
    {
        private const int Numero = 5;
        private const string Photo = "LA PHOTO";
        private readonly static DateTime DateAchat = DateTime.Now;
        private const string IdEtat = "00001";
        private const string Etat = "neuf";
        private const string id = "00025";

        private static readonly Exemplaire exemplaire = new Exemplaire(Numero, DateAchat, Photo, IdEtat, Etat, id);

        [TestMethod()]
        public void ExemplaireTest()
        {
            Assert.AreEqual(Numero, exemplaire.Numero, "devrait réussir : Numero valorisé");
            Assert.AreEqual(Photo, exemplaire.Photo, "devrait réussir : Photo valorisé");
            Assert.AreEqual(DateAchat, exemplaire.DateAchat, "devrait réussir : DateAchat valorisé");
            Assert.AreEqual(IdEtat, exemplaire.IdEtat, "devrait réussir : IdEtat valorisé");
            Assert.AreEqual(Etat, exemplaire.Etat, "devrait réussir : Etat valorisé");
            Assert.AreEqual(id, exemplaire.Id, "devrait réussir : id valorisé");
        }
    }
}