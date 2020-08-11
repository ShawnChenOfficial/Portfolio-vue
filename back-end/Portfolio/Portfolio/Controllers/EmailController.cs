using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Portfolio.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portfolio.Controllers
{
    public class EmailController : Controller
    {
        private IConfiguration _configuration;

        public EmailController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Send(string json)
        {
            Message message = JsonConvert.DeserializeObject<Message>(json);

            if (!ModelState.IsValid)
            {
                var errors_pair = new List<Object>();

                foreach (var x in ModelState.Keys)
                {
                    if (ModelState[x].Errors.Count() > 0)
                    {
                        var v1 = x;
                        var v2 = ModelState[x].Errors[0].ErrorMessage;

                        errors_pair.Add(new { Key = x, ErrorMessage = ModelState[x].Errors[0].ErrorMessage });
                    }
                }

                return Content(JsonConvert.SerializeObject(errors_pair));
            }

            var result = false;

            try
            {
                result = await new MaillingLib.MailingService(_configuration).SendAsync(message);
            }
            catch (Exception e)
            {
                var x = e.Message;
            }

            return Content(JsonConvert.SerializeObject(result));
        }
    }
}
