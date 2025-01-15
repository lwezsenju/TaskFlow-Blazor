using TaskFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskFlow.Services
{
    public class SprintService
    {
        private readonly DatabaseContext _context;
        public SprintService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Sprint>> GetByUserIdAsync(int userId)
        {
            return await _context.Sprints
                .Where(s => s.UserId == userId)
                .Include(c => c.Tickets)
                .ToListAsync();
        }

        public async Task<Sprint?> GetByIdAsync(int sprintId)
        {
            return await _context.Sprints
                .Include(c => c.Tickets)
                .Include(c => c.User)
                .FirstOrDefaultAsync(s => s.Id == sprintId);
        }


        // Create a new Sprint
        public async Task<Sprint> CreateAsync(Sprint sprint)
        {
            if (sprint == null)
            {
                throw new ArgumentNullException(nameof(sprint), "Sprint cannot be null");
            }

            _context.Sprints.Add(sprint);
            await _context.SaveChangesAsync();
            return sprint;
        }

        public async Task DeleteAsync(int sprintId, int userId)
        {
            var sprint = await _context.Sprints.FindAsync(sprintId);
            if (sprint != null && userId == sprint?.UserId)
            {
                _context.Sprints.Remove(sprint);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Sprint?> UpdateAsync(Sprint sprint)
        {
            if (sprint == null)
            {
                throw new ArgumentNullException(nameof(sprint), "Sprint cannot be null");
            }

            var existingSprint = await _context.Sprints.FindAsync(sprint.Id);
            if (existingSprint == null)
            {
                return null;
            }

            _context.Entry(existingSprint).CurrentValues.SetValues(sprint);
            await _context.SaveChangesAsync();
            return existingSprint;
        }
    }
}
