using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vault13Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault13ServerTests;
using System.Threading;

namespace Vault13Server.Tests
{
    [TestClass()]
    public class VaultServerTests
    {
        Task listenTask;

        int count = 0;
        void listenSocketDelegate()
        {
            count = 444;
            while(true)
            {
                vaultServer.ProcessClientRequestIfNeed();

            }
        }

        VaultServer vaultServer = new VaultServer();
        SimpleClient2Tests simpleTestClient = new SimpleClient2Tests();
        [TestMethod()]
        public void ExecuteCommandTestMoneyAfterSend()
        {

            //оптравляем жителя в пустоши - тратим 500 крышек, следующий запрос денег вернет 500 
            string[] argvSd = { "sd", "Name4", "50" };
            vaultServer.ExecuteCommand(argvSd.Count(), argvSd);

            int dwellerIndex = vaultServer.vault13.GetDwellersIndexByName(argvSd[1]);
            Dweller sentDweller = vaultServer.vault13.dwellersList[dwellerIndex];

            bool setDwellerStatusIsOk = sentDweller.PersonalStatus == Dweller.Status.IN_WASTELAND;

            string[] argvMuneh = { "muneh" };
            string reply = vaultServer.ExecuteCommand(argvMuneh.Count(), argvMuneh);

            bool moneyIsOk = reply == "Бюджет убежища: 500";

            if (!moneyIsOk || !setDwellerStatusIsOk)
                Assert.Fail();

        }

        [TestMethod()]
        public void ExecuteCommandTestMoney()
        {

            //оптравляем жителя в пустоши - тратим 500 крышек, следующий запрос денег вернет 500 
            string[] argvSd = { "sd", "Name4", "50" };
            vaultServer.ExecuteCommand(argvSd.Count(), argvSd);

            int dwellerIndex = vaultServer.vault13.GetDwellersIndexByName(argvSd[1]);
            Dweller sentDweller = vaultServer.vault13.dwellersList[dwellerIndex];

            bool setDwellerStatusIsOk = sentDweller.PersonalStatus == Dweller.Status.IN_WASTELAND;

            string[] argvMuneh = { "muneh" };
            string reply = vaultServer.ExecuteCommand(argvMuneh.Count(), argvMuneh);

            bool moneyIsOk = reply == "Бюджет убежища: 500";

            if (!moneyIsOk || !setDwellerStatusIsOk)
                Assert.Fail();

        }

        [TestMethod()]
        public void TestServerReponse()
        {
            listenTask = new Task(listenSocketDelegate);
            listenTask.Start();

            string serverResponse = simpleTestClient.SendRequestAndGetResponse("muneh");

            if (!serverResponse.Contains("Бюджет"))
                Assert.Fail();

        }
    }
}