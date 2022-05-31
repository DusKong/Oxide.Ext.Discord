using System.Collections.Generic;

namespace Oxide.Ext.Discord.Plugins
{
    internal static class LangKeys
    {
        public const string Chat = nameof(Chat);
        public const string Version = nameof(Version);
        public const string ReconnectWebSocket = nameof(ReconnectWebSocket);
        public const string ShowLog = nameof(ShowLog);
        public const string SetLog = nameof(SetLog);
        public const string InvalidLogEnum = nameof(InvalidLogEnum);
    }
    
    internal static class Localization
    {
        internal static readonly Dictionary<string, Dictionary<string, string>> Languages = new Dictionary<string, Dictionary<string, string>>
        {
            ["en"] = new Dictionary<string, string>
            {
                [LangKeys.Chat] = "[Discord Extension] {0}",
                [LangKeys.Version] = "Server is running Discord Extension v{0}",
                [LangKeys.ReconnectWebSocket] = "All websockets have been requested to reconnect",
                [LangKeys.ShowLog] = "{0} log is current set to {1}",
                [LangKeys.SetLog] = "{0} log has been set to {1}",
                [LangKeys.InvalidLogEnum] = "'{0}' is not a valid DiscordLogLevel enum. Valid values are Off, Error, Warning, Info, Debug, Verbose"
            }
        };
    }
}