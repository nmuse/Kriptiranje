using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kriptiranje
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        string fileContent = string.Empty;

        private void button1_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;


            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(fileContent));

                // Convert byte array to a string   
                /*StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                */
                SaveFileDialog saveFileDialogRSA = new SaveFileDialog();

                saveFileDialogRSA.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialogRSA.FilterIndex = 2;
                saveFileDialogRSA.RestoreDirectory = true;

                if (saveFileDialogRSA.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialogRSA.FileName))
                    {
                        //writer.WriteLine(builder.ToString());
                        writer.WriteLine(Convert.ToBase64String(bytes));
                        writer.Close();
                    }
                }
                
            }
        }
    }
}
