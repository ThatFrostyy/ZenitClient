using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
namespace DiscordBot
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                DiscordBot.Settings.Token = tokenBox.Text;
                DiscordBot.Settings.Prefix = prefixBox.Text;
                DiscordBot.Settings.RadioUrl = radioBox.Text;

                var clientConfig = new
                {
                    Token = DiscordBot.Settings.Token,
                    Prefix = DiscordBot.Settings.Prefix,
                    RadioUrl = DiscordBot.Settings.RadioUrl
                };
                var json = JsonConvert.SerializeObject(clientConfig, Formatting.Indented);

                if (!Directory.Exists("./data"))
                {
                    Directory.CreateDirectory("./data");
                }
                File.WriteAllText("./data/config.json", json);

                MessageBox.Show("Successfully saved the client configuration.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("PROGRAM = ERROR");
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public void FillControls()
        {
            try
            {
                tokenBox.Text = DiscordBot.Settings.Token;
                prefixBox.Text = DiscordBot.Settings.Prefix;
                radioBox.Text = DiscordBot.Settings.RadioUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine("PROGRAM = ERROR");
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            e.Cancel = true;
            this.Hide();
        }
    }
}
