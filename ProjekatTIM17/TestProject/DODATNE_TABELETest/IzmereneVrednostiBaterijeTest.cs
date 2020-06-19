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
    public class IzmereneVrednostiBaterijeTest
    {
        [Test]
        [TestCase("naziv", 3.2, 1)]
        [TestCase("naziv2", 10.2, 0)]
        [TestCase("naziv3", 3.2, 2)]
        public void IzmereneVrednostiBaterijeDobriParametri(string naziv, double? kapacitet, int? rezim)
        {
            DateTime d = DateTime.Now;
            IzmereneVrednostiBaterije b = new IzmereneVrednostiBaterije(naziv, kapacitet, rezim, d);
            Assert.AreEqual(b.NazivBaterije, naziv);
            Assert.AreEqual(b.Kapacitet, kapacitet);
            Assert.AreEqual(b.Rezim, rezim);
            Assert.AreEqual(b.Datum, d);
        }

        [Test]
        [TestCase("n", 0.001, 0)]
        [TestCase(".", 0.01, 0)]
        [TestCase("l", 0.0001, 2)]
        public void IzmereneVrednostiBaterijeGranicniParametri(string naziv, double? kapacitet, int? rezim)
        {
            DateTime d = DateTime.Now;
            IzmereneVrednostiBaterije b = new IzmereneVrednostiBaterije(naziv, kapacitet, rezim, d);
            Assert.AreEqual(b.NazivBaterije, naziv);
            Assert.AreEqual(b.Kapacitet, kapacitet);
            Assert.AreEqual(b.Rezim, rezim);
            Assert.AreEqual(b.Datum, d);
        }

        [Test]
        [TestCase(null,null,null,null)]
        [TestCase("naziv",2.3,null,null)]
        [TestCase(null,null,2,null)]
        [TestCase("naziv",2.3,1,null)]
        [TestCase(null,2.3,null,null)]
        public void IzmereneVrednostiBaterijeLosiParametri(string naziv, double? kapacitet, int? rezim, DateTime? datum)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                IzmereneVrednostiBaterije b = new IzmereneVrednostiBaterije(naziv, kapacitet, rezim, datum);
            });
        }

        [Test]
        [TestCase(null, null, null)]
        [TestCase("naziv", 2.3, null)]
        [TestCase(null, null, 2)]
        [TestCase("naziv", null, 1)]
        [TestCase(null, 2.3, null)]
        [TestCase("naziv", null, null)]
        public void IzmereneVrednostiBaterijeLosiParametri2(string naziv, double? kapacitet, int? rezim)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                IzmereneVrednostiBaterije b = new IzmereneVrednostiBaterije(naziv, kapacitet, rezim, DateTime.Now);
            });
        }

        [Test]
        [TestCase("", 2.3, 2)]
        [TestCase("", -2.3, 1)]
        [TestCase("naziv", 2.3, 8)]
        [TestCase("", 2.3, 8)]
        [TestCase("naziv", -2.3, 8)]
        public void IzmereneVrednostiBaterijeLosiParametri3(string naziv, double? kapacitet, int? rezim)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                IzmereneVrednostiBaterije b = new IzmereneVrednostiBaterije(naziv, kapacitet, rezim, DateTime.Now);
            });
        }
    }
}
