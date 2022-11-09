using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class VInventarioActual
    {
        public string TacNombre { get; set; } = null!;
        public int? Stock { get; set; }
        public DateTime? Lastdate { get; set; }
    }
}
