using System;
using System.Collections.Generic;
using System.Linq;
using oleksandrbachkai.Models.Context;
using oleksandrbachkai.Models.Entities;

namespace oleksandrbachkai.DataAccess
{
    public class SqlFoldersRepository : IFoldersRepository
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public IEnumerable<Folder> GetAll()
        {
            return _context.Folders;
        }

        public Folder Get(int id)
        {
            return _context.Folders.FirstOrDefault(f => f.FolderId == id);
        }

        public void Insert(Folder data)
        {
            _context.Folders.Add(data);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var folder = _context.Folders.FirstOrDefault(f => f.FolderId == id);

            if (folder == null)
            {
                throw new ArgumentException();
            }

            _context.Folders.Remove(folder);
            _context.SaveChanges();
        }        
       
        public void Update(int id, Folder data)
        {
            var folder = _context.Folders.FirstOrDefault(f => f.FolderId == id);

            if (folder == null)
            {
                throw new ArgumentException();
            }

            folder.Name = data.Name;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}