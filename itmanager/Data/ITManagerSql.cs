using itmanager.Models;
using Microsoft.Data.SqlClient;
using System.Linq;
using itmanager.Class;
using Microsoft.EntityFrameworkCore;

namespace itmanager.Data
{
    public class ITManagerSql : IITManager
    {
        public itmanagerContext iTManagerContext { get; }

        public ITManagerSql(itmanagerContext ITManagerContext) {
            iTManagerContext = ITManagerContext;
        }

        // Autenticación / Usuarios
        public IEnumerable<UsuUsuario> GetUsers()
        {
            return iTManagerContext.UsuUsuarios.ToList();
        }


        public UsuUsuario GetUserById(int id) {
            List<UsuUsuario> Users = iTManagerContext.UsuUsuarios.ToList();
            UsuUsuario filter = Users.Where(x => x.UsuId.Equals(id)).FirstOrDefault();

            return filter;
        }

        public IEnumerable<UsuUsuario> GetUserAuthentication(String username, String password) {

            IEnumerable<UsuUsuario> filter = new List<UsuUsuario>();

            try
            {
                List<UsuUsuario> Users = iTManagerContext.UsuUsuarios
                                        .Include(x=>x.UroUsuarioRols)                    
                                        .ToList();
                filter = Users
                        .Where(x => x.UsuLogin == username && x.UsuClave == password);
                return filter;
            }
            catch (SqlException s) {
                Console.WriteLine(s.Message);
                throw s;
            }
            return filter;
        }

        public IEnumerable<UsuUsuario> ListUsers()
        {
            return iTManagerContext.UsuUsuarios.ToList();
        }


        // Menu / Navegación
        public IEnumerable<OpcOpcion> GetOptions(int id = 0)
        {
            return iTManagerContext.OpcOpcions
                            .Include(x => x.OroOpcionRols.Where(a => a.RolId == id))
                            .ToList();
        }


        public IEnumerable<OpcOpcion> GetOptionById(int id)
        {
            List<OpcOpcion> Options = iTManagerContext.OpcOpcions
                                    .ToList();

            IEnumerable<OpcOpcion> filter =
            Options.Where(x => x.OpcId.Equals(id));

            return filter;
        }


        public IEnumerable<OpcOpcion> GetOptionsByUserAuth(String username, String password)
        {
            List<OpcOpcion> Options = iTManagerContext.OpcOpcions.ToList();

            IEnumerable<OpcOpcion> filter =
            Options.Where(x => x.OpcId.Equals(username));

            return filter;
        }

        public List<OpcOpcion> tOpcion { get; set; }


    }
}
