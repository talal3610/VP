using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_Software
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Account account = new Account();
            account.Show();
            this.Hide();
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            Userdetail userdetail = new Userdetail();
            userdetail.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Transactions transactions = new Transactions();
            transactions.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Searchuser searchuser = new Searchuser();
            searchuser.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Searchtransaction searchtransaction = new Searchtransaction();
            searchtransaction.Show();
            this.Hide();
        }
    }
}
