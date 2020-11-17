using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vault13Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Vault13Server.Tests
{
    [TestClass()]
    public class DwellerTests
    {
        [TestMethod()]
        public void GetDamageInWastelandsTestHpFill()
        {
            Dweller testDweller = new Dweller("Jhonny");
            //отправим в пустоши
            testDweller.PersonalStatus = Dweller.Status.IN_WASTELAND;
            testDweller.HP = 50;//уменьшим здоровье
            //При возвращении в убежище жителя лечат
            testDweller.PersonalStatus = Dweller.Status.IN_VAULT;
            Assert.AreEqual(testDweller.HP, 100);
        }

        [TestMethod()]
        public void GetDamageInWastelandsTestDead()
        {
            Dweller testDweller = new Dweller("Jimmy");
            //testDweller.HP = 1;

            testDweller.PersonalStatus = Dweller.Status.IN_WASTELAND;

            //Искусственно уменьшаем время начала путешествия, чтобы точно "убить жителя"
            testDweller.TimeOfAdventureBegin = new DateTime(2020, 11, 1);

            //Время в два раза большее смертельного
            long deadlyTimeBeginTimeMs = DateTime.Now.Ticks - TimeSpan.TicksPerMillisecond * 1000 * 12000;
            testDweller.TimeOfAdventureBegin = new DateTime(deadlyTimeBeginTimeMs);
            DateTime dateTime = new DateTime();

            Thread.Sleep(100);
            testDweller.GetDamageInWastelands();

            Assert.AreEqual(Dweller.Status.DEAD, testDweller.PersonalStatus);
        }

        [TestMethod()]
        public void FillHealthTest()
        {
            Dweller testDweller = new Dweller("Amy");

            testDweller.WastelandResearchTimeSec = 100;
            testDweller.PersonalStatus = Dweller.Status.IN_WASTELAND;

            //Время в два раза меньше смертельного. Должен выжить но точно получит урон
            long timeWithDamage = (DateTime.Now.Ticks - TimeSpan.TicksPerMillisecond * 1000 * 300);
            testDweller.TimeOfAdventureBegin = new DateTime(timeWithDamage);

            testDweller.GetDamageInWastelands();

            testDweller.FillHealth();


            Assert.AreEqual(100, testDweller.HP);
        }

        [TestMethod()]
        public void IsReturnedFromWastelandTest()
        {
            Dweller testDweller = new Dweller("Karen");
            testDweller.WastelandResearchTimeSec = 2;
            testDweller.PersonalStatus = Dweller.Status.IN_WASTELAND;
            Thread.Sleep(2500);


            Assert.AreEqual(true, testDweller.IsReturnedFromWasteland());
        }

        [TestMethod()]
        public void EarnMoneyInWastelandByTimeTest()
        {
            Dweller testDweller = new Dweller("Karen");
            testDweller.WastelandResearchTimeSec = 100;
            testDweller.PersonalStatus = Dweller.Status.IN_WASTELAND;

            //Нужно дать жителю хоть сколько-то денег, т.к. он может либо заработать, либо потерять всё
            const int testMoney = 20;
            testDweller.GainedMoney = testMoney;
            Thread.Sleep(2000);
            testDweller.EarnMoneyInWastelandByTime();

            if (testDweller.GainedMoney == testMoney)
                Assert.Fail();

        }
    }
}