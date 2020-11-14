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
        static string ExecuteCommand(int argc, string[] argv)
        {
            string reply = "Неизвестная команда";
            if (argc > 0)
            {
                if (argv[0] == "sd")
                {
                    if (argc >= 3)
                    {
                        string dwellerToWastelandName = argv[1];
                        UInt16 adventureTimeHours = Convert.ToUInt16(argv[2]);
                        if (vault13.SendDwellerToWasteland(dwellerToWastelandName, adventureTimeHours))
                        {
                            reply = "Житель отправлен на исследование пустошей";
                        }
                        else
                        {
                            reply = "Данный житель не может быть отправлен на исследование пустошей";
                        }

                    }
                    else
                    {
                        reply = "Неверный формат команды sd";
                    }

                }
                if (argv[0] == "howdy")
                {
                    if (argc >= 2)
                    {
                        string dwellerToWastelandName = argv[1];
                        int dwellersIndex = vault13.GetDwellersIndexByName(dwellerToWastelandName);
                        if(dwellersIndex >= 0)
                        {
                            string status =  vault13.dwellersList[dwellersIndex].GetStringStatus();
                            reply = String.Format("{0:s} {1:s}: ", dwellerToWastelandName, status);
                        }
                        else
                        {
                            reply = String.Format("В убежище нет жителя с именем {0:s}: ", dwellerToWastelandName);
                        }

                    }
                    else
                    {
                        reply = "Неверный формат команды sd";
                    }

                }
            }
            return reply;
        }
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
            VaultTecServerProtocol vaultTecServerProtocol = new VaultTecServerProtocol();

            InformationUpdateTask = new Task(UpdateVaultInfoDelegate);
            InformationUpdateTask.Start();
            while (true)
            {
                Console.WriteLine("EnterCommand");
                string cmd = Console.ReadLine();

                string[] argv = VaultTecProtocolParser.ParseCommandString(cmd);

                string reply = ExecuteCommand(argv.Count(),argv);
                Console.WriteLine(reply);
                Thread.Sleep(1000);
            }
        }
    }
}
