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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string passPath = @"\Pass.txt";
        public static string appKey = "";
        private void button1_Click(object sender, EventArgs e)
        {
            FileStream scanToRead = new FileStream(Application.UserAppDataPath + passPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader readFile = new StreamReader(scanToRead);
            appKey = readFile.ReadLine();
            readFile.Close();

            while (appKey==null)
            {
                SettingsForm.writeAkey();
                scanToRead = new FileStream(Application.UserAppDataPath + passPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                readFile = new StreamReader(scanToRead);
                appKey = readFile.ReadLine();
                readFile.Close();
            }

            scanToRead.Close();

            string source = passkeyInput.Text;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
                byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                if (appKey.Equals(hash))
                {


                    //To hide this tab
                    this.Hide();
                    var openAdminPanel = new AdminPanel();

                    openAdminPanel.Show();

                }
                else
                {
                    MessageBox.Show("Your passkey is incorrect!");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var goBack = new MainForm();

            goBack.Show();
        }
    }
}
