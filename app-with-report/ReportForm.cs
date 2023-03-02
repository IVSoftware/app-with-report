using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app_with_login
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
            pictureBox.Visible = false;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.None;
        }
        protected async override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
            {
                labelProgress.Text = "Retrieving employee records...";
                progressBar.Value = 5;
                await Task.Delay(1000);
                labelProgress.Text = "Analyzing sales...";
                progressBar.Value = 25;
                await Task.Delay(1000);
                labelProgress.Text = "Updating employee commissions...";
                progressBar.Value = 50;
                await Task.Delay(1000);
                labelProgress.Text = "Initializing print job...";
                progressBar.Value = 75;
                await Task.Delay(1000);
                labelProgress.Text = "Success!";
                progressBar.Value = 100;
                await Task.Delay(1000);

                viewReport();
            }
        }

        private void viewReport()
        {
            string imagePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Images",
                "template.png"
            );
            pictureBox.Image = Image.FromFile( imagePath );
            Size = new Size(pictureBox.Image.Width + 10, pictureBox.Image.Height + 50);
            progressBar.Visible = false;
            pictureBox.Visible = true;
            FormBorderStyle= FormBorderStyle.Sizable;
        }
    }
}
