using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Application.ViewModels;
using ProEventos.Domain;

namespace ProEventos.Application.Interfaces
{
    public interface ILecturerService
    {
         Task<LecturerViewModel> CreateLecturerAsync(Lecturer model);
         Task<LecturerViewModel> UpdateLecturerAsync(int LecturerId, Lecturer model);
         Task<bool> DeleteLecturerAsync(int eventId);
         Task<IEnumerable<LecturerViewModel>> GetAllLecturersAsync(bool includeEvents = false);
         Task<IEnumerable<LecturerViewModel>> GetAllLecturersByNameAsync(string name, bool includeEvents = false);
         Task<LecturerViewModel> GetLecturerByIdAsync(int id, bool includeEvents = false);
    }
}