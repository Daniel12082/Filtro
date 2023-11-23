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
    public class EmpleadoRepository : GenericRepository<Empleado> , IEmpleado 
    { 
        public FiltroContext _context { get; set; } 
        public EmpleadoRepository(FiltroContext context) : base(context) 
        { 
            _context = context; 
        } 
        public async Task<IEnumerable<object>> Consulta9()
        {
            var result = from e in _context.Empleados
                            join c in _context.Clientes on e.Id equals c.CodigoEmpleadoRepVentas into gj
                            from subc in gj.DefaultIfEmpty()
                            join j in _context.Empleados on e.CodigoJefe equals j.Id
                            where subc == null
                            select new { Empleado = $"{e.Nombre} {e.Apellido1} {e.Apellido2}", e.Email, e.Puesto, Jefe = $"{j.Nombre} {j.Apellido1} {j.Apellido2}" };
            return await result.ToListAsync();
        }
    } 
} 
