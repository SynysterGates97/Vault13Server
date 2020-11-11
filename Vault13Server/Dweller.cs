using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault13Server
{
    class Dweller
    {
        //мб еще айдишники придумать?
        const UInt16 maxHealth = 100;

        UInt16 healthPoints = maxHealth;
        DateTime timeOfAdventureBegin = DateTime.MinValue;

        Status personalStatus = Status.IN_VAULT;
        public enum Status
        {
            IN_VAULT,
            IN_WASTELAND,
            AT_THE_DOOR,
            DEAD,
        };



        public string Name
        {
            set; get;
        }

        

        public UInt16 HP
        {
            set
            {
            }
            get
            {
                return healthPoints;
            }
        }

        public void GetDamage(UInt16 damage)
        {
            if (damage >= healthPoints)
            {
                healthPoints = 0;
                PersonalStatus = Status.DEAD;
            }
            else
                healthPoints -= damage;
        }

        public void FillHealth()
        {
            healthPoints = maxHealth;
        }

        public Status PersonalStatus 
        { 
            get
            {
                return personalStatus;
            }
            set
            {
                if (value == Status.IN_WASTELAND)
                {
                    //Если отправляем жителя в пустоши - устанавливаем время начала путешествия.
                    TimeOfAdventureBegin = DateTime.Now;
                }
                else if(value == Status.IN_VAULT)
                {
                    //Если житель вернулся в убежище - его здоровье пополняется полностью
                    FillHealth();
                }
            }
        }

        public UInt16 WastelandResearchTimeSec
        {
            set; get;
        }

        public bool IsReturnedFromWasteland()
        {
            //long timePassedMs = (DateTime.UtcNow.Ticks- TimeOfAdventureBegin.Ticks)/ TimeSpan.TicksPerMillisecond; // Должно работать
            long timePassedMs = DateTime.UtcNow.ToFileTimeUtc() - TimeOfAdventureBegin.ToFileTimeUtc();
            if (timePassedMs >= WastelandResearchTimeSec)
                return true;

            return false;
        }
        public Dweller(string name)
        {
            Name = name;
        }

        static int NewDwellerNameCount = 0;
        public Dweller()
        {
            Name = "Stranger" + NewDwellerNameCount.ToString();
            NewDwellerNameCount++;
        }

        public DateTime TimeOfAdventureBegin
        {
            set;
            get;
        }
    }
}
