using System;
using System.Windows.Forms;
using ToolTip = System.Windows.Forms.ToolTip;
namespace DiscordBot
{
    public partial class EmbedDesigner : Form
    {
        private readonly ToolTip toolTip;

        public EmbedDesigner()
        {
            InitializeComponent();

            toolTip = new ToolTip();
            toolTip.SetToolTip(colorBox, "You MUST fill this with a hex color code");
            toolTip.SetToolTip(colorBox, "You MUST fill this with a channel id");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DiscordBot.Settings.ChannelId = string.IsNullOrEmpty(channelBox.Text) ? 0 : ulong.Parse(channelBox.Text);
                DiscordBot.Settings.Title = titleBox.Text;
                DiscordBot.Settings.Description = descriptionBox.Text;
                DiscordBot.Settings.Url = urlBox.Text;
                DiscordBot.Settings.Color = colorBox.Text;
                DiscordBot.Settings.ThumbnailUrl = thumbnailBox.Text;
                DiscordBot.Settings.ImageUrl = imageBox.Text;

                MessageBox.Show("Successfully saved the embed configuration.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
