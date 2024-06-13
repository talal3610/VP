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
    public partial class Userdetail : Form
    {
        public Userdetail()
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
        private void Userdetail_Load(object sender, EventArgs e)
        {
            try
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\OneDrive\Documents\ATMdb.mdf;Integrated Security=True;Connect Timeout=30";
                string query = "SELECT AccNum, Name, faname, Dob, Phone, Address, Education, Occupation, Balance, Pin FROM AccounTbl";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.Columns.Clear();
                        dataGridView1.Columns.Add("AccNum", "AccNum");
                        dataGridView1.Columns.Add("Name", "Name");
                        dataGridView1.Columns.Add("faname", "faname");
                        dataGridView1.Columns.Add("Dob", "Dob");
                        dataGridView1.Columns.Add("Phone", "Phone");
                        dataGridView1.Columns.Add("Address", "Address");
                        dataGridView1.Columns.Add("Education", "Education"); // Added missing Education column
                        dataGridView1.Columns.Add("Occupation", "Occupation");
                        dataGridView1.Columns.Add("Balance", "Balance");
                        dataGridView1.Columns.Add("Pin", "Pin");
                        foreach (DataRow row in dt.Rows)
                        {
                            dataGridView1.Rows.Add(row["AccNum"], row["Name"], row["faname"], row["Dob"], row["Phone"], row["Address"], row["Education"], row["Occupation"], row["Balance"], row["Pin"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while fetching user details: " + ex.Message);
            }
        }
    }
}
