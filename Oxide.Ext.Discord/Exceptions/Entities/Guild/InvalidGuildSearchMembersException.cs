using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Exceptions.Entities.Guild
{
    /// <summary>
    /// Represents an exception in guild member search requests
    /// </summary>
    public class InvalidGuildSearchMembersException : BaseDiscordException
    {
        private InvalidGuildSearchMembersException(string message) : base(message) { }

        internal static void ThrowIfInvalidQuery(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new InvalidGuildSearchMembersException($"{nameof(GuildSearchMembers)}.{nameof(GuildSearchMembers.Query)} cannot be less than 1 character");
            }
        }
        
        internal static void ThrowIfInvalidLimit(int? limit)
        {
            const int minLimit = 0;
            const int maxLimit = 1000;
            
            if (limit < minLimit)
            {
                throw new InvalidGuildSearchMembersException($"{nameof(GuildSearchMembers)}.{nameof(GuildSearchMembers.Limit)} cannot be less than {minLimit}");
            }
            
            if (limit > maxLimit)
            {
                throw new InvalidGuildSearchMembersException($"{nameof(GuildSearchMembers)}.{nameof(GuildSearchMembers.Limit)} cannot be more than {maxLimit}");
            }
        }
    }
}