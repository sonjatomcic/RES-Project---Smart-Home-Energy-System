using NUnit.Framework;
using SmartHomeEnergySystem.SOLARNI_PANEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.SOLARNI_PANELTest
{
    [TestFixture]
    public class SolarniPanelTest
    {
        [Test]
        [TestCase("panel1", 300)]
        [TestCase("panel2", 200)]
        [TestCase("panel3", 400)]
        public void SolarniPanelDobriParametri(String ime, int? maxSnaga)
        {
            SolarniPanel panel = new SolarniPanel(ime, maxSnaga);
            Assert.AreEqual(panel.Ime, ime);
            Assert.AreEqual(panel.MaxSnaga, maxSnaga);
        }

        [Test]
        [TestCase("panel1", 10)]
        [TestCase(".", 10)]
        [TestCase("p", 11)]
        [TestCase(".", 12)]
        public void SolarniPanelGranicniParametri(String ime, int? maxSnaga)
        {
            SolarniPanel panel = new SolarniPanel(ime, maxSnaga);
            Assert.AreEqual(panel.Ime, ime);
            Assert.AreEqual(panel.MaxSnaga, maxSnaga);
        }

        [Test]
        [TestCase("panel1",null)]
        [TestCase("panel2",null)]
        [TestCase(null,200)]
        [TestCase(null,30)]
        public void SolarniPanelLosiParametri(String ime, int? maxSnaga)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                SolarniPanel solarniPanel = new SolarniPanel(ime, maxSnaga);
            });
        }

        [Test]
        [TestCase("",300)]
        [TestCase("",250)]
        [TestCase("panel",9)]
        [TestCase("panel",0)]
        [TestCase("panel",-1)]
        public void SolarniPanelLosiParametri1(String ime, int? maxSnaga)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                SolarniPanel solarniPanel = new SolarniPanel(ime, maxSnaga);
            });
        }

    }
}
