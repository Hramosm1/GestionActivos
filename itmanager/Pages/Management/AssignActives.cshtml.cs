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

namespace itmanager.Pages.Management
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
        public AceActivoEmpleado ActivoEmpleado { get; set; }

        public async Task OnGetAsync(long id)
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                

                ActActivo = _context.ActActivos
                    .Include(a => a.Tac)
                    .Include(a => a.Tfa)
                    .Where(a => a.ActDadodebaja == false)
                    .Where(x => x.ActEstado == "soporte" || x.ActEstado == "pre-asignado-soporte")
                    //.Where(x => x.ActEstado != "asignado")
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

            long idemp = (long)id;
            long idactive = Convert.ToInt32(Request.Form["idactive"]);


            if (!ActivoEmpleadoExist(idemp, idactive))
            {
                // Add activoEmpleado
                ActivoEmpleado = new AceActivoEmpleado();
                ActivoEmpleado.EmpId = idemp;
                ActivoEmpleado.ActId = idactive;
                _context.AceActivoEmpleados.Add(ActivoEmpleado);
            }

            // Edit active state
            OneActivo = (ActActivo)_context.ActActivos
                .FirstOrDefault(x => x.ActId == idactive);

            OneActivo.ActEstado = "pre-asignado";
            _context.Attach(OneActivo).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Management/AssignActives", routeValues: new { id });
        }

        private bool ActivoEmpleadoExist(long id, long idactive)
        {
            return _context.AceActivoEmpleados
                .Where(e => e.ActId == idactive)
                .Any(d => d.EmpId == id);
        }


    }
}
