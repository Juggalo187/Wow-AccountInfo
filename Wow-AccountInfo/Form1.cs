using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Wow_Launcher
{
    public partial class Form1 : Form
    {

        public object TextBox1 { get; private set; }

        public Form1()
        {
            InitializeComponent();
            if (!Directory.Exists(@"Data\"))
            { Directory.CreateDirectory(@"Data\"); }

            textBox3.PasswordChar = '*';
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void ServerName_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
          
            var Invalidexists = "false";
            string ServerName = "Server=" + textBox1.Text;
            string AccName = "Account=" + textBox2.Text;
            string Password = "Password=" + textBox3.Text;
            string wowpaths;


            string fileName = @"Data\" + textBox1.Text + ".txt";

            


            var pn = fileName;
            var badStrings = new List<string>()
                 {
                 "<",">","?","*","#","/",":","\\","`","|","'/'"
                 };

            char[] invalidPathChars = Path.GetInvalidPathChars();
            foreach (var badString in invalidPathChars)
            {
                if (pn.Contains(badString))
                {
                    Invalidexists = "true";

                    DialogResult g;
                    g = MessageBox.Show("Error Invalid Character " + badString + " in Server Name.", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (g == DialogResult.OK)
                    {

                    }

                    else
                    {
                        Invalidexists = "false";
                    }

                }
            }


            if (Invalidexists == "false")
            {
                var openFileDialog = new OpenFileDialog();
                wowpaths = openFileDialog.FileName;

                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "exe files (*.exe)|*.exe";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                DialogResult s;
                s = MessageBox.Show("Choose WoW.exe Location", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (s == DialogResult.OK)
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        wowpaths = openFileDialog.FileName;
                    }
                }


                string wowpath2 = "wowpath=" + @"" + wowpaths;
                string[] lines = { ServerName, AccName, Password, wowpath2 };
            using (StreamWriter outputFile = new StreamWriter(fileName, true))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }

                string[] textFiles2 = Directory.GetFiles(@"Data\", "*.txt");
                foreach (string file in textFiles2)
                {
                    string filePath2 = file;
                    string directoryPath = Path.GetDirectoryName(filePath2);
                    string[] lines2 = File.ReadAllLines(file);
                    for (int i = 0; i < lines2.Length; i++)
                    {
                        // check if the line contains the search string 
                        if (lines[i].Contains("wowpath="))
                        {
                            // modify the line as desired  
                            string data1 = lines2[i].Substring(8);
                            string empty = "";
                            if (data1 == empty)
                            {
                                File.Delete(filePath2);
                            }
                            break;
                        }



                    }

                }   
            }


            comboBox1.BeginUpdate();
            comboBox1.Items.Clear();
            string[] textFiles = Directory.GetFiles(@"Data\", "*.txt");
            foreach (string file in textFiles)
            {
                // Remove the directory from the string
                string filename = file.Substring(file.LastIndexOf(@"\") + 1);
                // Remove the extension from the filename
                string name = filename.Substring(0, filename.LastIndexOf(@"."));
                // Add the name to the combo box
                comboBox1.Items.Add(name);
            }

            comboBox1.EndUpdate();
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;

            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filePath = @"Data\" + comboBox1.Text + ".txt";

            // read the contents of the file into a string array 
            string[] lines = File.ReadAllLines(filePath);

            // loop through each line in the array 
            for (int i = 0; i < lines.Length; i++)
            {
                // check if the line contains the search string 
                if (lines[i].Contains("Server="))
                {
                    // modify the line as desired  
                    string data1 = lines[i].Substring(7);
                    textBox1.Text = data1;
                    break;
                }

            }

            for (int i = 0; i < lines.Length; i++)
            {
                // check if the line contains the search string 
                if (lines[i].Contains("Account="))
                {
                    // modify the line as desired 
                    string data2 = lines[i].Substring(8);
                    textBox2.Text = data2;
                    break;
                }


            }

            for (int i = 0; i < lines.Length; i++)
            {
                // check if the line contains the search string 
                if (lines[i].Contains("Password="))
                {
                    // modify the line as desired 
                    string data3 = lines[i].Substring(9);
                    textBox3.Text = data3;
                    break;
                }


            }

            

            button2.Visible = true;
            button3.Visible = true;
            button5.Visible = true;

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;

            if (comboBox1.SelectedText == "")
            {
                //button1.Visible = true;
                button4.Visible = true;
            }


            string[] textFiles = Directory.GetFiles(@"Data\", "*.txt");
            foreach (string file in textFiles)
            {
                // Remove the directory from the string
                string filename = file.Substring(file.LastIndexOf(@"\") + 1);
                // Remove the extension from the filename
                string name = filename.Substring(0, filename.LastIndexOf(@"."));
                // Add the name to the combo box
                comboBox1.Items.Add(name);
            }
        }

        private void AccountName_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(textBox3.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            string filePath = @"Data\" + comboBox1.Text + ".txt";
            var vfile = new FileInfo(filePath);

            if (vfile.Exists)
            {
                File.Delete(filePath);
            }
            comboBox1.BeginUpdate();
            comboBox1.Items.Clear();
            string[] textFiles = Directory.GetFiles(@"Data\", "*.txt");
            foreach (string file in textFiles)
            {
                // Remove the directory from the string
                string filename = file.Substring(file.LastIndexOf(@"\") + 1);
                // Remove the extension from the filename
                string name = filename.Substring(0, filename.LastIndexOf(@"."));
                // Add the name to the combo box
                comboBox1.Items.Add(name);
            }

            comboBox1.EndUpdate();
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string filePath = @"Data\" + comboBox1.Text + ".txt";
            string filename = Path.GetFileName(filePath);
            var vfile = new FileInfo(filename);


            string[] lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length; i++)
            {
                // check if the line contains the search string 
                if (lines[i].Contains("wowpath="))
                {
                    // modify the line as desired  
                    string data1 = lines[i].Substring(8);
                    string empty = "";
                    if (data1 != empty)
                        {
                            string wowline4 = data1;
                            string directoryPath = Path.GetDirectoryName(wowline4);
                            var wow = new Process();
                            wow.StartInfo.WorkingDirectory = directoryPath;
                            wow.StartInfo.FileName = wowline4;
                            wow.StartInfo.Arguments = "";
                            wow.StartInfo.UseShellExecute = false;
                            wow.Start();

                        /*
                        var startwow = new Process();
                        var startwowstring = "/C " + "start " + wowline4 + "";
                        string directoryPath2 = Path.GetDirectoryName(wowline4);
                        startwow.StartInfo.FileName = "cmd.exe";
                        startwow.StartInfo.WorkingDirectory = directoryPath2;
                        startwow.StartInfo.Arguments = startwowstring;
                        startwow.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startwow.Start();
                        startwow.WaitForExit();
                        */


                    }
                    break;
                }

                

            }

            //button5.Visible = true;



            

        }
    }
}
