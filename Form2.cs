using System;
using System.Windows.Forms;

namespace DiscordBot
{
    public partial class Form2 : Form
    {
        private readonly Form1 _form1;

        public Form2(Form1 form1)
        {
            InitializeComponent();
            this._form1 = form1;
        }

        private void darkMode_CheckedChanged(object sender, EventArgs e)
        {
            if (darkMode.Checked)
            {
                _form1.SetDark();
                Properties.Settings.Default.DarkMode = true;
            }
            else
            {
                _form1.SetLight();
                Properties.Settings.Default.DarkMode = false;
            }
            Properties.Settings.Default.Save();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            e.Cancel = true;
            this.Hide();
        }
    }
}
