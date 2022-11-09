using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace itmanager.Models
{
    [Table("oro_opcion_rol")]
    public class OpcJoinRol
    {
        [Key]
        public int OpcRolId { get; set; }

        public int OpcId { get; set; }
        public OpcOpcion OpcOpcion { get; set; }
        
        public int RolId { get; set; }
        public RolRole RolRole { get; set; }

    }
}
