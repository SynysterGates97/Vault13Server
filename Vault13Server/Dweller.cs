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

        public Status PersonalStatus { get; set; }


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
    }
}
