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
    public partial class HelpCentre : Form
    {
        public HelpCentre()
        {
            InitializeComponent();
        }

        private void HelpCentre_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string subject = "Contact Us";
                string body = richTextBox1.Text;
                string to = "talalkayani361@gmail.com";
                string mailtoUri = $"mailto:{to}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = mailtoUri,
                    UseShellExecute = true 
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to open the email client: " + ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
