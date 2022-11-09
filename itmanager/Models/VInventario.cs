using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class VInventario
    {
        public DateTime? DinFecha { get; set; }
        public string? DinEstado { get; set; }
        public string? DinUsuario { get; set; }
        public string? DinComentarios { get; set; }
        public long? InvId { get; set; }
        public string InvNombre { get; set; } = null!;
        public DateTime InvInicio { get; set; }
        public DateTime InvFin { get; set; }
        public string ActModelo { get; set; } = null!;
        public string? ActSerie { get; set; }
        public string? ActEstado { get; set; }
        public string? ActCondicion { get; set; }
        public string? ActLicencia { get; set; }
        public string? ActImagen1 { get; set; }
        public string? ActImagen2 { get; set; }
        public long DinId { get; set; }
    }
}
