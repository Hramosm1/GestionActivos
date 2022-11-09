using itmanager.Class;
using itmanager.Data;
using itmanager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace itmanager.Pages
{
    public class IndexModel : PageModel
    {
        // Interfaces
        public List<UsuUsuario> Users;
        public MessagesCollection Messages = new MessagesCollection();

        public IITManager ItManager { get; }
        public IndexModel(IITManager itManager) {
            ItManager = itManager;
        }
       
        // BindProperties
        [BindProperty]
         public string userfield { get; set; }
        [BindProperty]
        public string passwordfield { get; set; }

        // Form Methods
        public void OnGet()
        {
            // Clear last user session
            HttpContext.Session.Clear();
        }

        public IActionResult OnPostAuthentication() {

            try
            {
                passwordfield = UtilitiesHandler.CreateMD5(passwordfield);
                UsuUsuario user = ItManager.GetUserAuthentication(userfield, passwordfield).FirstOrDefault();

                if (user != null)
                {
                    HttpContext.Session.SetInt32("loggednow", 1);
                    HttpContext.Session.SetInt32("userid", (int)user.UsuId);
                    HttpContext.Session.SetString("userlogin", user.UsuLogin);
                    HttpContext.Session.SetInt32("useradmin", user.UsuAdmin ? 1 : 0);

                    try {
                        UroUsuarioRol UsuRol = user.UroUsuarioRols.FirstOrDefault();
                        HttpContext.Session.SetInt32("userrole", (int)UsuRol.UsuId);
                    }
                    catch (Exception e) {
                        throw new Exception("El usuario no tiene rol asignado, asigne un rol con opciones e intente de nuevo");
                    }

                    return RedirectToPage("Dashboard");
                }
                else
                {
                    Messages.Collection.Add(new Message { title = "Acceso no permitido", message = "Usuario o clave incorrecta", messageType = TypeMessage.warning, timeout = 6000 });
                    return Page();
                }
            }
            catch (Exception ex) {
                Messages.Collection.Add(new Message { message = ex.Message, messageType = TypeMessage.error, timeout = 12000 });
            }
            return Page();
        }
    }
}