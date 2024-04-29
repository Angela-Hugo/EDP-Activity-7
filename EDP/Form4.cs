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
using OfficeOpenXml.Drawing.Chart;


namespace EDP
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            LoadCasesToDataGridView();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

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
        private void generateReportButton_Click(object sender, EventArgs e)
        {
           
        }





        private void SearchByAttorneyName(string searchQuery)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (DatabaseConnection conn = new DatabaseConnection())
                {
                    if (conn.OpenConnection())
                    {
                        // Execute your SQL query to retrieve case information by Attorney Name (searching both first name and last name)
                        string query = @"SELECT c.case_id, c.case_number, c.case_title, c.case_type, 
                                 c.case_description, c.case_status, c.case_filing_date, 
                                 c.case_court_location, CONCAT(a.attorney_lastname, ', ', a.attorney_firstname) AS attorney_name
                                 FROM courtcases c
                                 INNER JOIN caseassignments ca ON c.case_id = ca.case_id
                                 INNER JOIN attorneys a ON ca.attorney_id = a.attorney_id
                                 WHERE a.attorney_firstname LIKE @searchQuery OR a.attorney_lastname LIKE @searchQuery";
                        MySqlCommand cmd = new MySqlCommand(query, conn.Connection);
                        cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%"); // Use % for wildcard search
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

        private void ExportToExcel()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            try
            {
                string templateFilePath = "C:\\Users\\angela\\Documents\\Report\\Case_Assignment.xlsx";//e the template file path as needed
                string outputFilePath = "C:\\Users\\angela\\Documents\\Report\\output_assign.xlsx";// Update the output file path as needed

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
                        int startRow = lastUsedRow + 1;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                           
                            // Write case_number
                            worksheet.Cells[startRow + i, 2].Value = dataGridView1.Rows[i].Cells["case_number"].Value;
                            // Write case_title
                            worksheet.Cells[startRow + i, 3].Value = dataGridView1.Rows[i].Cells["case_title"].Value;
                            // Write case_type
                            worksheet.Cells[startRow + i, 4].Value = dataGridView1.Rows[i].Cells["case_type"].Value;
                            // Write case_status
                            worksheet.Cells[startRow + i, 5].Value = dataGridView1.Rows[i].Cells["case_description"].Value;
                            worksheet.Cells[startRow + i, 6].Value = dataGridView1.Rows[i].Cells["case_status"].Value;
                            // Write case_filing_date
                            if (dataGridView1.Rows[i].Cells["case_filing_date"].Value != null)
                            {
                                DateTime filingDate = (DateTime)dataGridView1.Rows[i].Cells["case_filing_date"].Value;
                                worksheet.Cells[startRow + i, 7].Value = filingDate.ToString("yyyy-MM-dd");
                            }
                            worksheet.Cells[startRow + i, 8].Value = dataGridView1.Rows[i].Cells["case_court_location"].Value;


                        }
                        worksheet.Cells["C6"].Value = dataGridView1.Rows[0].Cells["attorney_name"].Value;
                       
                        int totalCases = 0;

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                // Check if the row is not null and not a new row (which might be blank)
                                if (row != null && !row.IsNewRow)
                                {
                                    totalCases++;
                                }
                            }

                        // Dictionary to store the total of each case type
                        Dictionary<string, int> caseTypeTotals = new Dictionary<string, int>();

                        // Loop through the DataGridView to calculate totals
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            object caseTypeObj = row.Cells["case_type"].Value;

                            // Check if the case type value is not null
                            if (caseTypeObj != null)
                            {
                                string caseType = caseTypeObj.ToString();

                                // Update the total for the current case type
                                if (caseTypeTotals.ContainsKey(caseType))
                                {
                                    caseTypeTotals[caseType]++;
                                }
                                else
                                {
                                    caseTypeTotals.Add(caseType, 1);
                                }
                            }
                        }

                        // Write the total number of cases to the Excel sheet
                        worksheet.Cells[lastUsedRow + 9, 8].Value = "Total Cases:";
                        worksheet.Cells[lastUsedRow + 9, 9].Value = totalCases;

                        // Write the total of each case type to the Excel sheet
                        int columnOffset = 8; // Starting from column H
                    
                        foreach (var kvp in caseTypeTotals)
                        {
                            worksheet.Cells[lastUsedRow + 10, columnOffset].Value = $"{kvp.Key} Total:";
                            worksheet.Cells[lastUsedRow + 10, columnOffset + 1].Value = kvp.Value;
                            lastUsedRow += 1;
                        }
                        int nextRow = lastUsedRow + 11;

                        // Write "Prepared by" text in column B of the next row
                        worksheet.Cells[nextRow, 2].Value = "Prepared by:";

                        // Add a new worksheet for the pie chart
                        ExcelWorksheet chartWorksheet = existingPackage.Workbook.Worksheets.Add("PieChart");

                        // Write data for pie chart
                        int rowCounter = 1;
                        foreach (var kvp in caseTypeTotals)
                        {
                            chartWorksheet.Cells[rowCounter, 1].Value = kvp.Key;
                            chartWorksheet.Cells[rowCounter, 2].Value = kvp.Value;
                            rowCounter++;
                        }

                        // Create a range for the data to be plotted in the pie chart
                        ExcelRange dataRange = chartWorksheet.Cells[1, 2, rowCounter - 1, 2];

                        // Add the pie chart to the worksheet (2D)
                        var pieChart = chartWorksheet.Drawings.AddChart("PieChart", OfficeOpenXml.Drawing.Chart.eChartType.Pie);
                        pieChart.SetPosition(0, 0, 0, 0);
                        pieChart.SetSize(600, 400);
                        pieChart.Series.Add(dataRange, chartWorksheet.Cells["A1:A" + (rowCounter - 1)]);
                        pieChart.Title.Text = "Cases Type Handled";


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

        private void searchBox_TextChanged_1(object sender, EventArgs e)
        {
            string searchQuery = searchBox.Text.Trim();

            SearchByAttorneyName(searchQuery);
        }

        private void dash_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            Dasboard dashbo = new Dasboard();
            dashbo.Show();
        }

        private void case_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            Case caseform = new Case();
            caseform.ShowDialog();
        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            login login1 = new login();
            login1.Show();
        }
    }
}
