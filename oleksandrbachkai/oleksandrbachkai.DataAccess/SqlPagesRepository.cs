using System;
using oleksandrbachkai.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using oleksandrbachkai.Models.Context;
using System.Linq;
using System.Threading.Tasks;

namespace oleksandrbachkai.DataAccess
{
    public class SqlPagesRepository : IPagesRepository
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public async Task<IEnumerable<Page>> GetAll()
        {
            return await _context.Pages.ToListAsync();
        }

        public async Task<Page> Get(int id)
        {
            return await _context.Pages.FirstOrDefaultAsync(p => p.PageId == id);            
        }

        public async Task Insert(Page data)
        {
            _context.Pages.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var page = await _context.Pages.FirstOrDefaultAsync(p => p.PageId == id);

            if (page == null)
            {
                throw new ArgumentException();
            }

            _context.Pages.Remove(page);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Page data)
        {
            var page = await _context.Pages.FirstOrDefaultAsync(p => p.PageId == id);

            if (page == null)
            {
                throw new ArgumentException();
            }

            page.Name = data.Name;
            page.Content = data.Content;
            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<PageName>> GetPageNames()
        {
            return (await _context.Pages.ToListAsync()).Select(p => new PageName() {PageId = p.PageId, Name = p.Name});
        }

        public async Task UpdatePageContent(int id, string content)
        {
            var page = await _context.Pages.FirstOrDefaultAsync(p => p.PageId == id);

            if (page == null)
            {
                throw new ArgumentException();
            }

            page.Content = content;
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}