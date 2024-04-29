// DatabaseConnection class

using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

public class DatabaseConnection : IDisposable
{
    private MySqlConnection connection;
    private string server;
    private string database;
    private string uid;
    private string password;

    public DatabaseConnection()
    {
        Initialize();
    }

    private void Initialize()
    {
        server = "localhost";
        database = "case_management";
        uid = "root";
        password = "1234";
        string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

        connection = new MySqlConnection(connectionString);
    }

    public bool OpenConnection()
    {
        try
        {
            connection.Open();
            return true;
        }
        catch (MySqlException ex)
        {
            // Log the exception instead of showing a MessageBox
            Console.WriteLine("Error: " + ex.Message);
            return false;
        }
    }

    public bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            // Log the exception instead of showing a MessageBox
            Console.WriteLine("Error: " + ex.Message);
            return false;
        }
    }
    public static List<Tuple<string, string>> GetFirstAndLastNames()
    {
        List<Tuple<string, string>> names = new List<Tuple<string, string>>();

        using (DatabaseConnection conn = new DatabaseConnection())
        {
            if (conn.OpenConnection())
            {
                string query = "SELECT first_name, last_name FROM YourTableName"; // Update with your table name
                MySqlCommand cmd = new MySqlCommand(query, conn.connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName = reader.GetString("first_name");
                        string lastName = reader.GetString("last_name");
                        names.Add(new Tuple<string, string>(firstName, lastName));
                    }
                }
            }
        }

        return names;
    }


    public void InsertCase(string caseNumber, string caseTitle, string caseType, string caseDescription, string caseStatus, DateTime caseFilingDate, string courtLocation)
    {
        try
        {
            if (!IsConnectionOpen())
            {
                OpenConnection(); // Open the connection if it's not already open
            }

            string query = "INSERT INTO courtcases (case_number, case_title, case_type, case_description, case_status, case_filing_date, case_court_location) " +
                           "VALUES (@case_number, @case_title, @case_type, @case_description, @case_status, @case_filing_date, @case_court_location)";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@case_number", caseNumber);
                command.Parameters.AddWithValue("@case_title", caseTitle);
                command.Parameters.AddWithValue("@case_type", caseType);
                command.Parameters.AddWithValue("@case_description", caseDescription);
                command.Parameters.AddWithValue("@case_status", caseStatus);
                command.Parameters.AddWithValue("@case_filing_date", caseFilingDate);
                command.Parameters.AddWithValue("@case_court_location", courtLocation);

                command.ExecuteNonQuery();
            }

            MessageBox.Show("Case inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Refresh the ListBox after inserting a new case

        }
        catch (Exception ex)
        {
            MessageBox.Show("Error inserting case: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            CloseConnection();
        }
    }

    public void UpdateCaseInfo(string caseNumber, string updatedCaseTitle, string updatedCaseType, string updatedCaseDescription, string updatedCaseStatus, DateTime updatedCaseFilingDate, string updatedCourtLocation)
    {
        try
        {
            if (IsConnectionOpen())
            {
                string query = "UPDATE courtcases " +
                               "SET case_title = @updatedCaseTitle, case_type = @updatedCaseType, " +
                               "case_description = @updatedCaseDescription, case_status = @updatedCaseStatus, " +
                               "case_filing_date = @updatedCaseFilingDate, case_court_location = @updatedCourtLocation " +
                               "WHERE case_number = @caseNumber";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@caseNumber", caseNumber);
                    command.Parameters.AddWithValue("@updatedCaseTitle", updatedCaseTitle);
                    command.Parameters.AddWithValue("@updatedCaseType", updatedCaseType);
                    command.Parameters.AddWithValue("@updatedCaseDescription", updatedCaseDescription);
                    command.Parameters.AddWithValue("@updatedCaseStatus", updatedCaseStatus);
                    command.Parameters.AddWithValue("@updatedCaseFilingDate", updatedCaseFilingDate);
                    command.Parameters.AddWithValue("@updatedCourtLocation", updatedCourtLocation);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Case information updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Case not found for update. No rows affected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Error connecting to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error updating case information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            CloseConnection();
        }
    }
    public CaseInfo SearchCase(string caseNumber)
    {
        CaseInfo caseInfo = null;

        try
        {
            if (OpenConnection())
            {
                string query = "SELECT * FROM courtcases WHERE case_number = @caseNumber";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@caseNumber", caseNumber);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            caseInfo = new CaseInfo
                            {
                                CaseNumber = reader["case_number"].ToString(),
                                CaseTitle = reader["case_title"].ToString(),
                                CaseType = reader["case_type"].ToString(),
                                CaseDescription = reader["case_description"].ToString(),
                                CaseStatus = reader["case_status"].ToString(),
                                CaseFilingDate = Convert.ToDateTime(reader["case_filing_date"]),
                                CourtLocation = reader["case_court_location"].ToString()
                                // Add other properties accordingly
                            };
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Error connecting to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error searching for case: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            CloseConnection();
        }

        return caseInfo;
    }


    public List<CaseInfo> GetCases()
    {
        List<CaseInfo> cases = new List<CaseInfo>();

        try
        {
            string query = "SELECT * FROM courtcases";  // Replace 'your_table_name' with the actual name of your table

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Assuming CaseInfo is a class representing case information
                        CaseInfo caseInfo = new CaseInfo
                        {
                            CaseNumber = reader["case_number"].ToString(),
                            CaseTitle = reader["case_title"].ToString(),
                            // Add other properties accordingly
                        };

                        cases.Add(caseInfo);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving cases: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return cases;
    }
    public bool CheckEmailExists(string userEmail)
    {
        try
        {
            if (IsConnectionOpen())
            {
                // Check if the email exists in the Attorneys table
                string attorneyQuery = $"SELECT COUNT(*) FROM Attorneys WHERE attorney_email = '{userEmail}'";
                MySqlCommand attorneyCmd = new MySqlCommand(attorneyQuery, connection);
                int attorneyCount = Convert.ToInt32(attorneyCmd.ExecuteScalar());

                // Check if the email exists in the Prosecutors table
                string prosecutorQuery = $"SELECT COUNT(*) FROM Prosecutors WHERE prosecutor_email = '{userEmail}'";
                MySqlCommand prosecutorCmd = new MySqlCommand(prosecutorQuery, connection);
                int prosecutorCount = Convert.ToInt32(prosecutorCmd.ExecuteScalar());

                return (attorneyCount > 0 || prosecutorCount > 0);
            }
            else
            {
                MessageBox.Show("Error connecting to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error checking email existence: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }
    

    // Example of using a CaseInfo class
    public class CaseInfo
    {
        public string CaseNumber { get; set; }
        public string CaseTitle { get; set; }
        public string CaseType { get; set; }
        public string CaseDescription { get; set; }
        public string CaseStatus { get; set; }
        public DateTime CaseFilingDate { get; set; }
        public string CourtLocation { get; set; }
        // Add other properties accordingly
    }

    private bool IsConnectionOpen()
    {
        return connection.State == ConnectionState.Open;
    }

    public MySqlConnection Connection => connection;

    public void Dispose()
    {
        connection.Dispose();
    }
}


