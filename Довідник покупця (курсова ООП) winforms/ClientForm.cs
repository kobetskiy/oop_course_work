using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Довідник_покупця__курсова_ООП__winforms.Forms;
using Довідник_покупця__курсова_ООП__winforms.Models;
using Довідник_покупця__курсова_ООП__winforms.Services;

namespace Довідник_покупця__курсова_ООП__winforms
{
    public partial class ClientForm : Form
    {
        private List<Shop> shopList;
        private List<Shop> selectedShops = new List<Shop>();
        private List<CheckBox> checkBoxes = new List<CheckBox>();

        private Button loadToTxtBtn;
        private Button applySortBtn;
        private Button switchToAdminBtn;


        private ComboBox sortByBox;
        private ComboBox sortOrderBox;

        private Label sortLabel;
        private Panel cardsPanel;

        readonly ClientService clientService = new ClientService();

        public ClientForm()
        {
            InitializeComponent();
            UpdateLoadedShopCards();
            CreateControls();
            ApplyStyles();
        }

        private void UpdateLoadedShopCards()
        {
            try
            {
                // string jsonFilePath = @"C:\Users\Admin\Desktop\уник\ооп\Довідник покупця (курсова ООП)\Довідник покупця (курсова ООП) winforms\Data\data.json";
                string jsonFilePath = "Data/shop_data.json";
                string jsonData = File.ReadAllText(jsonFilePath);
                shopList = JsonConvert.DeserializeObject<List<Shop>>(jsonData);

                cardsPanel = new FlowLayoutPanel
                {
                    AutoScroll = true,
                    BackColor = Color.Transparent,
                    Dock = DockStyle.Right,
                    Width = ClientSize.Width * 2 / 3
                };
                Controls.Add(cardsPanel);
                AddShopCardsToPanel(shopList);
            }
            catch (Exception e)
            {
                MessageBox.Show($"An error occurred: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateControls()
        {
            int posX = 40;

            switchToAdminBtn = new Button
            {
                Text = "Switch to admin panel",
                Cursor = Cursors.Hand,
                Location = new Point(posX, 10),
                BackColor = Color.Gray,
                Width = 200,
                Height = 30
            };
            switchToAdminBtn.Click += SwitchToAdminBtn_Click;
            Controls.Add(switchToAdminBtn);

            sortLabel = new Label
            {
                Text = "Sort and save shops",
                Width = 200,
                Font = new Font("Calibri", 14, FontStyle.Bold)
            };
            sortLabel.Location = new Point(posX, (ClientSize.Height - sortLabel.Height) / 3);
            Controls.Add(sortLabel);

            sortByBox = new ComboBox();
            sortByBox.Items.AddRange(typeof(Shop).GetProperties().Select(p => p.Name).ToArray());
            sortByBox.Location = new Point(posX, sortLabel.Bottom + 10);
            sortByBox.Width = 200;
            Controls.Add(sortByBox);

            sortOrderBox = new ComboBox();
            sortOrderBox.Items.AddRange(new string[] { "Ascending", "Descending" });
            sortOrderBox.Location = new Point(posX, sortByBox.Bottom + 10);
            sortOrderBox.Width = 200;
            Controls.Add(sortOrderBox);

            applySortBtn = new Button
            {
                Text = "Apply sorting",
                Cursor = Cursors.Hand,
                Location = new Point(posX, sortOrderBox.Bottom + 15),
                BackColor = Color.FromArgb(41, 128, 185),
                Width = 200,
                Height = 30
            };
            applySortBtn.Click += ApplySortBtn_Click;
            Controls.Add(applySortBtn);

            loadToTxtBtn = new Button
            {
                Text = "Load shops to txt",
                Cursor = Cursors.Hand,
                Location = new Point(posX, applySortBtn.Bottom + 10),
                BackColor = Color.FromArgb(41, 128, 185),
                Width = 200,
                Height = 50
            };
            loadToTxtBtn.Click += LoadToTxtBtn_Click;
            Controls.Add(loadToTxtBtn);
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
                else if (ctrl is ComboBox comboBox)
                {
                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    comboBox.BackColor = Color.White;
                }
                else if (ctrl is Label label)
                {
                    label.ForeColor = Color.FromArgb(44, 62, 80);
                }
            }
        }

        private void AddShopCardsToPanel(List<Shop> shops)
        {
            int columnWidth = (cardsPanel.Width - 50) / 2;

            foreach (var shop in shops)
            {
                Panel cardPanel = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Size = new Size(columnWidth, 150),
                    Location = new Point(10, 10),
                    BackColor = Color.LightGray
                };

                CheckBox checkBox = new CheckBox
                {
                    Text = "Select",
                    AutoSize = true,
                    Location = new Point(10, 10)
                };
                cardPanel.Controls.Add(checkBox);

                checkBoxes.Add(checkBox);
                checkBox.Tag = shop;

                Label titleLabel = new Label
                {
                    AutoSize = true,
                    Text = $"Title: {shop.Title}",
                    Location = new Point(10, checkBox.Bottom + 5)
                };
                cardPanel.Controls.Add(titleLabel);

                Label addressLabel = new Label
                {
                    AutoSize = true,
                    Text = $"Address: {shop.Address}",
                    Location = new Point(10, titleLabel.Bottom + 5)
                };
                cardPanel.Controls.Add(addressLabel);

                Label phoneLabel = new Label
                {
                    AutoSize = true,
                    Text = $"Phone Number: {shop.PhoneNumber}",
                    Location = new Point(10, addressLabel.Bottom + 5)
                };
                cardPanel.Controls.Add(phoneLabel);

                Label specLabel = new Label
                {
                    AutoSize = true,
                    Text = $"Specialization: {shop.Specialization}",
                    Location = new Point(10, phoneLabel.Bottom + 5)
                };
                cardPanel.Controls.Add(specLabel);

                Label ownerLabel = new Label
                {
                    AutoSize = true,
                    Text = $"Ownership Form: {shop.OwnershipForm}",
                    Location = new Point(10, specLabel.Bottom + 5)
                };
                cardPanel.Controls.Add(ownerLabel);

                Label hoursLabel = new Label
                {
                    AutoSize = true,
                    Text = $"Working Hours: {shop.WorkingHours}",
                    Location = new Point(10, ownerLabel.Bottom + 5)
                };
                cardPanel.Controls.Add(hoursLabel);

                cardsPanel.Controls.Add(cardPanel);
            }
        }

        private void LoadToTxtBtn_Click(object sender, EventArgs e)
        {
            clientService.LoadToTxtBtnOnClick(sortByBox, sortOrderBox, selectedShops, cardsPanel);
        }

        private void SwitchToAdminBtn_Click(object sender, EventArgs e)
        {
            clientService.SwitchToAdminBtnOnClick(this);
        }

        private void ApplySortBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedField = sortByBox.SelectedItem?.ToString();
                string selectedSortOrder = sortOrderBox.SelectedItem?.ToString();

                if (selectedField == null || selectedSortOrder == null)
                {
                    MessageBox.Show("Please select sorting criteria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var property = typeof(Shop).GetProperty(selectedField);
                if (property == null)
                {
                    MessageBox.Show("Invalid sorting criteria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (selectedSortOrder == "Ascending")
                {
                    shopList = shopList.OrderBy(x => property.GetValue(x)).ToList();
                }
                else
                {
                    shopList = shopList.OrderByDescending(x => property.GetValue(x)).ToList();
                }

                cardsPanel.Controls.Clear();
                AddShopCardsToPanel(shopList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
