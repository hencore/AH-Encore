using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Diagnostics;

namespace auto_h_encore {
    public static class Utility {
        private static WebClient web = new WebClient();
        private static HttpClient http = new HttpClient();

        public static string GetEncKey(string aid) {
            try {
                string page = http.GetStringAsync(Reference.url_cma + aid).Result;
                return page.Substring(page.Length - 65, 64);
            } catch (Exception) {
                MessageBox.Show("Ошибка получение ключа дешифровки CMA");
                return "";
            }
        }

        public static void DownloadFile(Form1 form, bool incrementProgress, string url, string output) {
            while (true)
                try {
                    form.info("Загрузка " + output.Replace('/', '\\').Split('\\').Last());
                    web.DownloadFile(url, output);
                    form.info("     Готово!");
                    if (incrementProgress) form.incrementProgress();
                    return;
                } catch (WebException ex) {
                    if (MessageBox.Show("Ошибка загрузки файла " + url + "\r\n\r\nУбиедитесь, что у вас есть интернет-соединение и попробуйте еще раз..", "Ошибка", MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                        throw ex;
                }
        }

        public static void ExtractFile(Form1 form, bool incrementProgress, string filePath, string outputDirectory) {
            try {
                form.info("Распаковка " + filePath.Replace('/', '\\').Split('\\').Last());
                ZipFile.ExtractToDirectory(filePath, outputDirectory);
                form.info("      Готово!");
                if (incrementProgress) form.incrementProgress();
                return;
            } catch (DirectoryNotFoundException ex) {
                MessageBox.Show("Похоже, созданые файлы исчезли. Пожалуйста, перезапустите приложение и не трогайте файлы в его директориях");
                throw ex;
            } catch (UnauthorizedAccessException ex) {
                MessageBox.Show("Приложение не имеет прав для записи в эту директорию. Пожалуйста, переместите его в ту, в которой у вас такие права будут, или перезапустите приложение от имени администратора");
                throw ex;
            } catch (FileNotFoundException ex) {
                MessageBox.Show("Похоже, созданые файлы исчезли. Пожалуйста, перезапустите приложение и не трогайте файлы в его директориях");
                throw ex;
            } catch (IOException ex) {
                MessageBox.Show("Что-то пошло не так:\r\n\r\n" + ex.Message);
                throw ex;
            } catch (InvalidDataException ex) {
                MessageBox.Show("Ошибка загрузки. Пожалуйста, перезапустите приложение и убедитесь, что ваше интернет-соединение стабильно");
                throw ex;
            }

        }

        public static void PackageFiles(Form1 form, bool incrementProgress, string workingDirectory, string encryptionKey, string type) {
            try {
                form.info("Упаковка h-encore " + type + " с использованием psvimgtools...");
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.WorkingDirectory = workingDirectory;
                psi.FileName = Reference.path_psvimgtools + "psvimg-create.exe";
                psi.Arguments = "-n " + type + " -K " + encryptionKey + " " + type + " PCSG90096/" + type;
                Process process = Process.Start(psi);
                process.WaitForExit();
                form.info("     Готово!");
                if (incrementProgress) form.incrementProgress();
                return;
            } catch (FileNotFoundException ex) {
                MessageBox.Show("Похоже, созданые файлы исчезли. Пожалуйста, перезапустите приложение и не трогайте файлы в его директориях");
                throw ex;
            }
        }
    }
}
