using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault13Server
{
    class Dweller
    {
        const int deadlyTimeInWastelandMin_real = 10;
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


        Random rand = new Random();
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

        public void GetDamageInWastelands()
        {
            int deadlyTimeInWastelandSec_real = deadlyTimeInWastelandMin_real * 60;

            //в дальнейшем можно добавить учет опыта и характеристик.
            int maxRandomDamage = (int)(100 * TimeInWastelandSec / deadlyTimeInWastelandSec_real);
            int minRandomDamage = 0;
            int randomDamage = rand.Next(minRandomDamage, maxRandomDamage);

            if (randomDamage >= healthPoints)
            {
                healthPoints = 0;
                PersonalStatus = Status.DEAD;
            }
            else
                healthPoints -= (UInt16)randomDamage;//todo: переполнение разряда. Но вроде как невозможно.
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
                    personalStatus = value;
                }
                else if(value == Status.IN_VAULT)
                {
                    //Если житель вернулся в убежище - его здоровье пополняется полностью
                    personalStatus = value;
                    FillHealth();

                }
            }
        }

        public UInt16 WastelandResearchTimeSec
        {
            set; get;
        }

        public UInt32 TimeInWastelandSec
        {
            get
            {
                //long timePassedMs = (DateTime.UtcNow.Ticks- TimeOfAdventureBegin.Ticks)/ TimeSpan.TicksPerMillisecond; // Должно работать
                UInt32 timePassedSec = (UInt32)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - (UInt32)TimeOfAdventureBegin.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                return timePassedSec;
            }
        }

        public bool IsReturnedFromWasteland()
        {            
            if (TimeInWastelandSec >= WastelandResearchTimeSec)
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
            Name = "Name" + NewDwellerNameCount.ToString();
            NewDwellerNameCount++;
        }

        public DateTime TimeOfAdventureBegin
        {
            set;
            get;
        }
    }
}
