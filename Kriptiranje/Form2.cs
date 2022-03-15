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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        AesCryptoServiceProvider crypt_provider = new AesCryptoServiceProvider();

        private void button1_Click(object sender, EventArgs e)
        {
            
            crypt_provider.BlockSize = 128;
            crypt_provider.KeySize = 256;
            crypt_provider.GenerateIV();
            crypt_provider.GenerateKey();
            crypt_provider.Mode = CipherMode.CBC;
            crypt_provider.Padding = PaddingMode.PKCS7;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                {
                    writer.WriteLine(Convert.ToBase64String(crypt_provider.Key));
                    writer.Close();
                }
            }

            /*
            StreamWriter sw = new StreamWriter("C:/Users/lupko/Desktop/Kriptiranje/tajni_kljuc.txt");
            sw.WriteLine(Convert.ToBase64String(crypt_provider.Key));
            MessageBox.Show(Convert.ToBase64String(crypt_provider.Key));
            */
        }

        String fileContent = string.Empty;
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

        private void button3_Click(object sender, EventArgs e)
        {
            ICryptoTransform transform = crypt_provider.CreateEncryptor();
            byte[] encrypted_bytes = transform.TransformFinalBlock(ASCIIEncoding.ASCII.GetBytes(fileContent), 0, fileContent.Length);
            string str = Convert.ToBase64String(encrypted_bytes);

            SaveFileDialog saveFileDialog2 = new SaveFileDialog();

            saveFileDialog2.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog2.FilterIndex = 2;
            saveFileDialog2.RestoreDirectory = true;

            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog2.FileName))
                {
                    writer.WriteLine(str);
                    writer.Close();
                }
            }


        }

        private void button4_Click(object sender, EventArgs e)
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


            ICryptoTransform transform = crypt_provider.CreateDecryptor();
            byte[] enc_bytes = Convert.FromBase64String(fileContent);
            byte[] decrypted_bytes = transform.TransformFinalBlock(enc_bytes, 0, enc_bytes.Length);
            string str = ASCIIEncoding.ASCII.GetString(decrypted_bytes);



            SaveFileDialog saveFileDialog3 = new SaveFileDialog();

            saveFileDialog3.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog3.FilterIndex = 2;
            saveFileDialog3.RestoreDirectory = true;

            if (saveFileDialog3.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog3.FileName))
                {
                    writer.WriteLine(str);
                    writer.Close();
                }
            }



            MessageBox.Show("Dekriptirano,provjeri datoteku");
        }
    }
}
