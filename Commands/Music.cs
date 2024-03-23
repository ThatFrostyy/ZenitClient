using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
namespace DiscordBot.Commands
{
    public class Music : BaseCommandModule
    {
        //
        // For music to work you MUST add ffmpeg to the ffmpeg folder.
        //
        private Process ffmpegProcess;

        [Command("join")]
        public async Task Join(CommandContext ctx, DiscordChannel channel)
        {
            try
            {
                if (channel == null || channel.Type != ChannelType.Voice)
                {
                    await ctx.RespondAsync("That's an invalid channel mate.");
                    return;
                }

                await ctx.Message.CreateReactionAsync(DiscordEmoji.FromUnicode("\U00002611"));
                await channel.ConnectAsync();
                await ctx.RespondAsync($"Successfully connected to the voice channel: {channel.Name}");
            }
            catch (Exception ex)
            {
                await ctx.RespondAsync("⚠️ There was an error executing the command. Check the console for more detail. ⚠️");
                Console.WriteLine($"COMMAND ERROR: {ex.Message}");
            }
        }

        [Command("play")]
        public async Task Play(CommandContext ctx)
        {
            try
            {
                var vnext = ctx.Client.GetVoiceNext();
                var connection = vnext.GetConnection(ctx.Guild);

                if (connection == null)
                {
                    await ctx.RespondAsync("You must add me to a voice channel first mate.");
                    return;
                }
                await ctx.Message.CreateReactionAsync(DiscordEmoji.FromUnicode("\U00002611"));

                var transmit = connection.GetTransmitSink();

                var pcm = ConvertAudioToPcm(ctx);
                await pcm.CopyToAsync(transmit);
                pcm.Dispose();
            }
            catch (Exception ex)
            {
                await ctx.RespondAsync("⚠️ There was an error executing the command. Check the console for more detail. ⚠️");
                Console.WriteLine($"COMMAND ERROR: {ex.Message}");
            }
        }

        [Command("leave")]
        public async Task Leave(CommandContext ctx)
        {
            try
            {
                var vnext = ctx.Client.GetVoiceNext();
                var connection = vnext.GetConnection(ctx.Guild);

                if (connection == null)
                {
                    await ctx.RespondAsync("I am not in a voice channel mate.");
                    return;
                }

                await ctx.Message.CreateReactionAsync(DiscordEmoji.FromUnicode("\U00002611"));
                connection.Disconnect();
                ffmpegProcess?.Kill();
                ffmpegProcess = null;
                await ctx.RespondAsync($"Successfully disconnected from the voice channel.");
            }
            catch (Exception ex)
            {
                await ctx.RespondAsync("⚠️ There was an error executing the command. Check the console for more detail. ⚠️");
                Console.WriteLine($"COMMAND ERROR: {ex.Message}");
            }
        }

        private Stream ConvertAudioToPcm(CommandContext ctx)
        {
            try
            {
                if (DiscordBot.Settings.RadioUrl == string.Empty)
                {
                    MessageBox.Show("You must enter a radio url first! (Client -> Options -> Radio URL)", "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"COMMAND ERROR: You must enter a radio url first! (Client -> Options -> Radio URL)");
                    ctx.RespondAsync("⚠️ Looks like the radio url wasn't setup correctly. Check your console for more detail. ⚠️");
                    return null;
                }

                ffmpegProcess = Process.Start(new ProcessStartInfo
                {
                    FileName = "./ffmpeg/ffmpeg",
                    Arguments = $@"-i ""{DiscordBot.Settings.RadioUrl}"" -ac 2 -f s16le -ar 48000 pipe:1",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                });

                return ffmpegProcess.StandardOutput.BaseStream;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"COMMAND ERROR: {ex.Message}");
                return null;
            }
        }
    }
}