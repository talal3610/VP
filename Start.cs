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
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Myprogress_Click(object sender, EventArgs e)
        {

        }

        private async Task LoadTask()
        {
            label2.Visible = true;
            for (int i = 0; i <= 100; i++)
            {
                await Task.Delay(20);
                progressBar1.Value = i;
                label2.Text = i.ToString() + "%";
            }

        }

        private async void Start_Load(object sender, EventArgs e)
        {
            label2.Text = "0";
            progressBar1.Value = 0;
            await LoadTask();
            Account account = new Account();
            account.Show();
            this.Hide();
        }
    }
}
