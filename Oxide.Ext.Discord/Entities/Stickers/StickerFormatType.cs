using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/sticker#sticker-format-types">Sticker Format Types</a>
    /// </summary>
    public enum StickerFormatType : byte
    {
        /// <summary>
        /// Sticker format type PNG
        /// </summary>
        [Description("PNG")]
        Png = 1,
        
        /// <summary>
        /// Sticker format type APNG
        /// </summary>
        [Description("APNG")]
        Apng = 2,
        
        /// <summary>
        /// Sticker format type LOTTIE
        /// </summary>
        [Description("LOTTIE")]
        Lottie = 3,
        
        /// <summary>
        /// Sticker format type GIF
        /// </summary>
        [Description("GIF")]
        Gif = 4
    }
}