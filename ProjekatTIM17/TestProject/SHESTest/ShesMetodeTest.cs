
using Moq;
using NUnit.Framework;
using SmartHomeEnergySystem.COMMON;
using SmartHomeEnergySystem.DODATNE_TABELE;
using SmartHomeEnergySystem.SHES;
using SmartHomeEnergySystem.SOLARNI_PANEL;
using System;
using SmartHomeEnergySystem.BATERIJA;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeEnergySystem.POTROSAC;
using System.Globalization;

namespace TestProject.SHESTest
{
    [TestFixture]

    public class ShesMetodeTest
    {
       
        private const int _BROJ_IZMERENIH_VREDNOSTI_PANELA = 1;
        List<SolarniPanel> solarniPaneli;
        SolarniPanel s1, s2;
        Shes s;
        private const int vrednost=1;
        List<Baterija> baterije;
        Baterija baterija1;
        List<PanelIzmereneVrednosti> panelIzme;
        PanelIzmereneVrednosti pi1, pi2;
        Potrosac potr1, potr2;
        List<Potrosac> potrosaci;
        List<Potrosac> listaPotrosaca;
        Potrosac potrosac1;
        DateTime? prviDatum, poslednjiDatum;
        PotrosaciStanje st1, st2;

        List<double> listaGrafDouble;
        IzmereneVrednostiBaterije izB;
        List<IzmereneVrednostiBaterije> listaE1;
        List<IzmereneVrednostiBaterije> listaE2;
        List<IzmereneVrednostiBaterije> listaE3;
        List<PotrosaciStanje> listaStanja;


        [SetUp]
        public void SetUp()
        {

            
             s1 = new SolarniPanel("panel1", 350);
             s2  = new SolarniPanel("panel2", 200);
             solarniPaneli = new List<SolarniPanel>() { s1,s2 };  //lazna lista panela
             s = Shes.Instance();
             baterija1 = new Baterija("baterija1", 100,2.00);
            baterije = new List<Baterija>() { baterija1 };
            pi1 = new PanelIzmereneVrednosti() { Id = 1, Datum = DateTime.Now, IzmerenaSnaga = 250,  Panel = s1 };
            pi2 = new PanelIzmereneVrednosti() { Id = 2, Datum = DateTime.Now, IzmerenaSnaga = 25,  Panel = s2 };
            panelIzme = new List<PanelIzmereneVrednosti>() { pi1, pi2 };
            potr1 = new Potrosac("potrosac1", 2.0, true);
            potr2 = new Potrosac("potrosac2", 1.0, true);
            potrosaci = new List<Potrosac>() { potr1, potr2 };
             potrosac1 = new Potrosac("imePotrosaca", 1.0, true);
            listaPotrosaca = new List<Potrosac>() { potrosac1 };
            var cultureInfo = new CultureInfo("de-DE");
            prviDatum = DateTime.Parse("12/06/2020",cultureInfo);
            poslednjiDatum = DateTime.Parse("14/06/2020", cultureInfo);
            listaGrafDouble = new List<double>() { 10.0, 9.0 };
            izB = new IzmereneVrednostiBaterije() { Id = 1, NazivBaterije = "batetija1", Baterija = baterija1, Kapacitet = 2.0, Rezim = 1, Datum = DateTime.Now };
            listaE1 = new List<IzmereneVrednostiBaterije>() { izB};
            listaE2 = new List<IzmereneVrednostiBaterije>() {izB };
            listaE3 = new List<IzmereneVrednostiBaterije>() {izB };
            st1 = new PotrosaciStanje() { Id = 1, Datum = DateTime.Now, Snaga = 1.0 };
            st2 = new PotrosaciStanje() { Id = 2, Datum = DateTime.Now, Snaga = 1.0 };
            listaStanja = new List<PotrosaciStanje>() { st1, st2 };
        }

        #region SolarniPanel
        [Test]
        public void SacuvajIzmereneVrednostiPanelaUBazu()
        {
            IShes shesRepo = new FakeShesRepozitorijum();
            ShesMetode shesMetode = new ShesMetode(shesRepo);
            shesMetode.IzmereneSnagePanelaDodaj(s1.Ime, s.Vreme, 200);
            Assert.AreEqual(_BROJ_IZMERENIH_VREDNOSTI_PANELA, shesMetode.BrojIzmerenihSnagaSPanela());
        }

