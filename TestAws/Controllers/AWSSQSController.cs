using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAws.Models;
using TestAws.Services;

namespace TestAws.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class AWSSQSController : ControllerBase
    {
        private readonly IAWSSQSService _AWSSQSService;

        public AWSSQSController(IAWSSQSService aWSSQSService)
        {
            _AWSSQSService = aWSSQSService;
        }

        [Route("postMessage")]
        [HttpPost]
        public async Task<IActionResult> PostMessageAsync(User user)
        {
            var result = await _AWSSQSService.PostMessageAsync(user);
            return Ok(new { IsSuccess = result });
        }

        [Route("getAllMessages")]
        [HttpGet]
        public async Task<IActionResult> GetAllMessageAsync()
        {
            var result = await _AWSSQSService.GetAllMessages();
            return Ok(result);
        }

        [Route("deleteMessage")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMessageAsync(DeleteMessage deleteMessage)
        {
            var result = await _AWSSQSService.DeleteMessageAsync(deleteMessage);
            return Ok(new { IsSuccess = result });
        }

    }
}
