
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.ServiceBus;
using System.Text;
using AzureFuncLib;

namespace TopicTools
{
    public static class SendMessage
    {
        [FunctionName("SendMessage")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            if(data == null) return new BadRequestObjectResult("Please give topic and message");
            string topicConnection = data.topicConnection;
            string topicName = data.topicName;
            dynamic message = data.message;

            if (topicConnection == null || topicName == null || message == null)
            {
                log.LogWarning("Empty topic info");
                var webHookUrl = Environment.GetEnvironmentVariable("LoggerHttpFunctionUrl");
                await HttpClientHelper.PostAsync(webHookUrl, "Empty topic info");
                return new BadRequestObjectResult("Please give topic and message");
            }

            log.LogInformation($"Start sending message {message}");
            ITopicClient topicClient = new TopicClient(topicConnection, topicName);
            try
            {
                Message msg = new Message(Encoding.UTF8.GetBytes(Convert.ToString(message)))
                {
                    Label = data.label
                };
                await topicClient.SendAsync(msg);
            }
            catch (Exception ex)
            {
                log.LogError($"Exception in send message: Exception: {ex.Message}, Stack trace: {ex.StackTrace}");
                return new BadRequestObjectResult("Topic error");
            }

            return new OkObjectResult("OK");
        }
    }
}
