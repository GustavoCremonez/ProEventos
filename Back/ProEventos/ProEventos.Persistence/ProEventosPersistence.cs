using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistense;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    public class ProEventosPersistence : IProEventosPersistence
    {
        private readonly ProEventosContext _context;

        public ProEventosPersistence(ProEventosContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(x => x.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(x => x.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(x => x.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(x => x.Id).Where(x => x.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(x => x.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(x => x.Id).Where(x => x.Id == EventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
               .Include(x => x.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(x => x.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(x => x.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
               .Include(x => x.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(x => x.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query
                    .OrderBy(x => x.Id)
                    .Where(x => x.Name.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int PalestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
               .Include(x => x.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(x => x.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query
                    .OrderBy(x => x.Id)
                    .Where(x => x.Id == PalestranteId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
