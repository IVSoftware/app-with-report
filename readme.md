As I understand it from your post and comment, you have a report form with these requirements:

- Stays on top of the `Home` form. 
- Executes an animation.

***
To make it stay on top of the home form, use the `this` reference so that the home form is the `Owner` of the report form.

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

***
**Report**

To run animations on the report form, use async tasks for non-UI long running work, updating the UI thread in between the long running tasks.

[![animation][1]][1]

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
        .
        .
        .
    }

***
**Report view**

When the report is loaded, adjust the window parameters of the report form in its class.

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

[![report view][2]][2]

*Image Credit:* [Venngage.com](https://venngage.com/templates/reports/employee-daily-activity-report-c453f126-bc4c-407d-abbc-165ed2b3f109)


  [1]: https://i.stack.imgur.com/oT1zR.png
  [2]: https://i.stack.imgur.com/2Relx.png