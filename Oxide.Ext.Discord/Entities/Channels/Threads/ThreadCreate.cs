using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Channels.Threads
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/channel#group-dm-add-recipient-json-params">Thread Create Structure</a> within Discord.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ThreadCreate
    {
        /// <summary>
        /// 2-100 character thread name
        /// </summary>
        [JsonProperty("id")]
        public string Name { get; set; } 
        
        /// <summary>
        /// Duration in minutes to automatically archive the thread after recent activity, can be set to: 60, 1440, 4320, 10080
        /// </summary>
        [JsonProperty("auto_archive_duration")]
        public int AutoArchiveDuration { get; set; }

        /// <summary>
        /// The type of thread to create
        /// Can only be GuildNewsThread, GuildPublicThread, or GuildPrivateThread
        /// </summary>
        [JsonProperty("type")]
        public ChannelType Type { get; set; } = ChannelType.GuildPrivateThread;
    }
}