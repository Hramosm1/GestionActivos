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
    public class AssignActivesModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public AssignActivesModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        public IList<ActActivo> ActActivo { get;set; }
        public ActActivo OneActivo { get; set; }
        public AcuActivoUsuario ActivoUsuario { get; set; }

        public async Task OnGetAsync(long id)
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                

                ActActivo = _context.ActActivos
                    .Include(a => a.Tac)
                    .Include(a => a.Tfa)
                    .Where(x => x.ActEstado == "almacenado" || x.ActEstado == "pre-asignado-soporte")
                    .ToList();

                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            long idusu = (long)id;
            long idactive = Convert.ToInt32(Request.Form["idactive"]);


            if (!ActivoUsuarioExist(idusu, idactive))
            {
                // Add activoEmpleado
                ActivoUsuario = new AcuActivoUsuario();
                ActivoUsuario.UsuId = idusu;
                ActivoUsuario.ActId = idactive;
                _context.AcuActivoUsuarios.Add(ActivoUsuario);
            }

            // Edit active state
            OneActivo = (ActActivo)_context.ActActivos
                .FirstOrDefault(x => x.ActId == idactive);

            OneActivo.ActEstado = "pre-asignado-soporte";
            _context.Attach(OneActivo).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Support/AssignActives", routeValues: new { id });
        }

        private bool ActivoUsuarioExist(long id, long idactive)
        {
            return _context.AcuActivoUsuarios
                .Where(e => e.ActId == idactive)
                .Any(d => d.UsuId == id);
        }


    }
}
