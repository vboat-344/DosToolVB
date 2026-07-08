using System;
using System.IO;

namespace DosToolVB
{
    static class LicenseChecker
    {
        private static string GetExpectedLicenseText()
        {
            return @"ЛИЦЕНЗИОННОЕ СОГЛАШЕНИЕ (vboat, 2026)
1. ПРАВА ПОЛЬЗОВАТЕЛЯ
1.1 Вы можете свободно использовать программу в личных целях.
1.2 Вы можете распространять программу БЕЗ ИЗМЕНЕНИЙ, сохраняя этот файл лицензии.
1.3 Вы обязаны сохранять имя автора (vboat) и ссылку на оригинальный репозиторий в любых копиях программы.
2. ЗАПРЕЩЕНО
2.1 Продажа программы или её частей.
2.2 Модификация, декомпиляция, обратная разработка (reverse engineering) исходного кода.
2.3 Использование программы для атак на серверы без согласия владельцев (это не относится к вашим личным тестам).
3. ОТВЕТСТВЕННОСТЬ
3.1 Программа предоставляется ""КАК ЕСТЬ"" (AS-IS).
3.2 Автор не несёт ответственности за любые убытки, прямые или косвенные, возникшие в результате использования программы.
4. КОНФИДЕНЦИАЛЬНОСТЬ
4.1 Программа НЕ собирает, НЕ хранит и НЕ передаёт ваши личные данные третьим лицам.
5. АВТОРСКИЕ ПРАВА
5.1 Иконка приложения взята с сайта: https://www.pcgamingwiki.com/wiki/File:Generic_DOS_icon.svg
5.2 Автор программы: vboat (GitHub: github.com/vboat-344; Telegram: t.me/vboat_344).
6. ИЗМЕНЕНИЯ
6.1 Автор оставляет за собой право изменять условия лицензии с уведомлением пользователей через репозиторий.
7. Устанавливая и/или используя программу, вы подтверждаете, что полностью ознакомлены с условиями данного соглашения и принимаете их безоговорочно.";
        }

        public static bool Check()
        {
            bool hasRtf = File.Exists("license.rtf");
            bool hasTxt = File.Exists("license.txt");

            if (!hasRtf && !hasTxt)
            {
                Console.WriteLine("ОШИБКА: Файл лицензии (license.rtf или license.txt) не найден!");
                return false;
            }

            string actualText = "";

            if (hasRtf)
            {
                try
                {
                    actualText = File.ReadAllText("license.rtf", System.Text.Encoding.UTF8).Trim();
                }
                catch
                {
                    actualText = "";
                }
            }
            else if (hasTxt)
            {
                try
                {
                    actualText = File.ReadAllText("license.txt", System.Text.Encoding.UTF8).Trim();
                }
                catch
                {
                    actualText = "";
                }
            }

            if (string.IsNullOrEmpty(actualText))
            {
                CreateLicenseFile();
                return true;
            }

            string expectedText = GetExpectedLicenseText().Trim();

            string normalizedActual = actualText.Replace("\r\n", "\n").Replace("\r", "\n");
            string normalizedExpected = expectedText.Replace("\r\n", "\n").Replace("\r", "\n");

            if (string.Equals(normalizedActual, normalizedExpected, StringComparison.Ordinal))
            {
                return true;
            }

            Console.WriteLine("Файл лицензии изменён или повреждён. Пересоздаю...");
            CreateLicenseFile();
            return true;
        }

        private static void CreateLicenseFile()
        {
            try
            {
                if (File.Exists("license.rtf"))
                {
                    try { File.Delete("license.rtf"); } catch { }
                }
                if (File.Exists("license.txt"))
                {
                    try { File.Delete("license.txt"); } catch { }
                }

                string expectedText = GetExpectedLicenseText();
                File.WriteAllText("license.txt", expectedText, System.Text.Encoding.UTF8);
                Console.WriteLine("Файл лицензии создан: license.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при создании файла лицензии: " + ex.Message);
            }
        }
    }
}