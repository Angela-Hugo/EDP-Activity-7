using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EDP
{
    public partial class confirm_pass : Form
    {
        private string email;
        private string userType;

        public confirm_pass(string email, string userType)
        {
            InitializeComponent();
            this.email = email;
            this.userType = userType;
        }

        private void reset_btn_Click(object sender, EventArgs e)
        {
            string newPassword = new_pass_box.Text;
            string confirmPassword = confirm_box.Text;

            // Check if the new password and confirm password match
            if (newPassword == confirmPassword)
            {
                // Passwords match, perform the desired actions
                MessageBox.Show("Passwords match. Password changed successfully!");

                // Update password in the database
                if (userType == "Attorney")
                {
                    UpdateAttorneyPassword(email, newPassword);
                }
                else if (userType == "Prosecutor")
                {
                    UpdateProsecutorPassword(email, newPassword);
                }
                else
                {
                    MessageBox.Show("User not found in the database.");
                }
            }
            else
            {
                // Passwords don't match, display a warning message
                MessageBox.Show("Passwords do not match. Please try again.");
                // Additional actions for password mismatch
            }
        }

        // Method to update attorney password in the database
        private void UpdateAttorneyPassword(string email, string newPassword)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();

            try
            {
                if (dbConnection.OpenConnection())
                {
                    string updateQuery = "UPDATE Attorneys SET attorney_password = @newPassword WHERE attorney_email = @email";
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, dbConnection.Connection))
                    {
                        cmd.Parameters.AddWithValue("@newPassword", newPassword);
                        cmd.Parameters.AddWithValue("@email", email);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Password updated for Attorney!");
                            login login1 = new login();
                            login1.Show();
                            this.Hide();

                        }
                        else
                        {
                            MessageBox.Show("Failed to update password for Attorney.");
                        }
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

        // Method to update prosecutor password in the database
        private void UpdateProsecutorPassword(string email, string newPassword)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();

            try
            {
                if (dbConnection.OpenConnection())
                {
                    string updateQuery = "UPDATE Prosecutors SET prosecutor_password = @newPassword WHERE prosecutor_email = @email";
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, dbConnection.Connection))
                    {
                        cmd.Parameters.AddWithValue("@newPassword", newPassword);
                        cmd.Parameters.AddWithValue("@email", email);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Password updated for Prosecutor!");

                            login login1 = new login();
                            login1.Show();
                            this.Hide();
                        }

                        else
                        {
                            MessageBox.Show("Failed to update password for Prosecutor.");
                        }
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
       
    }
}
