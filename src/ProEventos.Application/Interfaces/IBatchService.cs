using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Application.ViewModels;

namespace ProEventos.Application.Interfaces
{
    public interface IBatchService
    {
        Task<IEnumerable<BatchViewModel>> SaveBathesAsync(int eventId, List<BatchViewModel> model);
        Task<bool> DeleteAsync(int eventId, int batchId);
        Task<IEnumerable<BatchViewModel>> GetBatchesByEventIdAsync(int eventId);
        Task<BatchViewModel> GetBatchByIdsAsync(int eventId, int batchId);
    }
}