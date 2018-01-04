using oleksandrbachkai.DataAccess;
using oleksandrbachkai.Models.Context;
using oleksandrbachkai.Models.Entities;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace oleksandrbachkai.Controllers
{
    [RoutePrefix("api/content")]
    public class ContentController : ApiController
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private readonly IPagesRepository _pagesRepository = new SqlPagesRepository();

        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPages()
        {
            return new OkNegotiatedContentResult<IEnumerable<Page>>(await _pagesRepository.GetAll(), this);
        }       

        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPage(int id)
        {
            var page = await _pagesRepository.Get(id);

            if (page == null)
            {
                return NotFound();
            }

            return new OkNegotiatedContentResult<Page>(page, this);
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertPage(Page page)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _pagesRepository.Insert(page);

            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeletePage(int id)
        {
            try
            {
                await _pagesRepository.Delete(id);
            }
            catch
            {
                return NotFound();
            }
            
            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdatePage(int id, Page newPage)
        {
            try
            {
                await _pagesRepository.Update(id, newPage);

            }
            catch
            {
                return NotFound();
            }
            
            return new OkNegotiatedContentResult<Page>(newPage, this);
        }

        [Route("names")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPageNames()
        {
            return new OkNegotiatedContentResult<IEnumerable<PageName>>(await _pagesRepository.GetPageNames(), this);
        }

        [Route("{id}/content")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdatePageContent(int id, [FromBody]string content)
        {
            try
            {
                await _pagesRepository.UpdatePageContent(id, content);

            }
            catch
            {
                return NotFound();
            }

            return new OkNegotiatedContentResult<string>(content, this);
        }      

        protected override void Dispose(bool disposing)
        {
            _context?.Dispose();            

            base.Dispose(disposing);
        }
    }
}