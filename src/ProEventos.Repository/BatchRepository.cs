using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Domain;
using ProEventos.Repository.Context;
using ProEventos.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ProEventos.Repository
{
    public class BatchRepository : IBatchRepository
    {
        private readonly DataContext _context;

        public BatchRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Batch> GetBatchByIdsAsync(int eventId, int batchId)
        {
            return await _context.Batch.AsNoTracking().
                FirstOrDefaultAsync(x => x.Id == batchId && x.EventId == eventId);
        }

        public async Task<IEnumerable<Batch>> GetBatchesByEventIdAsync(int eventId)
        {
            return await _context.Batch.AsNoTracking().Where(x => x.EventId == eventId).ToListAsync();
        }
    }
}