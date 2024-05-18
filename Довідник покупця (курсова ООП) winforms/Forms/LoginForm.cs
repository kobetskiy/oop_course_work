using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Довідник_покупця__курсова_ООП__winforms.Services;

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

            welcomeLabel = new Label
            {
                Text = "Welcome!",
                Width = 300,
                Font = new Font("Calibri", 24),
                Location = new Point(120, 40)
            };
            Controls.Add(welcomeLabel);

            usernameLabel = new Label
            {
                Text = "Username:",
                Width = 60,
                Location = new Point(posX, welcomeLabel.Bottom + 35)
            };
            Controls.Add(usernameLabel);

            usernameTextBox = new TextBox
            {
                Location = new Point(posX + 80, welcomeLabel.Bottom + 35),
                Width = 230
            };
            Controls.Add(usernameTextBox);

            passwordLabel = new Label
            {
                Text = "Password:",
                Width = 60,
                Location = new Point(posX, usernameTextBox.Bottom + 10)
            };
            Controls.Add(passwordLabel);

            passwordTextBox = new TextBox
            {
                Location = new Point(posX + 80, usernameTextBox.Bottom + 10),
                Width = 230,
                UseSystemPasswordChar = true
            };
            Controls.Add(passwordTextBox);

            loginButton = new Button
            {
                Text = "Login",
                Cursor = Cursors.Hand,
                Location = new Point(posX, passwordTextBox.Bottom + 30),
                BackColor = Color.FromArgb(41, 128, 185),
                Width = 310,
                Height = 30
            };
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
            LoginService loginService = new LoginService();
            loginService.Login(this, usernameTextBox.Text, passwordTextBox.Text);
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
