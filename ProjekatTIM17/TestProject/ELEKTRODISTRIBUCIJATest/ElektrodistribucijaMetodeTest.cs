using NUnit.Framework;
using SmartHomeEnergySystem.COMMON;
using SmartHomeEnergySystem.ELEKTRODISTRIBUCIJA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.ELEKTRODISTRIBUCIJATest
{
    [TestFixture]
    public class ElektrodistribucijaMetodeTest
    {
        Elektrodistribucija e;

        [SetUp]
        public void SetUp()
        {
            e = new Elektrodistribucija(25.3);
        }

        [Test]
        [TestCase(20.3)]
        [TestCase(15.3)]
        [TestCase(2.3)]
        public void PreuzmiRazlikuDobriParametri(double? razlika)
        {
            IElektrodistribucija iele = new ElektrodistribucijaMetode();
            Assert.DoesNotThrow(() =>
            {
                iele.PreuzmiRazliku(razlika, e);
            });
        }

        [Test]
        [TestCase(20.3, null)]
        [TestCase(15.3, null)]
        [TestCase(null, null)]
        public void PreuzmiRazlikuLosiParametri(double? razlika, Elektrodistribucija el)
        {
            IElektrodistribucija iele = new ElektrodistribucijaMetode();
            Assert.Throws<ArgumentNullException>(() =>
            {
                iele.PreuzmiRazliku(razlika, el);
            });
        }

        [Test]
        [TestCase(null)]
        [TestCase(null)]
        public void PreuzmiRazlikuLosiParametri2(double? razlika)
        {
            IElektrodistribucija iele = new ElektrodistribucijaMetode();
            Assert.Throws<ArgumentNullException>(() =>
            {
                iele.PreuzmiRazliku(razlika, e);
            });
        }

        [Test]
        public void PosaljiCenu()
        {
            IElektrodistribucija iele = new ElektrodistribucijaMetode();
            double rez = iele.PosaljiCenu();
            Assert.IsNotNull(rez);
        }

        [TearDown]
        public void TearDown()
        {
            e = null;
        }

    }
}
