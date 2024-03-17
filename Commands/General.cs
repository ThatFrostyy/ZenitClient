using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DiscordBot.Commands
{
    public class General: BaseCommandModule
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
                                     "!clear [number] - Deletes a specified number of messages from the chat."));

            await ctx.RespondAsync(embed);
        }
    }
}
