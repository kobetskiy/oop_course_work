using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Довідник_покупця__курсова_ООП__winforms.Forms
{
    public partial class LoginForm : Form
    {
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Label usernameLabel;
        private Label welcomeLabel;
        private Label passwordLabel;

        public LoginForm()
        {
            InitializeComponent();
            CreateLoginControls();
            ApplyStyles();
        }

        private void CreateLoginControls()
        {
            int posX = 20;
            int posY = 20;

            welcomeLabel = new Label();
            welcomeLabel.Text = "Welcome!";
            welcomeLabel.Width = 300;
            welcomeLabel.Font = new Font("Calibri", 24);
            welcomeLabel.Location = new Point(110, 40);
            Controls.Add(welcomeLabel);

            usernameLabel = new Label();
            usernameLabel.Text = "Username:";
            usernameLabel.Width = 60;
            usernameLabel.Location = new Point(posX, welcomeLabel.Bottom + 35);
            Controls.Add(usernameLabel);

            usernameTextBox = new TextBox();
            usernameTextBox.Location = new Point(posX + 80, welcomeLabel.Bottom + 35);
            usernameTextBox.Width = 230;
            Controls.Add(usernameTextBox);

            passwordLabel = new Label();
            passwordLabel.Text = "Password:";
            passwordLabel.Width = 60;
            passwordLabel.Location = new Point(posX, usernameTextBox.Bottom + 10);
            Controls.Add(passwordLabel);

            passwordTextBox = new TextBox();
            passwordTextBox.Location = new Point(posX + 80, usernameTextBox.Bottom + 10);
            passwordTextBox.Width = 230;
            passwordTextBox.UseSystemPasswordChar = true;
            Controls.Add(passwordTextBox);

            loginButton = new Button();
            loginButton.Text = "Login";
            loginButton.Cursor = Cursors.Hand;
            loginButton.Location = new Point(posX, passwordTextBox.Bottom + 30);
            loginButton.BackColor = Color.FromArgb(41, 128, 185);
            loginButton.Width = 310;
            loginButton.Height = 30;
            loginButton.Click += LoginButton_Click;
            Controls.Add(loginButton);
        }

        private void ApplyStyles()
        {
            BackColor = Color.FromArgb(240, 240, 240);
            Font = new Font("Calibri", 11);

            foreach (Control ctrl in Controls)
            {
                if (ctrl is Button button)
                {
                    button.ForeColor = Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                }
                else if (ctrl is Label label)
                {
                    label.ForeColor = Color.FromArgb(44, 62, 80);
                }
                else if (ctrl is TextBox textBox)
                {
                    textBox.BackColor = Color.White;
                    textBox.ForeColor = Color.FromArgb(44, 62, 80);
                }
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            if (username == "admin" && password == "12345")
            {
                Form1 mainForm = new Form1();
                mainForm.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
