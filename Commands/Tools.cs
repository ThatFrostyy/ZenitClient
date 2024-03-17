using System.Threading;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.Entities;
namespace DiscordBot.Commands
{
    public class Tools: BaseCommandModule
    {
        [Command("Clear")]
        public async Task Clear(CommandContext ctx, int num)
        {
            if (num > 100)
            {
                await ctx.RespondAsync("You can't delete more than 100 messages with a single command.");
                return;
            }

            await ctx.Message.CreateReactionAsync(DiscordEmoji.FromUnicode("\U00002611"));
            var messages = await ctx.Channel.GetMessagesAsync(num);

            foreach (var message in messages)
            {
                await ctx.Channel.DeleteMessageAsync(message);
            }

            var response = await ctx.RespondAsync($"Successfully deleted {num} messages in {ctx.Channel.Name}.");
            Thread.Sleep(2000);
            await ctx.Channel.DeleteMessageAsync(response);
        }
    }
}
