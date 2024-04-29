using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using MySql.Data.MySqlClient;
using System.Windows.Controls;
using OfficeOpenXml;
using System.IO;

namespace EDP
{
    public partial class Dasboard : Form
    {
        private int selectedAttorneyId;
        private int selectedProsecutorId;
        public Dasboard()
        {
            InitializeComponent();
        }

        private void case_btn_Click(object sender, EventArgs e)
        {
            Case caseform = new Case();
            caseform.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            LoadAttorneyData();
        }

        private void LoadAttorneyData()
        {
            try
            {
                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        // Query for retrieving data from the Attorneys table
                        string attorneyQuery = "SELECT attorney_id, attorney_lastname, attorney_firstname,attorney_middlename,attorney_office,attorney_phone,attorney_email,attorney_password,attorney_status FROM attorneys";
                        using (MySqlCommand attorneyCmd = new MySqlCommand(attorneyQuery, conn.Connection))
                        {
                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(attorneyCmd))
                            {
                                // Create a DataTable to hold the data
                                DataTable dataTable = new DataTable();

                                // Bind the DataTable to the DataGridView
                                dataGridView1.DataSource = dataTable;

                                // Fill the DataTable with the results from the query
                                adapter.Fill(dataTable);

                                // Clear existing columns in the DataGridView
                                dataGridView1.Columns.Clear();

                                // Set AutoGenerateColumns to false
                                dataGridView1.AutoGenerateColumns = false;

                                // Add columns to the DataGridView with specified widths
                                dataGridView1.Columns.Add("attorney_id", "ID");
                                dataGridView1.Columns["attorney_id"].DataPropertyName = "attorney_id";
                                dataGridView1.Columns["attorney_id"].Width = 100;

                                dataGridView1.Columns.Add("attorney_lastname", "LastName");
                                dataGridView1.Columns["attorney_lastname"].DataPropertyName = "attorney_lastname";
                                dataGridView1.Columns["attorney_lastname"].Width = 100;

                                dataGridView1.Columns.Add("attorney_firstname", "FirstName");
                                dataGridView1.Columns["attorney_firstname"].DataPropertyName = "attorney_firstname";
                                dataGridView1.Columns["attorney_firstname"].Width = 100;

                                dataGridView1.Columns.Add("attorney_middlename", "MiddleName");
                                dataGridView1.Columns["attorney_middlename"].DataPropertyName = "attorney_middlename";
                                dataGridView1.Columns["attorney_middlename"].Width = 100;

                                dataGridView1.Columns.Add("attorney_office", "Office");
                                dataGridView1.Columns["attorney_office"].DataPropertyName = "attorney_office";
                                dataGridView1.Columns["attorney_office"].Width = 100;

                                dataGridView1.Columns.Add("attorney_phone", "Phone");
                                dataGridView1.Columns["attorney_phone"].DataPropertyName = "attorney_phone";
                                dataGridView1.Columns["attorney_phone"].Width = 100;

                                dataGridView1.Columns.Add("attorney_email", "Email");
                                dataGridView1.Columns["attorney_email"].DataPropertyName = "attorney_email";
                                dataGridView1.Columns["attorney_email"].Width = 100;

                                dataGridView1.Columns.Add("attorney_password", "Password");
                                dataGridView1.Columns["attorney_password"].DataPropertyName = "attorney_password";
                                dataGridView1.Columns["attorney_password"].Width = 100;

                                dataGridView1.Columns.Add("attorney_status", "Status");
                                dataGridView1.Columns["attorney_status"].DataPropertyName = "attorney_status";
                                dataGridView1.Columns["attorney_status"].Width = 100;




                                // Set the font size for the DataGridView cells
                                dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10); // Change "Arial" and 10 to your desired font and size

                                // Set the font size for the DataGridView headers
                                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10); // Change "Arial" and 10 to your desired font and size
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            LoadProsecutorData();
        }
        private void LoadProsecutorData()
        {
            try
            {
                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        // Query for retrieving data from the Attorneys table
                        string prosecutorQuery = "SELECT prosecutor_id,prosecutor_lastname, prosecutor_firstname, prosecutor_middlename,prosecutor_office,prosecutor_phone,prosecutor_email,prosecutor_password,prosecutor_status FROM prosecutors";
                            
                        using (MySqlCommand attorneyCmd = new MySqlCommand(prosecutorQuery, conn.Connection))
                        {
                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(attorneyCmd))
                            {
                                // Create a DataTable to hold the data
                                DataTable dataTable = new DataTable();

                                // Bind the DataTable to the DataGridView
                                dataGridView1.DataSource = dataTable;

                                // Fill the DataTable with the results from the query
                                adapter.Fill(dataTable);

                                // Clear existing columns in the DataGridView
                                dataGridView1.Columns.Clear();

                                // Set AutoGenerateColumns to false
                                dataGridView1.AutoGenerateColumns = false;


                                // Add columns to the DataGridView with specified widths
                                dataGridView1.Columns.Add("prosecutor_id", "ID");
                                dataGridView1.Columns["prosecutor_id"].DataPropertyName = "prosecutor_id";
                                dataGridView1.Columns["prosecutor_id"].Width = 100;

                                dataGridView1.Columns.Add("prosecutor_lastname", "LastName");
                                dataGridView1.Columns["prosecutor_lastname"].DataPropertyName = "prosecutor_lastname";
                                dataGridView1.Columns["prosecutor_lastname"].Width = 100;

                                dataGridView1.Columns.Add("prosecutor_firstname", "FirstName");
                                dataGridView1.Columns["prosecutor_firstname"].DataPropertyName = "prosecutor_firstname";
                                dataGridView1.Columns["prosecutor_firstname"].Width = 100;

                                dataGridView1.Columns.Add("prosecutor_middlename", "MiddleName");
                                dataGridView1.Columns["prosecutor_middlename"].DataPropertyName = "prosecutor_middlename";
                                dataGridView1.Columns["prosecutor_middlename"].Width = 100;

                                dataGridView1.Columns.Add("prosecutor_office", "Office");
                                dataGridView1.Columns["prosecutor_office"].DataPropertyName = "prosecutor_office";
                                dataGridView1.Columns["prosecutor_office"].Width = 100;

                                dataGridView1.Columns.Add("prosecutor_phone", "Phone");
                                dataGridView1.Columns["prosecutor_phone"].DataPropertyName = "prosecutor_phone";
                                dataGridView1.Columns["prosecutor_phone"].Width = 100;

                                dataGridView1.Columns.Add("prosecutor_email", "Email");
                                dataGridView1.Columns["prosecutor_email"].DataPropertyName = "prosecutor_email";
                                dataGridView1.Columns["prosecutor_email"].Width = 100;

                                dataGridView1.Columns.Add("prosecutor_password", "Password");
                                dataGridView1.Columns["prosecutor_password"].DataPropertyName = "prosecutor_password";
                                dataGridView1.Columns["prosecutor_password"].Width = 100;

                                dataGridView1.Columns.Add("prosecutor_status", "Status");
                                dataGridView1.Columns["prosecutor_status"].DataPropertyName = "prosecutor_status";
                                dataGridView1.Columns["prosecutor_status"].Width = 100;




                                // Set the font size for the DataGridView cells
                                dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10); // Change "Arial" and 10 to your desired font and size

                                // Set the font size for the DataGridView headers
                                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10); // Change "Arial" and 10 to your desired font and size
                            }
                        }
                    }
                }
            }


            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void admin_add_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked) // Check if the radio button for attorney is selected
                {
                    AddAttorney();
                }
                else if (radioButton2.Checked) // Check if the radio button for prosecutor is selected
                {
                    AddProsecutor();
                }
                else
                {
                    MessageBox.Show("Please select either the Attorney or Prosecutor radio button.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddAttorney()
        {
            try
            {
                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        // Get values from textboxes
                        string lastName = last_box.Text;
                        string firstName = first_box.Text;
                        string middleName = middle_box.Text;
                        string office = office_box.Text;
                        string phone = phoneno_box.Text;
                        string email = email_add_box.Text;
                        string password = pass_add_box.Text;

                        // Query to insert values into the attorneys table
                        string insertQuery = "INSERT INTO attorneys (attorney_lastname, attorney_firstname, attorney_middlename, attorney_office, attorney_phone, attorney_email, attorney_password, attorney_status ) " +
                                             "VALUES (@lastName, @firstName, @middleName, @office, @phone, @email, @password, 'Active')";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn.Connection))
                        {
                            insertCmd.Parameters.AddWithValue("@lastName", lastName);
                            insertCmd.Parameters.AddWithValue("@firstName", firstName);
                            insertCmd.Parameters.AddWithValue("@middleName", middleName);
                            insertCmd.Parameters.AddWithValue("@office", office);
                            insertCmd.Parameters.AddWithValue("@phone", phone);
                            insertCmd.Parameters.AddWithValue("@email", email);
                            insertCmd.Parameters.AddWithValue("@password", password);

                            int rowsAffected = insertCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Attorney added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Optionally, clear the textboxes after successful insertion
                                ClearAttorneyTextBoxes();
                                LoadProsecutorData();
                            }
                            else
                            {
                                MessageBox.Show("Failed to add attorney.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error adding attorney: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddProsecutor()
        {
            try
            {
                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        // Get values from textboxes
                        string lastName = last_box.Text;
                        string firstName = first_box.Text;
                        string middleName = middle_box.Text;
                        string office = office_box.Text;
                        string phone = phoneno_box.Text;
                        string email = email_add_box.Text;
                        string password = pass_add_box.Text;

                        // Query to insert values into the prosecutors table
                        string insertQuery = "INSERT INTO prosecutors (prosecutor_lastname, prosecutor_firstname, prosecutor_middlename, prosecutor_office, prosecutor_phone, prosecutor_email, prosecutor_password, prosecutor_status ) " +
                                             "VALUES (@lastName, @firstName, @middleName, @office, @phone, @email, @password, 'Active')";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn.Connection))
                        {
                            insertCmd.Parameters.AddWithValue("@lastName", lastName);
                            insertCmd.Parameters.AddWithValue("@firstName", firstName);
                            insertCmd.Parameters.AddWithValue("@middleName", middleName);
                            insertCmd.Parameters.AddWithValue("@office", office);
                            insertCmd.Parameters.AddWithValue("@phone", phone);
                            insertCmd.Parameters.AddWithValue("@email", email);
                            insertCmd.Parameters.AddWithValue("@password", password);

                            int rowsAffected = insertCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Prosecutor added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Optionally, clear the textboxes after successful insertion
                                ClearAttorneyTextBoxes();
                                LoadAttorneyData();
                            }
                            else
                            {
                                MessageBox.Show("Failed to add prosecutor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error adding prosecutor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearAttorneyTextBoxes()
        {
            last_box.Text = string.Empty;
            first_box.Text = string.Empty;
            middle_box.Text = string.Empty;
            office_box.Text = string.Empty;
            phoneno_box.Text = string.Empty;
            email_add_box.Text = string.Empty;
            pass_add_box.Text = string.Empty;
        }

        private void admin_search_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = admin_search.Text.Trim();

            if (radioButton1.Checked)
            {
                if (int.TryParse(searchQuery, out _))
                {
                    // If the input is a valid integer, search by ID
                    SearchAttorneysById(searchQuery);
                }
                else
                {
                    // If the input is not a valid integer, search by name
                    SearchAttorneysByName(searchQuery);
                }
            }
            else if (radioButton2.Checked)
            {
                if (int.TryParse(searchQuery, out _))
                {
                    // If the input is a valid integer, search by ID
                    SearchProsecutorsById(searchQuery);
                }
                else
                {
                    // If the input is not a valid integer, search by name
                    SearchProsecutorsByName(searchQuery);
                }
            }
        }

        private void SearchAttorneysById(string id)
        {
            try
            {
                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        string searchQuery = "SELECT attorney_id, attorney_lastname, attorney_firstname, attorney_middlename, attorney_office, attorney_phone, attorney_email, attorney_password, attorney_status FROM attorneys " +
                                             "WHERE attorney_id = @id";

                        using (MySqlCommand searchCmd = new MySqlCommand(searchQuery, conn.Connection))
                        {
                            searchCmd.Parameters.AddWithValue("@id", id);

                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(searchCmd))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                dataGridView1.DataSource = dataTable;
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error searching attorneys by ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchAttorneysByName(string query)
        {
            try
            {
                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        string searchQuery = "SELECT attorney_id, attorney_lastname, attorney_firstname, attorney_middlename, attorney_office, attorney_phone, attorney_email, attorney_password, attorney_status FROM attorneys " +
                                             "WHERE attorney_lastname LIKE @query OR attorney_firstname LIKE @query";

                        using (MySqlCommand searchCmd = new MySqlCommand(searchQuery, conn.Connection))
                        {
                            searchCmd.Parameters.AddWithValue("@query", "%" + query + "%");

                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(searchCmd))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                dataGridView1.DataSource = dataTable;
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error searching attorneys by name: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchProsecutorsById(string id)
        {
            try
            {
                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        string searchQuery = "SELECT prosecutor_id, prosecutor_lastname, prosecutor_firstname, prosecutor_middlename, prosecutor_office, prosecutor_phone, prosecutor_email, prosecutor_password, prosecutor_status FROM prosecutors " +
                                             "WHERE prosecutor_id = @id";

                        using (MySqlCommand searchCmd = new MySqlCommand(searchQuery, conn.Connection))
                        {
                            searchCmd.Parameters.AddWithValue("@id", id);

                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(searchCmd))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                dataGridView1.DataSource = dataTable;
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error searching prosecutors by ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchProsecutorsByName(string query)
        {
            try
            {
                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        string searchQuery = "SELECT prosecutor_id, prosecutor_lastname, prosecutor_firstname, prosecutor_middlename, prosecutor_office, prosecutor_phone, prosecutor_email, prosecutor_password, prosecutor_status FROM prosecutors " +
                                             "WHERE prosecutor_lastname LIKE @query OR prosecutor_firstname LIKE @query";

                        using (MySqlCommand searchCmd = new MySqlCommand(searchQuery, conn.Connection))
                        {
                            searchCmd.Parameters.AddWithValue("@query", "%" + query + "%");

                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(searchCmd))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                dataGridView1.DataSource = dataTable;
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error searching prosecutors by name: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Check if a valid row is clicked
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Store the selected ID
                if (radioButton1.Checked) // Assuming radioButton1 is for Attorneys
                {
                    selectedAttorneyId = Convert.ToInt32(selectedRow.Cells["attorney_id"].Value);
                    PopulateAttorneyTextBoxes(selectedRow);
                }
                else if (radioButton2.Checked) // Assuming radioButton2 is for Prosecutors
                {
                    selectedProsecutorId = Convert.ToInt32(selectedRow.Cells["prosecutor_id"].Value);
                    PopulateProsecutorTextBoxes(selectedRow);
                }
            }
        }

        private void PopulateAttorneyTextBoxes(DataGridViewRow row)
        {
            last_box.Text = row.Cells["attorney_lastname"].Value.ToString();
            first_box.Text = row.Cells["attorney_firstname"].Value.ToString();
            middle_box.Text = row.Cells["attorney_middlename"].Value.ToString();
            office_box.Text = row.Cells["attorney_office"].Value.ToString();
            phoneno_box.Text = row.Cells["attorney_phone"].Value.ToString();
            email_add_box.Text = row.Cells["attorney_email"].Value.ToString();
            pass_add_box.Text = row.Cells["attorney_password"].Value.ToString();
        }

        private void PopulateProsecutorTextBoxes(DataGridViewRow row)
        {
            last_box.Text = row.Cells["prosecutor_lastname"].Value.ToString();
            first_box.Text = row.Cells["prosecutor_firstname"].Value.ToString();
            middle_box.Text = row.Cells["prosecutor_middlename"].Value.ToString();
            office_box.Text = row.Cells["prosecutor_office"].Value.ToString();
            phoneno_box.Text = row.Cells["prosecutor_phone"].Value.ToString();
            email_add_box.Text = row.Cells["prosecutor_email"].Value.ToString();
            pass_add_box.Text = row.Cells["prosecutor_password"].Value.ToString();
        }

        private void UpdateAttorney()
        {
            try
            {
                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        string updateQuery = "UPDATE attorneys " +
                                             "SET attorney_lastname = @lastname, " +
                                             "attorney_firstname = @firstname, " +
                                             "attorney_middlename = @middlename, " +
                                             "attorney_office = @office, " +
                                             "attorney_phone = @phone, " +
                                             "attorney_email = @email, " +
                                             "attorney_password = @password " +
                                             "WHERE attorney_id = @id";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn.Connection))
                        {
                            updateCmd.Parameters.AddWithValue("@id", selectedAttorneyId);
                            updateCmd.Parameters.AddWithValue("@lastname", last_box.Text);
                            updateCmd.Parameters.AddWithValue("@firstname", first_box.Text);
                            updateCmd.Parameters.AddWithValue("@middlename", middle_box.Text);
                            updateCmd.Parameters.AddWithValue("@office", office_box.Text);
                            updateCmd.Parameters.AddWithValue("@phone", phoneno_box.Text);
                            updateCmd.Parameters.AddWithValue("@email", email_add_box.Text);
                            updateCmd.Parameters.AddWithValue("@password", pass_add_box.Text);

                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Attorney updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadAttorneyData();
                            }
                            else
                            {
                                MessageBox.Show("Failed to update attorney. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error updating attorney: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateProsecutor()
        {
            try
            {
                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        string updateQuery = "UPDATE prosecutors " +
                                             "SET prosecutor_lastname = @lastname, " +
                                             "prosecutor_firstname = @firstname, " +
                                             "prosecutor_middlename = @middlename, " +
                                             "prosecutor_office = @office, " +
                                             "prosecutor_phone = @phone, " +
                                             "prosecutor_email = @email, " +
                                             "prosecutor_password = @password " +
                                             "WHERE prosecutor_id = @id";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn.Connection))
                        {
                            updateCmd.Parameters.AddWithValue("@id", selectedProsecutorId);
                            updateCmd.Parameters.AddWithValue("@lastname", last_box.Text);
                            updateCmd.Parameters.AddWithValue("@firstname", first_box.Text);
                            updateCmd.Parameters.AddWithValue("@middlename", middle_box.Text);
                            updateCmd.Parameters.AddWithValue("@office", office_box.Text);
                            updateCmd.Parameters.AddWithValue("@phone", phoneno_box.Text);
                            updateCmd.Parameters.AddWithValue("@email", email_add_box.Text);
                            updateCmd.Parameters.AddWithValue("@password", pass_add_box.Text);

                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Prosecutor updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadAttorneyData();
                            }
                            else
                            {
                                MessageBox.Show("Failed to update prosecutor. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error updating prosecutor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void admin_edit_btn_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) // Assuming radioButton1 is for Attorneys
            {
                UpdateAttorney();
            }
            else if (radioButton2.Checked) // Assuming radioButton2 is for Prosecutors
            {
                UpdateProsecutor();
            }
        }

        //Delete
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) // Assuming radioButton1 is for Attorneys
            {
                DeleteAttorney();
            }
            else if (radioButton2.Checked) // Assuming radioButton2 is for Prosecutors
            {
                DeleteProsecutor();
            }
        }

        private void DeleteProsecutor()
        {
            try
            {
                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        string updateQuery = "UPDATE prosecutors " +
                                             "SET prosecutor_status = @status " +
                                             "WHERE prosecutor_id = @id";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn.Connection))
                        {
                            string status = "Inactive";
                            updateCmd.Parameters.AddWithValue("@id", selectedProsecutorId);
                            updateCmd.Parameters.AddWithValue("@status", status);

                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Prosecutor Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadAttorneyData();
                            }
                            else
                            {
                                MessageBox.Show("Failed to Deleted prosecutor. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error Deleting prosecutor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteAttorney()
        {
            try
            {
                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        string updateQuery = "UPDATE attorneys " +
                                             "SET attorney_status = @status " +
                                             "WHERE attorney_id = @id";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn.Connection))
                        {
                            string status = "Inactive";
                            updateCmd.Parameters.AddWithValue("@id", selectedAttorneyId);
                            updateCmd.Parameters.AddWithValue("@status", status);

                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Attorney Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadAttorneyData();
                            }
                            else
                            {
                                MessageBox.Show("Failed to Deleted Attorney. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error Deleting Attorney: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            login login1 = new login();
            login1.Show();

        }

        private void Dasboard_Load(object sender, EventArgs e)
        {

        }

        private void report_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 Form3 = new Form3();
            Form3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) // Assuming radioButton1 is for Attorneys
            {
                ExportAttorney();
            }
            else if (radioButton2.Checked) // Assuming radioButton2 is for Prosecutors
            {
                ExportProsector();
            }
        }
        private void ExportAttorney()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            try
            {
                string templateFilePath = "C:\\Users\\angela\\Documents\\Report\\EMP_Summary.xlsx"; // Update the template file path as needed
                string outputFilePath = "C:\\Users\\angela\\Documents\\Report\\output_emp.xlsx"; // Update the output file path as needed

                // Check if the template file exists
                if (!File.Exists(templateFilePath))
                {
                    MessageBox.Show("Template Excel file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Load the template file into memory
                byte[] templateData = File.ReadAllBytes(templateFilePath);

                // Create a MemoryStream from the template data
                using (MemoryStream templateStream = new MemoryStream(templateData))
                {
                    // Open the template file using ExcelPackage
                    using (ExcelPackage existingPackage = new ExcelPackage(templateStream))
                    {
                        // Get Sheet 2 (assuming the name is "Sheet2", change as needed)
                        ExcelWorksheet worksheet = existingPackage.Workbook.Worksheets["Sheet1"];

                        // Find the last used row in the worksheet
                        int lastUsedRow = worksheet.Dimension?.End.Row ?? 1;

                        // Write the data starting from the next available row
                        int startRow = lastUsedRow + 2;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            // Write case_number
                            worksheet.Cells[startRow + i, 2].Value = dataGridView1.Rows[i].Cells["attorney_id"].Value;
                            // Write case_title
                            worksheet.Cells[startRow + i, 3].Value = dataGridView1.Rows[i].Cells["attorney_lastname"].Value;
                            // Write case_type
                            worksheet.Cells[startRow + i, 4].Value = dataGridView1.Rows[i].Cells["attorney_firstname"].Value;
                            // Write case_status
                            worksheet.Cells[startRow + i, 5].Value = dataGridView1.Rows[i].Cells["attorney_office"].Value;
                            worksheet.Cells[startRow + i, 6].Value = dataGridView1.Rows[i].Cells["attorney_phone"].Value;
                            worksheet.Cells[startRow + i, 7].Value = dataGridView1.Rows[i].Cells["attorney_status"].Value;
                        }
                        worksheet.Cells["C5"].Value = "Attorney Status Report";
                        int nextRow = lastUsedRow + 8; // Increment by 1 to get the next row

                        // Write "Prepared by" text in column H (index 8) of the next row
                        worksheet.Cells[nextRow, 8].Value = "Prepared by:";
                        ExcelWorksheet graphWorksheet = existingPackage.Workbook.Worksheets.Add("Sheet2");

                            // Count the number of active and inactive cases
                            int activeStatus = 0;
                            int inactiveStatus = 0;

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Cells["attorney_status"].Value?.ToString() == "Active")
                                {
                                    activeStatus++;
                                }
                                else if (row.Cells["attorney_status"].Value?.ToString() == "Inactive")
                                {
                                    inactiveStatus++;
                                }
                            }

                            // Add the counts to the graph worksheet
                            graphWorksheet.Cells["A1"].Value = "Attorney Status";
                            graphWorksheet.Cells["B1"].Value = "Number of Cases";
                            graphWorksheet.Cells["A2"].Value = "Active";
                            graphWorksheet.Cells["B2"].Value = activeStatus;
                            graphWorksheet.Cells["A3"].Value = "Inactive";
                            graphWorksheet.Cells["B3"].Value = inactiveStatus;

                            // Create a range for the data to be plotted in the graph
                            ExcelRange dataRange = graphWorksheet.Cells["B2:B3"]; // Adjusted to include only the counts of Active and Inactive

                            // Create a chart in the graph worksheet
                            var chart = graphWorksheet.Drawings.AddChart("CaseChart", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered);

                        // Set chart title
                        chart.Title.Text = "Attorney Employment Status";

                            // Set chart data
                            chart.Series.Add(dataRange, graphWorksheet.Cells["A2:A3"]); // Swapped the arguments to match the correct format

                        // Set category axis (X-axis) labels
                        chart.XAxis.Title.Text = " Employment Status";

                            // Set value axis (Y-axis) labels
                            chart.YAxis.Title.Text = "Number of Cases";

                            // Set the position and size of the chart
                            chart.SetPosition(0, 0, 0, 0);
                            chart.SetSize(800, 600);

                            // Assuming you have a variable 'loggedInUser' containing the name of the logged-in user
                            //string loggedInUser = "John Doe"; // Replace this with the actual name of the logged-in user
                            // worksheet.Cells[lastUsedRow + 3, 7].Value = login.LoggedInUser;


                            // Call the function to generate the graph in Sheet 2
                            //GeneratePieChart(worksheet);

                            // Save changes to the output Excel file
                            using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create))
                            {
                                existingPackage.SaveAs(outputFileStream);
                            }
                        }
                    }

                    MessageBox.Show("Data and graph appended to Excel file successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportProsector()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            try
            {
                string templateFilePath = "C:\\Users\\angela\\Documents\\Report\\EMP_Summary.xlsx"; // Update the template file path as needed
                string outputFilePath = "C:\\Users\\angela\\Documents\\Report\\output_emp.xlsx"; // Update the output file path as needed

                // Check if the template file exists
                if (!File.Exists(templateFilePath))
                {
                    MessageBox.Show("Template Excel file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Load the template file into memory
                byte[] templateData = File.ReadAllBytes(templateFilePath);

                // Create a MemoryStream from the template data
                using (MemoryStream templateStream = new MemoryStream(templateData))
                {
                    // Open the template file using ExcelPackage
                    using (ExcelPackage existingPackage = new ExcelPackage(templateStream))
                    {
                        // Get Sheet 2 (assuming the name is "Sheet2", change as needed)
                        ExcelWorksheet worksheet = existingPackage.Workbook.Worksheets["Sheet1"];

                        // Find the last used row in the worksheet
                        int lastUsedRow = worksheet.Dimension?.End.Row ?? 1;

                        // Write the data starting from the next available row
                        int startRow = lastUsedRow + 2;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            // Write case_number
                            worksheet.Cells[startRow + i, 2].Value = dataGridView1.Rows[i].Cells["prosecutor_id"].Value;
                            // Write case_title
                            worksheet.Cells[startRow + i, 3].Value = dataGridView1.Rows[i].Cells["prosecutor_lastname"].Value;
                            // Write case_type
                            worksheet.Cells[startRow + i, 4].Value = dataGridView1.Rows[i].Cells["prosecutor_firstname"].Value;
                            // Write case_status
                            worksheet.Cells[startRow + i, 5].Value = dataGridView1.Rows[i].Cells["prosecutor_office"].Value;

                            worksheet.Cells[startRow + i, 6].Value = dataGridView1.Rows[i].Cells["prosecutor_phone"].Value;
                            worksheet.Cells[startRow + i, 7].Value = dataGridView1.Rows[i].Cells["prosecutor_status"].Value;
                        }
                        worksheet.Cells["C5"].Value = "Prosecutor Employement Status";
                        int nextRow = lastUsedRow + 8; // Increment by 1 to get the next row

                        // Write "Prepared by" text in column H (index 8) of the next row
                        worksheet.Cells[nextRow, 2].Value = "Prepared by:";
                        ExcelWorksheet graphWorksheet = existingPackage.Workbook.Worksheets.Add("Sheet2");

                        // Count the number of active and inactive cases
                        int activeStatus = 0;
                        int inactiveStatus = 0;

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["prosecutor_status"].Value?.ToString() == "Active")
                            {
                                activeStatus++;
                            }
                            else if (row.Cells["prosecutor_status"].Value?.ToString() == "Inactive")
                            {
                                inactiveStatus++;
                            }
                        }

                        // Add the counts to the graph worksheet
                        graphWorksheet.Cells["A1"].Value = "Prosecutor Status";
                        graphWorksheet.Cells["B1"].Value = "Number of Cases";
                        graphWorksheet.Cells["A2"].Value = "Active";
                        graphWorksheet.Cells["B2"].Value = activeStatus;
                        graphWorksheet.Cells["A3"].Value = "Inactive";
                        graphWorksheet.Cells["B3"].Value = inactiveStatus;

                        // Create a range for the data to be plotted in the graph
                        ExcelRange dataRange = graphWorksheet.Cells["B2:B3"]; // Adjusted to include only the counts of Active and Inactive

                        // Create a chart in the graph worksheet
                        var chart = graphWorksheet.Drawings.AddChart("CaseChart", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered);

                        // Set chart title
                        chart.Title.Text = "Prosecutor Employment Status";

                        // Set chart data
                        chart.Series.Add(dataRange, graphWorksheet.Cells["A2:A3"]); // Swapped the arguments to match the correct format

                        // Set category axis (X-axis) labels
                        chart.XAxis.Title.Text = " Employment Status";

                        // Set value axis (Y-axis) labels
                        chart.YAxis.Title.Text = "Number of Cases";

                        // Set the position and size of the chart
                        chart.SetPosition(0, 0, 0, 0);
                        chart.SetSize(800, 600);
                        // Assuming you have a variable 'loggedInUser' containing the name of the logged-in user
                        //string loggedInUser = "John Doe"; // Replace this with the actual name of the logged-in user
                        // worksheet.Cells[lastUsedRow + 3, 7].Value = login.LoggedInUser;


                        // Call the function to generate the graph in Sheet 2
                        //GeneratePieChart(worksheet);

                        // Save changes to the output Excel file
                        using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create))
                        {
                            existingPackage.SaveAs(outputFileStream);
                        }

                    }

                    MessageBox.Show("Data and graph appended to Excel file successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
    



