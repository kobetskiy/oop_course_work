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

namespace Довідник_покупця__курсова_ООП__winforms
{
    public partial class Form1 : Form
    {
        private List<Shop> shoplist;
        private readonly List<Shop> selectedShops = new List<Shop>();
        private readonly List<CheckBox> checkBoxes = new List<CheckBox>();

        private Button loadToTxtBtn;
        private Button applySortBtn;
        private Button switchToAdminBtn;

        private ComboBox sortByBox;
        private ComboBox sortOrderBox;

        private Label sortLabel;
        private Panel cardsPanel;

        public Form1()
        {
            InitializeComponent();
            LoadShopCards();
            CreateControls();
            ApplyStyles();
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
                cardsPanel.Dock = DockStyle.Right;
                cardsPanel.Width = ClientSize.Width * 2 / 3;
                Controls.Add(cardsPanel);

                AddShopCardsToPanel(shoplist);
            }
            catch (Exception e)
            {
                MessageBox.Show($"An error occurred: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateControls()
        {
            int posX = 40;

            switchToAdminBtn = new Button();
            switchToAdminBtn.Text = "Switch to admin panel";
            switchToAdminBtn.Cursor = Cursors.Hand;
            switchToAdminBtn.Location = new Point(posX, 10);
            switchToAdminBtn.BackColor = Color.Gray;
            switchToAdminBtn.Width = 200;
            switchToAdminBtn.Height = 30;
            switchToAdminBtn.Click += SwitchToAdminBtn;
            Controls.Add(switchToAdminBtn);

            sortLabel = new Label();
            sortLabel.Text = "Sort and save cards";
            sortLabel.Width = 200;
            sortLabel.Font = new Font("Calibri", 14, FontStyle.Bold);
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

            applySortBtn = new Button();
            applySortBtn.Text = "Apply sorting";
            applySortBtn.Cursor = Cursors.Hand;
            applySortBtn.Location = new Point(posX, sortOrderBox.Bottom + 15);
            applySortBtn.BackColor = Color.FromArgb(41, 128, 185);
            applySortBtn.Width = 200;
            applySortBtn.Height = 30;
            applySortBtn.Click += ApplySortBtn_Click;
            Controls.Add(applySortBtn);

            loadToTxtBtn = new Button();
            loadToTxtBtn.Text = "Load cards to txt";
            loadToTxtBtn.Cursor = Cursors.Hand;
            loadToTxtBtn.Location = new Point(posX, applySortBtn.Bottom + 10);
            loadToTxtBtn.BackColor = Color.FromArgb(41, 128, 185);
            loadToTxtBtn.Width = 200;
            loadToTxtBtn.Height = 50;
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
                Panel cardPanel = new Panel();
                cardPanel.BorderStyle = BorderStyle.FixedSingle;
                cardPanel.Size = new Size(columnWidth, 150);
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

        private void LoadToTxtBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedField = sortByBox.SelectedItem?.ToString();
                string selectedSortOrder = sortOrderBox.SelectedItem?.ToString();

                if ((selectedField != null && selectedSortOrder != null) || (selectedField == null && selectedSortOrder == null))
                {
                    selectedShops.Clear();

                    foreach (Control control in cardsPanel.Controls)
                    {
                        if (control is Panel cardPanel)
                        {
                            CheckBox checkBox = cardPanel.Controls.OfType<System.Windows.Forms.CheckBox>().FirstOrDefault();
                            if (checkBox != null && checkBox.Checked)
                            {
                                selectedShops.Add((Shop)checkBox.Tag);
                            }
                        }
                    }

                    if (selectedShops.Count == 0)
                    {
                        MessageBox.Show("Please select at least one shop.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string filePath;
                    if (selectedField != null && selectedSortOrder != null)
                    {
                        filePath = $@"C:\Users\Admin\Desktop\уник\ооп\Довідник покупця (курсова ООП)\Довідник покупця (курсова ООП) winforms\data_{selectedField}_{selectedSortOrder}.txt";
                    }
                    else
                    {
                        filePath = $@"C:\Users\Admin\Desktop\уник\ооп\Довідник покупця (курсова ООП)\Довідник покупця (курсова ООП) winforms\Data\data.txt";
                    }

                    SaveDataToTxtFile(filePath, selectedShops);

                    MessageBox.Show("Data successfully saved to file.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please select both sorting criteria or none.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SwitchToAdminBtn(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            Hide();
        }

        private void SaveDataToTxtFile(string filePath, List<Shop> data)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (var shop in data)
                {
                    writer.WriteLine($"Title: {shop.title}");
                    writer.WriteLine($"Address: {shop.address}");
                    writer.WriteLine($"Phone Number: {shop.phoneNumber}");
                    writer.WriteLine($"Specialization: {shop.specialization}");
                    writer.WriteLine($"Ownership Form: {shop.ownershipForm}");
                    writer.WriteLine($"Working Hours: {shop.workingHours}");
                    writer.WriteLine();
                }
            }
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
                    shoplist = shoplist.OrderBy(x => property.GetValue(x)).ToList();
                }
                else
                {
                    shoplist = shoplist.OrderByDescending(x => property.GetValue(x)).ToList();
                }

                cardsPanel.Controls.Clear();
                AddShopCardsToPanel(shoplist);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
