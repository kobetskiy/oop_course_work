using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Довідник_покупця__курсова_ООП__winforms.Forms;
using Довідник_покупця__курсова_ООП__winforms.Models;

namespace Довідник_покупця__курсова_ООП__winforms.Services
{
    public class LoginService
    {
        public void Login(Form form, string username, string password)
        {
            string jsonFilePath = "Data/admin_data.json";
            if (!File.Exists(jsonFilePath))
            {
                MessageBox.Show("User credentials file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string json = File.ReadAllText(jsonFilePath);
            Admin admin = JsonConvert.DeserializeObject<Admin>(json);

            if (admin.Username == username && admin.Password == password)
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
