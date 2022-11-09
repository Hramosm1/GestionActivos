using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class VInventarioConcilium
    {
        public string? TacNombre { get; set; }
        public int? Teorico { get; set; }
        public int? Fisico { get; set; }
        public DateTime? Lastdate { get; set; }
        public int? Diff { get; set; }
        public double Perc { get; set; }
    }
}
