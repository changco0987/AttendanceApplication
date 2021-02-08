using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace Attendance_Application
{
    public partial class resetBoxcs : Form
    {
        public resetBoxcs()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            //to cancel the operation and go back to settings
            this.Hide();
            var goBack = new SettingsForm();

            goBack.Show();
        }

        private void resetKey_TextChanged(object sender, EventArgs e)
        {

        }

        public string passPath = @"\reset.txt";
        public static string appResetKey = "";

        public void checkKey()
        {
            try
            {
                //check if there is existing file and it have a data inside
                FileStream scanToRead = new FileStream(Application.UserAppDataPath + passPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamReader readFile = new StreamReader(scanToRead);
                appResetKey = readFile.ReadLine();
                readFile.Close();
                scanToRead.Close();

                while (appResetKey==null)
                {
                    writeAkey();
                }

            }
            catch
            {
                writeAkey();
            }
        }


        private void writeAkey()
        {
            //if the file doesn't have any data inside it writes a default value
            FileStream scanWriter = new FileStream(Application.UserAppDataPath + passPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter writeFile = new StreamWriter(scanWriter);

            string source = "PNB123456";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
                byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                writeFile.Write(hash);
                writeFile.Close();
                appResetKey = hash;
                scanWriter.Close();
            }

 
        }

        private void enterBtn_Click(object sender, EventArgs e)
        {
            string source = resetKey.Text;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
                byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                checkKey();
                if (appResetKey.Equals(hash))
                {
                    SettingsForm.writeAkey();
                    MessageBox.Show("Password reset successfully!");

                    this.Hide();
                    var goBack = new SettingsForm();
                    
                    goBack.Show();
                

                }
                else
                {
                    MessageBox.Show("Reset Failed");
                }
            }
        }
    }
}
