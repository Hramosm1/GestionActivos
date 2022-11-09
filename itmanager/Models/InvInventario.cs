using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class InvInventario
    {
        public InvInventario()
        {
            DinDetalleInventarios = new HashSet<DinDetalleInventario>();
        }

        public long InvId { get; set; }
        public string InvNombre { get; set; } = null!;
        public DateTime InvDate { get; set; }
        public DateTime InvInicio { get; set; }
        public DateTime InvFin { get; set; }

        public virtual ICollection<DinDetalleInventario> DinDetalleInventarios { get; set; }
    }
}
