using System.Text;
using Oxide.Ext.Discord.Entities.Api;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal static class RequestErrorPlaceholders
    {
        private static void HttpCode(StringBuilder builder, PlaceholderState state, RequestError error) => PlaceholderFormatting.Replace(builder, state, error.HttpStatusCode);
        private static void Message(StringBuilder builder, PlaceholderState state, RequestError error) => PlaceholderFormatting.Replace(builder, state, error.DiscordError?.Message ?? error.Message);

        public static void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterInternalPlaceholder<RequestError>("error.code", HttpCode);
            placeholders.RegisterInternalPlaceholder<RequestError>("error.message", Message);
        }
    }
}