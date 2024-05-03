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
        private List<Shop> shoplist;
        private List<CheckBox> checkBoxes = new List<CheckBox>();
        private Panel cardsPanel;

        public Form1()
        {
            InitializeComponent();
            LoadShopCards();
        }

        private void LoadShopCards()
        {
            try
            {
                string jsonFilePath = @"C:\Users\Admin\Desktop\уник\ооп\Довідник покупця (курсова ООП)\Довідник покупця (курсова ООП) winforms\Data\data.json";
                string jsonData = File.ReadAllText(jsonFilePath);
                shoplist = JsonConvert.DeserializeObject<List<Shop>>(jsonData);

                cardsPanel = new FlowLayoutPanel();
                cardsPanel.AutoScroll = true;
                cardsPanel.BackColor = Color.Transparent;
                cardsPanel.Dock = DockStyle.Left;
                cardsPanel.Width = this.ClientSize.Width / 2;
                this.Controls.Add(cardsPanel);

                AddShopCardsToPanel(shoplist);
            }
            catch (Exception e)
            {
                MessageBox.Show($"An error occurred: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddShopCardsToPanel(List<Shop> shops)
        {
            int columnWidth = (cardsPanel.Width - 50);

            foreach (var shop in shops)
            {
                Panel cardPanel = new Panel();
                cardPanel.BorderStyle = BorderStyle.FixedSingle;
                cardPanel.Size = new System.Drawing.Size(columnWidth, 150);
                cardPanel.Location = new Point(10, 10);
                cardPanel.BackColor = Color.LightGray;

                CheckBox checkBox = new CheckBox();
                checkBox.Text = "Select";
                checkBox.AutoSize = true;
                checkBox.Location = new Point(10, 10);
                cardPanel.Controls.Add(checkBox);

                checkBoxes.Add(checkBox);
                checkBox.Tag = shop;

                Label titleLabel = new Label();
                titleLabel.AutoSize = true;
                titleLabel.Text = $"Title: {shop.title}";
                titleLabel.Location = new Point(10, checkBox.Bottom + 5);
                cardPanel.Controls.Add(titleLabel);

                Label addressLabel = new Label();
                addressLabel.AutoSize = true;
                addressLabel.Text = $"Address: {shop.address}";
                addressLabel.Location = new Point(10, titleLabel.Bottom + 5);
                cardPanel.Controls.Add(addressLabel);

                Label phoneLabel = new Label();
                phoneLabel.AutoSize = true;
                phoneLabel.Text = $"Phone Number: {shop.phoneNumber}";
                phoneLabel.Location = new Point(10, addressLabel.Bottom + 5);
                cardPanel.Controls.Add(phoneLabel);

                Label specLabel = new Label();
                specLabel.AutoSize = true;
                specLabel.Text = $"Specialization: {shop.specialization}";
                specLabel.Location = new Point(10, phoneLabel.Bottom + 5);
                cardPanel.Controls.Add(specLabel);

                Label ownerLabel = new Label();
                ownerLabel.AutoSize = true;
                ownerLabel.Text = $"Ownership Form: {shop.ownershipForm}";
                ownerLabel.Location = new Point(10, specLabel.Bottom + 5);
                cardPanel.Controls.Add(ownerLabel);

                Label hoursLabel = new Label();
                hoursLabel.AutoSize = true;
                hoursLabel.Text = $"Working Hours: {shop.workingHours}";
                hoursLabel.Location = new Point(10, ownerLabel.Bottom + 5);
                cardPanel.Controls.Add(hoursLabel);

                cardsPanel.Controls.Add(cardPanel);
            }
        }
    }
}
