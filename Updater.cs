using System;
using System.Net;

namespace DosToolVB
{
    static class Updater
    {
        public static void CheckForUpdates()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                Console.WriteLine("Проверяю обновления...");
                using (WebClient client = new WebClient())
                {
                    string latestVersion = client.DownloadString("https://raw.githubusercontent.com/vboat-344/DosToolVB/main/version.txt").Trim();
                    string currentVersion = "3.0.0";

                    if (latestVersion != currentVersion)
                    {
                        Console.WriteLine("Доступна новая версия: " + latestVersion);
                        Console.Write("Скачать? (y/n): ");
                        string answer = Console.ReadLine();
                        if (answer != null && answer.ToLower() == "y")
                        {
                            client.DownloadFile("https://github.com/vboat-344/DosToolVB/releases/latest/download/DosToolVB_v3.0.0_setup.exe", "DosToolVB_update.exe");
                            Console.WriteLine("Обновление загружено. Запустите файл DosToolVB_update.exe");
                        }
                    }
                    else
                    {
                        Console.WriteLine("У вас последняя версия.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при проверке обновлений: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
                Console.ReadKey();
            }
        }
    }
}