using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class ConConfirmacion
    {
        public ConConfirmacion()
        {
            AudAuditoria = new HashSet<AudAuditorium>();
        }

        public string ConId { get; set; } = null!;
        public DateTime ConFecha { get; set; }
        public string? ConFirma { get; set; }

        public virtual ICollection<AudAuditorium> AudAuditoria { get; set; }
    }
}
