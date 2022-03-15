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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        string fileContentDatoteka = string.Empty;
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
                        fileContentDatoteka = reader.ReadToEnd();
                    }

                }
            }
        }

        string fileContentJavni = string.Empty;
        private void button2_Click(object sender, EventArgs e)
        {
            var filePathJavni = string.Empty;


            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePathJavni = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContentJavni = reader.ReadToEnd();
                    }

                }
            }
        }

        string fileContentPrivatni = string.Empty;
        private void button3_Click(object sender, EventArgs e)
        {
            var filePathPrivatni = string.Empty;


            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePathPrivatni = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContentPrivatni = reader.ReadToEnd();
                    }

                }
            }
        }

        
        /*
        private void button4_Click(object sender, EventArgs e)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                StringBuilder builder = new StringBuilder();
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(fileContentDatoteka));

                // Convert byte array to a string   
                
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                SaveFileDialog saveFileDialogRSA = new SaveFileDialog();

                saveFileDialogRSA.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialogRSA.FilterIndex = 2;
                saveFileDialogRSA.RestoreDirectory = true;

                if (saveFileDialogRSA.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialogRSA.FileName))
                    {
                        writer.WriteLine(builder.ToString());
                        writer.Close();
                    }
                }

            }
        }
        */
        

        
        private void button5_Click(object sender, EventArgs e)
        {
            //fileContentDatoteka
            using (RSACryptoServiceProvider RSA1 = new RSACryptoServiceProvider())
            {

                RSA1.FromXmlString(fileContentPrivatni);

                using (SHA256 sha256obj = SHA256Managed.Create())
                {

                    byte[] hashDatoteke = sha256obj.ComputeHash(Encoding.UTF8.GetBytes(fileContentDatoteka));

                    RSAPKCS1SignatureFormatter sigFormatter = new RSAPKCS1SignatureFormatter(RSA1);

                    sigFormatter.SetHashAlgorithm("SHA256");


                    byte[] digitalniPotpis = sigFormatter.CreateSignature(hashDatoteke);

                    //MessageBox.Show(Convert.ToBase64String(hashDatoteke));

                    SaveFileDialog saveFileDialogRSA = new SaveFileDialog();

                    saveFileDialogRSA.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialogRSA.FilterIndex = 2;
                    saveFileDialogRSA.RestoreDirectory = true;

                    if (saveFileDialogRSA.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter writer = new StreamWriter(saveFileDialogRSA.FileName))
                        {
                            writer.WriteLine(Convert.ToBase64String(digitalniPotpis));
                            writer.Close();
                        }
                    }
                }
            }
        }

        string fileContentPotpis = string.Empty;
        private void button6_Click(object sender, EventArgs e)
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
                        fileContentPotpis = reader.ReadToEnd();
                    }

                }
            }
        }

        string fileContentDat = string.Empty;
        private void button7_Click(object sender, EventArgs e)
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
                        fileContentDat = reader.ReadToEnd();
                    }

                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            SHA256 hash = SHA256Managed.Create();
            RSA.FromXmlString(fileContentJavni);

           
            byte[] sazetak = hash.ComputeHash(Encoding.UTF8.GetBytes(fileContentDat));
            byte[] potpis = Convert.FromBase64String(fileContentPotpis);

            bool provjera = RSA.VerifyHash(sazetak, CryptoConfig.MapNameToOID("SHA256"), potpis);

            if (provjera == true)
            {
                MessageBox.Show("Digitalni potpis je ispravan!");
            }
            else
            {
                MessageBox.Show("Digitalni potpis je neispravan!");
            }
        }
    }
}
