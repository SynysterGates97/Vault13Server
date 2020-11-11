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

        public UInt16 UpdateInfoPeriodSec
        {
            set;get;
        }
        //1 реальная секунда - 1 час игрового времени
        public void NotifyVault()
        {
            //стоит запускать в другой таске
            foreach (var dweller in dwellersList)
            {
                if (dweller.PersonalStatus == Dweller.Status.IN_WASTELAND)
                {
                    dweller.PersonalStatus = Dweller.Status.IN_WASTELAND;
                    if(DateTime.Now == dweller.TimeOfAdventureBegin)

                    //Запустить в другой таске таймер на timeInHours;
                    return true;
                }
            }
        }
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
