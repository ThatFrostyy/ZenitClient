using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordBot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.VoiceNext;
using Microsoft.Extensions.Logging;
namespace DiscordBot
{
    internal static class Program
    {
        private static DiscordClient Client { get; set; }
        private static CommandsNextExtension Commands { get; set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static async Task BotSetup()
        {
            try
            {
                if (DiscordBot.Settings.Token == string.Empty)
                {
                    MessageBox.Show("You must enter a token first! (Client -> Options -> Token)", "Invalid Token", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"CLIENT ERROR: You must enter a token first! (Client -> Options -> Token)");
                    return;
                }

                var discordConfig = new DiscordConfiguration()
                {
                    Intents = DiscordIntents.All,
                    Token = DiscordBot.Settings.Token,
                    TokenType = TokenType.Bot,
                    AutoReconnect = true,
                    MinimumLogLevel = LogLevel.Critical
                };

                Client = new DiscordClient(discordConfig);
                Client.UseVoiceNext();
                Client.Ready += Client_Ready;

                if (DiscordBot.Settings.Prefix == string.Empty)
                {
                    MessageBox.Show("You must enter a prefix first! (Client -> Options -> Prefix)", "Invalid Prefix", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"CLIENT ERROR: You must enter a prefix first! (Client -> Options -> Prefix)");
                    return;
                }

                var commandsConfig = new CommandsNextConfiguration()
                {
                    StringPrefixes = new string[] { DiscordBot.Settings.Prefix },
                    EnableMentionPrefix = true,
                    EnableDms = true,
                    EnableDefaultHelp = false

                };

                Commands = Client.UseCommandsNext(commandsConfig);
                Commands.RegisterCommands<Music>();
                Commands.RegisterCommands<Tools>();
                Commands.RegisterCommands<General>();
                Commands.RegisterCommands<Custom>();

                Console.WriteLine("BOT CLIENT = CONNECT");

                await Client.ConnectAsync();
                await Task.Delay(-1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("BOT CLIENT = ERROR");
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public static async Task BotStop()
        {
            try
            {
                Console.WriteLine("BOT CLIENT = DISCONNECT");
                await Client.DisconnectAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("BOT CLIENT = ERROR");
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            Console.WriteLine("BOT CLIENT = OK");
            Console.WriteLine("BOT CLIENT = READY");
            return Task.CompletedTask;
        }
    }
}