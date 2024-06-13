using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_Software
{
    public partial class Deposit : Form
    {
        public Deposit()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\OneDrive\Documents\ATMdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void addtransaction()
        {
            string insertTransactionQuery = "INSERT INTO TransactionTbl ( AccNum, Type, Amount, TDate) VALUES ( @AccNum, @Type, @Amount, @TDate)";

            SqlCommand cmd = new SqlCommand(insertTransactionQuery, Con);
            cmd.Parameters.AddWithValue("@AccNum", Login.AccNumber);
            cmd.Parameters.AddWithValue("@Type", "Deposit");
            cmd.Parameters.AddWithValue("@Amount", float.Parse(txtdeposit.Text));
            cmd.Parameters.AddWithValue("@TDate", DateTime.Now);
            cmd.ExecuteNonQuery();

        }
        private void loginbtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            float prevbalance = 0.0f;
            float updatedbalance = 0.0f;
            string fetchBalanceQuery = "SELECT Balance FROM AccounTbl WHERE AccNum = @AccNum";
            using (SqlCommand fetchBalanceCmd = new SqlCommand(fetchBalanceQuery, Con))
            {
                fetchBalanceCmd.Parameters.AddWithValue("@AccNum", Login.AccNumber);

                object result = fetchBalanceCmd.ExecuteScalar();
                if (result != null && float.TryParse(result.ToString(), out prevbalance))
                {
                    updatedbalance = prevbalance + float.Parse(txtdeposit.Text);

                    string updateBalanceQuery = "UPDATE AccounTbl SET Balance = @Balance WHERE AccNum = @AccNum";
                    using (SqlCommand updateBalanceCmd = new SqlCommand(updateBalanceQuery, Con))
                    {
                        updateBalanceCmd.Parameters.AddWithValue("@Balance", updatedbalance);
                        updateBalanceCmd.Parameters.AddWithValue("@AccNum", Login.AccNumber);

                        int rowsAffected = updateBalanceCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            addtransaction();

                            MessageBox.Show("Deposit successful");
                        }
                        else
                        {
                            MessageBox.Show("Deposit failed");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Account not found or invalid balance");
                }
                

             Con.Close();
             Home home = new Home();
             home.Show();
             this.Hide();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}
