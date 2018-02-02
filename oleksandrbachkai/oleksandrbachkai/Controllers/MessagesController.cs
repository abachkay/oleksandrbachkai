using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using oleksandrbachkai.App_Start;
using oleksandrbachkai.DataAccess;
using oleksandrbachkai.Models;
using oleksandrbachkai.Models.Entities;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace oleksandrbachkai.Controllers
{
    [RoutePrefix("api/messages")]
    [Authorize]
    public class MessagesController : ApiController
    {        
        private readonly IMessagesRepository _messagesRepository = new SqlMessagesRepository();

        [Route("")]
        [HttpGet]        
        public async Task<IHttpActionResult> GetMessages()
        {
            return new OkNegotiatedContentResult<IEnumerable<Message>>(await _messagesRepository.GetAll(), this);
        }       

        [Route("{id}")]
        [HttpGet]        
        public async Task<IHttpActionResult> GetMessage(int id)
        {
            var message = await _messagesRepository.Get(id);

            if (message == null)
            {
                return NotFound();
            }

            return new OkNegotiatedContentResult<Message>(message, this);
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            message.ApplicationUserId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();

            await _messagesRepository.Insert(message);

            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteMessage(int id)
        {
            try
            {
                await _messagesRepository.Delete(id);
            }
            catch
            {
                return NotFound();
            }
            
            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateMessage(int id, Message newMessage)
        {
            try
            {
                await _messagesRepository.Update(id, newMessage);

            }
            catch
            {
                return NotFound();
            }
            
            return new OkNegotiatedContentResult<Message>(newMessage, this);
        }             

        protected override void Dispose(bool disposing)
        {
            _messagesRepository?.Dispose();            

            base.Dispose(disposing);
        }
    }
}