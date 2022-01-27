using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Repository.Interfaces
{
    public interface IBatchRepository
    {
        Task<IEnumerable<Batch>> GetBatchesByEventIdAsync(int eventId);
        Task<Batch> GetBatchByIdsAsync(int eventId, int batchId);
    }
}