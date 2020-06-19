using NUnit.Framework;
using SmartHomeEnergySystem.COMMON;
using SmartHomeEnergySystem.POTROSAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.POTROSACTest
{
    [TestFixture]
    public class PotrosacMetodeTest
    {
        List<Potrosac> lista;
        Potrosac p1;
        Potrosac p2;
        Potrosac p3;
        Potrosac p4;


        [SetUp]
        public void SetUp()
        {
            p1 = new Potrosac("p1", 1.0, false);
            p2 = new Potrosac("p2", 2.0, true);
            p3 = new Potrosac("p3", 3.0, false);
            p4 = new Potrosac("p4", 4.0, true);
            lista = new List<Potrosac>() { p1, p2,p3,p4 };

        }

        [Test]
        public void ValidacijaDodatogPotrosacaDobriParametri() //DOBRI PARAMETRI
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaDodatogPotrosaca(lista, "s", "1.0");
            Assert.IsTrue(pom);
        }

        [Test]
        public void ValidacijaDodatogPotrosacaLosiParametri1() //NAZIV PRAZAN STRING
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaDodatogPotrosaca(lista, "", "1.0");
            Assert.IsFalse(pom);
        }

        [Test]
        public void ValidacijaDodatogPotrosacaLosiParametri2() //NEGATIVNA VREDNOST POTROSNJE
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaDodatogPotrosaca(lista, "pot", "-3.0");
            Assert.IsFalse(pom);
        }

        [Test]
        public void ValidacijaDodatogPotrosacaLosiParametri3() // POSTOJECI POTROSAC U BAZI 
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaDodatogPotrosaca(lista, "p1", "3.0");
            Assert.IsFalse(pom);
        }

        [Test]
        public void ValidacijaDodatogPotrosacaLosiParametri4() // POTROSNJA NIJE BROJ NEGO KARAKTER
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaDodatogPotrosaca(lista, "p1", "pppppp");
            Assert.IsFalse(pom);
        }

        [Test]
        public void ValidacijaUkljuciDobriParametri() //DOBRI PARAMETRI
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaUkljuci(lista, "p1");
            Assert.IsTrue(pom);
        }

        [Test]
        public void ValidacijaUkljuciLosiParametri1() //DA SE UGASI NEPOSTOJESI POTROSAC
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaUkljuci(lista, "pppp");
            Assert.IsFalse(pom);
        }

        [Test]
        public void ValidacijaUkljuciLosiParametri2() //DA SE UKLJUCI VEC UKLJUCEN POTROSAC
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaUkljuci(lista, "p2");
            Assert.IsFalse(pom);
        }

        [Test]
        public void ValidacijaUkljuciLosiParametri3() //UNET PRAZAN STRING
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaUkljuci(lista, "");
            Assert.IsFalse(pom);
        }

        [Test]
        public void ValidacijaIskljuciDobriParametri() //DOBRI PARAMETRI
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaIskljuci(lista, "p4");
            Assert.IsTrue(pom);
        }

        [Test]
        public void ValidacijaIskljuciLosiParametri1() //PRAZAN STRING
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaIskljuci(lista, "");
            Assert.IsFalse(pom);
        }

        [Test]
        public void ValidacijaIskljuciLosiParametri2() //POTROSAC NE POSTOJI
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaIskljuci(lista, "djurdja");
            Assert.IsFalse(pom);
        }

        [Test]
        public void ValidacijaIskljuciLosiParametri3() //ISKLJUCI VEC ISKLJUCEN POTROSAC
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaIskljuci(lista, "p1");
            Assert.IsFalse(pom);
        }

        [Test]
        public void ValidacijaObrisiDobriParametri() //DOBRI PARAMETRI
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaObrisiPotrosaca(lista, "p4");
            Assert.IsTrue(pom);
        }

        [Test]
        public void ValidacijaObrisiLosiParametri1() //UNET PRAZAN STRING
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaObrisiPotrosaca(lista, "");
            Assert.IsFalse(pom);
        }

        [Test]
        public void ValidacijaObrisiLosiParametri2() //BRISANJE NEPOSTOJECEG POTROSACA
        {
            IPotrosac ip = new PotrosacMetode();
            bool pom = ip.ValidacijaObrisiPotrosaca(lista, "ppppppp");
            Assert.IsFalse(pom);
        }


        [TearDown]
        public void TearDown()
        {
            p1 = null;
            p2 = null;
            p3 = null;
            p4 = null;
            lista = null;
        }
    }
}