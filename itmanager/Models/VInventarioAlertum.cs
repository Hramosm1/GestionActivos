using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class VInventarioAlertum
    {
        public string TacNombre { get; set; } = null!;
        public int? Stock { get; set; }
        public int TacInvMin { get; set; }
        public string? Msg { get; set; }
        public double Perc { get; set; }
    }
}
