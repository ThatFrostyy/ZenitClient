using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class Tools: BaseCommandModule
    {
        [Command("ping")]
        public async Task Ping(CommandContext ctx)
        {
            var ping = ctx.Client.Ping;
            await ctx.RespondAsync(ping.ToString());
        }
    }
}
