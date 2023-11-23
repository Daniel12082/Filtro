using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
using Api.Repository; 
using Domain.Entities; 
using Domain.Interfaces; 
using Microsoft.EntityFrameworkCore; 
using Persistence.Data; 

namespace Application.Repository 
{ 
    public class OficinaRepository : GenericRepository<Oficina> , IOficina 
    { 
        public FiltroContext _context { get; set; } 
        public OficinaRepository(FiltroContext context) : base(context) 
        { 
            _context = context; 
        } 
    } 
} 
