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

        VaultServer vaultServer = new VaultServer();

        [TestMethod()]
        public void ExecuteCommandTestMoneyAfterSend()
        {
            
            //оптравляем жителя в пустоши - тратим 500 крышек, следующий запрос денег вернет 500 
            string[] argvSd = { "sd", "Name0", "50" };
            vaultServer.ExecuteCommand(argvSd.Count(), argvSd);


            string[] argvMuneh = { "muneh" };
            string reply = vaultServer.ExecuteCommand(argvMuneh.Count(), argvMuneh);
            Assert.AreEqual(reply, "Бюджет убежища: 500");

        }
    }
}