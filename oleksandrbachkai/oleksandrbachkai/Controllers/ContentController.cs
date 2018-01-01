using oleksandrbachkai.Models.Context;
using oleksandrbachkai.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net;

namespace oleksandrbachkai.Controllers
{
    [RoutePrefix("api/content")]
    public class ContentController : ApiController
    {
        private DatabaseContext _context = new DatabaseContext();

        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPages()
        {
            return new OkNegotiatedContentResult<IEnumerable<Page>>(_context.Pages, this);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPage(int id)
        {
            var page = _context.Pages.FirstOrDefault(p => p.PageId == id);

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
            if (! ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Pages.Add(page);
            _context.SaveChanges();

            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeletePage(int id)
        {
            var page = _context.Pages.FirstOrDefault(p => p.PageId == id);

            if (page == null)
            {
                return NotFound();
            }

            _context.Pages.Remove(page);
            _context.SaveChanges();

            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdatePage(int id, Page newPage)
        {
            var page = _context.Pages.FirstOrDefault(p => p.PageId == id);

            if (page == null)
            {
                return NotFound();
            }

            page.Name = newPage.Name;
            page.Content = newPage.Content;
            _context.SaveChanges();

            return new OkNegotiatedContentResult<Page>(newPage, this);
        }
    }
}