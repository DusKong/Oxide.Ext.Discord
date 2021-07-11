using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Teams;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Entities.Webhooks;
using Oxide.Ext.Discord.Helpers.Cdn;

namespace Oxide.Ext.Discord.Entities.Applications
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/oauth2#application-object">Application Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordApplication
    {
        /// <summary>
        /// The id of the app
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// The name of the app
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// The icon hash of the app
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }
        
        /// <summary>
        /// The description of the app
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        
        /// <summary>
        /// An array of rpc origin urls, if rpc is enabled
        /// </summary>
        [JsonProperty("rpc_origins")]
        public List<string> RpcOrigins { get; set; }
        
        /// <summary>
        /// When false only app owner can join the app's bot to guilds
        /// </summary>
        [JsonProperty("bot_public")]
        public bool BotPublic { get; set; }
        
        /// <summary>
        /// When true the app's bot will only join upon completion of the full oauth2 code grant flow
        /// </summary>
        [JsonProperty("bot_require_code_grant")]
        public bool BotRequireCodeGrant { get; set; }
        
        /// <summary>
        /// The url of the app's terms of service
        /// </summary>
        [JsonProperty("terms_of_service_url")]
        public string TermsOfServiceUrl { get; set; }
        
        /// <summary>
        /// The url of the app's privacy policy
        /// </summary>
        [JsonProperty("privacy_policy_url")]
        public string PrivacyPolicyUrl { get; set; }
        
        /// <summary>
        /// Partial user object containing info on the owner of the application
        /// </summary>
        [JsonProperty("owner")]
        public DiscordUser Owner { get; set; }
        
        /// <summary>
        /// If this application is a game sold on Discord, this field will be the summary field for the store page of its primary sku
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }
        
        /// <summary>
        /// The hex encoded key for verification in interactions and the GameSDK's GetTicket
        /// </summary>
        [JsonProperty("verify_key")]
        public string Verify { get; set; }
        
        /// <summary>
        /// If the application belongs to a team, this will be a list of the members of that team
        /// </summary>
        [JsonProperty("team")]
        public DiscordTeam Team { get; set; }
        
        /// <summary>
        /// If this application is a game sold on Discord, this field will be the guild to which it has been linked
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake? GuildId { get; set; }
        
        /// <summary>
        /// If this application is a game sold on Discord, this field will be the id of the "Game SKU" that is created, if exists
        /// </summary>
        [JsonProperty("primary_sku_id")]
        public string PrimarySkuId { get; set; }
        
        /// <summary>
        /// If this application is a game sold on Discord, this field will be the URL slug that links to the store page
        /// </summary>
        [JsonProperty("slug")]
        public string Slug { get; set; }
        
        /// <summary>
        /// If this application is a game sold on Discord, this field will be the hash of the image on store embeds
        /// </summary>
        [JsonProperty("cover_image")]
        public string CoverImage { get; set; } 
        
        /// <summary>
        /// The application's public flags
        /// </summary>
        [JsonProperty("flags")]
        public ApplicationFlags? Flags { get; set; }

        /// <summary>
        /// Returns the URL for the applications Icon
        /// </summary>
        public string GetApplicationIconUrl => DiscordCdn.GetApplicationIconUrl(Id, Icon);
        
        /// <summary>
        /// Fetch all of the global commands for your application.
        /// Returns a list of ApplicationCommand.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#get-global-application-commands">Get Global Application Commands</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with list of application commands</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GetGlobalApplicationCommands(DiscordClient client, Action<List<DiscordApplicationCommand>> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/commands", RequestMethod.GET, null, callback, error);
        }
        
        /// <summary>
        /// Create a new global command.
        /// New global commands will be available in all guilds after 1 hour.
        /// Note: Creating a command with the same name as an existing command for your application will overwrite the old command.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#create-global-application-command">Create Global Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="create">Command to create</param>
        /// <param name="callback">Callback with the created command</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateGlobalApplicationCommand(DiscordClient client, CommandCreate create, Action<DiscordApplicationCommand> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/commands", RequestMethod.POST, create, callback, error);
        }
        
        /// <summary>
        /// Edit a global command.
        /// Updates will be available in all guilds after 1 hour.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#edit-global-application-command">Edit Global Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="update">Command Update</param>
        /// <param name="callback">Callback with updated command</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditGlobalApplicationCommand(DiscordClient client, CommandCreate update, Action<DiscordApplicationCommand> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/commands", RequestMethod.PATCH, update, callback, error);
        }
        
        /// <summary>
        /// Deletes a global command
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#delete-global-application-command">Delete Global Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="commandId">Command to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteGlobalApplicationCommand(DiscordClient client, Snowflake commandId, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/commands/{commandId}", RequestMethod.PATCH, null, callback, error);
        }

        /// <summary>
        /// Fetch all of the guild commands for your application for a specific guild.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#get-guild-application-commands">Get Guild Application Commands</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">ID of the guild to get commands for</param>
        /// <param name="callback">Callback with a list of guild application commands</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GetGuildApplicationCommands(DiscordClient client, Snowflake guildId, Action<List<DiscordApplicationCommand>> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/guilds/{guildId}/commands", RequestMethod.GET, null, callback, error);
        }
        
        /// <summary>
        /// Fetch all of the guild commands for your application for a specific guild.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#get-guild-application-commands">Get Guild Application Commands</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guild">Guild to get commands for</param>
        /// <param name="callback">Callback with a list of guild application commands</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GetGuildApplicationCommands(DiscordClient client, DiscordGuild guild, Action<List<DiscordApplicationCommand>> callback = null, Action<RestError> error = null)
        {
            GetGuildApplicationCommands(client, guild.Id, callback, error);
        }
        
        /// <summary>
        /// Create a new guild command.
        /// New guild commands will be available in the guild immediately.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#create-guild-application-command">Create Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to create the command in</param>
        /// <param name="create">Command to create</param>
        /// <param name="callback">Callback with the created command</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateGuildApplicationCommands(DiscordClient client, Snowflake guildId, CommandCreate create, Action<DiscordApplicationCommand> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/guilds/{guildId}/commands", RequestMethod.POST, create, callback, error);
        }
        
        /// <summary>
        /// Create a new guild command.
        /// New guild commands will be available in the guild immediately.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#create-guild-application-command">Create Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guild">Guild to create the command in</param>
        /// <param name="create">Command to create</param>
        /// <param name="callback">Callback with the created command</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateGuildApplicationCommands(DiscordClient client, DiscordGuild guild, CommandCreate create, Action<DiscordApplicationCommand> callback = null, Action<RestError> error = null)
        {
            CreateGuildApplicationCommands(client, guild.Id, create, callback, error);
        }
        
        /// <summary>
        /// Edit a guild command.
        /// Updates for guild commands will be available immediately.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#edit-guild-application-command">Edit Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to update the command in</param>
        /// <param name="update">Command update</param>
        /// <param name="callback">Callback with updated command</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditGuildApplicationCommands(DiscordClient client, Snowflake guildId, DiscordApplicationCommand update, Action<DiscordApplicationCommand> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/guilds/{guildId}/commands/{update.Id}", RequestMethod.PATCH, update, callback, error);
        }
        
        /// <summary>
        /// Edit a guild command.
        /// Updates for guild commands will be available immediately.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#edit-guild-application-command">Edit Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guild">Guild to update the command in</param>
        /// <param name="update">Command update</param>
        /// <param name="callback">Callback with updated command</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditGuildApplicationCommands(DiscordClient client, DiscordGuild guild, DiscordApplicationCommand update, Action<DiscordApplicationCommand> callback = null, Action<RestError> error = null)
        {
            EditGuildApplicationCommands(client, guild.Id, update, callback, error);
        }
        
        /// <summary>
        /// Delete a guild command.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#delete-guild-application-command">Delete Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to delete command from</param>
        /// <param name="commandId">Command ID to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteGuildApplicationCommands(DiscordClient client, Snowflake guildId, Snowflake commandId, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/guilds/{guildId}/commands/{commandId}", RequestMethod.DELETE, null, callback, error);
        }
        
        /// <summary>
        /// Delete a guild command.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#delete-guild-application-command">Delete Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guild">Guild to delete command from</param>
        /// <param name="delete">Command to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteGuildApplicationCommands(DiscordClient client, DiscordGuild guild, DiscordApplicationCommand delete, Action callback = null, Action<RestError> error = null)
        {
            DeleteGuildApplicationCommands(client, guild.Id, delete.Id, callback, error);
        }

        /// <summary>
        /// Fetches command permissions for all commands for your application in a guild. Returns an array of GuildApplicationCommandPermissions objects.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to get the permissions from</param>
        /// <param name="callback">Callback with the list of permissions</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GetGuildApplicationCommandPermissions(DiscordClient client, Snowflake guildId, Action<List<GuildCommandPermissions>> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/guilds/{guildId}/commands/permissions", RequestMethod.GET, null, callback, error);
        }
        
        /// <summary>
        /// Fetches command permissions for a specific command for your application in a guild. Returns a GuildApplicationCommandPermissions object.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to get the permissions from</param>
        /// <param name="commandId">ID of the command to get permissions for</param>
        /// <param name="callback">Callback with the permissions for the command</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GetApplicationCommandPermissions(DiscordClient client, Snowflake guildId, Snowflake commandId, Action<GuildCommandPermissions> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/guilds/{guildId}/commands/{commandId}/permissions", RequestMethod.GET, null, callback, error);
        }
        
        /// <summary>
        /// Edits command permissions for a specific command for your application in a guild.
        /// Warning: This endpoint will overwrite existing permissions for the command in that guild
        /// Warning: Deleting or renaming a command will permanently delete all permissions for that command
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to update the permissions for</param>
        /// <param name="commandId">ID of the command to get permissions for</param>
        /// <param name="permissions">List of permissions for the command</param>
        /// <param name="callback">Callback with the list of permissions</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditApplicationCommandPermissions(DiscordClient client, Snowflake guildId, Snowflake commandId, GuildCommandPermissions permissions, Action callback = null, Action<RestError> error = null)
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                ["permissions"] = permissions
            };
            
            client.Bot.Rest.DoRequest($"/applications/{Id}/guilds/{guildId}/commands/{commandId}/permissions", RequestMethod.PUT, data, callback, error);
        }
        
        /// <summary>
        /// Batch edits permissions for all commands in a guild.
        /// Warning: This endpoint will overwrite all existing permissions for all commands in a guild
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to update the permissions for</param>
        /// <param name="permissions">List of permissions for the commands</param>
        /// <param name="callback">Callback with the list of permissions</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void BatchEditApplicationCommandPermissions(DiscordClient client, Snowflake guildId, List<GuildCommandPermissions> permissions, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/guilds/{guildId}/commands/permissions", RequestMethod.PUT, permissions, callback, error);
        }
    }
}