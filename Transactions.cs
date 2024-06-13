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
    public partial class Transactions : Form
    {
        public Transactions()
        {
            InitializeComponent();
        }

        private void Transactions_Load(object sender, EventArgs e)
        {
            try
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\OneDrive\Documents\ATMdb.mdf;Integrated Security=True;Connect Timeout=30";
                string query = "SELECT Tid, AccNum, Type, Amount, TDate FROM TransactionTbl";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.Columns.Clear();
                        dataGridView1.Columns.Add("Tid", "Tid");
                        dataGridView1.Columns.Add("AccNum", "AccNum");
                        dataGridView1.Columns.Add("Type", "Type");
                        dataGridView1.Columns.Add("Amount", "Amount");
                        dataGridView1.Columns.Add("TDate", "TDate");
                        
                        foreach (DataRow row in dt.Rows)
                        {
                            dataGridView1.Rows.Add(row["Tid"], row["AccNum"], row["Type"], row["Amount"], row["TDate"] );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while fetching Transactions details: " + ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
