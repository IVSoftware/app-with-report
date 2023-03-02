using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app_with_login
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
            buttonReport.Click += onClickReport;
        }

        private void onClickReport(object sender, EventArgs e)
        {
            using (var report = new ReportForm())
            {
                // Use `this` to make Home form the parent
                // so that child windows will be on top.
                report.ShowDialog(this);
            }
        }
    }
}
