namespace Oxide.Ext.Discord.Exceptions.Entities.Interactions
{
    /// <summary>
    /// Exception for invalid Auto Complete choices
    /// </summary>
    public class InvalidAutoCompleteChoiceException : BaseDiscordException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        protected InvalidAutoCompleteChoiceException(string message) : base(message) { }

        internal static void ThrowIfInvalidName(string name)
        {
            const int maxLength = 100;
            
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidAutoCompleteChoiceException("Name must contain at least 1 character");
            }

            if (name.Length > maxLength)
            {
                throw new InvalidAutoCompleteChoiceException($"Name cannot be greater than {maxLength} characters");
            }
        }

        internal static void ThrowIfInvalidValue(object value)
        {
            if (value == null)
            {
                throw new InvalidAutoCompleteChoiceException("Value cannot be null");
            }
        }
    }
}