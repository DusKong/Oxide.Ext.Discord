using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Exceptions.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents an invalid application command
    /// </summary>
    public class InvalidApplicationCommandException : BaseDiscordException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        private InvalidApplicationCommandException(string message) : base(message)
        {
            
        }
        
        internal static void ThrowIfInvalidName(string name, bool allowNullOrEmpty)
        {
            const int maxLength = 32;
            
            if (!allowNullOrEmpty && string.IsNullOrEmpty(name))
            {
                throw new InvalidApplicationCommandException("Name cannot be less than 1 character");
            }
            
            if (name.Length > 32)
            {
                throw new InvalidApplicationCommandException($"Name cannot be more than {maxLength} characters");
            }
        }
        
        internal static void ThrowIfInvalidDescription(string description, ApplicationCommandType type)
        {
            const int maxLength = 100;
            if (type == ApplicationCommandType.ChatInput)
            {
                if (!string.IsNullOrEmpty(description) && description.Length > maxLength)
                {
                    throw new InvalidApplicationCommandException($"Description cannot be more than {maxLength} characters for {nameof(ApplicationCommandType)}.{type}");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(description))
                {
                    throw new InvalidApplicationCommandException($"Description must be null for {nameof(ApplicationCommandType)}.{type}");
                }
            }
        }
    }
}