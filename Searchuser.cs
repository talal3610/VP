using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_Software
{
    public partial class Searchuser : Form
    {
        public Searchuser()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\OneDrive\Documents\ATMdb.mdf;Integrated Security=True;Connect Timeout=30");

        private void loginbtn_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "SELECT COUNT(*) FROM AccounTbl WHERE AccNum = @AccNum";
                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    cmd.Parameters.AddWithValue("@AccNum", txt1.Text);
                    int count = (int)cmd.ExecuteScalar();

                    if (count == 1)
                    {
                        DisplayTransactions(txt1.Text);
                    }
                    else
                    {
                        MessageBox.Show("Account Number does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void DisplayTransactions(string accNum)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\OneDrive\Documents\ATMdb.mdf;Integrated Security=True;Connect Timeout=30";
            string query = "SELECT AccNum, Name, faname, Dob, Phone, Address, Education, Occupation, Balance, Pin FROM AccounTbl WHERE AccNum = @AccNum";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AccNum", accNum);
                        dataGridView1.Rows.Clear();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            dataGridView1.Rows.Add(row["AccNum"], row["Name"], row["faname"], row["Phone"], row["Dob"], row["Address"], row["Education"], row["Occupation"], row["Balance"], row["Pin"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching transaction details: " + ex.Message);
            }
        }
    }
}
