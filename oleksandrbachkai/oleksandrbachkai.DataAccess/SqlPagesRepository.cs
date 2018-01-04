using System;
using oleksandrbachkai.Models.Entities;
using System.Collections.Generic;
using oleksandrbachkai.Models.Context;
using System.Linq;

namespace oleksandrbachkai.DataAccess
{
    public class SqlPagesRepository : IPagesRepository
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public IEnumerable<Page> GetAll()
        {
            return _context.Pages;
        }

        public Page Get(int id)
        {
            return _context.Pages.FirstOrDefault(p => p.PageId == id);            
        }

        public void Insert(Page data)
        {
            _context.Pages.Add(data);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var page = _context.Pages.FirstOrDefault(p => p.PageId == id);

            if (page == null)
            {
                throw new ArgumentException();
            }

            _context.Pages.Remove(page);
            _context.SaveChanges();
        }

        public void Update(int id, Page data)
        {
            var page = _context.Pages.FirstOrDefault(p => p.PageId == id);

            if (page == null)
            {
                throw new ArgumentException();
            }

            page.Name = data.Name;
            page.Content = data.Content;
            _context.SaveChanges();
        }
        
        public IEnumerable<PageName> GetPageNames()
        {
            return _context.Pages.Select(p => new PageName() {PageId = p.PageId, Name = p.Name});
        }

        public void UpdatePageContent(int id, string content)
        {
            var page = _context.Pages.FirstOrDefault(p => p.PageId == id);

            if (page == null)
            {
                throw new ArgumentException();
            }

            page.Content = content;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}