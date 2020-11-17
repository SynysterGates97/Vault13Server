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
        public void StatusChangedWhenBackedFromWatelandTest()
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

            Assert.AreEqual(firstDwellerIndex, 0);
        }

        [TestMethod()]
        public void LetAllDwellersInTest()
        {
            Vault vault = new Vault();
            vault.dwellersList.Add(new Dweller("Pam"));
            vault.dwellersList.Add(new Dweller("Jim"));

            vault.SendDwellerToWasteland("Pam", 1);
            vault.SendDwellerToWasteland("Jim", 1);

            Thread.Sleep(1500);
            vault.NotifyVault();

            vault.LetAllDwellersIn();

            Dweller.Status status1 = vault.dwellersList[0].PersonalStatus;
            Dweller.Status status2 = vault.dwellersList[1].PersonalStatus;

            Assert.AreEqual(Dweller.Status.IN_VAULT, status1);

            //if (status1 != Dweller.Status.IN_VAULT ||
            //    status2 != Dweller.Status.IN_VAULT)
            //{
            //    Assert.Fail();
            //}
        }

    }
}