using Microsoft.EntityFrameworkCore;
using TaskFlow.Models;

namespace TaskFlow.Services
{
    public class TicketService
    {
        private readonly DatabaseContext _context;
        public TicketService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Ticket ticket)
        {
            var response = await _context.Tickets.AddAsync(ticket);
            if (response.State == EntityState.Added)
            {
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new HttpRequestException("Ticket konnte nicht erstellt werden.");
            }
        }
        public async Task UpdateAsync(Ticket ticket)
        {
            var response = _context.Tickets.Update(ticket);
            if (response.State == EntityState.Modified)
            {
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new HttpRequestException("Ticket konnte nicht aktualisiert werden.");
            }
        }
        public async Task<List<Ticket?>> GetByUserIdAsync(int userId)
        {
            return _context.Tickets.Where(t => t.UserId == userId).ToList();
        }
    }
}
