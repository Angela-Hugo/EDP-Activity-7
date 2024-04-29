using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Windows.Forms;
using static OfficeOpenXml.LicenseContext;
using OfficeOpenXml.Drawing.Chart;
using System.IO;
using static DatabaseConnection;




namespace EDP
{
    public partial class Case : Form
    {
        private DatabaseConnection dbConnection;

        public Case()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            LoadCasesToDataGridView();


        }

        private void InitializeDatabaseConnection()
        {
            dbConnection = new DatabaseConnection();
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
                        string query = @"SELECT case_number, case_title, case_type, case_status, case_filing_date
                                 FROM courtcases";
                                 
                        MySqlCommand cmd = new MySqlCommand(query, conn.Connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                        // Fill the DataTable with the data
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Assuming you want to manually define columns
                        dataGridView1.Columns.Clear();

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


        private void add_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // Get values from your form controls
                string caseNumber = caseno_box.Text;
                string caseTitle = case_title_box.Text;
                string caseType = case_type_box.Text;
                string caseDescription = case_descr_box.Text;
                string caseStatus = case_status_box.Text;

                // Parse the DateTime from the filing_box TextBox
                if (DateTime.TryParse(filing_box.Text, out DateTime caseFilingDate))
                {
                    string courtLocation = court_loc_box.Text;

                    // Using statement ensures proper disposal of resources, including closing the connection
                    using (DatabaseConnection dbConnection = new DatabaseConnection())
                    {
                        // Insert into the database
                        if (dbConnection.OpenConnection())
                        {
                            try
                            {
                                // Use the InsertCase method from the DatabaseConnection class
                                dbConnection.InsertCase(caseNumber, caseTitle, caseType, caseDescription, caseStatus, caseFilingDate, courtLocation);

                                MessageBox.Show("Case inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error inserting case: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error connecting to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid date format in filing_box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions at a higher level
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void update_button_Click(object sender, EventArgs e)
        {
            try
            {
                // Get values from your form controls
                string caseNumberToUpdate = caseno_box.Text; // Assuming you get the case number from a textbox
                string updatedCaseTitle = case_title_box.Text; // Assuming you get the updated title from a textbox
                string updatedCaseType = case_type_box.Text; // Assuming you get the updated type from a textbox
                string updatedCaseDescription = case_descr_box.Text; // Assuming you get the updated description from a textbox
                string updatedCaseStatus = case_status_box.Text; // Assuming you get the updated status from a textbox

                // Parse the DateTime from the updated_filing_box TextBox
                if (DateTime.TryParse(filing_box.Text, out DateTime updatedCaseFilingDate))
                {
                    string updatedCourtLocation = court_loc_box.Text; // Assuming you get the updated court location from a textbox

                    // Using statement ensures proper disposal of resources, including closing the connection
                    using (dbConnection = new DatabaseConnection())
                    {
                        // Update the database
                        if (dbConnection.OpenConnection())
                        {
                            // Use the UpdateCaseInfo method from the DatabaseConnection class
                            dbConnection.UpdateCaseInfo(caseNumberToUpdate, updatedCaseTitle, updatedCaseType, updatedCaseDescription, updatedCaseStatus, updatedCaseFilingDate, updatedCourtLocation);

                            // Optionally, you can reload the cases in the ListBox after the update
                            LoadCasesToDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Error connecting to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid date format in updated_filing_box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions at a higher level
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        
            

        private void dash_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dasboard dashbo = new Dasboard();
            dashbo.Show();
        }
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = searchBox.Text.Trim();

            if (DateTime.TryParse(searchQuery, out _))
            {
                // If the input is a valid integer, search by ID
                SearchByDate(searchQuery);
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


        private void SearchByDate(string searchQuery)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        if (DateTime.TryParse(searchQuery, out DateTime parsedDate))
                        {
                            DateTime startDate = new DateTime(parsedDate.Year, parsedDate.Month, 1);
                            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                            string query = @"SELECT case_number, case_title, case_type, case_status, case_filing_date
                                     FROM courtcases
                                     WHERE case_filing_date BETWEEN @startDate AND @endDate";
                            MySqlCommand cmd = new MySqlCommand(query, conn.Connection);
                            cmd.Parameters.AddWithValue("@startDate", startDate);
                            cmd.Parameters.AddWithValue("@endDate", endDate);
                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            adapter.Fill(dataTable);
                        }
                        else if (int.TryParse(searchQuery, out int searchYear))
                        {
                            string query = @"SELECT case_number, case_title, case_type, case_status, case_filing_date
                                             FROM courtcases
                                             WHERE YEAR(case_filing_date) = @searchYear";
                            MySqlCommand cmd = new MySqlCommand(query, conn.Connection);
                            cmd.Parameters.AddWithValue("@searchYear", searchYear);
                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            adapter.Fill(dataTable);
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid date or year in the format YYYY-MM or YYYY.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

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
                        // Execute your SQL query to retrieve case information by case status
                        string query = "SELECT case_number, case_title, case_type, case_status, case_filing_date FROM courtcases WHERE case_status = @searchQuery";
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
                        string query = "SELECT case_number, case_title, case_type, case_status, case_filing_date FROM courtcases WHERE case_type LIKE @searchQuery";
                        MySqlCommand cmd = new MySqlCommand(query, conn.Connection);
                        cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");
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

        private void SearchByStatus(string searchQuery)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        // Execute your SQL query to retrieve case information by status
                        string query = "SELECT case_number, case_title, case_type, case_status, case_filing_date FROM courtcases WHERE case_status = @searchQuery";
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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            login login1 = new login();
            login1.Show();

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
                string templateFilePath = "C:\\Users\\angela\\Documents\\Report\\Case_Trend.xlsx"; // Update the template file path as needed
                string outputFilePath = "C:\\Users\\angela\\Documents\\Report\\output_trend.xlsx"; // Update the output file path as needed

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
                        ExcelWorksheet worksheet = existingPackage.Workbook.Worksheets["Data"];

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
                        }
                        int nextRow = lastUsedRow + 7;

                        // Write "Prepared by" text in column B of the next row
                        worksheet.Cells[nextRow, 2].Value = "Prepared by:";

                        // Call the function to generate the graph in Sheet 2
                        //GeneratePieChart(worksheet);
                        // Add a new worksheet for the graph
                        ExcelWorksheet graphWorksheet = existingPackage.Workbook.Worksheets.Add("Graph");

                        // Count the number of active and inactive cases
                        int activeCases = 0;
                        int inactiveCases = 0;

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["case_status"].Value?.ToString() == "Active")
                            {
                                activeCases++;
                            }
                            else if (row.Cells["case_status"].Value?.ToString() == "Inactive")
                            {
                                inactiveCases++;
                            }
                        }

                        // Add the counts to the graph worksheet
                        graphWorksheet.Cells["A1"].Value = "Case Status";
                        graphWorksheet.Cells["B1"].Value = "Number of Cases";
                        graphWorksheet.Cells["A2"].Value = "Active";
                        graphWorksheet.Cells["B2"].Value = activeCases;
                        graphWorksheet.Cells["A3"].Value = "Inactive";
                        graphWorksheet.Cells["B3"].Value = inactiveCases;

                        // Create a range for the data to be plotted in the graph
                        ExcelRange dataRange = graphWorksheet.Cells["B2:B3"]; // Adjusted to include only the counts of Active and Inactive

                        // Create a chart in the graph worksheet
                        var chart = graphWorksheet.Drawings.AddChart("CaseChart", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered);

                        // Set chart title
                        chart.Title.Text = "Case Status";

                        // Set chart data
                        chart.Series.Add(dataRange, graphWorksheet.Cells["A2:A3"]); // Swapped the arguments to match the correct format

                        // Set category axis (X-axis) labels
                        chart.XAxis.Title.Text = "Case Status";

                        // Set value axis (Y-axis) labels
                        chart.YAxis.Title.Text = "Number of Cases";

                        // Set the position and size of the chart
                        chart.SetPosition(0, 0, 0, 0);
                        chart.SetSize(800, 600);

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

 






        private bool IsFileLocked(string filePath)
        {
            try
            {
                using (File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return false;
                }
            }
            catch (IOException)
            {
                return true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Get the selected year from the DateTimePicker
            int selectedYear = dateTimePicker1.Value.Year;

            // Call the SearchByYear method with the selected year
            SearchByYear(selectedYear);
        }

        private void SearchByYear(int year)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        // Execute your SQL query to retrieve case information by year
                        string query = "SELECT case_number, case_title, case_type, case_status, case_filing_date FROM courtcases WHERE YEAR(case_filing_date) = @searchYear";
                        MySqlCommand cmd = new MySqlCommand(query, conn.Connection);
                        cmd.Parameters.AddWithValue("@searchYear", year);
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

        private void report_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 report = new Form4();
            report.ShowDialog();
        }
    }
}
  
       
