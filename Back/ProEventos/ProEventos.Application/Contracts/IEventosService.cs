using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Application.Contracts
{
    public interface IEventosService
    {
        Task<Evento> AddEventos(Evento model);

        Task<Evento> Update(int eventoId, Evento model);

        Task<bool> DeleteEvento(int eventoId);

        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);

        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);

        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false);
    }
}
