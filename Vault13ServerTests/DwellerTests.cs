using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vault13Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault13Server.Tests
{
    [TestClass()]
    public class DwellerTests
    {
        [TestMethod()]
        public void GetDamageInWastelandsTest()
        {
            Dweller testDweller = new Dweller("Jhonny");
            //отправим в пустоши
            testDweller.PersonalStatus = Dweller.Status.IN_WASTELAND;
            testDweller.HP = 50;//уменьшим здоровье
            //При возвращении в убежище жителя лечат
            testDweller.PersonalStatus = Dweller.Status.IN_VAULT;
            Assert.AreEqual(testDweller.HP,100);
        }
    }
}