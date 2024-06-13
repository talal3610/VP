using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Linq;
namespace ATM_Software
{
    public partial class Account : Form
    {
        public Account()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\OneDrive\Documents\ATMdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void Account_Load(object sender, EventArgs e)
        {

        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            float bal = 0;
            if(AccNumtb.Text == "" || AccNametb.Text=="" || Fanametb.Text=="" || Addresstb.Text=="" || Phonetb.Text=="" || Pintb.Text=="" || Educationtb.Text=="" || Occupationtb.Text=="" || Dobtb.Text == "")
            {
                MessageBox.Show("Missing Form Fields");
            }
            else
            {
                try
                {
                    Con.Open();
                    string q = "Insert into AccounTbl values (@AccNum,@Name,@faname,@Dob,@Phone,@Address,@Education,@Occupation,@Balance,@Pin)";
                    SqlCommand cmd = new SqlCommand(q, Con);
                    cmd.Parameters.AddWithValue("@AccNum", AccNumtb.Text);
                    cmd.Parameters.AddWithValue("@Name", AccNametb.Text);
                    cmd.Parameters.AddWithValue("@faname", Fanametb.Text);
                    cmd.Parameters.AddWithValue("@Dob", Dobtb.Text);
                    cmd.Parameters.AddWithValue("@Phone", Phonetb.Text);
                    cmd.Parameters.AddWithValue("@Address", Addresstb.Text);
                    cmd.Parameters.AddWithValue("@Education", Educationtb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Occupation", Occupationtb.Text);
                    cmd.Parameters.AddWithValue("@Balance", bal);
                    cmd.Parameters.AddWithValue("@Pin", Pintb.Text);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Account Created Succesfully");
                    Login login = new Login();
                    login.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Adminlogin adminlogin = new Adminlogin();
            adminlogin.Show();
            this.Hide();
        }
    }
}
