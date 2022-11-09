using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace itmanager.Models
{
    public partial class TmaMarca
    {
        [DisplayName("ID")]
        public long TmaId { get; set; }
        [DisplayName("Marca")]
        public string? TmaNombre { get; set; }
    }
}
