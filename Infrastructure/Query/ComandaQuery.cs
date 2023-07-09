﻿using Application.Interface.Query;
using Domain.Entity;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query
{
    public class ComandaQuery: IComandaQuery
    {
        private readonly AppDbContext _context;
        public ComandaQuery(AppDbContext context)
        {
            _context = context;
        }

        public List<Comanda> GetListComanda()
        { 
            var comandas = _context.Comanda
                .Include(s => s.FKFormaEntrega)
                .Include(s => s.LsComandaMercaderia)
                 .ThenInclude(s => s.FKMercaderia)
                .OrderBy(s => s.Fecha)
                .ToList();
            return comandas;         
        } 
        
        public Comanda GetComandaId(Guid comandaId)
        {
            var comandas = _context.Comanda
             .Include(s => s.FKFormaEntrega)
             .Include(s => s.LsComandaMercaderia)
              .ThenInclude(s => s.FKMercaderia)
              .ThenInclude(s => s.FKTipoMercaderia)
             .FirstOrDefault(s => s.ComandaId == comandaId);
            return comandas;
        }

    }
}
