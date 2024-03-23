namespace DiscordBot
{
    public class Settings
    {
        // Embed
        public static ulong ChannelId { get; set; }
        public static string Title { get; set; }
        public static string Description { get; set; }
        public static string Url { get; set; }
        public static string Color { get; set; }
        public static string ThumbnailUrl { get; set; }
        public static string ImageUrl { get; set; }


        // Client
        public static string Token { get; set; }
        public static string Prefix { get; set; }
        public static string RadioUrl { get; set; }
    }
}
