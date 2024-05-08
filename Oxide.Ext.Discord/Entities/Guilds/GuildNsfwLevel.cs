using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#guild-nsfw-level">Guild NSFW Level</a>
    /// </summary>
    public enum GuildNsfwLevel : byte
    {
        /// <summary>
        /// Default NSFW Level
        /// </summary>
        [Description("DEFAULT")]
        Default = 0,
        
        /// <summary>
        /// Guild is explicitly NSFW
        /// </summary>
        [Description("EXPLICIT")]
        Explicit = 1,
        
        /// <summary>
        /// Guild is safe from NSFW
        /// </summary>
        [Description("SAFE")]
        Safe = 2,
        
        /// <summary>
        /// Guild is age restricted
        /// </summary>
        [Description("AGE_RESTRICTED")]
        AgeRestricted = 3
    }
}