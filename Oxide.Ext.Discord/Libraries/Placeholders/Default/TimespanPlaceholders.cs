using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="TimeSpan"/> placeholders
    /// </summary>
    public static class TimeSpanPlaceholders
    {
        internal const string TimeSpanKey = "timespan";
        
        /// <summary>
        /// <see cref="TimeSpan.Days"/> placeholder
        /// </summary>
        public static int Days(TimeSpan time) => time.Days;
        
        /// <summary>
        /// <see cref="TimeSpan.Hours"/> placeholder
        /// </summary>
        public static int Hours(TimeSpan time) => time.Hours;
        
        /// <summary>
        /// <see cref="TimeSpan.Minutes"/> placeholder
        /// </summary>
        public static int Minutes(TimeSpan time) => time.Minutes;
        
        /// <summary>
        /// <see cref="TimeSpan.Seconds"/> placeholder
        /// </summary>
        public static int Seconds(TimeSpan time) => time.Seconds;

        /// <summary>
        /// <see cref="TimeSpan.Milliseconds"/> placeholder
        /// </summary>
        public static int Milliseconds(TimeSpan time) => time.Milliseconds;
        
        /// <summary>
        /// <see cref="TimeSpan.TotalDays"/> placeholder
        /// </summary>
        public static double TotalDays(TimeSpan time) => time.TotalDays;
        
        /// <summary>
        /// <see cref="TimeSpan.TotalHours"/> placeholder
        /// </summary>
        public static double TotalHours(TimeSpan time) => time.TotalHours;
        
        /// <summary>
        /// <see cref="TimeSpan.TotalMinutes"/> placeholder
        /// </summary>
        public static double TotalMinutes(TimeSpan time) => time.TotalMinutes;
        
        /// <summary>
        /// <see cref="TimeSpan.TotalSeconds"/> placeholder
        /// </summary>
        public static double TotalSeconds(TimeSpan time) => time.TotalSeconds;
        
        /// <summary>
        /// <see cref="TimeSpan.TotalMilliseconds"/> placeholder
        /// </summary>
        public static double TotalMilliseconds(TimeSpan time) => time.TotalMilliseconds;

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "timespan", TimeSpanKey);
        }
        
        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<TimeSpan>(plugin, $"{placeholderPrefix}.time", dataKey);
            placeholders.RegisterPlaceholder<TimeSpan, int>(plugin, $"{placeholderPrefix}.days", dataKey, Days);
            placeholders.RegisterPlaceholder<TimeSpan, int>(plugin, $"{placeholderPrefix}.hours", dataKey, Hours);
            placeholders.RegisterPlaceholder<TimeSpan, int>(plugin, $"{placeholderPrefix}.minutes", dataKey, Minutes);
            placeholders.RegisterPlaceholder<TimeSpan, int>(plugin, $"{placeholderPrefix}.seconds", dataKey, Seconds);
            placeholders.RegisterPlaceholder<TimeSpan, int>(plugin, $"{placeholderPrefix}.milliseconds", dataKey, Milliseconds);
            placeholders.RegisterPlaceholder<TimeSpan, double>(plugin, $"{placeholderPrefix}.total.days", dataKey, TotalDays);
            placeholders.RegisterPlaceholder<TimeSpan, double>(plugin, $"{placeholderPrefix}.total.hours", dataKey, TotalHours);
            placeholders.RegisterPlaceholder<TimeSpan, double>(plugin, $"{placeholderPrefix}.total.minutes", dataKey, TotalMinutes);
            placeholders.RegisterPlaceholder<TimeSpan, double>(plugin, $"{placeholderPrefix}.total.seconds", dataKey, TotalSeconds);
            placeholders.RegisterPlaceholder<TimeSpan, double>(plugin, $"{placeholderPrefix}.total.milliseconds", dataKey, TotalMilliseconds);
        }
    }
}