using itmanager.Class;
using itmanager.Data;
using itmanager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace itmanager.Pages
{
    public class DashboardModel : PageModelController
    {
        //public IITManager dbContext { get; }

        private readonly itmanager.Models.itmanagerContext _context;
        public UsuUsuario loggedUser;
        public MessagesCollection Messages = new MessagesCollection();

        public List<OpcOpcion> menuOptions;
        public List<VInventarioTipo> InventarioTipo;
        public List<VInventarioActual> InventarioToma;
        public List<VUltimasAsignacione> Asignaciones;
        public List<VUltimosRetiro> Retiros;
        public List<VInventarioConcilium> Conciliacion;
        public List<VInventarioAlertum> Alertas;
        public List<VActividadMe> Actividad;

        public DashboardModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        public void OnGet()
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) {

                if (HttpContext.Session.GetInt32("loggednow") == 1)
                {
                    Messages.Collection.Add(new Message { title = "IT Manager +", message = "Bienvenido al sistema " + sessionInfo.UserSession.UsuNombre, messageType = TypeMessage.success, timeout = 3000 });
                    HttpContext.Session.SetInt32("loggednow", 0);
                }

                InventarioTipo = _context.VInventarioTipos.ToList();
                InventarioToma = _context.VInventarioActuals.ToList();
                Asignaciones = _context.VUltimasAsignaciones.ToList();
                Retiros = _context.VUltimosRetiros.ToList();
                Conciliacion = _context.VInventarioConcilia.ToList();
                Alertas = _context.VInventarioAlerta.ToList();
                Actividad = _context.VActividadMes.ToList();

                ViewData["Breadcrumb"] = sessionInfo;

            } else {
                // Clear last user session
                base.NoSession();
            }

        }

    }
}