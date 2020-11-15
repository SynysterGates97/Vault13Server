using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault13Server
{
    public class VaultTecProtocolParser
    {
        static public string[] ParseCommandString(string cmd)
        {
            string[] argv = cmd.Split(',');

            return argv;
        }
    }
}
