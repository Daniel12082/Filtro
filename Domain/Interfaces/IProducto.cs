using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProducto : IGenericString<Producto>
    {
        Task<IEnumerable<object>>Consulta4();
        Task<List<object>>Consulta5();
        Task<string>Consulta6();
        Task<IEnumerable<object>>Consulta10();
    }
} 
