using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OfficeOpenXml;

namespace EDP
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            LoadCasesToDataGridView();
        }
        private void LoadCasesToDataGridView()
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        // Execute your SQL query to retrieve case information
                        string query = @"SELECT c.case_id, c.case_number, c.case_title, c.case_type, 
                                        c.case_description, c.case_status, c.case_filing_date, 
                                        c.case_court_location, CONCAT(a.attorney_lastname, ', ', a.attorney_firstname) AS attorney_name
                                        FROM courtcases c
                                        INNER JOIN caseassignments ca ON c.case_id = ca.case_id
                                        INNER JOIN attorneys a ON ca.attorney_id = a.attorney_id";

                        MySqlCommand cmd = new MySqlCommand(query, conn.Connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                        // Fill the DataTable with the data
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Assuming you want to manually define columns
                        dataGridView1.Columns.Clear();

                        dataGridView1.Columns.Add("case_id", "case id");
                        dataGridView1.Columns["case_id"].DataPropertyName = "case_id";
                        dataGridView1.Columns["case_id"].Width = 100;

                        dataGridView1.Columns.Add("case_number", "Case Number");
                        dataGridView1.Columns["case_number"].DataPropertyName = "case_number";
                        dataGridView1.Columns["case_number"].Width = 100;

                        dataGridView1.Columns.Add("case_title", "Case Title");
                        dataGridView1.Columns["case_title"].DataPropertyName = "case_title";
                        dataGridView1.Columns["case_title"].Width = 100;

                        dataGridView1.Columns.Add("case_type", "Case Type");
                        dataGridView1.Columns["case_type"].DataPropertyName = "case_type";
                        dataGridView1.Columns["case_title"].Width = 100;

                        dataGridView1.Columns.Add("case_status", "Case status");
                        dataGridView1.Columns["case_status"].DataPropertyName = "case_status";
                        dataGridView1.Columns["case_status"].Width = 100;

                        dataGridView1.Columns.Add("case_filing_date", "Filing Date");
                        dataGridView1.Columns["case_filing_date"].DataPropertyName = "case_filing_date";
                        dataGridView1.Columns["case_filing_date"].Width = 100;

                        dataGridView1.Columns.Add("case_description", "Case Description");
                        dataGridView1.Columns["case_description"].DataPropertyName = "case_description";
                        dataGridView1.Columns["case_description"].Width = 100;

                        dataGridView1.Columns.Add("case_court_location", "case_court_location ");
                        dataGridView1.Columns["case_court_location"].DataPropertyName = "case_court_location";
                        dataGridView1.Columns["case_court_location"].Width = 100;

                        dataGridView1.Columns.Add("attorney_name", "Attorney Name");
                        dataGridView1.Columns["attorney_name"].DataPropertyName = "attorney_name";
                        dataGridView1.Columns["attorney_name"].Width = 100;

                        dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10); // Change "Arial" and 10 to your desired font and size

                        // Set the font size for the DataGridView headers
                        dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);
                    }
                    else
                    {
                        MessageBox.Show("Error connecting to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = searchBox.Text.Trim();

            if (DateTime.TryParse(searchQuery, out _))
            {
                // If the input is a valid integer, search by ID
                SearchByDate(searchQuery);
            }
            else if (searchQuery.StartsWith("ATT"))
            {
                // If the input starts with "ATT", search by Attorney ID
                string attorneyID = searchQuery.Substring(3); // Remove "ATT" prefix
                SearchByAttorneyID(attorneyID);
            }
            else if (searchQuery.StartsWith("PROS"))
            {
                // If the input starts with "PROS", search by Prosecutor ID
                string prosecutorID = searchQuery.Substring(4); // Remove "PROS" prefix
                SearchByProsecutorID(prosecutorID);
            }
            else if (searchQuery.Equals("ACTIVE", StringComparison.OrdinalIgnoreCase) || searchQuery.Equals("INACTIVE", StringComparison.OrdinalIgnoreCase))
            {
                // If the input matches "ACTIVE" or "CLOSED", search by Case Status
                SearchByCaseStatus(searchQuery.ToUpper()); // Convert to uppercase for case-insensitive comparison
            }
            else
            {
                // If the input is not a valid integer or attorney/prosecutor ID, search by name
                SearchByType(searchQuery);
            }
        }



        private void SearchByAttorneyID(string searchQuery)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        // Execute your SQL query to retrieve case information by Attorney ID
                        string query = @"SELECT courtcases.case_number, courtcases.case_title, courtcases.case_type, courtcases.case_status, courtcases.case_filing_date, 
                                 caseassignments.attorney_ID, caseassignments.prosecutor_ID 
                                 FROM courtcases 
                                 INNER JOIN caseassignments ON courtcases.case_ID = caseassignments.case_ID
                                 WHERE attorney_ID = @searchQuery";
                        MySqlCommand cmd = new MySqlCommand(query, conn.Connection);
                        cmd.Parameters.AddWithValue("@searchQuery", searchQuery);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                        // Fill the DataTable with the data
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("Error connecting to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchByProsecutorID(string searchQuery)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        // Execute your SQL query to retrieve case information by Prosecutor ID
                        string query = @"SELECT courtcases.case_number, courtcases.case_title, courtcases.case_type, courtcases.case_status, courtcases.case_filing_date, 
                                 caseassignments.attorney_ID, caseassignments.prosecutor_ID 
                                 FROM courtcases 
                                 INNER JOIN caseassignments ON courtcases.case_ID = caseassignments.case_ID
                                 WHERE prosecutor_ID = @searchQuery";
                        MySqlCommand cmd = new MySqlCommand(query, conn.Connection);
                        cmd.Parameters.AddWithValue("@searchQuery", searchQuery);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                        // Fill the DataTable with the data
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("Error connecting to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchByCaseStatus(string searchQuery)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        // Execute your SQL query to retrieve case information by Case Status
                        string query = @"SELECT courtcases.case_number, courtcases.case_title, courtcases.case_type, courtcases.case_status, courtcases.case_filing_date, 
                                 caseassignments.attorney_ID, caseassignments.prosecutor_ID 
                                 FROM courtcases 
                                 INNER JOIN caseassignments ON courtcases.case_ID = caseassignments.case_ID 
                                 WHERE case_status = @status";
                        MySqlCommand cmd = new MySqlCommand(query, conn.Connection);
                        cmd.Parameters.AddWithValue("@status", searchQuery);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                        // Fill the DataTable with the data
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("Error connecting to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchByType(string searchQuery)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        // Execute your SQL query to retrieve case information by type
                        string query = @"SELECT courtcases.case_number, courtcases.case_title, courtcases.case_type, courtcases.case_status, courtcases.case_filing_date, 
                                 caseassignments.attorney_ID, caseassignments.prosecutor_ID 
                                 FROM courtcases 
                                 INNER JOIN caseassignments ON courtcases.case_ID = caseassignments.case_ID 
                                 WHERE case_type LIKE @searchQuery";
                        MySqlCommand cmd = new MySqlCommand(query, conn.Connection);
                        cmd.Parameters.AddWithValue("@searchQuery", $"%{searchQuery}%");
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                        // Fill the DataTable with the data
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("Error connecting to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SearchByDate(string searchQuery)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        // Parse the searchQuery string to a DateTime object
                        if (DateTime.TryParse(searchQuery, out DateTime date))
                        {
                            // Format the date string in a format compatible with MySQL (YYYY-MM-DD)
                            string formattedDate = date.ToString("yyyy-MM-dd");

                            // Execute your SQL query to retrieve case information by date
                            string query = @"SELECT courtcases.case_number, courtcases.case_title, courtcases.case_type, courtcases.case_status, courtcases.case_filing_date, 
                                     caseassignments.attorney_ID, caseassignments.prosecutor_ID 
                                     FROM courtcases 
                                     INNER JOIN caseassignments ON courtcases.case_ID = caseassignments.case_ID 
                                     WHERE DATE(case_filing_date) = @searchDate";
                            MySqlCommand cmd = new MySqlCommand(query, conn.Connection);
                            cmd.Parameters.AddWithValue("@searchDate", formattedDate);
                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                            // Fill the DataTable with the data
                            adapter.Fill(dataTable);

                            // Bind the DataTable to the DataGridView
                            dataGridView1.DataSource = dataTable;
                        }
                        else
                        {
                            // Display a message if the input is not a valid date
                            MessageBox.Show("Please enter a valid date in the format YYYY-MM-DD.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error connecting to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void generateReportButton_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
        private void ExportToExcel()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            try
            {
                string templateFilePath = "C:\\Users\\angela\\Documents\\Report\\Case_Assignment.xlsx"; // Update the template file path as needed
                string outputFilePath = "C:\\Users\\angela\\Documents\\Report\\output_cases.xlsx"; // Update the output file path as needed

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
                            worksheet.Cells[startRow + i, 2].Value = dataGridView1.Rows[i].Cells["case_number"].Value;
                            // Write case_title
                            worksheet.Cells[startRow + i, 3].Value = dataGridView1.Rows[i].Cells["case_title"].Value;
                            // Write case_type
                            worksheet.Cells[startRow + i, 4].Value = dataGridView1.Rows[i].Cells["case_type"].Value;
                            // Write case_status
                            worksheet.Cells[startRow + i, 5].Value = dataGridView1.Rows[i].Cells["case_status"].Value;
                            // Write case_filing_date
                            if (dataGridView1.Rows[i].Cells["case_filing_date"].Value != null)
                            {
                                DateTime filingDate = (DateTime)dataGridView1.Rows[i].Cells["case_filing_date"].Value;
                                worksheet.Cells[startRow + i, 6].Value = filingDate.ToString("yyyy-MM-dd");
                            }
                            worksheet.Cells[startRow + i, 7].Value = dataGridView1.Rows[i].Cells["attorney_ID"].Value;
                            worksheet.Cells[startRow + i, 8].Value = dataGridView1.Rows[i].Cells["prosecutor_ID"].Value;
                        }
                        worksheet.Cells[lastUsedRow + 3, 6].Value = "Prepared by:";
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


        private void button2_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
    }


}


