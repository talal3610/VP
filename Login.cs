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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        public static String AccNumber;
        public static String Pin;
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\OneDrive\Documents\ATMdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void loginbtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda= new SqlDataAdapter("select count(*) from AccounTbl where AccNum='"+txtaccnum.Text+"' and Pin="+txtpin.Text+"",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                AccNumber=txtaccnum.Text;
                Pin=txtpin.Text;
                Home home = new Home();
                home.Show();
                this.Hide();
                //Con.Close();
            }
            else
            {
                MessageBox.Show("Invalid Account Number or Password");
            }
            Con.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            Account account = new Account();
            account.Show();
            this.Hide();
        }

        private void txtpin_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
