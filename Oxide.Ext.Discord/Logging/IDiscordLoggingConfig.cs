﻿namespace Oxide.Ext.Discord.Logging
{
    /// <summary>
    /// Interface for Discord Logging Configuration
    /// </summary>
    public interface IDiscordLoggingConfig
    {
        /// <summary>
        /// Log Level for the Console
        /// </summary>
        DiscordLogLevel ConsoleLogLevel { get; }
        
        /// <summary>
        /// Log Level for file Logging
        /// </summary>
        DiscordLogLevel FileLogLevel { get; }
        
        /// <summary>
        /// Format for console logging
        /// </summary>
        string ConsoleLogFormat { get; }
        
        /// <summary>
        /// Format for file logging
        /// </summary>
        string FileLogFormat { get; }
    }
}