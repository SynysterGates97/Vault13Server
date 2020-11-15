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

        UInt64 vaultBudget = 1000;
        public UInt64 VaultBudget
        {
            set
            {
                vaultBudget = value;
            }
            get
            {
                return vaultBudget;
            }
        }
        public 
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

        public bool LetAllDwellersIn()
        {
            int dwellersInCount = 0;
            for (int i = 0; i < dwellersList.Count(); i++)
            {
                if (dwellersList[i].PersonalStatus == Dweller.Status.AT_THE_DOOR)
                {
                    dwellersList[i].PersonalStatus = Dweller.Status.IN_VAULT;
                    VaultBudget += dwellersList[i].GainedMoney;
                    dwellersList[i].GainedMoney = 0;

                    dwellersInCount++;
                }
            }
            return dwellersInCount > 0;
        }
        public bool LetDwellerIn(string name)
        {
            int index = GetDwellersIndexByName(name);
            if (index >= 0 && dwellersList[index].PersonalStatus == Dweller.Status.AT_THE_DOOR)
            {
                dwellersList[index].PersonalStatus = Dweller.Status.IN_VAULT;
                VaultBudget += dwellersList[index].GainedMoney;
                dwellersList[index].GainedMoney = 0;
                return true;
            }
            return false;

        }

        public int GetDwellersIndexByName(string name)
        {
            for (int i = 0; i < dwellersList.Count(); i++)
            {
                if (dwellersList[i].Name == name)
                {
                    return i;
                }
            }
            return -1;
        }
        //1 реальная минута - 1 час игрового времени
        //потоконебезопасная функция
        public void NotifyVault()
        {
            //стоит запускать в другой таске
            for (int i = 0; i < dwellersList.Count(); i++)
            {
                if (dwellersList[i].PersonalStatus == Dweller.Status.IN_WASTELAND)//для жителей в пустошах
                {
                    if (dwellersList[i].IsReturnedFromWasteland())
                    {
                        dwellersList[i].PersonalStatus = Dweller.Status.AT_THE_DOOR;
                    }
                    else
                    {
                        dwellersList[i].EarnMoneyInWastelandByTime();
                        dwellersList[i].GetDamageInWastelands();
                    }

                }
            }
        }
        public bool SendDwellerToWasteland(string name, UInt16 timeInHours)
        {
            if (vaultBudget >= 500)
            {
                vaultBudget -= 500;
                int dwellersIndex = GetDwellersIndexByName(name);
                if (dwellersIndex >= 0 && timeInHours != 0 && dwellersList[dwellersIndex].PersonalStatus == Dweller.Status.IN_VAULT)
                {
                    dwellersList[dwellersIndex].PersonalStatus = Dweller.Status.IN_WASTELAND;
                    dwellersList[dwellersIndex].WastelandResearchTimeSec = timeInHours /** 60*/;
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
