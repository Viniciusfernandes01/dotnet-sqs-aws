using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAws.Models;

namespace TestAws.Services
{
    public interface IAWSSQSService
    {
        Task<bool> PostMessageAsync(User user);
        Task<List<AllMessage>> GetAllMessages();
        Task<bool> DeleteMessageAsync(DeleteMessage deleteMessage);
    }
}
