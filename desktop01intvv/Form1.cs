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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    DisplayGridView();
        //}
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            {
                string regno = txtregno.Text;
                if (string.IsNullOrWhiteSpace(regno))
                {
                    MessageBox.Show("Please enter a registration number.");
                    return;
                }

                string connectionString = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;
                string query = "SELECT Reg_no, name, age, address FROM studentreg WHERE Reg_no = @regno";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@regno", regno);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            txtregno1.Text = reader["reg_no"].ToString();
                            txtname.Text = reader["name"].ToString();
                            txtage.Text = reader["age"].ToString();
                            txtaddress.Text = reader["address"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No records found.");
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}");
                    }
                }
            }
        }
        public void DisplayGridView()
        {


            string connectionString = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;


            string query = "SELECT Reg_no, name, age, address FROM studentreg";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand command = new SqlCommand(query, connection))

                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    try
                    {
                        connection.Open();
                        dataAdapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }


                }
            }
        }
        private void btnedit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtregno1.Text))
            {
                MessageBox.Show("Type reg no and search in above.");
                return;
            }

            string regno = txtregno1.Text;
            string name = txtname.Text;
            string age = txtage.Text;
            string address = txtaddress.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(age) || string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("All fields must be filled out.");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;
            string updateQuery = "UPDATE studentreg SET name = @name, age = @age, address = @address WHERE reg_no = @regno";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@regno", regno);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@address", address);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No records found to update.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void btnback_Click(object sender, EventArgs e)
        {


        }

        private void btninsertnew_Click(object sender, EventArgs e)
        {
            this.Hide();
            Insert mainForm = new Insert();
            mainForm.Show();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            DisplayGridView();
        }

        private void btnreport_Click(object sender, EventArgs e)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Reg_no, name, age, address FROM studentreg";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();

                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataSet, "StudentDataTable");

                    // Assuming your Crystal Report is named CrystalReport1
                    CrystalReport1 report = new CrystalReport1();
                    report.SetDataSource(dataSet.Tables["StudentDataTable"]);

                    // Assuming you have a CrystalReportViewer named crystalReportViewer1
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
    }

}
