using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Довідник_покупця__курсова_ООП__winforms.Models;
using Довідник_покупця__курсова_ООП__winforms.Services;

namespace Довідник_покупця__курсова_ООП__winforms.Forms
{
    public partial class AdminPanel : Form
    {
        private List<Shop> shopList;
        private DataGridView shopDataGridView;
        private Button backButton;
        private Button addButton;
        private Button deleteButton;

        AdminService adminService = new AdminService();

        public AdminPanel()
        {
            InitializeComponent();
            LoadShopData();
            CreateControls();
            ApplyStyles();
        }

        public void LoadShopData()
        {
            try
            {
                string jsonFilePath = "Data/shop_data.json";
                string jsonData = File.ReadAllText(jsonFilePath);
                shopList = JsonConvert.DeserializeObject<List<Shop>>(jsonData);
            }
            catch (Exception e)
            {
                MessageBox.Show($"An error occurred while loading data: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateControls()
        {
            backButton = new Button
            {
                Text = "Switch to client app",
                Location = new Point(10, 10),
                Height = 40,
                Width = 200,
                ForeColor = Color.White,
                BackColor = Color.Gray,
                FlatStyle = FlatStyle.Flat,
            };
            backButton.Click += SwitchToClienBtn_Click;

            addButton = new Button
            {
                Text = "Add Shop",
                Location = new Point(100, 10),
                Width = 200,
                Height = 40,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(41, 128, 185),
                FlatStyle = FlatStyle.Flat,
            };
            addButton.Click += AddButton_Click;

            deleteButton = new Button
            {
                Text = "Delete Shop",
                Location = new Point(200, 10),
                Width = 200,
                Height = 40,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(41, 128, 185),
                FlatStyle = FlatStyle.Flat,
            };
            deleteButton.Click += DeleteButton_Click;

            FlowLayoutPanel bottomPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10),
                AutoSize = true
            };

            FlowLayoutPanel topPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10),
                AutoSize = true
            };

            shopDataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = shopList,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToOrderColumns = true,
                EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
            };
            shopDataGridView.CellEndEdit += ShopDataGridView_CellEndEdit;

            topPanel.Controls.Add(backButton);
            bottomPanel.Controls.Add(addButton);
            bottomPanel.Controls.Add(deleteButton);

            Controls.Add(topPanel);
            Controls.Add(bottomPanel);
            Controls.Add(shopDataGridView);
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
                    button.BackColor = Color.FromArgb(41, 128, 185);
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                }
            }
        }

        private void ShopDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            adminService.SaveData(shopList);
        }

        private void SwitchToClienBtn_Click(object sender, EventArgs e)
        {
            adminService.SwitchToClienBtnOnClick(this);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            adminService.AddButtonOnClick(shopList, shopDataGridView);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            adminService.DeleteButtonOnClick(shopList, shopDataGridView);
        }

        private void AdminPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
