using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace DiscordBot
{
    public partial class Form1 : Form
    {
        private readonly Form2 _form2;
        private readonly Form3 _form3;
        private readonly EmbedDesigner _embedDesigner;

        public Form1()
        {
            InitializeComponent();
            Console.SetOut(new TextBoxStreamWriter(consoleBox));

            _form2 = new Form2(this);
            _form3 = new Form3();
            _embedDesigner = new EmbedDesigner();
            if (Properties.Settings.Default.DarkMode)
            {
                _form2.darkMode.Checked = true;
            }

            LoadConfig();
        }

        private async void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                startButton.Enabled = false;
                stopButton.Enabled = true;

                await Program.BotSetup();
            }
            catch (Exception ex)
            {
                Console.WriteLine("BOT CLIENT = ERROR");
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        private async void stopButton_Click(object sender, EventArgs e)
        {
            try
            {
                stopButton.Enabled = false;
                startButton.Enabled = true;

                await Program.BotStop();
            }
            catch (Exception ex)
            {
                Console.WriteLine("BOT CLIENT = ERROR");
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _form2.Show();
        }

        private void optionsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _form3.Show();
            _form3.FillControls();
        }

        private void commandsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void embedDesignerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _embedDesigner.Show();
        }

        public void SetDark()
        {
            // Form1
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.LightGray;
            this.consoleBox.BackColor = Color.FromArgb(30, 30, 30);
            this.consoleBox.ForeColor = Color.LightGray;
            foreach (var button in this.Controls.OfType<Button>())
            {
                button.BackColor = Color.FromArgb(30, 30, 30);
                button.ForeColor = Color.LightGray;
            }

            // Form2
            _form2.BackColor = Color.FromArgb(30, 30, 30);
            _form2.ForeColor = Color.LightGray;

            // Form3
            _form3.BackColor = Color.FromArgb(30, 30, 30);
            _form3.ForeColor = Color.LightGray;
            foreach (var button in _form3.Controls.OfType<Button>())
            {
                button.BackColor = Color.FromArgb(30, 30, 30);
                button.ForeColor = Color.LightGray;
            }
        }

        public void SetLight()
        {
            // Form1
            this.BackColor = DefaultBackColor;
            this.ForeColor = DefaultBackColor;
            this.consoleBox.BackColor = DefaultBackColor;
            this.consoleBox.ForeColor = DefaultForeColor;
            foreach (var button in this.Controls.OfType<Button>())
            {
                button.BackColor = SystemColors.Control;
                button.ForeColor = SystemColors.ControlText;
            }

            // Form2
            _form2.BackColor = DefaultBackColor;
            _form2.ForeColor = DefaultForeColor;

            // Form3
            _form3.BackColor = DefaultBackColor;
            _form3.ForeColor = DefaultForeColor;
            foreach (var button in _form3.Controls.OfType<Button>())
            {
                button.BackColor = SystemColors.Control;
                button.ForeColor = SystemColors.ControlText;
            }
            foreach (var box in _form3.Controls.OfType<TextBox>())
            {
                box.BackColor = SystemColors.Control;
                box.ForeColor = SystemColors.ControlText;
            }
        }

        private void LoadConfig()
        {
            if (File.Exists("./data/config.json"))
            {
                var json = File.ReadAllText("./data/config.json");
                var discordBotConfig = JsonConvert.DeserializeObject<dynamic>(json);

                DiscordBot.Settings.Token = discordBotConfig.Token;
                DiscordBot.Settings.Prefix = discordBotConfig.Prefix;
                DiscordBot.Settings.RadioUrl = discordBotConfig.RadioUrl;

                Console.WriteLine("Config file was successfully loaded.");
            }
            else
            {
                Console.WriteLine("Config file was not found. Setup your client in (Client -> Options -> Client Configuration)");
            }
        }
    }

    public class TextBoxStreamWriter : TextWriter
    {
        private readonly TextBox _output = null;

        public TextBoxStreamWriter(TextBox output)
        {
            _output = output;
        }

        public override void WriteLine(string value)
        {
            base.WriteLine(value);
            AppendText($"{DateTime.Now}: {value}\r\n");
        }

        private void AppendText(string text)
        {
            if (_output.InvokeRequired)
            {
                _output.Invoke((MethodInvoker)delegate
                {
                    _output.AppendText(text);
                });
            }
            else
            {
                _output.AppendText(text);
            }
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}