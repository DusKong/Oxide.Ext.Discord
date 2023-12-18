using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries
{
    /// <summary>
    /// Modal Templates Library
    /// </summary>
    public class DiscordEmbedTemplates : BaseMessageTemplateLibrary<DiscordEmbedTemplate>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        internal DiscordEmbedTemplates(ILogger logger) : base(TemplateType.Embed, logger) { }
    }
}