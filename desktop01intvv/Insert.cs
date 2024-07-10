using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace desktop01intvv
{
    public partial class Insert : Form
    {
        public Insert()
        {
            InitializeComponent();
        }

        private void btninsert_Click(object sender, EventArgs e)
        {

            string name = txtname.Text;
            string age = txtage.Text;
            string address = txtaddress.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(age) || string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("All fields must be filled out.");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;
            string insertQuery = "INSERT INTO studentreg ( name, age, address) VALUES ( @name, @age, @address)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertQuery, connection);

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@address", address);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record inserted successfully.");
                        clearall();
                    }
                    else
                    {
                        MessageBox.Show("Failed to insert record.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }

            }
        }

        public void clearall()
        {
            txtname.Text = "";
            txtage.Text = "";
            txtaddress.Text = "";

        }



    }
}
