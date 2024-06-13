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
    public partial class Withdraw : Form
    {
        public Withdraw()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\OneDrive\Documents\ATMdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Withdraw_Load(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Balance from AccounTbl where AccNum='" + Login.AccNumber+"'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label8.Text = dt.Rows[0][0].ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
        private void addtransaction()
        {
            string insertTransactionQuery = "INSERT INTO TransactionTbl ( AccNum, Type, Amount, TDate) VALUES ( @AccNum, @Type, @Amount, @TDate)";

            SqlCommand cmd = new SqlCommand(insertTransactionQuery, Con);
            cmd.Parameters.AddWithValue("@AccNum", Login.AccNumber);
            cmd.Parameters.AddWithValue("@Type", "Withdrawal");
            cmd.Parameters.AddWithValue("@Amount", withdrawalAmount);
            cmd.Parameters.AddWithValue("@TDate", DateTime.Now);
            cmd.ExecuteNonQuery();
        }
        private float withdrawalAmount=0.0f;
        private void loginbtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt1.Text) && float.TryParse(txt1.Text, out withdrawalAmount) && withdrawalAmount > 0)
            {
                float currentBalance = float.Parse(label8.Text);

                if (withdrawalAmount > currentBalance)
                {
                    MessageBox.Show("Insufficient Amount");
                    txt1.Text = "";
                }
                else
                {
                    float newBalance = currentBalance - withdrawalAmount;

                    try
                    {
                        using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\OneDrive\Documents\ATMdb.mdf;Integrated Security=True;Connect Timeout=30"))
                        {
                            con.Open();
                            string query = "UPDATE AccounTbl SET Balance = @NewBalance WHERE AccNum = @AccNum";

                            using (SqlCommand cmd = new SqlCommand(query, con))
                            {
                                cmd.Parameters.AddWithValue("@NewBalance", newBalance);
                                cmd.Parameters.AddWithValue("@AccNum", Login.AccNumber);

                                cmd.ExecuteNonQuery();
                                label8.Text = newBalance.ToString();
                                addtransaction();
                                MessageBox.Show("Withdrawal successful.");
                                
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Amount field is missing or invalid input. Please enter a valid amount.");
            }
            Home home = new Home();
            home.Show();
            this.Hide();
        }
        
    }
}
