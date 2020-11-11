using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault13Server
{
    class Vault
    {
        public List<Dweller> dwellersList = new List<Dweller>();

        UInt16 updateInfoPeriodSec = 1;
        public UInt16 UpdateInfoPeriodSec
        {
            set
            {
                updateInfoPeriodSec = value;
            }
            get
            {
                return updateInfoPeriodSec;
            }
        }
        //1 реальная минута - 1 час игрового времени
        //потоконебезопасная функция
        public void NotifyVault()
        {
            //стоит запускать в другой таске
            foreach (var dweller in dwellersList)
            {
                if (dweller.PersonalStatus == Dweller.Status.IN_WASTELAND)//для жителей в пустошах
                {
                    dweller.GetDamageInWastelands();
                }
            }
        }
        public bool SendDwellerToWasteland(string name, UInt16 timeInHours)
        {
            for(int i=0; i< dwellersList.Count();i++)
            {
                if (dwellersList[i].Name == name && dwellersList[i].PersonalStatus != Dweller.Status.IN_WASTELAND)
                {
                    dwellersList[i].PersonalStatus = Dweller.Status.IN_WASTELAND;
                    //Запустить в другой таске таймер на timeInHours;
                    return true;
                }
            }
            return false;
        }

        public Vault()
        {
            UpdateInfoPeriodSec = 1;
        }

        public Vault(int DwellersCount)
        {
            for(int i = 0; i< DwellersCount; i++)
            {
                Dweller dweller = new Dweller();
                dwellersList.Add(dweller);
            }
        }
    }
}
