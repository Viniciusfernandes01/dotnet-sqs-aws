using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TestAws.Configuration;
using TestAws.Models;

namespace TestAws.Helpers
{
    public class AWSSQSHelper : IAWSSQSHelper
    {
        private readonly IAmazonSQS _sqs;
        private readonly ServiceConfiguration _settings;

        public AWSSQSHelper(IAmazonSQS sqs, IOptions<ServiceConfiguration> settings)
        {
            _sqs = sqs;
            _settings = settings.Value;
        }

        public async Task<bool> DeleteMessageAsync(string messageReceiptHandle)
        {
            try
            {
                // apagar alguma mensagem dentro da fila sqs
                var deletResult = await _sqs.DeleteMessageAsync(_settings.AWSSQS.QueueUrl,
                    messageReceiptHandle);

                return deletResult.HttpStatusCode == HttpStatusCode.OK;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Message>> ReceiveMessageAsync()
        {
            try
            {
                // nova instancia
                var request = new ReceiveMessageRequest
                {
                    QueueUrl = _settings.AWSSQS.QueueUrl,
                    MaxNumberOfMessages = 10,
                    WaitTimeSeconds = 5
                };

                //checar se tem alguma mensagem para ler
                var result = await _sqs.ReceiveMessageAsync(request);

                return result.Messages.Any() ? result.Messages : new List<Message>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> SendMessageAsync(UserDetail userDetail)
        {
            try
            {
                string message = JsonConvert.SerializeObject(userDetail);
                var sendRequest = new SendMessageRequest(_settings.AWSSQS.QueueUrl, message);
                var sendResult = await _sqs.SendMessageAsync(sendRequest);

                return sendResult.HttpStatusCode == HttpStatusCode.OK;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
