using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Vault13Server
{
    class Program
    {
        static void UpdateVaultInfoDelegate()
        {
            if (vault13 == null)
                return;

            while (true)
            {
                vault13.NotifyVault();

                Thread.Sleep(vault13.UpdateInfoPeriodSec * 1000);
            }

        }
        static Vault vault13 = new Vault(20);

        static Task InformationUpdateTask;
        static void Main(string[] args)
        {
            InformationUpdateTask = new Task(UpdateVaultInfoDelegate);
            InformationUpdateTask.Start();
            while (true)
            {
                
                Thread.Sleep(1000);
            }
        }
    }
}
