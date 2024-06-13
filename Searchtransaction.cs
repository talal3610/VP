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
    public partial class Searchtransaction : Form
    {
        public Searchtransaction()
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


            dataGridView1.Rows.Clear();
            try
            {
                Con.Open();
                string query = "SELECT COUNT(*) FROM TransactionTbl WHERE Tid = @Tid";
                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    cmd.Parameters.AddWithValue("@Tid", txt1.Text);
                    int count = (int)cmd.ExecuteScalar();

                    if (count == 1)
                    {
                        DisplayTransactions(txt1.Text);
                    }
                    else
                    {
                        MessageBox.Show("Transaction ID does not exist");
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

        private void DisplayTransactions(string tid)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\OneDrive\Documents\ATMdb.mdf;Integrated Security=True;Connect Timeout=30";
            string query = "SELECT Tid, AccNum, Type, Amount, TDate FROM TransactionTbl WHERE Tid = @Tid";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Tid", tid);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {
                            dataGridView1.Rows.Add(row["Tid"], row["AccNum"], row["Type"], row["Amount"], row["TDate"]);
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
