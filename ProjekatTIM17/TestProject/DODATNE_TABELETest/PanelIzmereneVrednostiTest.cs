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
    public class PanelIzmereneVrednostiTest
    {
        [Test]
        [TestCase("naziv",12.3)]
        [TestCase("naziv2",350.3)]
        [TestCase("naziv3",123.3)]
        public void PanelIzmereneVrednostiDobriParametri(string naziv, double? snaga)
        {
            DateTime datum = DateTime.Now;
            PanelIzmereneVrednosti p = new PanelIzmereneVrednosti(naziv, datum, snaga);
            Assert.AreEqual(p.NazivPanela, naziv);
            Assert.AreEqual(p.Datum, datum);
            Assert.AreEqual(p.IzmerenaSnaga, snaga);
        }

        [Test]
        [TestCase(".", 0.001)]
        [TestCase("n", 350.3)]
        [TestCase("naziv3", 0.00001)]
        public void PanelIzmereneVrednostiGranicniiParametri(string naziv, double? snaga)
        {
            DateTime datum = DateTime.Now;
            PanelIzmereneVrednosti p = new PanelIzmereneVrednosti(naziv, datum, snaga);
            Assert.AreEqual(p.NazivPanela, naziv);
            Assert.AreEqual(p.Datum, datum);
            Assert.AreEqual(p.IzmerenaSnaga, snaga);
        }

        [Test]
        [TestCase(null,null,null)]
        [TestCase("naziv",null,null)]
        [TestCase("naziv",null,12.2)]
        [TestCase(null,null,12.2)]
        public void PanelIzmereneVrednostiLosiParametri(string naziv, DateTime? datum, double? snaga)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                PanelIzmereneVrednosti p = new PanelIzmereneVrednosti(naziv, datum, snaga);
            });
        }

        [Test]
        [TestCase(null, null)]
        [TestCase("naziv", null)]
        [TestCase(null, 12.2)]
        public void PanelIzmereneVrednostiLosiParametri2(string naziv, double? snaga)
        {
            DateTime datum = DateTime.Now;
            Assert.Throws<ArgumentNullException>(() =>
            {
                PanelIzmereneVrednosti p = new PanelIzmereneVrednosti(naziv, datum, snaga);
            });
        }

        [Test]
       [TestCase("", 12.5)]
       [TestCase("", -5.6)]
       [TestCase("ssss", -10.2)]
        public void PanelIzmereneVrednostiLosiParametri3(string naziv, double? snaga)
        {
            DateTime datum = DateTime.Now;
            Assert.Throws<ArgumentException>(() =>
            {
                PanelIzmereneVrednosti p = new PanelIzmereneVrednosti(naziv, datum, snaga);
            });
        }
    }
}
