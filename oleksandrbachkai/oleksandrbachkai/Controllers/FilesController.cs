using System.Collections.Generic;
using System.Linq;
using oleksandrbachkai.Adapters;
using oleksandrbachkai.Models.Context;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using oleksandrbachkai.Models;
using oleksandrbachkai.Models.Entities;

namespace oleksandrbachkai.Controllers
{
    [RoutePrefix("api/files")]
    public class FilesController : ApiController
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private readonly GoogleDriveAdapter _driveAdapter = new GoogleDriveAdapter();

        [Route("folders")]
        [HttpGet]
        public async Task<IHttpActionResult> GetFolders()
        {
            return new OkNegotiatedContentResult<IEnumerable<Folder>>(_context.Folders.ToList(), this);
        }

        [Route("folders")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateFolder([FromBody]string folderName)
        {
            _context.Folders.Add(new Folder()
            {
                Name = folderName
            });

            _context.SaveChanges();

            return Ok();
        }

        [Route("folders/{folderId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteFolder(int folderId)
        {
            var folder = _context.Folders.FirstOrDefault(f => f.FolderId == folderId);

            if (folder == null)
            {
                return NotFound();                
            }

            foreach (var file in folder.Files)
            {
                _driveAdapter.DeleteFile(file.DriveId);
            }
            _context.Files.RemoveRange(folder.Files);            

            _context.Folders.Remove(folder);
            _context.SaveChanges();

            return Ok();
        }


        [Route("{folderId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetFolderFiles(int folderId)
        {
            var files = _context.Folders.FirstOrDefault(f => f.FolderId == folderId)?.Files;

            if (files == null || files.Count == 0)
            {
                return NotFound();
            }

            return new OkNegotiatedContentResult<IEnumerable<File>>(files, this);
        }

        [Route("{folderId}/manual")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateFileInFolderManual(int folderId, FileRequestModel fileModel)
        {
            var folder = _context.Folders.FirstOrDefault(f => f.FolderId == folderId);

            if (folder == null)
            {
                return NotFound();                
            }

            var file = new File
            {
                FileName = fileModel.FileName,
                Url = fileModel.Url,
                DriveId = fileModel.DriveId,
                FolderId = folderId
            };

            _context.Files.Add(file);
            _context.SaveChanges();

            return Ok();
        }

        [Route("{folderId}")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateFileInFolder(int folderId)
        {
            var folder = _context.Folders.FirstOrDefault(f => f.FolderId == folderId);

            if (folder == null)
            {
                return NotFound();
            }

            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            var provider = new MultipartMemoryStreamProvider();
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
            }
            catch 
            {
                return BadRequest();
            }            
            var responseFile = provider.Contents.Last();
           
            var filename = responseFile.Headers.ContentDisposition.FileName.Trim('\"');
            var buffer = await responseFile.ReadAsByteArrayAsync();

            var driveId = _driveAdapter.UploadFile(filename, buffer);
            
            var file = new File
            {
                FileName = filename,
                Url = GoogleDriveAdapter.GetUrlByFileId(driveId),
                DriveId = driveId,
                FolderId = folderId
            };

            _context.Files.Add(file);
            _context.SaveChanges();

            return Ok();
        }

        [Route("{folderId}/{fileId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteFileInFolder(int folderId, int fileId)
        {
            var file = _context.Folders.FirstOrDefault(f => f.FolderId == folderId)?.Files.FirstOrDefault(f => f.FileId == fileId);

            if (file == null)
            {
                return NotFound();
            }

            _context.Files.Remove(file);
            _context.SaveChanges();

            _driveAdapter.DeleteFile(file.DriveId);

            return Ok();
        }
    }
}