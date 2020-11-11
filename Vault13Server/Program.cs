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
        static void ExecuteCommand(int argc, string[] argv)
        {
            if (argc > 0)
            {
                if (argv[0] == "sd")
                {
                    if (argc >= 3)
                    {
                        string dwellerToWastelandName = argv[1];
                        UInt16 adventureTimeHours = Convert.ToUInt16(argv[2]);
                        if(vault13.SendDwellerToWasteland(dwellerToWastelandName, adventureTimeHours))
                        {
                            Console.WriteLine("Житель отправлен на исследование пустошей");
                        }
                        else
                        {
                            Console.WriteLine("Данный житель не может быть отправлен на исследование пустошей");
                        }    

                    }
                    Console.WriteLine("Неверный формат команды sd");

                }
                
            }
            else
            {
                Console.WriteLine("Неверная команда используйте help для вывода списка команд");
            }    

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

                ExecuteCommand(argv.Count(),argv);
                Thread.Sleep(1000);
            }
        }
    }
}
