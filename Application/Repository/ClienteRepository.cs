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
    public class ClienteRepository : GenericRepository<Cliente>, ICliente 
    { 
        public FiltroContext _context { get; set; } 
        public ClienteRepository(FiltroContext context) : base(context) 
        { 
            _context = context; 
        }
        public async Task<IEnumerable<object>> Consulta1()
        {
            var result = from p in _context.Pedidos
                            where p.FechaEntrega > p.FechaEsperada
                            select new { p.Id, p.CodigoCliente, p.FechaEsperada, p.FechaEntrega };
            return await result.ToListAsync();
        }
        public async Task<IEnumerable<object>> Consulta2()
        {
            var result = from c in _context.Clientes
                        join e in _context.Empleados on c.CodigoEmpleadoRepVentas equals e.Id into empJoin
                        from representante in empJoin.DefaultIfEmpty()
                        join oficina in _context.Oficinas on representante.CodigoOficina equals oficina.Id into oficinaJoin
                        from ciudadOficina in oficinaJoin.DefaultIfEmpty()
                        where !_context.Pagos.Any(p => p.Id == c.Id)
                        select new
                        {
                            NombreCliente = c.NombreCliente,
                            NombreRepresentante = representante != null ? $"{representante.Nombre} {representante.Apellido1}" : "Sin Representante",
                            CiudadRepresentante = ciudadOficina != null ? ciudadOficina.Ciudad : "Sin Oficina"
                        };
            return await result.ToListAsync();
        }
        public async Task<List<object>> Consulta7()
        {
            var query1 = await _context.Clientes
                .GroupJoin(_context.Pedidos, c => c.Id, p => p.CodigoCliente, (cliente, pedidos) => new
                {
                    cliente.NombreCliente,
                    CantidadPedidos = pedidos.Count()
                })
                .ToListAsync<object>();

            return query1;
        }
        public async Task<List<object>> Consulta8()
        {
            var query5 = await _context.Clientes
                .Join(_context.Empleados, c => c.CodigoEmpleadoRepVentas, e => e.Id, (cliente, empleado) => new
                {
                    cliente.NombreCliente,
                    Empleado = $"{empleado.Nombre} {empleado.Apellido1}",
                    CiudadOficina = empleado.CodigoOficinaNavigation.Ciudad
                })
                .ToListAsync<object>();

            return query5;
        }
    } 
}