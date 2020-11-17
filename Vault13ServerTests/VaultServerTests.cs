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

        void listenSocketDelegate()
        {
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
            vaultServer.vault13.dwellersList.Add(new Dweller("Turk"));

            string[] argvSd = { "sd", "Turk", "50" };
            vaultServer.ExecuteCommand(argvSd.Count(), argvSd);

            int dwellerIndex = vaultServer.vault13.GetDwellersIndexByName(argvSd[1]);
            Dweller sentDweller = vaultServer.vault13.dwellersList[dwellerIndex];

            bool setDwellerStatusIsOk = sentDweller.PersonalStatus == Dweller.Status.IN_WASTELAND;

            string[] argvMuneh = { "muneh" };
            string reply = vaultServer.ExecuteCommand(argvMuneh.Count(), argvMuneh);

            bool moneyIsOk = reply == "Бюджет убежища: 500";

            vaultServer.Terminate();
            if (!moneyIsOk || !setDwellerStatusIsOk)
                Assert.Fail();

        }

        [TestMethod()]
        public void ExecuteCommandTestMoney()
        {

            //оптравляем жителя в пустоши - тратим 500 крышек, следующий запрос денег вернет 500 
            vaultServer.vault13.dwellersList.Add(new Dweller("John Dorian"));
            string[] argvSd = { "sd", "John Dorian", "50" };
            vaultServer.ExecuteCommand(argvSd.Count(), argvSd);

            int dwellerIndex = vaultServer.vault13.GetDwellersIndexByName(argvSd[1]);
            Dweller sentDweller = vaultServer.vault13.dwellersList[dwellerIndex];

            bool setDwellerStatusIsOk = sentDweller.PersonalStatus == Dweller.Status.IN_WASTELAND;

            string[] argvMuneh = { "muneh" };
            string reply = vaultServer.ExecuteCommand(argvMuneh.Count(), argvMuneh);

            bool moneyIsOk = reply == "Бюджет убежища: 500";

            vaultServer.Terminate();
            if (!moneyIsOk || !setDwellerStatusIsOk)
                Assert.Fail();

        }

        [TestMethod()]
        public void TestServerReponse()
        {
            listenTask = new Task(listenSocketDelegate);
            listenTask.Start();

            string serverResponse = simpleTestClient.SendRequestAndGetResponse("muneh");

            vaultServer.Terminate();
            if (!serverResponse.Contains("Бюджет"))
                Assert.Fail();

        }
    }
}