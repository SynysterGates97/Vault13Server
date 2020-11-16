using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vault13Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Threading;

namespace Vault13Server.Tests
{
    [TestClass()]
    public class VaultTests
    {
        [TestMethod()]
        public void SendDwellerToWastelandTest()
        {
            Vault vault = new Vault(10);

            vault.SendDwellerToWasteland("Name0", 500);
            vault.SendDwellerToWasteland("Name1", 500);
            bool flagCanSendDwellerWithoutMoney = vault.SendDwellerToWasteland("Name0", 500);

            if (flagCanSendDwellerWithoutMoney)
                Assert.Fail();
        }

        [TestMethod()]
        public void LetAllDwellersInTest()
        {
            Vault vault = new Vault();

            vault.dwellersList.Add(new Dweller("Pam"));

            vault.SendDwellerToWasteland("Pam", 2);
            
            Thread.Sleep(3000);

            vault.NotifyVault();

            int firstDwellerIndex = vault.GetDwellersIndexByName("Pam");


            Assert.AreEqual(vault.dwellersList[firstDwellerIndex].PersonalStatus, Dweller.Status.AT_THE_DOOR);
        }

        [TestMethod()]
        public void GetDwellersIndexByNameTest()
        {
            Vault vault = new Vault();
            vault.dwellersList.Add(new Dweller("Pam"));
            vault.dwellersList.Add(new Dweller("Jim"));

            int firstDwellerIndex = vault.GetDwellersIndexByName("Pam");

            Assert.AreEqual(firstDwellerIndex,0);
        }
    }
}