        [Test]
        public void PreuzmiPanele()
        {
            Mock<IShes> shesRepository = new Mock<IShes>();
            shesRepository.Setup(x => x.PreuzmiPanele()).Returns(solarniPaneli);
            ShesMetode shesMetode = new ShesMetode(shesRepository.Object);
            List<SolarniPanel> result = shesMetode.PreuzmiSolarnePanele();

            Assert.IsNotNull(result);
            Assert.AreEqual(solarniPaneli.Count, result.Count);
            Assert.AreEqual(solarniPaneli.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(solarniPaneli.ElementAt(1), result.ElementAt(1));
           
        }

        [Test]
        public void BrojPanela()
        {
            Mock<IShes> shesRepository = new Mock<IShes>();
            shesRepository.Setup(x => x.VratiBrojIzmerenihSnagaPanela()).Returns(3);
            ShesMetode shesMetode = new ShesMetode(shesRepository.Object);
            int result = shesMetode.BrojIzmerenihSnagaSPanela();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void PreuzmiIzmereneVrednostiPanele()
        {
            Mock<IShes> shesRepository = new Mock<IShes>();
            shesRepository.Setup(x => x.VratiIzmereneVrednostiPanela()).Returns(panelIzme);
            ShesMetode shesMetode = new ShesMetode(shesRepository.Object);
            List<PanelIzmereneVrednosti> result = shesMetode.PreuzmiIzmereneVrednostiPanela();

            Assert.IsNotNull(result);
            Assert.AreEqual(panelIzme.Count, result.Count);
            Assert.AreEqual(panelIzme.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(panelIzme.ElementAt(1), result.ElementAt(1));

        }
        #endregion

        #region Baterija
        [Test]
        public void PreuzimanjePodatakaOdBaterije()
        {
            IShes rep = new FakeShesRepozitorijum();
            ShesMetode metode = new ShesMetode(rep);
            metode.PreuzmiPodatkeOdBaterije(15.00, 1, "baterija1", DateTime.Now);
            Assert.AreEqual(vrednost,metode.BrojIzmerenihKapacitetaBaterija());
            
        }
        
        [Test]
        public void ValidacijaVremenaZaRezimBaterije()
        {
            IShes rep = new FakeShesRepozitorijum();
            ShesMetode s = new ShesMetode(rep);
            int rezim = s.ValidacijaVremenaZaRezim();
            Assert.IsNotNull(rezim);
        }

        [Test]
        public void PreuzimanjeBaterijeIzBaze()
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            shesRepo.Setup(x => x.PreuzmiBaterije()).Returns(baterije);
            ShesMetode shesMetode = new ShesMetode(shesRepo.Object);
            List<Baterija> lista = shesMetode.PreuzmiBaterijeIzBaze();
            Assert.IsNotNull(lista);
            Assert.AreEqual(baterije.Count, lista.Count);
            Assert.AreEqual(baterije.ElementAt(0), lista.ElementAt(0));
       

        }

        [Test]
        public void BrojIzmerenihKapacitetaBaterije()
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            shesRepo.Setup(x => x.BrojIzmerenihKapaciteta()).Returns(1);
            ShesMetode shesMetode = new ShesMetode(shesRepo.Object);
            int result = shesMetode.BrojIzmerenihKapacitetaBaterija();
            Assert.AreEqual(1, result);
        }

        [Test]
        [TestCase(null)]
        public void PosaljiKomanduLosiParametri1(IBaterija ibaterija)
        {
           
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                shes.PosaljiKomanduNaBateriju(ibaterija);
            });
        }
        #endregion


