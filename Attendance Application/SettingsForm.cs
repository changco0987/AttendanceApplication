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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }
        public static string passPath = @"\Pass.txt";
        public static string appKey = "";

        public void checkKey()
        {
            try
            {
                //check if there is existing file and it have a data inside
                FileStream scanToRead = new FileStream(Application.UserAppDataPath+passPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamReader readFile = new StreamReader(scanToRead);
                appKey = readFile.ReadLine();
                readFile.Close();
                scanToRead.Close();

            }
            catch
            {
                writeAkey();
            }
        }

        public static void writeAkey()
        {
            //if the file doesn't have any data inside it writes a default value
            FileStream scanToWrite = new FileStream(Application.UserAppDataPath + passPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter writeToFile = new StreamWriter(scanToWrite);

            string source = "012345";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
                byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                writeToFile.Write(hash);
                writeToFile.Close();

                FileStream scanToRead = new FileStream(Application.UserAppDataPath + passPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamReader readFile = new StreamReader(scanToRead);
                appKey = readFile.ReadLine();
                readFile.Close();
                scanToWrite.Close();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //To return to Main Menu
            this.Hide();
            var goBack = new MainForm();

            goBack.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var openResetBox = new resetBoxcs();

            openResetBox.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            //To get the user input
            string key1 = currentKey.Text;
            string keyVerifier = verifyCurrentKey.Text;
            //Check if the current key is inputted correctly
            if (key1.Equals("") || keyVerifier.Equals(""))
            {
                MessageBox.Show("Please Enter your current key!");
            }
            else if (key1.Equals(keyVerifier))
            {
                //making a key1 variable a hash value equivalent
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    //From String to byte array
                    byte[] sourceBytes = Encoding.UTF8.GetBytes(key1);
                    byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                    string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                    //To get the appKey hash value
                    checkKey();

                    //Checking if the hash value in the file equals to a hash value from key1
                    if (appKey.Equals(hash))
                    {
                        //Converting new key into hash equivalent
                        string newUserKey = newKey.Text;
                        sourceBytes = Encoding.UTF8.GetBytes(newUserKey);
                        hashBytes = sha256Hash.ComputeHash(sourceBytes);
                        hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                        //Saving new key hash value into file
                        FileStream scan = new FileStream(Application.UserAppDataPath + passPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        StreamWriter writeToFile = new StreamWriter(scan);
                        writeToFile.Write(hash);
                        writeToFile.Close();
                        scan.Close();
                        MessageBox.Show("Passkey has been changed successfully!");

                        //To go back in the main screen and exit to settings
                        this.Hide();
                        var goBack = new MainForm();

                        goBack.Show();
                    }
                    else
                    {
                        MessageBox.Show("Your passkey is incorrect!");
                    }
                }

            }
            else
            {
                MessageBox.Show("Your passkey doesn't match!");
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }

        private void currentKey_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
