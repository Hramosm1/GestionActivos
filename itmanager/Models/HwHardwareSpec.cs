using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class HwHardwareSpec
    {
        public long HwId { get; set; }
        public string? HwModeloproc { get; set; }
        public string? HwGeneracionproc { get; set; }
        public string? HwMemoria { get; set; }
        public long? AtcId { get; set; }
        public long? TprId { get; set; }

        public virtual ActActivo? Atc { get; set; }
        public virtual TprTipoProcesador? Tpr { get; set; }
    }
}
