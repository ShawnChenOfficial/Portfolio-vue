using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Portfolio.Data;
using Portfolio.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portfolio.Controllers
{
    [Route("api/[action]")]
    public class ToolsController : Controller
    {

        private ApplicationDbContext _context;
        private IHttpContextAccessor _accessor;
        private IConfiguration _configuration;

        public ToolsController(ApplicationDbContext context, IHttpContextAccessor accessor, IConfiguration configuration)
        {
            this._context = context;
            this._accessor = accessor;
            this._configuration = configuration;
        }

        [HttpPost("{json}")]
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
        public string GetViewers()
        {
            var x = _context.ViewHistory.GroupBy(g => g.IP).Select(s => s.Max(m => m.UTC_DateTime)).Count().ToString();

            return x;
        }

        public List<ViewHistory> GetViewsWithDetails()
        {
            return _context.ViewHistory.ToList();
        }

        public async Task<bool> NewViewer()
        {
            try
            {
                await _context.ViewHistory.AddAsync(new ViewHistory
                {
                    IP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString()
                });

                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
