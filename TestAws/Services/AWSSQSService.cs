using Amazon.SQS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAws.Helpers;
using TestAws.Models;

namespace TestAws.Services
{
    public class AWSSQSService : IAWSSQSService
    {
        private readonly IAWSSQSHelper _AWSSQSHelper;

        public AWSSQSService(IAWSSQSHelper AWSSQSHelper)
        {
            _AWSSQSHelper = AWSSQSHelper;
        }

        public async Task<bool> DeleteMessageAsync(DeleteMessage deleteMessage)
        {
            try
            {
                return await _AWSSQSHelper.DeleteMessageAsync(deleteMessage.ReceiptHandle);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<AllMessage>> GetAllMessages()
        {
            List<AllMessage> allMessages = new List<AllMessage>();
            try
            {
                List<Message> messages = await _AWSSQSHelper.ReceiveMessageAsync();
                allMessages = messages.Select(c => new AllMessage
                {
                    MessageId = c.MessageId,
                    ReceiptHandle = c.ReceiptHandle,
                    UserDetail = JsonConvert.DeserializeObject<UserDetail>(c.Body)
                }).ToList();

                return allMessages;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> PostMessageAsync(User user)
        {
            try
            {
                UserDetail userDetail = new UserDetail();
                userDetail.Id = new Random().Next(999999999);
                userDetail.FirstName = user.FirstName;
                userDetail.LastName = user.LastName;
                userDetail.UserName = user.UserName;
                userDetail.EmailId = user.EmailId;
                userDetail.CreatedOn = DateTime.UtcNow;
                userDetail.UpdatedOn = DateTime.UtcNow;

                return await _AWSSQSHelper.SendMessageAsync(userDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
