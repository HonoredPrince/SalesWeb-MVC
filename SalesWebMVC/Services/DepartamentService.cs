using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class DepartamentService
    {
        private readonly SalesWebMVCContext _context;

        public DepartamentService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Departament>> FindAllAsync()
        {
            return await _context.Departament.OrderBy(dp => dp.Name).ToListAsync();
        }

        public async Task<ICollection<Seller>> FindAllSellersInDepartamentAsync(int id)
        {
            var sellers = await _context.Seller.OrderBy(sl => sl.Name).Where(sl => sl.Departament.Id == id).ToListAsync();
            return sellers;
        }
    }
}
