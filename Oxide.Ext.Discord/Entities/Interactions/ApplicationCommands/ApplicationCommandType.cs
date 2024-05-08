using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-types">Application Command Type</a>
    /// </summary>
    public enum ApplicationCommandType : byte
    {
        /// <summary>
        /// Slash commands; a text-based command that shows up when a user types /
        /// </summary>
        [Description("CHAT_INPUT")]
        ChatInput = 1,
        
        /// <summary>
        /// A UI-based command that shows up when you right click or tap on a user
        /// </summary>
        [Description("USER")]
        User = 2,
        
        /// <summary>
        /// A UI-based command that shows up when you right click or tap on a messages
        /// </summary>
        [Description("MESSAGE")]
        Message = 3
    }
}