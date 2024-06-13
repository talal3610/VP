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
    public partial class Adminlogin : Form
    {
        public Adminlogin()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\OneDrive\Documents\ATMdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void loginbtn_Click(object sender, EventArgs e)
        {

            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from Admin where Id='" + txt1.Text + "' and pin=" +txt2.Text + "", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                Admin admin = new Admin();
                admin.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Account Number or Password");
            }
            Con.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            //Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
