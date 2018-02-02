using oleksandrbachkai.Models.Context;
using oleksandrbachkai.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace oleksandrbachkai.DataAccess
{
    public class SqlMessagesRepository : IMessagesRepository
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public async Task<IEnumerable<Message>> GetAll()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<Message> Get(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(f => f.MessageId == id);
        }

        public async Task Insert(Message data)
        {
            _context.Messages.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(f => f.MessageId == id);

            if (message == null)
            {
                throw new ArgumentException();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Message data)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(f => f.MessageId == id);

            if (message == null)
            {
                throw new ArgumentException();
            }

            message.Text = data.Text;
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}