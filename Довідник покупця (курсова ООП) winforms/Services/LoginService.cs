using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Довідник_покупця__курсова_ООП__winforms.Forms;

namespace Довідник_покупця__курсова_ООП__winforms.Services
{
    public class LoginService
    {
        public void Login(Form form, string username, string password)
        {
            if (username == "admin" && password == "12345")
            {
                AdminPanel adminPanel = new AdminPanel();
                adminPanel.Show();
                form.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
