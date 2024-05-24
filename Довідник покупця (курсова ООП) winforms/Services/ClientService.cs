using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Довідник_покупця__курсова_ООП__winforms.Forms;
using Довідник_покупця__курсова_ООП__winforms.Models;

namespace Довідник_покупця__курсова_ООП__winforms.Services
{
    public class ClientService
    {
        public void LoadToTxtBtnOnClick(ComboBox sortByBox, ComboBox sortOrderBox, List<Shop> selectedShops, Panel cardsPanel)
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

                    SaveDataToTxtFile(selectedShops);
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

        private void SaveDataToTxtFile(List<Shop> data)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                OverwritePrompt = true,
                CheckPathExists = true
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(saveDialog.FileName, false))
                    {
                        foreach (var shop in data)
                        {
                            writer.WriteLine($"Title: {shop.Title}");
                            writer.WriteLine($"Address: {shop.Address}");
                            writer.WriteLine($"Phone Number: {shop.PhoneNumber}");
                            writer.WriteLine($"Specialization: {shop.Specialization}");
                            writer.WriteLine($"Ownership Form: {shop.OwnershipForm}");
                            writer.WriteLine($"Working Hours: {shop.WorkingHours}");
                            writer.WriteLine();
                        }
                        MessageBox.Show("Data successfully saved to file.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void SwitchToAdminBtnOnClick(Form form)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            form.Hide();
        }
    }
}
