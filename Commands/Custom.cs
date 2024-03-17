using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.VoiceNext;
using DSharpPlus.Interactivity;
using DSharpPlus;
using DSharpPlus.Entities;
namespace DiscordBot.Commands
{
    public class Custom : BaseCommandModule
    {
        // Example 1
        [Command("example")]
        public async Task Example(CommandContext ctx)
        {
            await ctx.RespondAsync("You just used an example command!");
        }

        // Example 2
        [Command("example2")]
        public async Task Example2(CommandContext ctx, int num1, int num2)
        {
            var num3 = num1 + num2;
            await ctx.RespondAsync($"You just used an example command with arguments! Result: {num3}");
        }

        // Example 3
        [Command("example3")]
        public async Task Example3(CommandContext ctx, DiscordUser user)
        {
            await ctx.RespondAsync($"You just used an example command with entities!\n\n" +
                                   $"Name: {user.Username}\n" +
                                   $"ID: {user.Id}" +
                                   $"Avatar URL: {user.AvatarUrl}\n");
        }
        
        // Example 4
        [Command("example4")]
        public async Task Example4(DiscordChannel channel)
        {
            ExampleMethod1(channel);
        }
        private void ExampleMethod1(DiscordChannel channel)
        {
            channel.DeleteAsync();
        }

        // Example 5
        [Command("example5")]
        public async Task Example5(CommandContext ctx, string name)
        {
            var result = await ExampleMethod2(ctx, name);
            await ctx.RespondAsync($"Created a channel with the name: {result.Item1} and ID: {result.Item2}");
        }
        private async Task<(string, ulong)> ExampleMethod2(CommandContext ctx, string name)
        {
            var channel = await ctx.Guild.CreateChannelAsync(name, ChannelType.Text, null);
            return (channel.Name, channel.Id);
        }
    }
}