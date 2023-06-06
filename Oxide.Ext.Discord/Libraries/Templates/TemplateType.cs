namespace Oxide.Ext.Discord.Libraries.Templates
{
    /// <summary>
    /// Represents available template type
    /// </summary>
    public enum TemplateType : byte
    {
        /// <summary>
        /// Message Template Type
        /// </summary>
        Message,
        
        /// <summary>
        /// Embed Template Type
        /// </summary>
        Embed,
        
        /// <summary>
        /// Embed Field Type
        /// </summary>
        EmbedField,
        
        /// <summary>
        /// Modal Template Type
        /// </summary>
        Modal,
        
        /// <summary>
        /// Command Template Type
        /// </summary>
        Command,

        /// <summary>
        /// Auto Complete Choice Template Type
        /// </summary>
        AutoCompleteChoice
    }
}