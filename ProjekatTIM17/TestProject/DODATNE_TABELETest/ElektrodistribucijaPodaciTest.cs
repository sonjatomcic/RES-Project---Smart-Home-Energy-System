using NUnit.Framework;
using SmartHomeEnergySystem.DODATNE_TABELE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.DODATNE_TABELETest
{
    [TestFixture]
    public class ElektrodistribucijaPodaciTest
    {
        

        [Test]
        [TestCase(-1, 12.2)]
        [TestCase(1, 15.3)]
        [TestCase(-1, 85.3)]
        public void ElektrodistribucijaPodaciDobriParametri(int? snagaR, double? cena)
        {
            ElektrodistribucijaPodaci e =new ElektrodistribucijaPodaci(DateTime.Now, snagaR, cena);
            Assert.AreEqual(e.Datum, DateTime.Now);
            Assert.AreEqual(e.SnagaRazmene, snagaR);
            Assert.AreEqual(e.Cena, cena);
        }

        [Test]
        [TestCase(-1, 0.001)]
        [TestCase(1, 0.1)]
        [TestCase(-1, 0.000001)]
        public void ElektrodistribucijaGranicniParametri(int? snagaR, double? cena)
        {
            DateTime a = DateTime.Now;
            ElektrodistribucijaPodaci e = new ElektrodistribucijaPodaci(a, snagaR, cena);
            Assert.AreEqual(e.Datum, a);
            Assert.AreEqual(e.SnagaRazmene, snagaR);
            Assert.AreEqual(e.Cena, cena);
        }

        [Test]
        [TestCase(-1, 12.2)]
        [TestCase(1, 15.3)]
        [TestCase(-1, 85.3)]
        public void ElektrodistribucijaPodaciLosiParametri(int? snagaR, double? cena)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ElektrodistribucijaPodaci e = new ElektrodistribucijaPodaci(null, snagaR, cena);
            });
        }

        [Test]
        [TestCase(-1, null)]
        [TestCase(null, 15.3)]
        [TestCase(1, null)]
        public void ElektrodistribucijaPodaciLosiParametri2(int? snagaR, double? cena)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ElektrodistribucijaPodaci e = new ElektrodistribucijaPodaci(DateTime.Now, snagaR, cena);
            });
        }

        [Test]
        [TestCase(null, null,null)]    
        public void ElektrodistribucijaPodaciLosiParametri3(DateTime? datum,int? snagaR, double? cena)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ElektrodistribucijaPodaci e = new ElektrodistribucijaPodaci(datum, snagaR, cena);
            });
        }

        [Test]
        [TestCase(-8, 12.3)]
        [TestCase(-1, -10.3)]
        [TestCase(1, -10.3)]
        [TestCase(8, 10.3)]
        public void ElektrodistribucijaPodaciLosiParametri3(int? snagaR, double? cena)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                ElektrodistribucijaPodaci e = new ElektrodistribucijaPodaci(DateTime.Now, snagaR, cena);
            });
        }


    }
}
