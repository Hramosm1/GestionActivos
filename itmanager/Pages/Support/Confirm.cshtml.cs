using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;
using Z.EntityFramework.Plus;

namespace itmanager.Pages.Support
{
    public class ConfirmModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public ConfirmModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        [BindProperty]
        public ConConfirmacion OneConfirmacion { get; set; }
        public IList<ActActivo> ActActivo { get; set; }
        
        public AcuActivoUsuario ActivoUsuario { get; set; }
        public UsuUsuario OneUser { get; set; }
        public ActActivo OneActivo { get; set; }

        public async Task OnGetAsync(long? id)
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                id = (id == null) ? sessionInfo.UserID : id;
                
                // User Selected
                OneUser = (UsuUsuario)_context.UsuUsuarios
                .FirstOrDefault(x => x.UsuId == id);

                sessionInfo.SelectID = id.ToString();
                sessionInfo.SelectName = OneUser.UsuNombre;

                // Active List
                ActActivo = _context.ActActivos
                    .Include(a => a.Tac)
                    .Include(a => a.Tfa)
                    .Where(x => x.AcuActivoUsuarios.Any(a => a.UsuId == id)).ToList();

                ViewData["Breadcrumb"] = sessionInfo;
            }
            else {
                // Clear last user session
                base.NoSession();
            }

        }

        public async Task<IActionResult> OnPostConfirm(long? id)
        {
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            id = (id == null) ? sessionInfo.UserID : id;

            // Active List for Users
            IList<ActActivo> ActivosAssign = _context.ActActivos
                .Where(x => x.AcuActivoUsuarios.Any(a => a.UsuId == id)).ToList();

            // For Confirmation
            string uid = Guid.NewGuid().ToString();

            // Loop
            foreach (var oneActive in ActivosAssign)
            {
                string oldstate = oneActive.ActEstado;
                switch (oneActive.ActEstado)
                {
                    case "pre-asignado-soporte":
                        oneActive.ActEstado = "soporte";
                        break;

                    case "pre-retiro-soporte":
                        oneActive.ActEstado = "almacenado";

                        // Remove active from employee
                        ActivoUsuario = new AcuActivoUsuario();
                        ActivoUsuario.UsuId = (long)id;
                        ActivoUsuario.ActId = oneActive.ActId;

                        _context.AcuActivoUsuarios.Remove(ActivoUsuario);
                        await _context.SaveChangesAsync();
                        break;
                }

                // Update state in Active
                _context.Attach(oneActive).State = EntityState.Modified;

                // Audit
                AudAuditorium OneAudit = Audit(oneActive, (long)id, oldstate, uid, UtilitiesHandler.ValidateSession(HttpContext, _context), "usu");
                _context.Attach(OneAudit).State = EntityState.Added;
            }

            // Confirmed && Signed
            OneConfirmacion.ConId = uid;
            _context.ConConfirmacions.Add(OneConfirmacion);

            await _context.SaveChangesAsync();
            return RedirectToPage("/Support/Actives", routeValues: new { id });
        }



        public async Task<IActionResult> OnPostAsync(long? id)
        {
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            id = (id == null) ? sessionInfo.UserID : id;

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                .Where(y => y.Count > 0)
                .ToList();
                //return Page();
            }

            long idusu = (long)id;
            long idactive = Convert.ToInt32(Request.Form["idactive"]);

            if (ActivoUsuarioExist(idusu, idactive))
            {
                OneActivo = (ActActivo)_context.ActActivos
                .FirstOrDefault(x => x.ActId == idactive);

                string oldstate = OneActivo.ActEstado;
                OneActivo.ActEstado = "pre-retiro-soporte";

                _context.Attach(OneActivo).State = EntityState.Modified;

                // Audit
                //AudAuditorium OneAudit = Audit(OneActivo, (long)id, oldstate, null, UtilitiesHandler.ValidateSession(HttpContext, _context));
                //_context.Attach(OneAudit).State = EntityState.Added;

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Support/Actives", routeValues: new { id });
        }

        private bool ActivoUsuarioExist(long id, long idactive)
        {
            return _context.AcuActivoUsuarios
                .Where(e => e.ActId == idactive)
                .Any(d => d.UsuId == id);
        }

    }
}
