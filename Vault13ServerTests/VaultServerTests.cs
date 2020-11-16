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
    public class VaultServerTests
    {

        
        [TestMethod()]
        public void ExecuteCommandTestMoneyAfterSend()
        {
            VaultServer vaultServer = new VaultServer();

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
    }
}