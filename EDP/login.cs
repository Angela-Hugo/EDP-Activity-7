using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace EDP
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            string username = user_txt.Text;
            string password = pass_txt.Text;

            using (DatabaseConnection dbConnection = new DatabaseConnection())
            {
                try
                {
                    if (dbConnection.OpenConnection())
                    {
                        // Query for retrieving data from the Attorneys table
                        string attorneyQuery = "SELECT * FROM attorneys WHERE attorney_email = @username AND attorney_password = @password AND attorney_status = 'Active'";
                        MySqlCommand attorneyCmd = new MySqlCommand(attorneyQuery, dbConnection.Connection);
                        attorneyCmd.Parameters.AddWithValue("@username", username);
                        attorneyCmd.Parameters.AddWithValue("@password", password);

                        using (MySqlDataReader attorneyDataReader = attorneyCmd.ExecuteReader())
                        {
                            // Check if the login is successful for Attorneys
                            if (attorneyDataReader.Read())
                            {
                                MessageBox.Show("Login successful as Attorney!");
                                // Additional actions for successful Attorney login

                                Case cases = new Case();
                                cases.Show();
                                this.Hide();
                                return;
                            }
                        }

                        // If not successful for Attorneys, try Prosecutors
                        string prosecutorQuery = "SELECT * FROM prosecutors WHERE prosecutor_email = @username AND prosecutor_password = @password AND prosecutor_status = 'Active'";
                        MySqlCommand prosecutorCmd = new MySqlCommand(prosecutorQuery, dbConnection.Connection);
                        prosecutorCmd.Parameters.AddWithValue("@username", username);
                        prosecutorCmd.Parameters.AddWithValue("@password", password);

                        using (MySqlDataReader prosecutorDataReader = prosecutorCmd.ExecuteReader())
                        {
                            // Check if the login is successful for Prosecutors
                            if (prosecutorDataReader.Read())
                            {
                                MessageBox.Show("Login successful as Prosecutor!");
                                // Additional actions for successful Prosecutor login

                                Case cases = new Case();
                                cases.Show();
                                this.Hide();
                                return;
                            }
                        }

                        // If not successful for Prosecutors, try Judges
                        string judgeQuery = "SELECT * FROM judges WHERE judge_email = @username AND judge_password = @password";
                        MySqlCommand judgeCmd = new MySqlCommand(judgeQuery, dbConnection.Connection);
                        judgeCmd.Parameters.AddWithValue("@username", username);
                        judgeCmd.Parameters.AddWithValue("@password", password);

                        using (MySqlDataReader judgeDataReader = judgeCmd.ExecuteReader())
                        {
                            // Check if the login is successful for Judges
                            if (judgeDataReader.Read())
                            {
                                MessageBox.Show("Login successful as Judge!");
                                // Additional actions for successful Judge login
                                Dasboard dashboard = new Dasboard();
                                dashboard.Show();
                                this.Hide();
                                return;
                            }
                        }

                        // If not successful for any role, show an error message
                        MessageBox.Show("Invalid username, password, or Inactive Status");
                    }
                    else
                    {
                        MessageBox.Show("Failed to connect to the database");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Reset reset = new Reset();
            reset.Show();
            this.Hide();
        }
    }
}
