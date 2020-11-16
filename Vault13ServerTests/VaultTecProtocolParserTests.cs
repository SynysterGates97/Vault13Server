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
    public class VaultTecProtocolParserTests
    {
        [TestMethod()]
        public void ParseCommandStringTestSepComma()
        {
            string inputString = "sd,Jimmy X,100";
            string[] argv = VaultTecProtocolParser.ParseCommandString(inputString);

            if (argv.Count() != 3 ||
                argv[0] != "sd" ||
                argv[1] != "Jimmy X" ||
                argv[2] != "100")
            {
                Assert.Fail();
            }


        }

        [TestMethod()]
        public void ParseCommandStringTestSepSpace()
        {
            string inputString = "howdy Name0";
            string[] argv = VaultTecProtocolParser.ParseCommandString(inputString);

            if (argv.Count() != 1 ||
                argv[0] != inputString)
            {
                Assert.Fail();
            }


        }
    }
}