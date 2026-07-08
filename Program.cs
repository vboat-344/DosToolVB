using System;
using System.IO;
using System.Net;
using System.Reflection;

[assembly: AssemblyTitle("DosToolVB")]
[assembly: AssemblyDescription("Многофункциональный сетевой тул")]
[assembly: AssemblyCompany("vboat")]
[assembly: AssemblyProduct("DosToolVB")]
[assembly: AssemblyCopyright("vboat, 2026")]
[assembly: AssemblyVersion("3.0.0.0")]
[assembly: AssemblyFileVersion("3.0.0.0")]

namespace DosToolVB
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Console.Title = "DosToolVB v3.0.0";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║          DosToolVB v3.0.0               ║");
            Console.WriteLine("║    Многофункциональный сетевой тул      ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝");
            Console.ResetColor();

            Settings.Load();

            if (!LicenseChecker.Check())
            {
                Console.WriteLine("Лицензия не найдена. Завершение работы.");
                Console.ReadKey();
                return;
            }

            if (!NetworkHelper.CheckInternet())
            {
                Console.WriteLine("Нет соединения с интернетом!");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔═══════════════════════════════════════════╗");
                Console.WriteLine("║          DosToolVB v3.0.0               ║");
                Console.WriteLine("║    Многофункциональный сетевой тул      ║");
                Console.WriteLine("╚═══════════════════════════════════════════╝");
                Console.ResetColor();

                Console.WriteLine("\n═══════════════════════════════════════════");
                Console.WriteLine("  1. Атака на цель");
                Console.WriteLine("  2. Настройки (размер пакета, скорость, длительность)");
                Console.WriteLine("  3. Показать логи");
                Console.WriteLine("  4. Проверить обновления");
                Console.WriteLine("  0. Выход");
                Console.Write("Выберите: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AttackMenu();
                        break;
                    case "2":
                        SettingsMenu();
                        break;
                    case "3":
                        Logger.ShowLogs();
                        break;
                    case "4":
                        Updater.CheckForUpdates();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        static void AttackMenu()
        {
            Console.Clear();
            Console.Write("Введите IP или домен: ");
            string target = Console.ReadLine();
            if (string.IsNullOrEmpty(target)) return;

            Console.Write("Протокол (ICMP/TCP/UDP): ");
            string protocol = Console.ReadLine().ToUpper();
            if (protocol != "ICMP" && protocol != "TCP" && protocol != "UDP")
            {
                Console.WriteLine("Неверный протокол. Использую ICMP.");
                protocol = "ICMP";
            }

            Console.Write("Порт (только для TCP/UDP, Enter для пропуска): ");
            string portInput = Console.ReadLine();
            int port = 0;
            bool usePort = int.TryParse(portInput, out port) && port > 0;

            if (!usePort && (protocol == "TCP" || protocol == "UDP"))
            {
                Console.WriteLine("Порт не указан. Использую стандартный: 80.");
                port = 80;
            }

            int packetsPerSecond = Settings.PacketsPerSecond;
            int packetSize = Settings.PacketSize;
            int durationSec = Settings.DurationSeconds;

            Console.WriteLine("\nНачинаю атаку на " + target + "...");
            AttackEngine.StartAttack(target, protocol, port, packetSize, packetsPerSecond, durationSec);
        }

        static void SettingsMenu()
        {
            Console.Clear();
            Console.WriteLine("Текущие настройки:");
            Console.WriteLine("  Размер пакета: " + Settings.PacketSize + " байт");
            Console.WriteLine("  Скорость: " + Settings.PacketsPerSecond + " пакетов/сек");
            string durationText;
            if (Settings.DurationSeconds == 0)
                durationText = "бесконечно";
            else
                durationText = Settings.DurationSeconds + " сек";
            Console.WriteLine("  Длительность: " + durationText);
            Console.Write("Изменить? (y/n): ");
            string answer = Console.ReadLine();
            if (answer != null && answer.ToLower() == "y")
            {
                Console.Write("Размер пакета (64/512/1024/1400/65500): ");
                string sizeInput = Console.ReadLine();
                int size;
                if (int.TryParse(sizeInput, out size) && size > 0)
                    Settings.PacketSize = size;

                Console.Write("Скорость (пакетов/сек): ");
                string speedInput = Console.ReadLine();
                int speed;
                if (int.TryParse(speedInput, out speed) && speed > 0)
                    Settings.PacketsPerSecond = speed;

                Console.Write("Длительность (сек, 0 = бесконечно): ");
                string durationInput = Console.ReadLine();
                int duration;
                if (int.TryParse(durationInput, out duration))
                    Settings.DurationSeconds = duration;

                Settings.Save();
                Console.WriteLine("Настройки сохранены.");
                Console.ReadKey();
            }
        }
    }

    static class Settings
    {
        public static int PacketSize = 1400;
        public static int PacketsPerSecond = 10;
        public static int DurationSeconds = 0;

        private static string SettingsFile = "settings.ini";

        public static void Load()
        {
            if (!File.Exists(SettingsFile)) return;

            try
            {
                string[] lines = File.ReadAllLines(SettingsFile);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith(";")) continue;
                    string[] parts = line.Split('=');
                    if (parts.Length != 2) continue;

                    string key = parts[0].Trim();
                    string value = parts[1].Trim();

                    switch (key)
                    {
                        case "PacketSize":
                            int.TryParse(value, out PacketSize);
                            break;
                        case "PacketsPerSecond":
                            int.TryParse(value, out PacketsPerSecond);
                            break;
                        case "DurationSeconds":
                            int.TryParse(value, out DurationSeconds);
                            break;
                    }
                }
            }
            catch { }
        }

        public static void Save()
        {
            try
            {
                string content = 
                    "; Настройки DosToolVB v3.0.0\n" +
                    "PacketSize=" + PacketSize + "\n" +
                    "PacketsPerSecond=" + PacketsPerSecond + "\n" +
                    "DurationSeconds=" + DurationSeconds + "\n";
                File.WriteAllText(SettingsFile, content);
            }
            catch { }
        }
    }
}