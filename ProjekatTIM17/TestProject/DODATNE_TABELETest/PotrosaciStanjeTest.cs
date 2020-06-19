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
    public class PotrosaciStanjeTest
    {
        [Test]
        [TestCase(2.3)]
        [TestCase(1.3)]
        [TestCase(1.0)]
        public void PotrosaciStanjeDobriParametri(double? snaga)
        {
            DateTime d = DateTime.Now;
            PotrosaciStanje s = new PotrosaciStanje(snaga, d);
            Assert.AreEqual(s.Snaga, snaga);
            Assert.AreEqual(s.Datum, d);
        }

        [Test]
        [TestCase(0.1)]
        [TestCase(0.0001)]
        [TestCase(0.001)]
        public void PotrosaciStanjeGranicniParametri(double? snaga)
        {
            DateTime d = DateTime.Now;
            PotrosaciStanje s = new PotrosaciStanje(snaga, d);
            Assert.AreEqual(s.Snaga, snaga);
            Assert.AreEqual(s.Datum, d);
        }

        [Test]
        [TestCase(null,null)]
        [TestCase(2.3,null)]
        public void PotrosaciStanjeLosiParametri(double? snaga, DateTime? datum)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                PotrosaciStanje s = new PotrosaciStanje(snaga, datum);
            });
        }

        [Test]
        [TestCase(null)]
        public void PotrosaciStanjeLosiParametr2(double? snaga)
        {
            DateTime d = DateTime.Now;
            Assert.Throws<ArgumentNullException>(() =>
            {
                PotrosaciStanje s = new PotrosaciStanje(snaga, d);
            });
        }

        [Test]
        [TestCase(-2.3)]
        public void PotrosaciStanjeLosiParametr3(double? snaga)
        {
            DateTime d = DateTime.Now;
            Assert.Throws<ArgumentException>(() =>
            {
                PotrosaciStanje s = new PotrosaciStanje(snaga, d);
            });
        }

    }
}
