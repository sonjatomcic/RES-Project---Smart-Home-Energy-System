using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeEnergySystem.BATERIJA;
using SmartHomeEnergySystem.COMMON;
using Moq;
using SmartHomeEnergySystem.SHES;

namespace TestProject.BATERIJATest
{
    [TestFixture]
    public class BaterijaMetodeTest
    {
        
        [Test]
        [TestCase(11)]
        [TestCase(10)]
        [TestCase(20)]
        public void PreuzmiRezimLosiParametri1(int? rezim)
        {
            IBaterija Ib = new BaterijaMetode();
            Assert.Throws<ArgumentException>(() =>
            {
                Ib.PreuzmiRezim(rezim);
            });
        }

        [Test]
        [TestCase(null)]
       
        public void PreuzmiRezimLosiParametri2(int? rezim)
        {
            IBaterija Ib = new BaterijaMetode();
            Assert.Throws<ArgumentNullException>(() =>
            {
                Ib.PreuzmiRezim(rezim);
            });
        }
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void PreuzmiRezimDobriParametri1(int? rezim)
        {
            IBaterija Ib = new BaterijaMetode();
            Assert.DoesNotThrow(() =>
            {
                Ib.PreuzmiRezim(rezim);
            });
        }

        [Test]
        [TestCase(null)]
        public void RukovanjeKapacitetom2Test(ShesMetode shess)
        {
            IBaterija bat = new BaterijaMetode();
            Assert.Throws<ArgumentNullException>(() =>
            {
                bat.RukovanjeKapacitetom2(shess);
            });
        }



    }
}
