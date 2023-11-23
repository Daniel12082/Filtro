using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
using API.Repository;
using Domain.Entities; 
using Domain.Interfaces; 
using Microsoft.EntityFrameworkCore; 
using Persistence.Data; 

namespace Application.Repository
{
    public class GamaProductoRepository : GenericStringRepository<GamaProducto>, IGamaProducto
    {
        public FiltroContext _context { get; set; }

        public GamaProductoRepository(FiltroContext context) : base(context)
        {
            _context = context;
        }


    }
}
