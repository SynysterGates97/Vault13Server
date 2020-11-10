using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault13Server
{
    class Vault
    {
        List<Dweller> dwellersList;

        public bool SendDwellerToWasteland(string name, UInt16 timeInHours)
        {
            foreach (var dweller in dwellersList)
            {
                if (dweller.Name == name)
                {
                    dweller.PersonalStatus = Dweller.Status.IN_WASTELAND;
                    //Запустить в другой таске таймер на timeInHours;
                    return true;
                }
            }
            return false;
        }
    }
}
