using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class DdDiscoDuro
    {
        public long DdId { get; set; }
        public string DdCapacidad { get; set; } = null!;
        public long TddId { get; set; }
        public long? ActId { get; set; }

        public virtual ActActivo? Act { get; set; }
        public virtual TddTipoDisco Tdd { get; set; } = null!;
    }
}
