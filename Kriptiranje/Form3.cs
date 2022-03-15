using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace Kriptiranje
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        byte[] plaintext;
        byte[] encryptedtext;
        RSAParameters javni_kljuc;
        RSAParameters privatni_kljuc;
        string javni_kljuc_string;
        string privatni_kljuc_string;

        string fileContent = string.Empty;


        private void button1_Click(object sender, EventArgs e)
        {
            javni_kljuc=RSA.ExportParameters(false);
            privatni_kljuc = RSA.ExportParameters(true);

            javni_kljuc_string = RSA.ToXmlString(false);
            privatni_kljuc_string = RSA.ToXmlString(true);

            RSAParameters rsaKeyInfo = RSA.ExportParameters(false);

            SaveFileDialog saveFileDialogRSA = new SaveFileDialog();

            saveFileDialogRSA.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialogRSA.FilterIndex = 2;
            saveFileDialogRSA.RestoreDirectory = true;

            if (saveFileDialogRSA.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialogRSA.FileName))
                {
                    writer.WriteLine(javni_kljuc_string);
                    writer.Close();
                }
            }

            if (saveFileDialogRSA.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialogRSA.FileName))
                {
                    writer.WriteLine(privatni_kljuc_string);
                    writer.Close();
                }
            }

            //MessageBox.Show("javni kljuc ->" + javni_kljuc_string + "privatni kljuc ->" + privatni_kljuc_string);
        }
        
        private void button2_Click(object sender, EventArgs e)
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

        byte[] encryptedData;

        private void button3_Click(object sender, EventArgs e)
        {
                
                using (RSACryptoServiceProvider RSA1 = new RSACryptoServiceProvider())
                {
                    RSA1.ImportParameters(javni_kljuc);
                    plaintext = ByteConverter.GetBytes(fileContent);
                    encryptedData = RSA1.Encrypt(plaintext, false);
                }

            SaveFileDialog saveFileDialogRSA = new SaveFileDialog();

            saveFileDialogRSA.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialogRSA.FilterIndex = 2;
            saveFileDialogRSA.RestoreDirectory = true;

            if (saveFileDialogRSA.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialogRSA.FileName))
                {
                    writer.WriteLine(Convert.ToBase64String(encryptedData));
                    writer.Close();
                }
            }

        }


        private void button5_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            byte[] decryptedtext=Convert.FromBase64String(fileContent);

            byte[] decryptedData;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(privatni_kljuc);
                decryptedData = RSA.Decrypt(decryptedtext, false);
            }

            SaveFileDialog saveFileDialogRSA = new SaveFileDialog();

            saveFileDialogRSA.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialogRSA.FilterIndex = 2;
            saveFileDialogRSA.RestoreDirectory = true;

            if (saveFileDialogRSA.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialogRSA.FileName))
                {
                    writer.WriteLine(ByteConverter.GetString(decryptedData));
                    writer.Close();
                }
            }
        }

        
    }
}
