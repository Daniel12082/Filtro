using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{ 
    public interface ICliente:IGeneric<Cliente> 
    {
        Task<IEnumerable<object>> Consulta1();
        Task<IEnumerable<object>> Consulta2();
        Task<List<object>> Consulta7();
        Task<List<object>> Consulta8();
    }
} 
