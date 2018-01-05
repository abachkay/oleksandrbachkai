using oleksandrbachkai.Adapters;
using oleksandrbachkai.DataAccess;
using oleksandrbachkai.Models;
using oleksandrbachkai.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace oleksandrbachkai.Controllers
{
    [RoutePrefix("api/files")]
    [Authorize]
    [Authorize(Roles = "Administrator")]
    public class FilesController : ApiController
    {        
        private readonly GoogleDriveAdapter _driveAdapter = new GoogleDriveAdapter();
        private readonly IFoldersRepository _foldersRepository = new SqlFoldersRepository();
        private readonly IFilesRepository _filesRepository = new SqlFilesRepository();

        [Route("folders")]
        [HttpGet]
        public async Task<IHttpActionResult> GetFolders()
        {
            return new OkNegotiatedContentResult<IEnumerable<Folder>>(await _foldersRepository.GetAll(), this);
        }

        [Route("folders")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateFolder([FromBody]string folderName)
        {
            await _foldersRepository.Insert(new Folder()
            {
                Name = folderName
            });                        

            return Ok();
        }

        [Route("folders/{folderId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteFolder(int folderId)
        {
            var folder = await _foldersRepository.Get(folderId);

            if (folder == null)
            {
                return NotFound();                
            }

            foreach (var file in folder.Files)
            {
                _driveAdapter.DeleteFile(file.DriveId);
            }

            try
            {
                await _foldersRepository.Delete(folderId);
            }
            catch
            {
                return NotFound();
                
            }
            
            return Ok();
        }


        [Route("{folderId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetFolderFiles(int folderId)
        {
            var files = (await _foldersRepository.Get(folderId))?.Files;

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
            var folder = await _foldersRepository.Get(folderId);

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

            await _filesRepository.Insert(file);

            return Ok();
        }

        [Route("{folderId}")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateFileInFolder(int folderId)
        {
            var folder = await _foldersRepository.Get(folderId);

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

            await _filesRepository.Insert(file);

            return Ok();
        }

        [Route("{folderId}/{fileId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteFileInFolder(int folderId, int fileId)
        {
            var file = await _filesRepository.Get(fileId);

            if (file == null)
            {
                return NotFound();
            }

            await _filesRepository.Delete(fileId);

            _driveAdapter.DeleteFile(file.DriveId);

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            _foldersRepository?.Dispose();
            _filesRepository?.Dispose();
            _driveAdapter?.Dispose();

            base.Dispose(disposing);
        }
    }
}