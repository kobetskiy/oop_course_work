using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Довідник_покупця__курсова_ООП__winforms.Models;

namespace Довідник_покупця__курсова_ООП__winforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Shop> data;
            string filePath = "C:/Users/Admin/Desktop/уник/ооп/Довідник покупця (курсова ООП)/Довідник покупця (курсова ООП) winforms/Data/data.json";
            using (StreamReader r = new StreamReader(filePath))
            {
                string dataJson = r.ReadToEnd();
                data = JsonConvert.DeserializeObject<List<Shop>>(dataJson);
            }
            if (data != null && data.Count > 0)
            {
                titleLabel.Text = data[0].Name;
                specializationLabel.Text = data[0].Specialization;
                ownershipFormLabel.Text = $"({data[0].OwnershipForm})";
                addressLabel.Text = data[0].Address;
                phoneLabel.Text = data[0].PhoneNumber;
                workingHoursLabel.Text = data[0].WorkingHours;
            }
            else
                titleLabel.Text = "Not Found";
        }
    }
}
