﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Net;
using System.Net.Sockets;

namespace Vault13Server
{
    class Program
    {
        private static Mutex mutex = new Mutex();
        
        static void UpdateVaultInfoDelegate()
        {
            if (vault13 == null)
                return;

            while (true)
            {
                if (mutex.WaitOne(1000))
                {
                    vault13.NotifyVault();
                }
                mutex.ReleaseMutex();

                Thread.Sleep(vault13.UpdateInfoPeriodSec * 1000);
            }

        }

        static Vault vault13 = new Vault(20);

        static IPAddress serverIp = new IPAddress(new byte[] { 127, 0, 0, 1 });
        static int serverPort = 8000;

        static IPEndPoint serverIpEndPoint = new IPEndPoint(serverIp, serverPort);

        static Socket listenSocket = new Socket(AddressFamily.InterNetwork,
                                                SocketType.Stream,
                                                ProtocolType.Tcp);

        static Task InformationUpdateTask;

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
                            reply = "Данный житель не может быть отправлен на исследование. Бюджет убежища сейчас " + vault13.VaultBudget;
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
                        if (dwellersIndex >= 0)
                        {
                            string status = vault13.dwellersList[dwellersIndex].GetStringStatus();
                            reply = String.Format("{0:s} {1:s}: ", dwellerToWastelandName, status);
                        }
                        else
                        {
                            reply = String.Format("В убежище нет жителя с именем {0:s}: ", dwellerToWastelandName);
                        }

                    }
                    else
                    {
                        reply = "Неверный формат команды howdy";
                    }

                }
                if (argv[0] == "muneh")
                {
                    if (argc == 1)
                    {
                        reply = "Бюджет убежища: " + vault13.VaultBudget.ToString();

                    }
                    else
                    {
                        reply = "Неверный формат команды muneh";
                    }

                }
                if (argv[0] == "letin")
                {
                    if (argc == 2 && argv[1] == "all")
                    {
                        reply = vault13.LetAllDwellersIn() ? "все жители вошли в убежище" : "некому открывать дверь";
                    }
                    else if (argc == 2)
                    {
                        if (vault13.LetDwellerIn(argv[1]))
                            reply = "Житель " + argv[1] + " попал в убежище";

                    }
                    else
                    {
                        reply = "Неверный формат команды letin";
                    }

                }
            }
            return reply;
        }

        static void Main(string[] args)
        {
            VaultTecServerProtocol vaultTecServerProtocol = new VaultTecServerProtocol();

            InformationUpdateTask = new Task(UpdateVaultInfoDelegate);
            InformationUpdateTask.Start();

            listenSocket.Bind(serverIpEndPoint);
            listenSocket.Listen(100);

            while (true)
            {
                try
                {
                    Socket connectedSocket = listenSocket.Accept();

                    int rxBytesCount = 0;
                    byte[] rxTxBuf = new byte[1024];

                    rxBytesCount = connectedSocket.Receive(rxTxBuf);
                    if(rxBytesCount > 0)
                    {
                        string clientsRequest = Encoding.UTF8.GetString(rxTxBuf, 0, rxBytesCount);
                        Console.WriteLine(">> " + clientsRequest);

                        string[] cmdArgvs = VaultTecProtocolParser.ParseCommandString(clientsRequest);

                        string reply = "";
                        if (mutex.WaitOne(1000))
                        {
                            reply = ExecuteCommand(cmdArgvs.Count(), cmdArgvs);
                        }
                        mutex.ReleaseMutex();

                        Console.WriteLine("<< " + reply);

                        rxTxBuf = Encoding.UTF8.GetBytes(reply);//по идее нужна проверка размера

                        connectedSocket.Send(rxTxBuf);
                    }

                    
                    connectedSocket.Shutdown(SocketShutdown.Both);
                    connectedSocket.Close();
                }
                catch
                {

                }
            }        
        }
    }
}
