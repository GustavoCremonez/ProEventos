using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext _context;

        public PalestrantePersist(ProEventosContext context)
        {
            _context = context;
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

            query = query.AsNoTracking().OrderBy(x => x.Id);

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

            return await query.AsNoTracking().ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
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
                    .Where(x => x.Id == palestranteId);

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
