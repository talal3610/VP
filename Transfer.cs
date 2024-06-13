using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ATM_Software
{
    public partial class Transfer : Form
    {
        public Transfer()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\OneDrive\Documents\ATMdb.mdf;Integrated Security=True;Connect Timeout=30");

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void Transfer_Load(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select Balance from AccounTbl where AccNum='" + Login.AccNumber + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    label8.Text = dt.Rows[0][0].ToString();
                    label9.Text=Login.AccNumber;
                }
                else
                {
                    MessageBox.Show("Failed to retrieve account balance.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void addtransaction(string type,string acc, int amount)
        {
            string insertTransactionQuery = "INSERT INTO TransactionTbl (AccNum, Type, Amount, TDate) VALUES (@AccNum, @Type, @Amount, @TDate)";
            SqlCommand cmd = new SqlCommand(insertTransactionQuery, Con);
            cmd.Parameters.AddWithValue("@AccNum",acc);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@TDate", DateTime.Now);
            cmd.ExecuteNonQuery();
        }

        private int tamount = 0;
        string taccount = "";

        private void loginbtn_Click(object sender, EventArgs e)
        {
            if (Login.AccNumber != txt1.Text)
            {
                try
                {
                    Con.Open();

                    SqlDataAdapter sda = new SqlDataAdapter("select Balance from AccounTbl where AccNum='" + Login.AccNumber + "'", Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Sender account number does not exist.");
                        return;
                    }

                    int sbal = Convert.ToInt32(dt.Rows[0][0]);
                    SqlDataAdapter sda1 = new SqlDataAdapter("select Balance from AccounTbl where AccNum='" + txt1.Text + "'", Con);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);

                    if (dt1.Rows.Count == 0)
                    {
                        MessageBox.Show("Receiver account number does not exist.");
                        return;
                    }

                    if (int.TryParse(txt2.Text, out int transferAmount) && transferAmount > 0 && transferAmount <= sbal)
                    {
                        taccount = txt1.Text;
                        int rbal = Convert.ToInt32(dt1.Rows[0][0]);
                        sbal -= transferAmount;
                        string updateSenderQuery = "UPDATE AccounTbl SET Balance = @NewBalance WHERE AccNum = @AccNum";
                        using (SqlCommand cmd = new SqlCommand(updateSenderQuery, Con))
                        {
                            cmd.Parameters.AddWithValue("@NewBalance", sbal);
                            cmd.Parameters.AddWithValue("@AccNum", Login.AccNumber);
                            cmd.ExecuteNonQuery();
                        }
                        label8.Text = sbal.ToString();

                        rbal += transferAmount;
                        string updateReceiverQuery = "UPDATE AccounTbl SET Balance = @NewBalance WHERE AccNum = @AccNum";
                        using (SqlCommand cmd = new SqlCommand(updateReceiverQuery, Con))
                        {
                            cmd.Parameters.AddWithValue("@NewBalance", rbal);
                            cmd.Parameters.AddWithValue("@AccNum", txt1.Text);
                            cmd.ExecuteNonQuery();
                        }

                        tamount = transferAmount;
                        addtransaction("Transferred to "+taccount ,Login.AccNumber, tamount);
                        addtransaction("Received from " +Login.AccNumber, taccount, tamount);

                        MessageBox.Show("Transfer successful.");
                        txt1.Text = "";
                        txt2.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Invalid amount.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Con.Close();
                }
            }
            else
            {
                MessageBox.Show("Sender and Reciever account number could not be same or reciever number cannot be empty");
                txt1.Text = "";
                txt2.Text = "";
            }
        }
    }
}
