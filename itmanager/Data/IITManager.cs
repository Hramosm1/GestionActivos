using itmanager.Models;

namespace itmanager.Data
{
    public interface IITManager
    {
         // Users // Authentication
        IEnumerable<UsuUsuario> GetUsers();
        UsuUsuario GetUserById(int id);
        IEnumerable<UsuUsuario> GetUserAuthentication(String username, String password);

        // Navegación / Menu
        IEnumerable<OpcOpcion> GetOptions(int id);
        IEnumerable<UsuUsuario> ListUsers();

    }
}
