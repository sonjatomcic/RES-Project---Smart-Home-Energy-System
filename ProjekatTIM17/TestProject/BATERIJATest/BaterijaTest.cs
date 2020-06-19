using NUnit.Framework;
using SmartHomeEnergySystem.BATERIJA;
using SmartHomeEnergySystem.COMMON;
using SmartHomeEnergySystem.SHES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.BATERIJATest
{
  [TestFixture]
  public class BaterijaTest
    {
        [Test]
        [TestCase("baterija1", 100,10.00)]
        [TestCase("baterija2", 200,15.00)]
        [TestCase("baterija3", 150,20.00)]
        public void BaterijaDobriParametri(string ime,int ? maxSnaga,double ? kapacitet)
        {
            Baterija baterija = new Baterija(ime, maxSnaga, kapacitet);
            Assert.AreEqual(baterija.Ime, ime);
            Assert.AreEqual(baterija.MaxSnaga, maxSnaga);
            Assert.AreEqual(baterija.Kapacitet, kapacitet);
            
        }

        [Test]
        [TestCase("b",0,0.00)]
        [TestCase("baterija1",0,10.00)]
        [TestCase("bat",100,0.00)]
        [TestCase(",",1,1.00)]
        public void BaterijaGranicniParametri(string ime,int ? maxSnaga,double ? kapacitet)
        {
            Baterija baterija = new Baterija(ime, maxSnaga, kapacitet);
            Assert.AreEqual(baterija.Ime, ime);
            Assert.AreEqual(baterija.MaxSnaga, maxSnaga);
            Assert.AreEqual(baterija.Kapacitet, kapacitet);
        }


        [Test]
        [TestCase("baterija", -1, 10.00)]
        [TestCase("baterija3", -155, 15.00)]
        [TestCase("baterija8", 150, -10.00)]
        [TestCase("baterija2", 200, -2.00)]
        [TestCase("", 180, 20.00)]
        [TestCase("", 270, 30.00)]


        public void BaterijaLosiParametri1(string ime, int? maxSnaga, double? kapacitet)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Baterija baterija = new Baterija(ime, maxSnaga, kapacitet);
            });
        } 


        [Test]
        [TestCase("baterija", null,15.00)]
        [TestCase("baterija3", null,10.00)]
        [TestCase("baterija8", 160, null)]
        [TestCase("baterija2", 210, null)]
        [TestCase(null, 170, 10.00)]
        [TestCase(null, 250, 25.00)]


        public void BaterijaLosiParametri2(string ime,int? maxSnaga,double? kapacitet)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Baterija baterija = new Baterija(ime, maxSnaga,kapacitet);
            });
        }

     

    }
}
