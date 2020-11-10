using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault13Server
{
    class Dweller
    {
        UInt16 healthPoints = 100;
        public string Name
        {
            set; get;
        }

        enum Status
        {
            IN_VAULT,
            IN_WASTELAND,
            AT_THE_DOOR,
            DEAD,
        };

        public UInt16 HP
        {
            set
            {
                if(value <= 100)
                {
                    healthPoints = value;

                }
            }
        }
        void la(string fi)
        {
            fi = "aasd";
        }
    }
}
