using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Contracts
{
    public interface IEventoPersist
    {
        //Eventos

        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);

        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);

        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false);
    }
}
