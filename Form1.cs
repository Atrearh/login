using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace WindowsFormsApp2
{
    public partial class FormAuth : Form
    {
        public FormAuth()
        {
            InitializeComponent();
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Close();
            //solt for md5 hashing =>   _solt
        }



        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            var isAuth = Authorization(textBoxLogin.Text, textBoxPass.Text);
            // var message = string.Empty;
            var message = "Неверный логин или пароль";
            if (isAuth)
            {
                message = "Вход";
            }

            MessageBox.Show(message);
        }

        private bool Authorization(string login, string pass)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                var dict = GetCredenhtiolString();
                
                if (!dict.ContainsKey(login))
                {
                    return false;
                }
                var originalPass = dict[login];
                string hash = GetMd5Hash(md5Hash, pass + "solt");
                if (originalPass != hash)
                {
                    return false;
                }

                return true;
            }

        }

        private Dictionary<string, string> GetCredenhtiolString()
        {
            var result = new Dictionary<string, string>();
            string pathDbLogin = "F:/DB.csv";
            string text = System.IO.File.ReadAllText(pathDbLogin);
            foreach (var itemLogin in text.Split('\n'))
            {
                var authLog = itemLogin.Split(',');

                result.Add(authLog[0].Trim(), authLog[1].Trim());
            }

            return result;

        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        
    }

}

        
       
            
 
