﻿using PusherClient;

namespace PusherServer.Tests.Helpers
{
    internal class InMemoryAuthorizer: IAuthorizer
    {
        PusherServer.Pusher _pusher;
        PresenceChannelData _presenceData;

        public InMemoryAuthorizer(PusherServer.Pusher pusher):
            this(pusher, null)
        {
        }

        public InMemoryAuthorizer(PusherServer.Pusher pusher, PresenceChannelData presenceData)
        {
            _pusher = pusher;
            _presenceData = presenceData;
        }

        public string Authorize(string channelName, string socketId)
        {
            IAuthenticationData auth = null;
            if (_presenceData != null)
            {
                auth = _pusher.Authenticate(channelName, socketId, _presenceData);
            }
            else
            {
                auth = _pusher.Authenticate(channelName, socketId);
            }
            return auth.ToJson();
        }
    }
}
