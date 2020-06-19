
using NUnit.Framework;
using SmartHomeEnergySystem.ELEKTRODISTRIBUCIJA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.ELEKTRODISTRIBUCIJATest
{
    [TestFixture]
    public class ElektrodistribucijaTest
    {
        [Test]
        [TestCase(20.5)]
        [TestCase(15.9)]
        [TestCase(5.9)]
        public void ElektrodistribucijaDobriParametri(double? cena)
        {
            Elektrodistribucija e = new Elektrodistribucija(cena);
            Assert.AreEqual(e.Cena, cena);
        }

        [Test]
        [TestCase(0.1)]
        [TestCase(1.0)]
        [TestCase(0.001)]
        [TestCase(0.00001)]
        public void ElektrodistribucijaGRanicniParametri(double? cena)
        {
            Elektrodistribucija e = new Elektrodistribucija(cena);
            Assert.AreEqual(e.Cena, cena);
        }

        [Test]
        [TestCase(null)]
        public void ElektrodistribucijaLosiParametri(double? cena)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Elektrodistribucija e = new Elektrodistribucija(cena);
            });
        }

        [Test]
        [TestCase(-0.001)]
        [TestCase(-0.01)]
        [TestCase(-5.2)]
        [TestCase(-6.5)]
        public void ElektrodistribucijaLosiParametri1(double? cena)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Elektrodistribucija e = new Elektrodistribucija(cena);
            });
        }


    }
}
