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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void label5_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Balance balance = new Balance();
            balance.Show();
            this.Hide();

        }

        private void Home_Load(object sender, EventArgs e)
        {
            label2.Text="Account Number : "+Login.AccNumber;
        }
            private void loginbtn_Click(object sender, EventArgs e)
        {
            Deposit deposit = new Deposit();
            deposit.Show(); 
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangePin changePin = new ChangePin();
            changePin.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Withdraw withdraw = new Withdraw();
            withdraw.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ministatement ministatement = new Ministatement();  
            ministatement.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            HelpCentre helpCentre = new HelpCentre();
            helpCentre.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            QR qR = new QR();
            qR.Show();  
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Weather weather = new Weather();    
            weather.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Transfer transfer = new Transfer();
            transfer.Show();
            this.Hide();
        }
    }
}
