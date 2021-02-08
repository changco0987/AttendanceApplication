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
using System.Runtime.InteropServices;


namespace Attendance_Application
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var time = DateTime.Now;
            FileStream scan = new FileStream(Application.UserAppDataPath + cacheFile + time.ToString("dd-MM-yyyy") + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            scan.Close();
        }

        private void adminBtn_Click(object sender, EventArgs e)
        {   
            //To open Admin Tab
            this.Hide();
            var openForm2 = new Form2();

            openForm2.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //To open Settings Tab
            this.Hide();
            var openSettings = new SettingsForm();

            openSettings.Show();
        }

        string cacheFile = @"\cache for ";
        int inputCount = 0;


        private void cache()
        {
            try
            {

                inputCount++;
                //User inputted name
                string inputName = enterName.Text;
                //User date and time that he/she Time in
                var time = DateTime.Now;
                string formattedDate = time.ToString("dd/MM/yyyy");
                string formattedTime = time.ToString("hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                FileStream scan = new FileStream(Application.UserAppDataPath + cacheFile+time.ToString("dd-MM-yyyy")+".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter writer = new StreamWriter(scan);
                StreamReader reader = new StreamReader(scan);
                using (writer)
                {
                    /*
                     * To check the last empty line of the file
                     * in order to continue to write in that last empty line
                     */
                    string line = reader.ReadLine();

                    while (line != null)
                    {
                        line = reader.ReadLine();
                    }
                    //inputted into cache file if the blank section find
                    writer.WriteLine(char.ToUpper(inputName[0]) + inputName.Substring(1));
                    writer.WriteLine(formattedDate);
                    writer.WriteLine(formattedTime);
                    MessageBox.Show("Time in: " + formattedTime);

                }
            }
            catch
            {
                MessageBox.Show("Cannot save Empty input!");
            }

        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            var time = DateTime.Now;
            string formattedTime = time.ToString("hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
            cache();
            enterName.Text = "";

        }
    }
}
