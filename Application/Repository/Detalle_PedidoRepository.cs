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
    public class Detalle_PedidoRepository : GenericRepository<Detalle_Pedido> , IDetalle_Pedido 
    { 
        public FiltroContext _context { get; set; } 
        public Detalle_PedidoRepository(FiltroContext context) : base(context) 
        { 
            _context = context; 
        } 
    } 
} 
