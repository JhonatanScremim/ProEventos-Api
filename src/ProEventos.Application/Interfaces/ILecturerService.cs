using System.Threading.Tasks;

using ProEventos.Domain;

namespace ProEventos.Application.Interfaces
{
    public interface ILecturerService
    {
         Task<Lecturer> CreateLecturerAsync(Lecturer model);
         Task<Lecturer> UpdateLecturerAsync(int LecturerId, Lecturer model);
         Task<bool> DeleteLecturerAsync(int eventId);
         Task<Lecturer[]> GetAllLecturersAsync(bool includeEvents = false);
         Task<Lecturer[]> GetAllLecturersByNameAsync(string name, bool includeEvents = false);
         Task<Lecturer> GetLecturerByIdAsync(int id, bool includeEvents = false);
    }
}