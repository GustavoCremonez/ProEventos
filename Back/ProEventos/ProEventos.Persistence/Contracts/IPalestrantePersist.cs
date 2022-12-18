﻿using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Contracts
{
    public interface IPalestrantePersist
    {
        //Palestrantes
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false);

        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false);

        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false);
    }
}