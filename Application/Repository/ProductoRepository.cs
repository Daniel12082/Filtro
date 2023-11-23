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
    public class ProductoRepository : GenericStringRepository<Producto> , IProducto 
    { 
        public FiltroContext _context { get; set; } 
        public ProductoRepository(FiltroContext context) : base(context) 
        { 
            _context = context; 
        }
        public async Task<IEnumerable<object>> Consulta4()
        {
            var result = (from dp in _context.DetallePedidos
                            group dp by dp.CodigoProducto into g
                            orderby g.Sum(dp => dp.Cantidad) descending
                            select new { CodigoProducto = g.Key, UnidadesVendidas = g.Sum(dp => dp.Cantidad) }).Take(20);
            return await result.ToListAsync();
        }
        public async Task<string> Consulta6()
        {
            var nombreProductoMasVendido = await _context.Productos
                .Join(_context.DetallePedidos, p => p.Id, dp => dp.CodigoProducto, (p, dp) => new { Producto = p, DetallePedido = dp })
                .GroupBy(x => x.Producto.Nombre)
                .OrderByDescending(g => g.Sum(x => x.DetallePedido.Cantidad))
                .Select(g => g.Key)
                .FirstOrDefaultAsync();

            return nombreProductoMasVendido;
        }
        public async Task<IEnumerable<object>> Consulta10()
        {
            var productosSinPedidos = await _context.Productos
                .Where(p => !_context.DetallePedidos.Any(dp => dp.CodigoProducto == p.Id))
                .Select(p => new
                {
                    p.Nombre,
                    p.Descripcion,
                    p.Gama
                })
                .ToListAsync();

            return productosSinPedidos;
        }





    } 
} 

