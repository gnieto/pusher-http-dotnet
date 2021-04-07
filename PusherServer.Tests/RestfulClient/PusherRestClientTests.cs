﻿using System;
using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;
using PusherServer.RestfulClient;
using PusherServer.Tests.RestfulClient.Fakes;

namespace PusherServer.Tests.RestfulClient
{
    [TestFixture]
    public class When_making_a_request
    {
        [Test]
        public async Task then_the_get_request_should_be_made_with_a_valid_resource()
        {
            var factory = new AuthenticatedRequestFactory(Config.AppKey, Config.AppId, Config.AppSecret);
            var request = factory.Build(PusherMethod.GET, "/channels/newRestClient");

            Version version = Version.Parse(typeof(Pusher).GetTypeInfo().Assembly.GetName().Version.ToString(3));
            var client = new PusherRestClient($"http://{Config.HttpHost}", "pusher-http-dotnet", version);
            var response = await client.ExecuteGetAsync<TestOccupied>(request).ConfigureAwait(false);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.Data.Occupied);
        }
    }
}