        #region Potrosac
        [Test]
        public void VratiListuPotrosacaTest()
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            shesRepo.Setup(x => x.VratiListuPotrosacaRepo()).Returns(listaPotrosaca);
            ShesMetode shesMetode = new ShesMetode(shesRepo.Object);
            List<Potrosac> lista = shesMetode.VratiListuPotrosaca();
            Assert.IsNotNull(lista);
            Assert.AreEqual(listaPotrosaca.Count, lista.Count);
            Assert.AreEqual(listaPotrosaca.ElementAt(0), lista.ElementAt(0));
        }




        [Test]
        [TestCase(null)]
        public void DodajPotrosacTest(string naziv)
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                shes.DodajPotrosac(naziv,1.0);
            });
        }

        [Test]
        [TestCase(null)]
        public void UkljuciPotrosacTest(string naziv)
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                shes.Ukljuci(naziv);
            });
        }


        [Test]
        [TestCase(null)]
        public void IskljuciPotrosacTest(string naziv)
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                shes.Iskljuci(naziv);
            });
        }

        [Test]
        [TestCase(null)]
        public void ObrisiPotrosacTest(string naziv)
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                shes.ObrisiPotrosac(naziv);
            });
        }
        #endregion


        #region Elektrodistribucija
        [Test]
        public void SacuvajVrednostiZaElektrodistribucijuUBazu()
        {
            IShes shesRepo = new FakeShesRepozitorijum();
            ShesMetode shesMetode = new ShesMetode(shesRepo);
            shesMetode.SacuvajElektroDistribucijaPodatke(DateTime.Now, -1, 5.2);
            Assert.AreEqual(_BROJ_IZMERENIH_VREDNOSTI_PANELA, shesMetode.BrojElektrodistribucijaPodatakaBaza());
        }

        [Test]
        public void BrojPodatakaElektrodistribucija()
        {
            Mock<IShes> shesRepository = new Mock<IShes>();
            shesRepository.Setup(x => x.BrojPodatakaElektrodistribucija()).Returns(3);
            ShesMetode shesMetode = new ShesMetode(shesRepository.Object);
            int result = shesMetode.BrojElektrodistribucijaPodatakaBaza();
            Assert.AreEqual(3, result);
        }

        [Test]
        [TestCase(null, 2.3)]
        [TestCase(null, null)]
        public void ProizvodnjaSolarnihPanelaLosiParametri1(ISolarniPanel p, double? vreme)
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                shes.ProizvodnjaSolarnihPanela(p,solarniPaneli,vreme);
            });
        }

        [Test]
        [TestCase(null, 2.3)]
        [TestCase(null, null)]
        public void ProizvodnjaSolarnihPanelaLosiParametri2(List<SolarniPanel> solarniPaneli, double? vreme)
        {
            Mock<ISolarniPanel> panel = new Mock<ISolarniPanel>();
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                shes.ProizvodnjaSolarnihPanela(panel.Object, solarniPaneli,vreme);
            });
        }

        [Test]
        [TestCase(null,null,null)]
        [TestCase(null,null,2.3)]
        public void ProizvodnjaSolarnihPanelaLosiParametri3(ISolarniPanel p, List<SolarniPanel> solarniPaneli, double? vreme)
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                shes.ProizvodnjaSolarnihPanela(p, solarniPaneli,vreme);
            });
        }

        [Test]
        [TestCase(2.0)]
        [TestCase(3.0)]
        public void ProizvodnjaSolarnihPanelaDobriParametri(double? vreme)
        {
            Mock<ISolarniPanel> panel = new Mock<ISolarniPanel>();
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            double rezultat = shes.ProizvodnjaSolarnihPanela(panel.Object, solarniPaneli,vreme);
            Assert.IsNotNull(rezultat);
        }



        [Test]
        public void PotrosnjaPotrosacaDobriParametri()
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            double rezultat = shes.PotrosnjaPotrosaca(potrosaci);
            Assert.IsNotNull(rezultat);
        }

        [Test]
        [TestCase(null)]
        public void PotrosnjaPotrosacaLosiParametri(List<Potrosac> potrosaci)
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                shes.PotrosnjaPotrosaca(potrosaci);
            });
        }

        [Test]
        [TestCase(2.0)]
        [TestCase(3.0)]
        public void PotrosnjaProizvodnjaBaterijeDobriParametri(double? vreme)
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            double rezultat = shes.PotrosnjaProizvodnjaBaterije(baterije,vreme);
            Assert.IsNotNull(rezultat);
        }

        [Test]
        [TestCase(null, 2.0)]
        [TestCase(null, null)]
        public void PotrosnjaProizvodnjaBaterijeLosiParametri(List<Baterija> b, double? vreme)
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                shes.PotrosnjaProizvodnjaBaterije(b,vreme);
            });
        }

        [Test]
        [TestCase(null)]
        public void PotrosnjaProizvodnjaBaterijeLosiParametri2(double? vreme)
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                shes.PotrosnjaProizvodnjaBaterije(baterije, vreme);
            });
        }
        #endregion


        #region grafik i racunanje troskova
        [Test]

        [TestCase(null,null,null)]
        [TestCase("06/12/2020",null,null)]
        public void ValidacijaDatumaLosiParametri1(string datum, DateTime? prvi, DateTime? poslednji)

        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {

                shes.ValidacijaDatuma(datum, prvi, poslednji);
            });
        }

        [Test]
        [TestCase(null, null)]
        [TestCase("06/12/2020", null)]
        public void ValidacijaDatumaLosiParametri2(string datum, DateTime? prvi)
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                shes.ValidacijaDatuma(datum, prvi, poslednjiDatum);
            });
        }

        [Test]
        [TestCase(null, null)]
        [TestCase("06/12/2020", null)]
        public void ValidacijaDatumaLosiParametri3(string datum, DateTime? poslednji)
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                shes.ValidacijaDatuma(datum, prviDatum, poslednji);
            });
        }

        [Test]
        [TestCase(null)]
        public void ValidacijaDatumaLosiParametri3(string datum)
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                shes.ValidacijaDatuma(datum, prviDatum, poslednjiDatum);
            });
        }

        [Test]
        [TestCase("12/a/fa")]
        [TestCase("10/06/2020")]
        [TestCase("20/06/2020")]
        public void ValidacijaDatumaFalse(string datum)
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            bool rezultat = shes.ValidacijaDatuma(datum, prviDatum, poslednjiDatum);
            Assert.IsFalse(rezultat);
        }

        [Test]
        [TestCase("12/06/2020")]
        [TestCase("13/06/2020")]
        [TestCase("14/06/2020")]
        public void ValidacijaDatumaTrue(string datum)
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            bool rezultat = shes.ValidacijaDatuma(datum, prviDatum, poslednjiDatum);
            Assert.IsTrue(rezultat);
        }

        

        [TestCase(null)]
        public void StanjePotrosacaLosiParametri(List<Potrosac> potrosaci)

        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);

             Assert.Throws<ArgumentNullException>(() =>
            {
               shes.StanjePotrosacaa(potrosaci);
            });
                
            
        }

       
        [Test]
        public void GrafikProizvodnjaPanela()
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            double rez=  shes.GrafikProizvodnjaPanela(DateTime.Now, listaGrafDouble);
            Assert.IsNotNull(rez);
        }

        [Test]
        public void GrafikEnergijaBaterije()
        {

            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            double rez = shes.GrafikEnergijaBaterija(DateTime.Now, listaE1, listaE2, listaE3);
            Assert.IsNotNull(rez);
        }

        [Test]
        public void GrafikUvozED()
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            double rez = shes.GrafikUvozED(DateTime.Now, 10.0, listaGrafDouble, listaGrafDouble);
            Assert.IsNotNull(rez);
        }

        [Test]
        public void GrafikPotrosnja()
        {
            Mock<IShes> shesRepo = new Mock<IShes>();
            ShesMetode shes = new ShesMetode(shesRepo.Object);
            double rez = shes.GrafikPotrosnja(DateTime.Now, listaGrafDouble);
            Assert.IsNotNull(rez);
        }

        [Test]
        public void StanjePotrosacaTest()
        {
            IShes shesRepo = new FakeShesRepozitorijum();
            ShesMetode shesMetode = new ShesMetode(shesRepo);
            shesMetode.StanjePotrosacaa(listaPotrosaca);
            Assert.AreEqual(1, shesMetode.BrojStanjaPotrosacaIzBaza());


        }

        [Test]
        public void BrojStanjaPotrosaca()
        {
            Mock<IShes> shesRepository = new Mock<IShes>();
            shesRepository.Setup(x => x.VratiBrojStanjaPotrosaca()).Returns(3);
            ShesMetode shesMetode = new ShesMetode(shesRepository.Object);
            int result = shesMetode.BrojStanjaPotrosacaIzBaza();
            Assert.AreEqual(3, result);
        } 

        [Test]
        public void VratiListuStanjaPotrosaca()
        {
            Mock<IShes> shesRepository = new Mock<IShes>();
            shesRepository.Setup(x => x.VratiListuStanjaPotrosaca()).Returns(listaStanja);
            ShesMetode shesMetode = new ShesMetode(shesRepository.Object);
            List<PotrosaciStanje> result = shesMetode.VratiStanjaPotrosacaIzBaze();

            Assert.IsNotNull(result);
            Assert.AreEqual(listaStanja.Count, result.Count);
            Assert.AreEqual(listaStanja.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(listaStanja.ElementAt(1), result.ElementAt(1));
        }
        #endregion

        [TearDown]
        public void TearDown()
        {
            s1 = null;
            s2 = null;
            solarniPaneli = null;
            s = null;
             baterija1 = null;
            baterije = null;
            pi1 = null;
            pi2 = null;
            panelIzme = null;
            potr1 = null;
            potr2 = null;
            potrosaci = null;
            potrosac1=null;
            listaPotrosaca=null;
            prviDatum = null;
            poslednjiDatum = null;
            listaGrafDouble = null;
            st1 = null;
            st2 = null;
            listaStanja = null;
        }

    }
}
