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
        private List<Shop> selectedShops = new List<Shop>();
        private List<CheckBox> checkBoxes = new List<CheckBox>();

        private Button loadToTxtBtn;
        private Button applySortBtn;

        private ComboBox sortByBox;
        private ComboBox sortOrderBox;

        private Label sortLabel;
        private Panel cardsPanel;

        public Form1()
        {
            InitializeComponent();
            LoadShopCards();
            CreateControls();
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
                cardsPanel.Width = this.ClientSize.Width * 2 / 3;
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
            loadToTxtBtn = new Button();
            loadToTxtBtn.Text = "Load cards to txt";
            loadToTxtBtn.Cursor = Cursors.Hand;
            loadToTxtBtn.Size = new System.Drawing.Size(150, 60);
            loadToTxtBtn.Click += LoadToTxtBtn_Click;
            loadToTxtBtn.Location = new Point(20, (this.ClientSize.Height - loadToTxtBtn.Height) / 3);
            this.Controls.Add(loadToTxtBtn);

            sortLabel = new Label();
            sortLabel.Text = "Sort cards";
            sortLabel.Font = new Font("Arial", 12);
            sortLabel.Location = new Point(20, loadToTxtBtn.Bottom + 10);
            this.Controls.Add(sortLabel);

            sortByBox = new ComboBox();
            sortByBox.Items.AddRange(typeof(Shop).GetProperties().Select(p => p.Name).ToArray());
            sortByBox.Location = new Point(20, sortLabel.Bottom + 10);
            this.Controls.Add(sortByBox);

            sortOrderBox = new ComboBox();
            sortOrderBox.Items.AddRange(new string[] { "Ascending", "Descending" });
            sortOrderBox.Location = new Point(20, sortByBox.Bottom + 10);
            this.Controls.Add(sortOrderBox);

            applySortBtn = new Button();
            applySortBtn.Text = "Apply sorting";
            applySortBtn.Cursor = Cursors.Hand;
            applySortBtn.Click += ApplySortBtn_Click;
            applySortBtn.Location = new Point(20, sortOrderBox.Bottom + 10);
            this.Controls.Add(applySortBtn);
        }

        private void AddShopCardsToPanel(List<Shop> shops)
        {
            int columnWidth = (cardsPanel.Width - 50) / 2;

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
                            System.Windows.Forms.CheckBox checkBox = cardPanel.Controls.OfType<System.Windows.Forms.CheckBox>().FirstOrDefault();
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

        }
    }
}
