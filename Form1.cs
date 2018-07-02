using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using Microsoft.Win32;

namespace auto_h_encore {
    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
            VerifyUserInfo();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            lblVersion.Text = "ah-encore by nikolay2002 and noahc3  v." + Reference.version;
            lblInfo.Text = "FAQ: \r\n1. Установите и откройте QCMA (щелкнув на эти строчки)\r\n2. Подключите Вашу PS Vita к PC и запустите на ней \"Управление данными\"\r\n3. Выберите \"Скопировать данные\", после Компьютер -> Система PS Vita - Приложения - PS Vita\r\n4. Теперь в этой программе нажмите на \"Проверить наличие QCMA\" и после - на \"Start\"\r\n5. Дождитесь окончания работы программы, после на вашей PS Vita нажмите стрелочку назад и снова откройте вкладку \"PS Vita\", поставьте галочку на H-Encore и нажмите Копировать\r\n7.Теперь откройте H-Encore на PS Vita, после чего установите HENkaku и VitaShell\r\n8. После каждой перезагрузки PS Vita открывайте H-Encore и нажимайте Exit";
        }

        private void VerifyUserInfo() {
            Reference.app_PATH = (string)Registry.CurrentUser.OpenSubKey("Software\\codestation\\qcma").GetValue("appsPath", "");
            if (Directory.Exists(Reference.app_PATH + "\\APP\\"))
            {
                set_AID();
                info("QCMA найден, можете начинать установку! (начинайте с пункта 4)");
                btnStart.Enabled = true;
            }
            else btnStart.Enabled = false;
        }

        private void generateDirectories(string AID) {
            info("Generating working directories...");
            if (Directory.Exists(Reference.path_data)) Directory.Delete(Reference.path_data, true);
            if (Directory.Exists(Reference.app_PATH + "\\APP\\" + AID + "\\PCSG90096\\")) {
                if (MessageBox.Show("Вы должны удалить старый бекап BitterSmile из вашей QCMA директории. Удалить?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    Directory.Delete(Reference.app_PATH + "\\APP\\" + Reference.AID + "\\PCSG90096\\", true);
                } else {
                    throw new IOException("Directory Already Exists");
                }
            }
            Directory.CreateDirectory(Reference.path_data);
            Directory.CreateDirectory(Reference.path_hencore);
            Directory.CreateDirectory(Reference.path_psvimgtools);
            Directory.CreateDirectory(Reference.path_pkg2zip);
            Directory.CreateDirectory(Reference.path_downloads);
            incrementProgress();
        }

        private void downloadFiles() {
            Utility.DownloadFile(this, true, Reference.url_hencore, Reference.path_downloads + "hencore.zip");
            Utility.ExtractFile(this, true, Reference.path_downloads + "hencore.zip", Reference.path_hencore);
            
            Utility.DownloadFile(this, true, Reference.url_psvimgtools, Reference.path_downloads + "psvimgtools.zip");
            Utility.ExtractFile(this, true, Reference.path_downloads + "psvimgtools.zip", Reference.path_psvimgtools);
            
            Utility.DownloadFile(this, true, Reference.url_pkg2zip, Reference.path_downloads + "pkg2zip.zip");
            Utility.ExtractFile(this, true, Reference.path_downloads + "pkg2zip.zip", Reference.path_pkg2zip);
            
            Utility.DownloadFile(this, true, Reference.url_bittersmile, Reference.path_downloads + "bittersmile.pkg");
        }

        private void PackageHencore(string encKey) {
            Utility.PackageFiles(this, true, Reference.path_hencore + "h-encore\\", encKey, "app");
            Utility.PackageFiles(this, true, Reference.path_hencore + "h-encore\\", encKey, "appmeta");
            Utility.PackageFiles(this, true, Reference.path_hencore + "h-encore\\", encKey, "license");
            Utility.PackageFiles(this, true, Reference.path_hencore + "h-encore\\", encKey, "savedata");
        }


        private void toggleControls(bool state) {
            if (InvokeRequired) {
                Invoke(new Action(() => {
                    btnStart.Enabled = state;                   
                    barProgress.Value = 0;
                }));
            } else {
                btnStart.Enabled = state;
                barProgress.Value = 0;
            }
        }
        
        private void btnStart_Click(object sender, EventArgs e) {
            chk_QCMA.Enabled = false;
            toggleControls(false);

            //run code on new thread to keep UI responsive
            Task.Factory.StartNew(new Action(() => {

                try {
                    generateDirectories(Reference.AID);
                    downloadFiles();
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                    toggleControls(true);
                    return;
                }
                
                try {
                    info("Распаковка BitterSmile с помощью pkg2zip...");
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.WorkingDirectory = Reference.path_pkg2zip;
                    psi.Arguments = "-x \"" + Reference.path_downloads + "bittersmile.pkg\"";
                    psi.FileName = Reference.path_pkg2zip + "pkg2zip.exe";
                    Process process = Process.Start(psi);
                    process.WaitForExit();
                    info("      Done!");
                    incrementProgress();
                } catch (FileNotFoundException) {
                    MessageBox.Show("Похоже, скачанные файлы исчезли. Пожалуйста, перезапустите приложение и не трогайте файлы в его директориях");
                    toggleControls(true);
                    return;
                }
                
                try {
                    foreach (string k in Directory.EnumerateFiles(Reference.path_pkg2zip + "app\\PCSG90096\\")) {
                        info("Перемещение " + k.Split('\\').Last() + " в рабочую директорию h-encore...");
                        FileSystem.MoveFile(k, Reference.path_hencore + "\\h-encore\\app\\ux0_temp_game_PCSG90096_app_PCSG90096\\" + k.Split('\\').Last());
                    }
                } catch (DirectoryNotFoundException) {
                    MessageBox.Show("Похоже, созданые файлы исчезли. Пожалуйста, перезапустите приложение и не трогайте файлы в его директориях");
                    toggleControls(true);
                    return;
                } catch (UnauthorizedAccessException) {
                    MessageBox.Show("Приложение не имеет прав для записи в эту директорию. Пожалуйста, переместите его в ту, в которой у вас такие права будут, или перезапустите приложение от имени администратора");
                    toggleControls(true);
                    return;
                } catch (IOException ex) {
                    MessageBox.Show("Что-то пошло не так:\r\n\r\n" + ex.Message);
                    toggleControls(true);
                    return;
                }

                try {
                    foreach (string k in Directory.EnumerateDirectories(Reference.path_pkg2zip + "app\\PCSG90096\\")) {
                        info("Перемещение " + k.Split('\\').Last() + " в рабочую директорию h-encore...");
                        FileSystem.MoveDirectory(k, Reference.path_hencore + "\\h-encore\\app\\ux0_temp_game_PCSG90096_app_PCSG90096\\" + k.Split('\\').Last());
                    }
                } catch (DirectoryNotFoundException) {
                    MessageBox.Show("Похоже, созданые файлы исчезли. Пожалуйста, перезапустите приложение и не трогайте файлы в его директориях");
                    toggleControls(true);
                    return;
                } catch (UnauthorizedAccessException) {
                    MessageBox.Show("Приложение не имеет прав для записи в эту директорию. Пожалуйста, переместите его в ту, в которой у вас такие права будут, или перезапустите приложение от имени администратора");
                    toggleControls(true);
                    return;
                } catch (IOException ex) {
                    MessageBox.Show("Что-то пошло не так:\r\n\r\n" + ex.Message);
                    toggleControls(true);
                    return;
                }

                incrementProgress();

                try {
                    info("Перемещение файла лицензии...");
                    FileSystem.MoveFile(Reference.path_hencore + "\\h-encore\\app\\ux0_temp_game_PCSG90096_app_PCSG90096\\sce_sys\\package\\temp.bin", Reference.path_hencore + "\\h-encore\\license\\ux0_temp_game_PCSG90096_license_app_PCSG90096\\6488b73b912a753a492e2714e9b38bc7.rif");
                    info("      Done!");
                    incrementProgress();
                } catch (DirectoryNotFoundException) {
                    MessageBox.Show("Похоже, созданые файлы исчезли. Пожалуйста, перезапустите приложение и не трогайте файлы в его директориях");
                    toggleControls(true);
                    return;
                } catch (UnauthorizedAccessException) {
                    MessageBox.Show("Приложение не имеет прав для записи в эту директорию. Пожалуйста, переместите его в ту, в которой у вас такие права будут, или перезапустите приложение от имени администратора");
                    toggleControls(true);
                    return;
                } catch (IOException ex) {
                    MessageBox.Show("Что-то пошло не так:\r\n\r\n" + ex.Message);
                    toggleControls(true);
                    return;
                }

                string encKey;

                try {
                    info("Получение ключа расшифровки CMA с использованием вашего AID " + Reference.AID);
                    encKey = Utility.GetEncKey(Reference.AID);
                    if (encKey.Length != 64) return;
                    info("Ключ получен " + encKey);
                    incrementProgress();
                } catch (Exception) {
                    toggleControls(true);
                    return;
                }
                
                try {
                    PackageHencore(encKey);
                } catch (Exception) {
                    toggleControls(true);
                    return;
                }

                try {
                    info("Перемещение h-encore файлов в QCMA APP директорию...\r\n");
                    FileSystem.MoveDirectory(Reference.path_hencore + "h-encore\\PCSG90096\\", Reference.app_PATH + "\\APP\\" + Reference.AID + "\\PCSG90096\\");
                    incrementProgress();
                    info("Готово!! ");
                } catch (DirectoryNotFoundException) {
                    MessageBox.Show("Ваша директория QCMA исчезла! Убедитесь, что она не была случайно удалена!");
                    toggleControls(true);
                    return;
                } catch (UnauthorizedAccessException) {
                    MessageBox.Show("Приложение не имеет прав для записи в директорию QCMA. Пожалуйста, перезапустите приложение от имени администратора");
                    toggleControls(true);
                    return;
                } catch (IOException ex) {
                    MessageBox.Show("Что-то пошло не так:\r\n\r\n" + ex.Message);
                    toggleControls(true);
                    return;
                }


                toggleControls(true);
            }));
            
        }

        public void incrementProgress() {
            if (InvokeRequired) Invoke(new Action(() => barProgress.Value++));
            else barProgress.Value++;
        }

        public void info(string message) {
            if (InvokeRequired) Invoke(new Action(() => txtLog.AppendText("[" + DateTime.Now.ToLongTimeString() + "] " + message + "\r\n")));
            else txtLog.AppendText("[" + DateTime.Now.ToLongTimeString() + "] " + message + "\r\n");

        }

        private void set_AID()
        {
            DirectoryInfo inf = new DirectoryInfo(Reference.app_PATH + "\\APP\\");
            DirectoryInfo[] dirs = inf.GetDirectories();
            if (dirs.Length != 0 && dirs[0].Name.Length == 16)
            {
                Reference.AID = dirs[0].Name;
            }
        }

        private void chk_QCMA_Click(object sender, EventArgs e)
        {
            VerifyUserInfo();
        }

        private void bt_About_Click(object sender, EventArgs e)
        {
            Form ab = new About();
            ab.Show();
        }

        private void lblInfo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://codestation.github.io/qcma/");
        }
    }
}
