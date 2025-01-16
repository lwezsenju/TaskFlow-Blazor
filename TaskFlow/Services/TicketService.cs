using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
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
        public async Task DeleteAsync(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }
        public async Task<Ticket?> GetByIdAsync(int id,int userId)
        {
            return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }
        public async Task<List<Ticket?>> GetByUserIdAsync(int userId)
        {
            return _context.Tickets.Where(t => t.UserId == userId).ToList();
        }
        public async Task<List<Ticket?>> Filters(string filter, int userId)
        {
            IQueryable<Ticket> query = _context.Tickets;

            switch (filter.ToLower())
            {
                case "finished":
                    query = query.Where(t => t.Status == TicketStatus.Erledigt&&t.UserId==userId);
                    break;

                case "inprogress":
                    query = query.Where(t => t.Status == TicketStatus.InBearbeitung && t.UserId == userId);
                    break;

                case "open":
                    query = query.Where(t => t.Status == TicketStatus.Offen && t.UserId == userId);
                    break;

                case "withsprints":
                    query = query.Where(t => t.SprintId.HasValue && t.UserId == userId); // Tickets with an associated Sprint
                    break;

                case "withoutssprints":
                    query = query.Where(t => !t.SprintId.HasValue && t.UserId == userId); // Tickets without an associated Sprint
                    break;
                case "sortbypoints":
                    query = query.Where(x=>x.UserId==userId&& !x.SprintId.HasValue).OrderBy(t => t.Points);
                    break;
                case "sortbypointsdesc":
                    query = query.Where(x => x.UserId == userId&&!x.SprintId.HasValue).OrderByDescending(t => t.Points);
                    break;
                default:
                    break; // No filter applied
            }

            return await query.ToListAsync();
        }
    }
}
