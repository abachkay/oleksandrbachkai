using System;
using System.Collections.Generic;
using System.Linq;
using oleksandrbachkai.Models.Context;
using oleksandrbachkai.Models.Entities;

namespace oleksandrbachkai.DataAccess
{
    public class SqlFilesRepository : IFilesRepository
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public IEnumerable<File> GetAll()
        {
            return _context.Files;
        }

        public File Get(int id)
        {
            return _context.Files.FirstOrDefault(f => f.FileId == id);
        }

        public void Insert(File data)
        {
            _context.Files.Add(data);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var file = _context.Files.FirstOrDefault(f => f.FileId == id);

            if (file == null)
            {
                throw new ArgumentException();
            }

            _context.Files.Remove(file);
            _context.SaveChanges();
        }

        public void Update(int id, File data)
        {
            var file = _context.Files.FirstOrDefault(f => f.FileId == id);

            if (file == null)
            {
                throw new ArgumentException();
            }

            file.DriveId = data.DriveId;
            file.FolderId = data.FolderId;
            file.FileName = data.FileName;
            file.Url = data.Url;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}