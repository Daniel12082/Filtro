using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{ 
    public interface IOficina:IGenericString<Oficina> 
    {
        Task<IEnumerable<object>> Consulta3();

    }
} 
