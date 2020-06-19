using NUnit.Framework;
using SmartHomeEnergySystem.POTROSAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.POTROSACTest
{
    [TestFixture]
    public class PotrosacTest
    {
        [Test]
        [TestCase("potrosac1", 123.0, true)]
        [TestCase("potrosac2", 2.0, true)]
        [TestCase("potrosac3", 15.0, false)]
        public void PotrosacDobriParametri(string ime, double? potrosnja, bool? upaljeno)
        {
            Potrosac potrosac= new Potrosac(ime, potrosnja, upaljeno);
            Assert.AreEqual(potrosac.Ime, ime);
            Assert.AreEqual(potrosac.Potrosnja, potrosnja);
            Assert.AreEqual(potrosac.Upaljeno, upaljeno);
        }

        [Test]
        [TestCase("potrosac1", -3.0, true)]
        [TestCase("", 1.0, true)]
        public void PotrosacLosiParametri1(string ime,double? potrosnja,bool? upaljeno)
        {
            Assert.Throws<ArgumentException>(() =>
            {
               Potrosac potrosac= new Potrosac(ime, potrosnja, upaljeno);
            });
        }

        [Test]
        [TestCase(null,4.0, true)]
        [TestCase("po",null, true)]
        [TestCase("potrosac98", 1.0, null)]
        public void PotrosacLosiParametri2(string ime, double? potrosnja, bool? upaljeno)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Potrosac potrosac = new Potrosac(ime, potrosnja, upaljeno);
            });
        }
    }
}
