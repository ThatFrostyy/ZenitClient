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
            FillControls();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                DiscordBot.Bot.Token = tokenBox.Text;
                DiscordBot.Bot.Prefix = prefixBox.Text;
                DiscordBot.Bot.RadioUrl = radioBox.Text;

                var clientConfig = new
                {
                    Token = DiscordBot.Bot.Token,
                    Prefix = DiscordBot.Bot.Prefix,
                    RadioUrl = DiscordBot.Bot.RadioUrl
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

        private void FillControls()
        {
            try
            {
                tokenBox.Text = DiscordBot.Bot.Token;
                prefixBox.Text = DiscordBot.Bot.Prefix;
                radioBox.Text = DiscordBot.Bot.RadioUrl;
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
