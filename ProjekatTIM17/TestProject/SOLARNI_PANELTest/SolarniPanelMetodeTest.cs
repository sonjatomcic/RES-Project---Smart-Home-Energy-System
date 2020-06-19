using Moq;
using NUnit.Framework;
using SmartHomeEnergySystem.COMMON;
using SmartHomeEnergySystem.SHES;
using SmartHomeEnergySystem.SOLARNI_PANEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.SOLARNI_PANELTest
{
    [TestFixture]
    public class SolarniPanelMetodeTest
    {
        [Test]
        [TestCase("30")]
        [TestCase("60")]
        [TestCase("80")]
        [TestCase("20")]
        public void ValidacijaSnageSuncaDobriParametri(string str)
        {
            ISolarniPanel metode = new SolarniPanelMetode();
            bool result = metode.ValidacijaSnageSunca(str);
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase("0")]
        [TestCase("100")]
        [TestCase("001")]
        [TestCase("010")]
        public void ValidacijaSnageSuncaGranicniParametri(string str)
        {
            ISolarniPanel metode = new SolarniPanelMetode();
            bool result = metode.ValidacijaSnageSunca(str);
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase("-10")]
        [TestCase("200")]
        [TestCase("120")]
        [TestCase("./a")]
        [TestCase("aaaa")]
        public void ValidacijaSnageSuncaLosiParametri(string str)
        {
            ISolarniPanel metode = new SolarniPanelMetode();
            bool result = metode.ValidacijaSnageSunca(str);
            Assert.IsFalse(result);
        }

        
    }
}
