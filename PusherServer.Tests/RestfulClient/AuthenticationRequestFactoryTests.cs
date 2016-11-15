﻿using NUnit.Framework;
using PusherServer.RestfulClient;
using PusherServer.Tests.RestfulClient.Fakes;

namespace PusherServer.Tests.RestfulClient
{
    [TestFixture]
    public class When_creating_an_authenticated_request_for_pusher
    {
        private AuthenticatedRequestFactory _authenticatedRequestFactory;
        private MyTestObject _requestParameters;
        private MyTestObject _requestBody;

        private TestObjectFactory _factory;

        [TestFixtureSetUp]
        public void Setup()
        {
            // Arrange
            _factory = new TestObjectFactory();

            _authenticatedRequestFactory = new AuthenticatedRequestFactory("test_app_key", "test_app_id", "test_app_secret");
            _requestParameters = _factory.Create("Test Property 1", 2, true);
            _requestBody = _factory.Create("Test Property 4", 5, false);
        }

        [Test]
        public void then_the_request_should_be_made_with_no_parameters()
        {
            // Act
            var request = _authenticatedRequestFactory.Build(PusherMethod.GET, "/testPath");

            // Assert
            Assert.IsNotNull(request);
            Assert.AreEqual(PusherMethod.GET, request.Method);
            StringAssert.StartsWith(@"/apps/test_app_id/testPath?auth_key=test_app_key&auth_timestamp=", request.ResourceUri);
            StringAssert.Contains(@"&auth_version=1.0&auth_signature=", request.ResourceUri);
        }

        [Test]
        public void then_the_request_should_be_made_with_parameters_from_a_source_object()
        {
            // Act
            var request = _authenticatedRequestFactory.Build(PusherMethod.GET, "/testPath", _requestParameters);

            // Assert
            Assert.IsNotNull(request);
            Assert.AreEqual(PusherMethod.GET, request.Method);
            StringAssert.StartsWith(@"/apps/test_app_id/testPath?Property1=Test Property 1&Property2=2&Property3=True&auth_key=test_app_key&auth_timestamp=", request.ResourceUri);
            StringAssert.Contains(@"&auth_version=1.0&auth_signature=", request.ResourceUri);
        }

        [Test]
        public void then_the_request_should_be_made_with_a_content_object()
        {
            // Act
            var request = _authenticatedRequestFactory.Build(PusherMethod.POST, "/testPath", requestBody: _requestBody);

            // Assert
            Assert.IsNotNull(request);
            Assert.AreEqual(PusherMethod.POST, request.Method);
            StringAssert.StartsWith(@"/apps/test_app_id/testPath?auth_key=test_app_key&auth_timestamp=", request.ResourceUri);
            StringAssert.Contains(@"&auth_version=1.0&body_md5=41db1761b31df9dc02b2811b38c010d4", request.ResourceUri);
            StringAssert.Contains(@"&auth_signature=", request.ResourceUri);
        }

        [Test]
        public void then_the_request_should_be_made_with_parameters_from_a_source_object_and_a_content_object()
        {
            // Act
            var request = _authenticatedRequestFactory.Build(PusherMethod.POST, "/testPath", _requestParameters, _requestBody);

            // Assert
            Assert.IsNotNull(request);
            Assert.AreEqual(PusherMethod.POST, request.Method);
            StringAssert.StartsWith(@"/apps/test_app_id/testPath?Property1=Test Property 1&Property2=2&Property3=True&auth_key=test_app_key&auth_timestamp=", request.ResourceUri);
            StringAssert.Contains(@"&auth_version=1.0&body_md5=41db1761b31df9dc02b2811b38c010d4", request.ResourceUri);
            StringAssert.Contains(@"&auth_signature=", request.ResourceUri);
        }

        [Test]
        public void then_the_request_should_return_the_body_as_a_string()
        {
            // Act
            var request = _authenticatedRequestFactory.Build(PusherMethod.POST, "/testPath", _requestParameters, _requestBody);

            // Assert
            Assert.IsNotNull(request);
            Assert.AreEqual(PusherMethod.POST, request.Method);
            StringAssert.Contains("{\"Property1\":\"Test Property 4\",\"Property2\":5,\"Property3\":false}", request.GetContentAsJsonString());
        }
    }
}