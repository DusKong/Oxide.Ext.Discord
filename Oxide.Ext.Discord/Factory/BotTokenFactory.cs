﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using Oxide.Ext.Discord.Clients;
using Oxide.Ext.Discord.Connections;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Singleton;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Factory
{
    internal class BotTokenFactory : Singleton<BotTokenFactory>
    {
        private readonly Regex _tokenValidator = new Regex(@"^[\w-]+\.[\w-]+\.[\w-]+$", RegexOptions.Compiled);
        private readonly char[] _splitArgs = {'.'};

        private readonly Hash<string, BotTokenData> _tokens = new Hash<string, BotTokenData>();
        
        private BotTokenFactory() { }

        internal BotTokenData CreateFromClient(DiscordClient client)
        {
            string token = client.Connection.ApiToken;
            
            BotTokenData botToken = _tokens[token];
            if (botToken == null)
            {
                botToken = ParseToken(token, client.PluginName);
                _tokens[token] = botToken;
            }
            
            if (!_tokenValidator.IsMatch(token))
            {
                DiscordExtension.GlobalLogger.Warning("API Token does not appear to be a valid discord bot token: {0} for plugin {1}. " +
                                                      "Please confirm you are using the correct bot token. " +
                                                      "If the token is correct and this message is showing please let the Discord Extension Developers know.", botToken.HiddenToken, client.PluginName);
            }

            return botToken;
        }
        
        private static string GenerateHiddenToken(string token)
        {
            StringBuilder sb = DiscordPool.Internal.GetStringBuilder();

            int last = token.LastIndexOf('.') + 1;
            sb.Append(token, 0, last);
            sb.Append('#', token.Length - last);

            return DiscordPool.Internal.FreeStringBuilderToString(sb);
        }
        
        private BotTokenData ParseToken(string token, string pluginName)
        {
            string hiddenToken = GenerateHiddenToken(token);
            string[] args = token.Split(_splitArgs);
            if (args.Length != 3)
            {
                DiscordExtension.GlobalLogger.Error("Failed to parse token {0} for plugin {1}", hiddenToken, pluginName);
                return new BotTokenData(hiddenToken, default(Snowflake));
            }

            if (!TryParseApplicationId(args[0], out Snowflake appId))
            {
                DiscordExtension.GlobalLogger.Error("Failed to parse application ID from bot token. Bot token is invalid. Token: {0}", hiddenToken);
            }

            return new BotTokenData(hiddenToken, appId);
        }

        private bool TryParseApplicationId(string base64AppId, out Snowflake id)
        {
            try
            {
                string appId = Encoding.UTF8.GetString(Convert.FromBase64String(base64AppId));
                id = new Snowflake(appId);
                return true;
            }
            catch(Exception ex)
            {
                DiscordExtension.GlobalLogger.Exception("An error occured parsing Token Application ID: {0}", base64AppId, ex);
                id = default(Snowflake);
                return false;
            }
        }
    }
}