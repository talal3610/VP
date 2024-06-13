using System;
using System.Drawing;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

namespace ATM_Software
{
    public partial class QR : Form
    {
        public QR()
        {
            InitializeComponent();
        }

        private void QR_Load(object sender, EventArgs e)
        {
            string text = Login.AccNumber ;
            EncodingOptions options = new EncodingOptions
            {
                Width = 200,
                Height = 200
            };

            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = options
            };
            Bitmap qrCodeImage = writer.Write(text);
            int x = (pictureBox1.Width - qrCodeImage.Width) / 2;
            int y = (pictureBox1.Height - qrCodeImage.Height) / 2;
            Bitmap centeredImage = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(centeredImage))
            {
                g.Clear(Color.White);
                g.DrawImage(qrCodeImage, new Point(x, y));
            }
            pictureBox1.Image = centeredImage;

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
