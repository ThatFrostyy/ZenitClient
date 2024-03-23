using System;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using System.Drawing;
using DSharpPlus;
using System.Threading.Channels;
namespace DiscordBot.Commands
{
    public class General : BaseCommandModule
    {
        [Command("ping")]
        public async Task Ping(CommandContext ctx)
        {
            var ping = ctx.Client.Ping;
            await ctx.RespondAsync(ping.ToString());
        }

        [Command("help")]
        public async Task Help(CommandContext ctx)
        {
            var embed = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Black)
                    .WithTitle("Commands")
                    .WithDescription("!ping - Checks the bot's latency.\n\n" +
                                     "!join [channel] - Joins a voice channel.\n" +
                                     "!play - Plays the radio URL that was specified in the bot configuration.\n" +
                                     "!leave - Leaves a voice channel.\n\n" +
                                     "!clear [number] - Deletes a specified number of messages from the chat.\n" +
                                     "!ban [user] [reason] - Bans a specified member."));

            await ctx.RespondAsync(embed);
        }

        [Command("embed")]
        public async Task Embed(CommandContext ctx)
        {
            try
            {
                var color = new DiscordColor(DiscordBot.Settings.Color);

                var embed = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                        .WithTitle(DiscordBot.Settings.Title)
                        .WithDescription(DiscordBot.Settings.Description)
                        .WithUrl(DiscordBot.Settings.Url)
                        .WithColor(color)
                        .WithThumbnail(DiscordBot.Settings.ThumbnailUrl)
                        .WithImageUrl(DiscordBot.Settings.ImageUrl));

                var channel = await ctx.Client.GetChannelAsync(DiscordBot.Settings.ChannelId);
                if (channel == null || channel.Type != ChannelType.Text)
                {
                    await ctx.RespondAsync("That's an invalid channel mate.");
                    return;
                }

                await ctx.Message.DeleteAsync();
                await channel.SendMessageAsync(embed);
            }
            catch (Exception ex)
            {
                await ctx.RespondAsync("⚠️ There was an error executing the command. Check the console for more detail. ⚠️");
                Console.WriteLine($"COMMAND ERROR: {ex.Message}");
            }
        }
    }
}
