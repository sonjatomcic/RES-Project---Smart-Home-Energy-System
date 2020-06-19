using NUnit.Framework;
using SmartHomeEnergySystem.SHES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.SHESTest
{
    [TestFixture]
    public class ShesTest
    {
        [Test]
        [TestCase(null)]
        public void UbrzajVremeLosiParametri(string sekund)
        {
            Shes shes = Shes.Instance();
            Assert.Throws<ArgumentNullException>(() =>
            {
                shes.UbrzajVreme(sekund);
           });
        }

        [Test]
        [TestCase("")]
        [TestCase("-1")]
        public void UbrzajVremeLosiParametri2(string sekund)
        {
            Shes shes = Shes.Instance();
            Assert.Throws<ArgumentException>(() =>
            {
                shes.UbrzajVreme(sekund);
            });
        }
    }
}
