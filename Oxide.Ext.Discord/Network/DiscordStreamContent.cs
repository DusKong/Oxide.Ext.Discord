using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Extensions;

namespace Oxide.Ext.Discord.Network
{
    /// <summary>
    /// Stream content that is sent over HTTP
    /// This is used because <see cref="StreamContent"/> disposes the underlying stream when disposed and we don't want that since we cache our stream
    /// </summary>
    public class DiscordStreamContent : HttpContent
    {
        private readonly Stream _content;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">Stream content to send over HTTP</param>
        /// <exception cref="ArgumentNullException">Throws if content is null</exception>
        public DiscordStreamContent(Stream content)
        {
            _content = content ?? throw new ArgumentNullException(nameof(content));
        }

        ///<inheritdoc/>
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            _content.Position = 0;
            return _content.CopyToPooledAsync(stream);
        }

        ///<inheritdoc/>
        protected override bool TryComputeLength(out long length)
        {
            length = _content.Length;
            return true;
        }

        ///<inheritdoc/>
        protected override Task<Stream> CreateContentReadStreamAsync() => Task.FromResult(_content);
    }
}