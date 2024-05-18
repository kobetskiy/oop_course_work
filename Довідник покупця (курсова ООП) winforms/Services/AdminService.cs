using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Довідник_покупця__курсова_ООП__winforms.Models;

namespace Довідник_покупця__курсова_ООП__winforms.Services
{
    public class AdminService
    {
        public void SwitchToClienBtnOnClick(Form form)
        {
            ClientForm clienForm = new ClientForm();
            clienForm.Show();
            form.Hide();
        }

        public void AddButtonOnClick(List<Shop> shopList, DataGridView shopDataGridView)
        {
            Shop newShop = new Shop();
            shopList.Add(newShop);
            shopDataGridView.DataSource = null;
            shopDataGridView.DataSource = shopList;
            SaveData(shopList);
        }

        public void DeleteButtonOnClick(List<Shop> shopList, DataGridView shopDataGridView)
        {
            foreach (DataGridViewRow row in shopDataGridView.SelectedRows)
            {
                shopList.Remove(row.DataBoundItem as Shop);
            }
            shopDataGridView.DataSource = null;
            shopDataGridView.DataSource = shopList;
            SaveData(shopList);
        }

        public void SaveData(List<Shop> shopList)
        {
            try
            {
                string jsonFilePath = @"C:\Users\Admin\Desktop\уник\ооп\Довідник покупця (курсова ООП)\Довідник покупця (курсова ООП) winforms\Data\data.json";
                string jsonData = JsonConvert.SerializeObject(shopList, Formatting.Indented);
                File.WriteAllText(jsonFilePath, jsonData);
            }
            catch (Exception e)
            {
                MessageBox.Show($"An error occurred while saving data: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
