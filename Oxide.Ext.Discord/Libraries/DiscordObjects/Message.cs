﻿using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Libraries.WebSockets;

namespace Oxide.Ext.Discord.Libraries.DiscordObjects
{
    public class Message
    {
        public string webhook_id { get; set; }
        public int type { get; set; }
        public bool tts { get; set; }
        public string timestamp { get; set; }
        public bool pinned { get; set; }
        public object nonce { get; set; }
        public List<User> mentions { get; set; }
        public List<string> mention_roles { get; set; }
        public bool mention_everyone { get; set; }
        public string id { get; set; }
        public List<Embed> embeds { get; set; }
        public Embed embed { get; set; }
        public object edited_timestamp { get; set; }
        public string content { get; set; }
        public string channel_id { get; set; }
        public Author author { get; set; }
        public List<object> attachments { get; set; }

        public void CreateReaction(DiscordClient client, string emoji)
        {
            client.REST.DoRequest($"/channels/{channel_id}/messages/{id}/reactions/{emoji}/@me", "PUT");
        }

        public void DeleteOwnReaction(DiscordClient client, string emoji)
        {
            client.REST.DoRequest($"/channels/{channel_id}/messages/{id}/reactions/{emoji}/@me", "DELETE");
        }

        public void DeleteOwnReaction(DiscordClient client, string emoji, string userID)
        {
            client.REST.DoRequest($"/channels/{channel_id}/messages/{id}/reactions/{emoji}/{userID}", "DELETE");
        }

        public void GetReactions(DiscordClient client, string emoji, Action<List<User>> callback = null)
        {
            client.REST.DoRequest<List<User>>($"/channels/{channel_id}/messages/{id}/reactions/{emoji}", "GET", null, (returnValue) =>
            {
                callback?.Invoke(returnValue as List<User>);
            });
        }

        public void DeleteAllReactions(DiscordClient client)
        {
            client.REST.DoRequest($"/channels/{channel_id}/messages/{id}/reactions", "DELETE");
        }

        public void EditMessage(DiscordClient client, Action<Message> callback = null)
        {
            client.REST.DoRequest<Message>($"/channels/{channel_id}/messages/{id}", "PATCH", this, (returnValue) =>
            {
                callback?.Invoke(returnValue as Message);
            });
        }

        public void DeleteMessage(DiscordClient client, Action<Message> callback = null)
        {
            client.REST.DoRequest($"/channels/{channel_id}/messages/{id}", "DELETE");
        }

        public void AddPinnedChannelMessage(DiscordClient client)
        {
            client.REST.DoRequest($"/channels/{channel_id}/pins/{id}", "PUT");
        }

        public void DeletePinnedChannelMessage(DiscordClient client)
        {
            client.REST.DoRequest($"/channels/{channel_id}/pins/{id}", "DELETE");
        }
    }
}
