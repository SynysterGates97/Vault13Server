using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault13Server
{
    class VaultTecProtocolParser
    {
        VaultTecServerProtocol.Command ParseCommandString(string cmd)
        {
            return VaultTecServerProtocol.Command.UNKNOWN_COMMAND;
        }
    }
}
