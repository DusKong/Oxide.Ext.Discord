﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Builders;
using Oxide.Ext.Discord.Clients;
using Oxide.Ext.Discord.Exceptions;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Json;
using Oxide.Ext.Discord.Libraries;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Types;
using Oxide.Plugins;
using UserData = Oxide.Ext.Discord.Data.UserData;

namespace Oxide.Ext.Discord.Entities
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/channel#message-object">Message Structure</a> sent in a channel within Discord..
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordMessage : IFileAttachments
    {
        /// <summary>
        /// ID of the message
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }

        /// <summary>
        /// ID of the channel the message was sent in
        /// </summary>
        [JsonProperty("channel_id")]
        public Snowflake ChannelId { get; set; }

        /// <summary>
        /// ID of the guild the message was sent in
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake? GuildId { get; set; }

        /// <summary>
        /// The author of this message (not guaranteed to be a valid user)
        /// The author object follows the structure of the user object, but is only a valid user in the case where the message is generated by a user or bot user.
        /// If the message is generated by a webhook, the author object corresponds to the webhook's id, username, and avatar.
        /// You can tell if a message is generated by a webhook by checking for the webhook_id on the message object.
        /// <see cref="DiscordUser"/>
        /// </summary>
        [JsonProperty("author")]
        public DiscordUser Author { get; set; }

        /// <summary>
        /// Member properties for this message's author
        /// <see cref="GuildMember"/>
        /// </summary>
        [JsonProperty("member")]
        public GuildMember Member { get; set; }
        
        /// <summary>
        /// Contents of the message
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// When this message was sent
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// When this message was edited (or null if never)
        /// </summary>
        [JsonProperty("edited_timestamp")]
        public DateTime? EditedTimestamp { get; set; }
        
        /// <summary>
        /// Whether this was a TTS message
        /// </summary>
        [JsonProperty("tts")]
        public bool Tts { get; set; }

        /// <summary>
        /// Whether this message mentions everyone
        /// </summary>
        [JsonProperty("mention_everyone")]
        public bool MentionEveryone { get; set; }

        /// <summary>
        /// Users specifically mentioned in the message
        /// <see cref="DiscordUser"/>
        /// </summary>
        [JsonConverter(typeof(HashListConverter<DiscordUser>))]
        [JsonProperty("mentions")]
        public Hash<Snowflake, DiscordUser> Mentions { get; set; }

        /// <summary>
        /// Roles specifically mentioned in this message
        /// </summary>
        [JsonProperty("mention_roles")]
        public List<Snowflake> MentionRoles { get; set; }
        
        /// <summary>
        /// Channels specifically mentioned in this message
        /// <see cref="ChannelMention"/>
        /// </summary>
        [JsonConverter(typeof(HashListConverter<ChannelMention>))]
        [JsonProperty("mention_channels")]
        public Hash<Snowflake, ChannelMention> MentionsChannels { get; set; }

        /// <summary>
        /// Any attached files
        /// <see cref="MessageAttachment"/>
        /// </summary>
        [JsonConverter(typeof(HashListConverter<MessageAttachment>))]
        [JsonProperty("attachments")]
        public Hash<Snowflake, MessageAttachment> Attachments { get; set; }

        /// <summary>
        /// Any embedded content
        /// <see cref="DiscordEmbed"/>
        /// </summary>
        [JsonProperty("embeds")]
        public List<DiscordEmbed> Embeds { get; set; }

        /// <summary>
        /// Reactions to the message
        /// <see cref="MessageReaction"/>
        /// </summary>
        [JsonProperty("reactions")]
        public List<MessageReaction> Reactions { get; set; }
        
        /// <summary>
        /// Used for validating a message was sent
        /// </summary>
        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        /// <summary>
        /// Whether this message is pinned
        /// </summary>
        [JsonProperty("pinned")]
        public bool Pinned { get; set; }

        /// <summary>
        /// If the message is generated by a webhook, this is the webhook's id
        /// </summary>
        [JsonProperty("webhook_id")]
        public Snowflake? WebhookId { get; set; }

        /// <summary>
        /// Type of message
        /// <see cref="MessageType"/>
        /// </summary>
        [JsonProperty("type")]
        public MessageType? Type { get; set; }
        
        /// <summary>
        /// Sent with Rich Presence-related chat embeds
        /// <see cref="MessageActivity"/>
        /// </summary>
        [JsonProperty("activity")]
        public MessageActivity Activity { get; set; }
        
        /// <summary>
        /// Sent with Rich Presence-related chat embeds
        /// <see cref="DiscordApplication"/>
        /// </summary>
        [JsonProperty("application")]
        public DiscordApplication Application { get; set; }
        
        /// <summary>
        /// If the message is an Interaction or application-owned webhook, this is the id of the application
        /// </summary>
        [JsonProperty("application_id")]
        public Snowflake? ApplicationId { get; set; }

        /// <summary>
        /// Data showing the source of a crosspost, channel follow add, pin, or reply message
        /// <see cref="Entities.MessageReference"/>
        /// </summary>
        [JsonProperty("message_reference")]
        public MessageReference MessageReference { get; set; }
        
        /// <summary>
        /// Message flags combined as a bitfield
        /// <see cref="MessageFlags"/>
        /// </summary>
        [JsonProperty("flags")]
        public MessageFlags Flags { get; set; }

        /// <summary>
        /// The message associated with the message_reference
        /// </summary>
        [JsonProperty("referenced_message")]
        public DiscordMessage ReferencedMessage { get; internal set; }
        
        /// <summary>
        /// Sent if the message is a response to an Interaction
        /// </summary>
        [JsonProperty("interaction")]
        public MessageInteraction Interaction { get; set; }
        
        /// <summary>
        /// The thread that was started from this message, includes thread member object
        /// </summary>
        [JsonProperty("thread")]
        public DiscordChannel Thread { get; set; }
        
        /// <summary>
        /// Sent if the message contains components like buttons, action rows, or other interactive components
        /// </summary>
        [JsonProperty("components")]
        public List<ActionRowComponent> Components { get; set; }
        
        /// <summary>
        /// Sent if the message contains stickers
        /// </summary>
        [JsonConverter(typeof(HashListConverter<DiscordSticker>))]
        [JsonProperty("sticker_items")]
        public Hash<Snowflake, DiscordSticker> StickerItems { get; set; }       
        
        /// <summary>
        /// A generally increasing integer (there may be gaps or duplicates) that represents the approximate position of the message in a thread, it can be used to estimate the relative position of the message in a thread in company with total_message_sent on parent thread
        /// </summary>
        [JsonProperty("position")]
        public int? Position { get; set; }
        
        /// <summary>
        /// The data of the role subscription purchase or renewal that prompted this ROLE_SUBSCRIPTION_PURCHASE message
        /// </summary>
        [JsonProperty("role_subscription_data")]
        public RoleSubscription RoleSubscriptionData { get; set; }
        
        /// <summary>
        /// File Attachments to add to the message on edit
        /// </summary>
        public List<MessageFileAttachment> FileAttachments { get; set; }
        
        /// <summary>
        /// Post a message to a guild text or DM channel.
        /// If operating on a guild channel, this endpoint requires the SEND_MESSAGES permission to be present on the current user.
        /// If the tts field is set to true, the SEND_TTS_MESSAGES permission is required for the message to be spoken.
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-message">Create Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to send the message to</param>
        /// <param name="message">Message to be created</param>
        public static IPromise<DiscordMessage> Create(DiscordClient client, Snowflake channelId, MessageCreate message)
        {
            InvalidSnowflakeException.ThrowIfInvalid(channelId, nameof(channelId));
            UserData userData = client.Bot.DirectMessagesByChannelId[channelId]?.UserData;
            DateTime? isBlocked = userData?.GetBlockedUntil();
            
            if (isBlocked.HasValue && isBlocked.Value > DateTime.UtcNow)
            {
                DiscordUser user = userData.GetUser();
                client.Logger.Debug("Blocking CreateMessage. User {0} ({1}) is DM blocked until {2}.", user.FullUserName, user.Id, userData.GetBlockedUntil());
                return Promise<DiscordMessage>.Rejected(new BlockedUserException(userData.GetUser(), isBlocked.Value));
            }

            IPromise<DiscordMessage> response = client.Bot.Rest.Post<DiscordMessage>(client, $"channels/{channelId}/messages", message);
            if (userData != null)
            {
                response.Catch<ResponseError>(ex => userData.ProcessError(client, ex));
            }
            
            return response;
        }
        
        /// <summary>
        /// Post a message to a guild text or DM channel.
        /// If operating on a guild channel, this endpoint requires the SEND_MESSAGES permission to be present on the current user.
        /// If the tts field is set to true, the SEND_TTS_MESSAGES permission is required for the message to be spoken.
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-message">Create Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to send the message to</param>
        /// <param name="builder">Builder for the message</param>
        public static IPromise<DiscordMessage> Create(DiscordClient client, Snowflake channelId, DiscordMessageBuilder builder)
        {
            return Create(client, channelId, builder.Build());
        }

        /// <summary>
        /// Post a message to a guild text or DM channel.
        /// If operating on a guild channel, this endpoint requires the SEND_MESSAGES permission to be present on the current user.
        /// If the tts field is set to true, the SEND_TTS_MESSAGES permission is required for the message to be spoken.
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-message">Create Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to send the message to</param>
        /// <param name="message">Content of the message</param>
        public static IPromise<DiscordMessage> Create(DiscordClient client, Snowflake channelId, string message)
        {
            InvalidSnowflakeException.ThrowIfInvalid(channelId, nameof(channelId));
            MessageCreate createMessage = new MessageCreate
            {
                Content = message
            };

            return Create(client, channelId, createMessage);
        }

        /// <summary>
        /// Post a message to a guild text or DM channel.
        /// If operating on a guild channel, this endpoint requires the SEND_MESSAGES permission to be present on the current user.
        /// If the tts field is set to true, the SEND_TTS_MESSAGES permission is required for the message to be spoken.
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-message">Create Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to send the message to</param>
        /// <param name="embed">Embed to be send in the message</param>
        public static IPromise<DiscordMessage> Create(DiscordClient client, Snowflake channelId, DiscordEmbed embed)
        {
            InvalidSnowflakeException.ThrowIfInvalid(channelId, nameof(channelId));
            MessageCreate createMessage = new MessageCreate
            {
                Embeds = new List<DiscordEmbed> {embed}
            };

            return Create(client, channelId, createMessage);
        }
        
        /// <summary>
        /// Post a message to a guild text or DM channel.
        /// If operating on a guild channel, this endpoint requires the SEND_MESSAGES permission to be present on the current user.
        /// If the tts field is set to true, the SEND_TTS_MESSAGES permission is required for the message to be spoken.
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-message">Create Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to send the message to</param>
        /// <param name="embeds">Embeds to be send in the message</param>
        public static IPromise<DiscordMessage> Create(DiscordClient client, Snowflake channelId, List<DiscordEmbed> embeds)
        {
            InvalidSnowflakeException.ThrowIfInvalid(channelId, nameof(channelId));
            MessageCreate createMessage = new MessageCreate
            {
                Embeds = embeds
            };

            return Create(client, channelId, createMessage);
        }
        
        /// <summary>
        /// Send a message in the channel with the given ID using a global message template
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to send the message in</param>
        /// <param name="plugin">Plugin for the template</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="message">Message to use (optional)</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        public static IPromise<DiscordMessage> CreateGlobalTemplateMessage(DiscordClient client, Snowflake channelId, Plugin plugin, string templateName, MessageCreate message = null, PlaceholderData placeholders = null)
        {
            MessageCreate template = DiscordExtension.DiscordMessageTemplates.GetGlobalTemplate(plugin, templateName).ToMessage(placeholders, message);
            return Create(client, channelId, template);
        }

        /// <summary>
        /// Send a message in the channel with the given ID using a localized message template
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to send the message in</param>
        /// <param name="plugin">Plugin for the template</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="language">Oxide language to use</param>
        /// <param name="message">Message to use (optional)</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        public static IPromise<DiscordMessage> CreateTemplateMessage(DiscordClient client, Snowflake channelId, Plugin plugin, string templateName, string language = DiscordLocales.DefaultServerLanguage, MessageCreate message = null, PlaceholderData placeholders = null)
        {
            MessageCreate template = DiscordExtension.DiscordMessageTemplates.GetLocalizedTemplate(plugin, templateName, language).ToMessage(placeholders, message);
            return Create(client, channelId, template);
        }

        /// <summary>
        /// Returns a specific message in the channel.
        /// If operating on a guild channel, this endpoint requires the 'READ_MESSAGE_HISTORY' permission to be present on the current user.
        /// See <a href="https://discord.com/developers/docs/resources/channel#get-channel-message">Get Channel Messages</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID where the message is</param>
        /// <param name="messageId">Message ID of the message</param>
        public static IPromise<DiscordMessage> GetMessage(DiscordClient client, Snowflake channelId, Snowflake messageId)
        {
            InvalidSnowflakeException.ThrowIfInvalid(channelId, nameof(channelId));
            InvalidSnowflakeException.ThrowIfInvalid(messageId, nameof(messageId));
            return client.Bot.Rest.Get<DiscordMessage>(client,$"channels/{channelId}/messages/{messageId}");
        }
        
        /// <summary>
        /// Replies to a previously sent message
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-message">Create Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="message">Message to send</param>
        public IPromise<DiscordMessage> Reply(DiscordClient client, MessageCreate message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (message.MessageReference == null)
            {
                message.MessageReference = new MessageReference {MessageId = Id, GuildId = GuildId};
            }
            
            return Create(client, ChannelId, message);
        }
        
        /// <summary>
        /// Replies to a previously sent message
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-message">Create Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="builder">Builder for the message</param>
        public IPromise<DiscordMessage> Reply(DiscordClient client, DiscordMessageBuilder builder)
        {
            return Reply(client, builder.Build());
        }
        
        /// <summary>
        /// Replies to a previously sent message
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-message">Create Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="message">Message text to send</param>
        public IPromise<DiscordMessage> Reply(DiscordClient client, string message)
        {
            MessageCreate newMessage = new MessageCreate
            {
                Content = message
            };

            return Reply(client, newMessage);
        }

        /// <summary>
        /// Replies to a previously sent message
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-message">Create Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="embed">Embed to send</param>
        public IPromise<DiscordMessage> Reply(DiscordClient client, DiscordEmbed embed)
        {
            return Reply(client, new List<DiscordEmbed> {embed});
        }
        
        /// <summary>
        /// Replies to a previously sent message
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-message">Create Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="embeds">Embeds to send</param>
        public IPromise<DiscordMessage> Reply(DiscordClient client, List<DiscordEmbed> embeds)
        {
            MessageCreate newMessage = new MessageCreate
            {
                Embeds = embeds,
            };

            return Reply(client, newMessage);
        }

        /// <summary>
        /// Reply to a message using a global message template
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="message">Message to use (optional)</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        public IPromise<DiscordMessage> ReplyWithGlobalTemplate(DiscordClient client, string templateName, MessageCreate message = null, PlaceholderData placeholders = null)
        {
            MessageCreate template = DiscordExtension.DiscordMessageTemplates.GetGlobalTemplate(client.Plugin, templateName).ToMessage(placeholders, message);
            return Reply(client, template);
        }

        /// <summary>
        /// Reply to a message using a global message template
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="language">Oxide language to use</param>
        /// <param name="message">Message to use (optional)</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        public IPromise<DiscordMessage> ReplyWithTemplate(DiscordClient client, string templateName, string language = DiscordLocales.DefaultServerLanguage, MessageCreate message = null, PlaceholderData placeholders = null)
        {
            MessageCreate template = DiscordExtension.DiscordMessageTemplates.GetLocalizedTemplate(client.Plugin, templateName, language).ToMessage(placeholders, message);
            return Reply(client, template);
        }
        
        /// <summary>
        /// Crosspost a message in a News Channel to following channels.
        /// This endpoint requires the 'SEND_MESSAGES' permission, if the current user sent the message, or additionally the 'MANAGE_MESSAGES' permission, for all other messages, to be present for the current user.
        /// See <a href="https://discord.com/developers/docs/resources/channel#crosspost-message">Crosspost Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageId">Message ID to cross post</param>
        public IPromise<DiscordMessage> CrossPostMessage(DiscordClient client, Snowflake messageId)
        {
            InvalidSnowflakeException.ThrowIfInvalid(Id, nameof(Id));
            InvalidSnowflakeException.ThrowIfInvalid(messageId, nameof(messageId));
            return client.Bot.Rest.Post<DiscordMessage>(client,$"channels/{Id}/messages/{messageId}/crosspost", null);
        }
        
        /// <summary>
        /// Crosspost a message in a News Channel to following channels.
        /// This endpoint requires the 'SEND_MESSAGES' permission, if the current user sent the message, or additionally the 'MANAGE_MESSAGES' permission, for all other messages, to be present for the current user.
        /// See <a href="https://discord.com/developers/docs/resources/channel#crosspost-message">Crosspost Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="message">Message to cross post</param>
        public IPromise<DiscordMessage> CrossPostMessage(DiscordClient client, DiscordMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            return CrossPostMessage(client, message.Id);
        }

        /// <summary>
        /// Create a reaction for the message.
        /// This endpoint requires the 'READ_MESSAGE_HISTORY' permission to be present on the current user.
        /// Additionally, if nobody else has reacted to the message using this emoji, this endpoint requires the 'ADD_REACTIONS' permission to be present on the current user.
        /// Valid emoji formats are the unicode emoji character '😀' or for custom emoji format must be &lt;emojiName:emojiId&gt;
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-reaction">Create Reaction</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="emoji">Emoji to react with.</param>
        public IPromise CreateReaction(DiscordClient client, DiscordEmoji emoji)
        {
            if (emoji == null) throw new ArgumentNullException(nameof(emoji));
            return CreateReaction(client, emoji.ToDataString());
        }
        
        /// <summary>
        /// Create a reaction for the message.
        /// This endpoint requires the 'READ_MESSAGE_HISTORY' permission to be present on the current user.
        /// Additionally, if nobody else has reacted to the message using this emoji, this endpoint requires the 'ADD_REACTIONS' permission to be present on the current user.
        /// Valid emoji formats are the unicode emoji character '😀' or for custom emoji format must be &lt;emojiName:emojiId&gt;
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-reaction">Create Reaction</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="emoji">Emoji to react with.</param>
        public IPromise CreateReaction(DiscordClient client, string emoji)
        {
            InvalidSnowflakeException.ThrowIfInvalid(Id, nameof(Id));
            InvalidSnowflakeException.ThrowIfInvalid(ChannelId, nameof(ChannelId));
            InvalidEmojiException.ThrowIfInvalidEmojiString(emoji);
            return client.Bot.Rest.Put(client,$"channels/{ChannelId}/messages/{Id}/reactions/{emoji}/@me", null);
        }

        /// <summary>
        /// Delete a reaction the current user has made for the message
        /// Valid emoji formats are the unicode emoji character '😀' or for custom emoji format must be &lt;emojiName:emojiId&gt;
        /// See <a href="https://discord.com/developers/docs/resources/channel#delete-own-reaction">Delete Own Reaction</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="emoji">Emoji to delete</param>
        public IPromise DeleteOwnReaction(DiscordClient client, DiscordEmoji emoji)
        {
            if (emoji == null) throw new ArgumentNullException(nameof(emoji));
            return DeleteOwnReaction(client, emoji.ToDataString());
        }
        
        /// <summary>
        /// Delete a reaction the current user has made for the message
        /// Valid emoji formats are the unicode emoji character '😀' or for custom emoji format must be &lt;emojiName:emojiId&gt;
        /// See <a href="https://discord.com/developers/docs/resources/channel#delete-own-reaction">Delete Own Reaction</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="emoji">Emoji to delete</param>
        public IPromise DeleteOwnReaction(DiscordClient client, string emoji)
        {
            InvalidSnowflakeException.ThrowIfInvalid(Id, nameof(Id));
            InvalidSnowflakeException.ThrowIfInvalid(ChannelId, nameof(ChannelId));
            InvalidEmojiException.ThrowIfInvalidEmojiString(emoji);
            return client.Bot.Rest.Delete(client,$"channels/{ChannelId}/messages/{Id}/reactions/{emoji}/@me");
        }

        /// <summary>
        /// Deletes another user's reaction.
        /// This endpoint requires the 'MANAGE_MESSAGES' permission to be present on the current user.
        /// Valid emoji formats are the unicode emoji character '😀' or for custom emoji format must be &lt;emojiName:emojiId&gt;
        /// See <a href="https://discord.com/developers/docs/resources/channel#delete-user-reaction">Delete User Reaction</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="emoji">Emoji to delete</param>
        /// <param name="userId">User ID who add the reaction</param>
        public IPromise DeleteUserReaction(DiscordClient client, DiscordEmoji emoji, Snowflake userId)
        {
            return DeleteUserReaction(client, emoji.ToDataString(), userId);
        }

        /// <summary>
        /// Deletes another user's reaction.
        /// This endpoint requires the 'MANAGE_MESSAGES' permission to be present on the current user.
        /// Valid emoji formats are the unicode emoji character '😀' or for custom emoji format must be &lt;emojiName:emojiId&gt;
        /// See <a href="https://discord.com/developers/docs/resources/channel#delete-user-reaction">Delete User Reaction</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="emoji">Emoji to delete</param>
        /// <param name="userId">User ID who add the reaction</param>
        public IPromise DeleteUserReaction(DiscordClient client, string emoji, Snowflake userId)
        {
            InvalidSnowflakeException.ThrowIfInvalid(Id, nameof(Id));
            InvalidSnowflakeException.ThrowIfInvalid(ChannelId, nameof(ChannelId));
            InvalidSnowflakeException.ThrowIfInvalid(userId, nameof(userId));
            InvalidEmojiException.ThrowIfInvalidEmojiString(emoji);
            return client.Bot.Rest.Delete(client,$"channels/{ChannelId}/messages/{Id}/reactions/{emoji}/{userId}");
        }

        /// <summary>
        /// Get a list of users that reacted with this emoji
        /// Valid emoji formats are the unicode emoji character '😀' or for custom emoji format must be &lt;emojiName:emojiId&gt;
        /// See <a href="https://discord.com/developers/docs/resources/channel#get-reactions">Get Reactions</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="emoji">Emoji to get the list for</param>
        public IPromise<List<DiscordUser>> GetReactions(DiscordClient client, DiscordEmoji emoji)
        {
            if (emoji == null) throw new ArgumentNullException(nameof(emoji));
            return GetReactions(client, emoji.ToDataString());
        }
        
        /// <summary>
        /// Get a list of users that reacted with this emoji
        /// Valid emoji formats are the unicode emoji character '😀' or for custom emoji format must be &lt;emojiName:emojiId&gt;
        /// See <a href="https://discord.com/developers/docs/resources/channel#get-reactions">Get Reactions</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="emoji">Emoji to get the list for</param>
        public IPromise<List<DiscordUser>> GetReactions(DiscordClient client, string emoji)
        {
            InvalidEmojiException.ThrowIfInvalidEmojiString(emoji);
            return client.Bot.Rest.Get<List<DiscordUser>>(client,$"channels/{ChannelId}/messages/{Id}/reactions/{emoji}");
        }

        /// <summary>
        /// Deletes all reactions on a message.
        /// This endpoint requires the 'MANAGE_MESSAGES' permission to be present on the current user.
        /// </summary>
        /// <param name="client">Client to use</param>
        public IPromise DeleteAllReactions(DiscordClient client)
        {
            InvalidSnowflakeException.ThrowIfInvalid(Id, nameof(Id));
            InvalidSnowflakeException.ThrowIfInvalid(ChannelId, nameof(ChannelId));
            return client.Bot.Rest.Delete(client,$"channels/{ChannelId}/messages/{Id}/reactions");
        }
        
        /// <summary>
        /// Deletes all the reactions for a given emoji on a message.
        /// This endpoint requires the MANAGE_MESSAGES permission to be present on the current user.
        /// Valid emoji formats are the unicode emoji character '😀' or for custom emoji format must be &lt;emojiName:emojiId&gt;
        /// See <a href="https://discord.com/developers/docs/resources/channel#delete-all-reactions-for-emoji">Delete All Reactions for Emoji</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="emoji">Emoji to delete all reactions for</param>
        public IPromise DeleteAllReactionsForEmoji(DiscordClient client, DiscordEmoji emoji)
        {
            if (emoji == null) throw new ArgumentNullException(nameof(emoji));
            return DeleteAllReactionsForEmoji(client, emoji.ToDataString());
        }
        
        /// <summary>
        /// Deletes all the reactions for a given emoji on a message.
        /// This endpoint requires the MANAGE_MESSAGES permission to be present on the current user.
        /// Valid emoji formats are the unicode emoji character '😀' or for custom emoji format must be &lt;emojiName:emojiId&gt;
        /// See <a href="https://discord.com/developers/docs/resources/channel#delete-all-reactions-for-emoji">Delete All Reactions for Emoji</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="emoji">Emoji to delete all reactions for</param>
        public IPromise DeleteAllReactionsForEmoji(DiscordClient client, string emoji)
        {
            InvalidEmojiException.ThrowIfInvalidEmojiString(emoji);
            return client.Bot.Rest.Delete(client,$"channels/{ChannelId}/messages/{Id}/reactions/{emoji}");
        }

        /// <summary>
        /// Edit a previously sent message.
        /// The fields content, embed, allowed_mentions and flags can be edited by the original message author.
        /// Other users can only edit flags and only if they have the MANAGE_MESSAGES permission in the corresponding channel.
        /// When specifying flags, ensure to include all previously set flags/bits in addition to ones that you are modifying.
        /// Only flags documented in the table below may be modified by users (unsupported flag changes are currently ignored without error).
        /// See <a href="https://discord.com/developers/docs/resources/channel#edit-message">Edit Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="update">Update to be applied to the message</param>
        public IPromise<DiscordMessage> Edit(DiscordClient client, MessageUpdate update)
        {
            InvalidSnowflakeException.ThrowIfInvalid(Id, nameof(Id));
            InvalidSnowflakeException.ThrowIfInvalid(ChannelId, nameof(ChannelId));
            return client.Bot.Rest.Patch<DiscordMessage>(client,$"channels/{ChannelId}/messages/{Id}", update);
        }

        /// <summary>
        /// Edit a message using a global message template
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        /// <param name="update">Update to be applied to the message</param>
        public IPromise<DiscordMessage> EditGlobalTemplateMessage(DiscordClient client, string templateName, PlaceholderData placeholders = null, MessageUpdate update = null)
        {
            MessageUpdate template = DiscordExtension.DiscordMessageTemplates.GetGlobalTemplate(client.Plugin, templateName).ToMessage(placeholders, update);
            return Edit(client, template);
        }

        /// <summary>
        /// Edit a message using a localized message template
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="language">Oxide language to use</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        /// <param name="update">Update to be applied tothe message</param>
        public IPromise<DiscordMessage> EditTemplateMessage(DiscordClient client, string templateName, string language = DiscordLocales.DefaultServerLanguage, PlaceholderData placeholders = null, MessageUpdate update = null)
        {
            MessageUpdate template = DiscordExtension.DiscordMessageTemplates.GetLocalizedTemplate(client.Plugin, templateName, language).ToMessage(placeholders, update);
            return Edit(client, template);
        }

        /// <summary>
        /// Delete a message.
        /// If operating on a guild channel and trying to delete a message that was not sent by the current user, this endpoint requires the MANAGE_MESSAGES permission.
        /// See <a href="https://discord.com/developers/docs/resources/channel#delete-message">Delete Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        public IPromise Delete(DiscordClient client)
        {
            InvalidSnowflakeException.ThrowIfInvalid(Id, nameof(Id));
            InvalidSnowflakeException.ThrowIfInvalid(ChannelId, nameof(ChannelId));
            InvalidMessageException.ThrowIfCantBeDeleted(this);
            return client.Bot.Rest.Delete(client,$"channels/{ChannelId}/messages/{Id}");
        }

        /// <summary>
        /// Pin a message in a channel.
        /// Requires the MANAGE_MESSAGES permission.
        /// See <a href="https://discord.com/developers/docs/resources/channel#add-pinned-channel-message">Add Pinned Channel Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        public IPromise Pin(DiscordClient client)
        {
            InvalidSnowflakeException.ThrowIfInvalid(Id, nameof(Id));
            InvalidSnowflakeException.ThrowIfInvalid(ChannelId, nameof(ChannelId));
            return client.Bot.Rest.Put(client,$"channels/{ChannelId}/pins/{Id}", null);
        }

        /// <summary>
        /// Delete a pinned message in a channel.
        /// Requires the MANAGE_MESSAGES permission.
        /// See <a href="https://discord.com/developers/docs/resources/channel#delete-pinned-channel-message">Unpin Pinned Channel Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        public IPromise Unpin(DiscordClient client)
        {
            return client.Bot.Rest.Delete(client,$"channels/{ChannelId}/pins/{Id}");
        }
        
        /// <summary>
        /// Creates a new public thread this message
        /// See <a href="https://discord.com/developers/docs/resources/channel#start-thread-from-message"></a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="create">Data to use when creating the thread</param>
        public IPromise<DiscordChannel> StartThread(DiscordClient client, ThreadCreateFromMessage create)
        {
            if (create == null) throw new ArgumentNullException(nameof(create));
            return client.Bot.Rest.Post<DiscordChannel>(client,$"channels/{ChannelId}/messages/{Id}/threads", create);
        }
    }
}
