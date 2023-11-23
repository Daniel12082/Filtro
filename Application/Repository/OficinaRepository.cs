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
    public class OficinaRepository : GenericStringRepository<Oficina> , IOficina 
    { 
        public FiltroContext _context { get; set; } 
        public OficinaRepository(FiltroContext context) : base(context) 
        { 
            _context = context; 
        }
        public async Task<IEnumerable<object>> Consulta3()
        {
            var oficinasSinRepresentantesFrutales = await _context.Oficinas
                .Where(o => !_context.Empleados
                    .Where(e => _context.Clientes
                        .Where(c => _context.Pedidos
                            .Where(p => _context.DetallePedidos
                                .Where(dp => _context.Productos
                                    .Any(pr => pr.Id == dp.CodigoProducto && pr.Gama == "Frutales")
                                )
                                .Any(dp => dp.Id == p.Id)
                            )
                            .Any(p => p.CodigoCliente == c.Id)
                        )
                        .Any(c => c.CodigoEmpleadoRepVentas == e.Id)
                    )
                    .Any(e => e.CodigoOficina == o.Id)
                )
                .Select(o => o)
                .ToListAsync();

            return oficinasSinRepresentantesFrutales.Cast<object>().ToList();
        }
    } 
} 
