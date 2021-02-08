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
using Excel = Microsoft.Office.Interop.Excel;

namespace Attendance_Application
{
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        //To avoid the windows form from flickering
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParams = base.CreateParams;
                handleParams.ExStyle |= 0x02000000;
                return handleParams;
            }
        }
        
        

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            //To get the exact date to input in cache file name
            var time = DateTime.Now;
            string formattedDate = time.ToString("dd-MM-yyyy");

            try
            {
                string row1;
                string row2;
                string row3;
                DataTable dt = new DataTable();

                //Default table Rows data indicator
                dt.Columns.Add("Names");
                dt.Columns.Add("Date");
                dt.Columns.Add("Time");

                FileStream scan = new FileStream(Application.UserAppDataPath + @"\cache for "+formattedDate+".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamReader readData = new StreamReader(scan);

                //To input first data to table
                row1 = readData.ReadLine();
                row2 = readData.ReadLine();
                row3 = readData.ReadLine();
                dt.Rows.Add(row1, row2, row3);

                //To input the rest of the data to table
                while (row1 != null)
                {
                    row1 = readData.ReadLine();
                    row2 = readData.ReadLine();
                    row3 = readData.ReadLine();
                    dt.Rows.Add(new object[] { row1, row2, row3 });

                }

                readData.Close();

                dataGridView1.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("There is no data to show!");

            }
        }

        public string fileName;

        //The Import button
        private void button1_Click(object sender, EventArgs e)
        {
            //To get the exact date to input in cache file name
            var time = DateTime.Now;
            string formattedDate = time.ToString("dd-MM-yyyy");

            try
            {
                //Data types to access table rows and column correctly
                string row1;
                string row2;
                string row3;
                int column = 2;
                DateTime dateTime = DateTime.Now;
                //The Excel file name
                fileName = @"\Attendance for " + dateTime.ToString("dd-MM-yyyy") + ".xls";

                FileStream scan = new FileStream(Application.UserAppDataPath + @"\cache for " + formattedDate + ".txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(scan);

                Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                xlWorkSheet.Cells[1, 1] = "Name";
                xlWorkSheet.Cells[1, 2] = "Date";
                xlWorkSheet.Cells[1, 3] = "Time";

                //To read Name, Date, and Time from cache.txt
                row1 = reader.ReadLine();
                row2 = reader.ReadLine();
                row3 = reader.ReadLine();
                //To transfer Name, Date, and Time from cache.txt into Excel file
                xlWorkSheet.Cells[column, 1] = row1;
                xlWorkSheet.Cells[column, 2] = row2;
                xlWorkSheet.Cells[column, 3] = row3;
                //To read and transfer the rest of the data from cache.txt into Excel file
                while (row1 != null)
                {
                    column++;
                    row1 = reader.ReadLine();
                    row2 = reader.ReadLine();
                    row3 = reader.ReadLine();
                    xlWorkSheet.Cells[column, 1] = row1;
                    xlWorkSheet.Cells[column, 2] = row2;
                    xlWorkSheet.Cells[column, 3] = row3;

                }
                //To close the file streamer
                reader.Close();
                scan.Close();

                xlWorkBook.SaveAs(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + fileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);

                MessageBox.Show("File Imported Successfully!");
            }
            catch
            {
                MessageBox.Show("Error! please Make sure to close the "+fileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //To go back to main screen
            this.Hide();
            var goBackToMainScreen = new MainForm();

            goBackToMainScreen.Show();
        }


        //The Save as button
        private void button2_Click(object sender, EventArgs e)
        {
            //To get the exact date to input in cache file name
            var time = DateTime.Now;
            string formattedDate = time.ToString("dd-MM-yyyy");

            //To save Excel file manually
            try
            {
                //Data types to access table rows and column correctly
                string row1;
                string row2;
                string row3;
                int column = 2;
                DateTime dateTime = DateTime.Now;
                //The Excel file name
                fileName = @"\Attendance for " + dateTime.ToString("dd-MM-yyyy") + ".xls";

                FileStream scan = new FileStream(Application.UserAppDataPath + @"\cache for " + formattedDate + ".txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(scan);

                Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                xlWorkSheet.Cells[1, 1] = "Name";
                xlWorkSheet.Cells[1, 2] = "Date";
                xlWorkSheet.Cells[1, 3] = "Time";

                //To read Name, Date, and Time from cache.txt
                row1 = reader.ReadLine();
                row2 = reader.ReadLine();
                row3 = reader.ReadLine();
                //To transfer Name, Date, and Time from cache.txt into Excel file
                xlWorkSheet.Cells[column, 1] = row1;
                xlWorkSheet.Cells[column, 2] = row2;
                xlWorkSheet.Cells[column, 3] = row3;
                //To read and transfer the rest of the data from cache.txt into Excel file
                while (row1 != null)
                {
                    column++;
                    row1 = reader.ReadLine();
                    row2 = reader.ReadLine();
                    row3 = reader.ReadLine();
                    xlWorkSheet.Cells[column, 1] = row1;
                    xlWorkSheet.Cells[column, 2] = row2;
                    xlWorkSheet.Cells[column, 3] = row3;

                }

                // Show save file dialog
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    xlWorkBook.SaveAs(saveFileDialog.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(false, misValue,misValue);
                    //To quit the excel app 
                    xlApp.Quit();

                    //To close the File streamer properly
                    scan.Close();
                    reader.Close();
                }
                xlApp.Quit();
                //To close the File streamer properly
                scan.Close();
                reader.Close();
                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);

            }
            catch
            {
                MessageBox.Show("Error! please Make sure to close the " + fileName);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
