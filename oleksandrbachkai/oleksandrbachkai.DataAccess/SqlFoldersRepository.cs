using oleksandrbachkai.Models.Context;
using oleksandrbachkai.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace oleksandrbachkai.DataAccess
{
    public class SqlFoldersRepository : IFoldersRepository
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public async Task<IEnumerable<Folder>> GetAll()
        {
            return await _context.Folders.ToListAsync();
        }

        public async Task<Folder> Get(int id)
        {
            return await _context.Folders.FirstOrDefaultAsync(f => f.FolderId == id);
        }

        public async Task Insert(Folder data)
        {
            _context.Folders.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var folder = await _context.Folders.FirstOrDefaultAsync(f => f.FolderId == id);

            if (folder == null)
            {
                throw new ArgumentException();
            }

            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();
        }        
       
        public async Task Update(int id, Folder data)
        {
            var folder = await _context.Folders.FirstOrDefaultAsync(f => f.FolderId == id);

            if (folder == null)
            {
                throw new ArgumentException();
            }

            folder.Name = data.Name;
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}