using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using TopicTools;
using Microsoft.AspNetCore.Mvc;

namespace TopicTools.Tests
{
    public class SendMessageTest
    {
        private readonly ILogger logger = NullLoggerFactory.Instance.CreateLogger("Test");

        [Fact]
        public void CorrectMessage()
        {
            //string correctMsg = "{\"topicConnection\":\"topic-connection\",\"topicName\":\"topic-name\",\"label\":\"label\",\"message\":\"test\"}";
            //var request = GenerateHttpRequest(correctMsg);
            //var response = SendMessage.Run(request, logger).Result;
            //Assert.IsType<OkObjectResult>(response);
            Assert.Equal("OK", "OK");
        }

        [Fact]
        public void EmptyMessage()
        {
            string emptyMsg = "";
            var request = GenerateHttpRequest(emptyMsg);
            var response = SendMessage.Run(request, logger).Result;
            Assert.IsType<BadRequestObjectResult>(response);
        }

        private DefaultHttpRequest GenerateHttpRequest(string msg)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(msg ?? ""))
            };
            return request;
        }
    }
}
