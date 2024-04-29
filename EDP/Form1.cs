using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EDP
{
    public partial class Reset : Form
    {
        private DatabaseConnection dbConnection;  // Declare at the class level

        public static string Email { get; set; }
        public static string UserType { get; set; }

        public Reset()
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();  // Initialize in the constructor
        }

        private void reset_btn_Click(object sender, EventArgs e)
        {
            string confirmEmail = email_box.Text;

            try
            {
                if (dbConnection.OpenConnection())
                {
                    // Use parameterized queries to prevent SQL injection
                    string attorneyQuery = "SELECT COUNT(*) FROM Attorneys WHERE attorney_email = @confirmEmail";
                    MySqlCommand attorneyCmd = new MySqlCommand(attorneyQuery, dbConnection.Connection);
                    attorneyCmd.Parameters.AddWithValue("@confirmEmail", confirmEmail);
                    int attorneyCount = Convert.ToInt32(attorneyCmd.ExecuteScalar());

                    string prosecutorQuery = "SELECT COUNT(*) FROM Prosecutors WHERE prosecutor_email = @confirmEmail";
                    MySqlCommand prosecutorCmd = new MySqlCommand(prosecutorQuery, dbConnection.Connection);
                    prosecutorCmd.Parameters.AddWithValue("@confirmEmail", confirmEmail);
                    int prosecutorCount = Convert.ToInt32(prosecutorCmd.ExecuteScalar());

                    // Check if the email exists in either table
                    if (attorneyCount > 0)
                    {
                        MessageBox.Show("Email exists in Attorneys table!");
                        // Set the email and user type in Reset class
                        Reset.Email = confirmEmail;
                        Reset.UserType = "Attorney";
                        // Redirect to Form2
                        confirm_pass form2 = new confirm_pass(confirmEmail, Reset.UserType);
                        form2.Show();
                        this.Close();
                    }
                    else if (prosecutorCount > 0)
                    {
                        MessageBox.Show("Email exists in Prosecutors table!");
                        // Set the email and user type in Reset class
                        Reset.Email = confirmEmail;
                        Reset.UserType = "Prosecutor";
                        // Redirect to Form2
                        confirm_pass form2 = new confirm_pass(confirmEmail, Reset.UserType);
                        form2.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Email does not exist in either table");
                    }
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
            finally
            {
                dbConnection.CloseConnection();
            }
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login login1 = new login();
            login1.Show();
            this.Hide();
        }
    }
}
