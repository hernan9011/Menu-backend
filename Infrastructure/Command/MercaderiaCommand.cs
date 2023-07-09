using Application.Interface;
using Application.Interface.Command;
using Domain.Entity;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Command
{
    public class MercaderiaCommand : IMercaderiaCommand
    {
        private readonly AppDbContext _context;
        public MercaderiaCommand(AppDbContext context) 
        { 
         _context = context;
        }
        public void InsertMercaderia(Mercaderia mercaderia)
        {
            _context.Mercaderia.Add(mercaderia);
            _context.SaveChanges();
        }  
        public void UpdateMercaderia(Mercaderia mercaderia)
        {
            _context.Entry(mercaderia);
            _context.SaveChanges();
        }

        public void RemoveMercaderia(Mercaderia mercaderia)
        {
            _context.Mercaderia.Remove(mercaderia);
            _context.SaveChanges();
        }
    }
}
