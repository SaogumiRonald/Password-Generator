using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PasswordGenerator {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) {
            int passwordLength = (int)numericUpDown1.Value;

            bool useNumbers = checkBox3.Checked;
            bool useLoverCase = checkBox1.Checked;
            bool useUpperCase = checkBox2.Checked;
            bool useSpecialSymbols = checkBox4.Checked;

            const string numbers = "0123456789";
            const string loverCaseLetters = "abcdefghijklmnopqrstuvwxyz";
            const string upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string specialSymbols = textBox1.Text;

            string charTypes = (string)string.Empty;
            textBox2.Text = string.Empty;

            if (useNumbers) {
                charTypes += numbers;
                charTypes += numbers;
                charTypes += numbers;
            }
            if (useLoverCase) { charTypes += loverCaseLetters; }
            if (useUpperCase) { charTypes += upperCaseLetters; }
            if (useSpecialSymbols) { charTypes += specialSymbols; }

            Random rand = new Random();
            for (int j = 0; j < passwordLength; j++) {
                int index = rand.Next(0, charTypes.Length);
                if (string.IsNullOrEmpty(charTypes)) {
                    MessageBox.Show("Оберіть допустимі символи", "Увага!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.DefaultDesktopOnly);
                    break;
                }

                textBox2.Text += charTypes[index];
            }

            switch (getPasswordStrength(textBox2.Text)) {
                case 1:
                    label3.ForeColor = Color.DarkRed;
                    label3.Text = "Дуже ненадійний";
                    break;
                case 2:
                    label3.ForeColor = Color.Red;
                    label3.Text = "Ненадійний";
                    break;
                case 3:
                    label3.ForeColor = Color.Orange;
                    label3.Text = "Нормальний";
                    break;
                case 4:
                    label3.ForeColor = Color.Green;
                    label3.Text = "Надійний";
                    break;
                case 5:
                    label3.ForeColor = Color.DarkGreen;
                    label3.Text = "Дуже надійний";
                    break;
                default:
                    label3.Text = " ";
                    break;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e) {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            textBox1.Text = "~!@#$%^&*()_-+={}[]\\|:;\"'<>,.?/";
        }

        private void button3_Click(object sender, EventArgs e) {
            string password = textBox2.Text;
            Clipboard.SetData(DataFormats.Text, (Object)password);
            MessageBox.Show("Пароль скопійовано в буфер обміну", "Скопійовано", MessageBoxButtons.OK,
                           MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                           MessageBoxOptions.DefaultDesktopOnly);
        }

        private void button4_Click(object sender, EventArgs e) {
            string filePath = "passwords.txt";
            string text = textBox2.Text;

            using (StreamWriter fileStream = File.Exists(filePath) ? File.AppendText(filePath) : File.CreateText(filePath)) {
                fileStream.WriteLine(text);
            }
            MessageBox.Show("Пароль збережено до файлу \"passwords\"", "Збережено", MessageBoxButtons.OK,
                           MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                           MessageBoxOptions.DefaultDesktopOnly);
        }


        static int getPasswordStrength(string password) {
            int passwordStrength = 0;

            if (password.Length >= 8) {
                passwordStrength++;
            }

            foreach (char c in password) {
                if (char.IsDigit(c)) {
                    passwordStrength++;
                    break;
                }
            }
            
            foreach (char c in password) {
                if (char.IsLower(c)) {
                    passwordStrength++;
                    break;
                }
            }
            
            foreach (char c in password) {
                if (char.IsUpper(c)) {
                    passwordStrength++;
                    break;
                }
            }

            foreach (char c in password) {
                if (!char.IsLetterOrDigit(c)) {
                    passwordStrength++;
                    break;
                }
            }

            return passwordStrength;
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void label3_Click(object sender, EventArgs e) {

        }
    }
}
