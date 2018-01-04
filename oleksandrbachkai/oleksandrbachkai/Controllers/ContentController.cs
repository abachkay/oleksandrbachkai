using oleksandrbachkai.Models.Context;
using oleksandrbachkai.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net;
using System.Net.Http;
using oleksandrbachkai.Adapters;

namespace oleksandrbachkai.Controllers
{
    [RoutePrefix("api/content")]
    public class ContentController : ApiController
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPages()
        {
            return new OkNegotiatedContentResult<IEnumerable<Page>>(_context.Pages, this);
        }

        [Route("names")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPageNames()
        {
            return new OkNegotiatedContentResult<IEnumerable<PageName>>(_context.Pages.Select(p => new PageName(){PageId = p.PageId,Name = p.Name}), this);
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

        [Route("{id}/content")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdatePageContent(int id, [FromBody]string content)
        {
            var page = _context.Pages.FirstOrDefault(p => p.PageId == id);

            if (page == null)
            {
                return NotFound();
            }
           
            page.Content = content;
            _context.SaveChanges();

            return new OkNegotiatedContentResult<Page>(page, this);
        }

        [Route("file")]
        [HttpPost]
        public async Task<IHttpActionResult> PostFileAsync()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            if (provider.Contents.Count != 1)
            {
                return BadRequest("Only uploading of 1 file is supported");
            }
            foreach (var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                var buffer = await file.ReadAsByteArrayAsync();

                var driveAdapter = new GoogleDriveAdapter();
                driveAdapter.UploadFile(filename, buffer);
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            _context?.Dispose();            

            base.Dispose(disposing);
        }
    }
}