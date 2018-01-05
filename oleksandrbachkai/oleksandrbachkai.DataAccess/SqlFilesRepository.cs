using oleksandrbachkai.Models.Context;
using oleksandrbachkai.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace oleksandrbachkai.DataAccess
{
    public class SqlFilesRepository : IFilesRepository
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public async Task<IEnumerable<File>> GetAll()
        {
            return await _context.Files.ToListAsync();
        }

        public async Task<File> Get(int id)
        {
            return await _context.Files.FirstOrDefaultAsync(f => f.FileId == id);
        }

        public async Task Insert(File data)
        {
            _context.Files.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var file = await _context.Files.FirstOrDefaultAsync(f => f.FileId == id);

            if (file == null)
            {
                throw new ArgumentException();
            }

            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, File data)
        {
            var file = await _context.Files.FirstOrDefaultAsync(f => f.FileId == id);

            if (file == null)
            {
                throw new ArgumentException();
            }

            file.DriveId = data.DriveId;
            file.FolderId = data.FolderId;
            file.FileName = data.FileName;
            file.Url = data.Url;
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}