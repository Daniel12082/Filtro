using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 

namespace API.Dtos; 
    public class ProductoDto : BaseStringDto
    { 
        public string Nombre { get; set; }

        public string Gama { get; set; }

        public string Dimensiones { get; set; }

        public string Proveedor { get; set; }

        public string Descripcion { get; set; }

        public short CantidadEnStock { get; set; }

        public decimal PrecioVenta { get; set; }

        public decimal? PrecioProveedor { get; set; }
    } 
