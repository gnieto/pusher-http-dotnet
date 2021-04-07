﻿namespace PusherServer.Tests.Helpers
{
    /// <summary>
    /// Contains test application configuration.
    /// </summary>
    public class ApplicationConfig : IApplicationConfig
    {

        /// <inheritdoc/>
        public string AppId { get; set; }

        /// <inheritdoc/>
        public string AppKey { get; set; }

        /// <inheritdoc/>
        public string AppSecret { get; set; }

        /// <inheritdoc/>
        public string Cluster { get; set; } = "mt1";

        /// <inheritdoc/>
        public string HttpHost 
        { 
            get
            {
                return $"api-{Cluster}.pusher.com";
            }
        }

        /// <inheritdoc/>
        public string WebSocketHost
        {
            get
            {
                return $"ws-{Cluster}.pusher.com";
            }
        }
    }
}
