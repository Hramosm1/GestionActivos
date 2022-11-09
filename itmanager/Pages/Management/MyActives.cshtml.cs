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
    public class ActivesModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public ActivesModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        [BindProperty]
        public ConConfirmacion OneConfirmacion { get; set; }
        [BindProperty]
        public IList<ActActivo> ActActivo { get; set; }
        public AceActivoEmpleado ActivoEmpleado { get; set; }
        
        public ActActivo OneActivo { get; set; }

        public async Task OnGetAsync(long id)
        {
            EmpEmpleado OneEmployee;

            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                

                // Employee Selected
                OneEmployee = (EmpEmpleado)_context.EmpEmpleados
                .FirstOrDefault(x => x.EmpId == id);

                sessionInfo.SelectID = id.ToString();
                sessionInfo.SelectName = OneEmployee.EmpNombre;

                // Active List
                ActActivo = _context.ActActivos
                    .Include(a => a.Tac)
                    .Include(a => a.Tfa)
                    .Where(x => x.AceActivoEmpleados.Any(a => a.EmpId == id)).ToList();

                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }
        }

        public async Task<IActionResult> OnPostConfirm(long? id)
        {
            // Active List for Employee
            IList<ActActivo> ActivosAssign = _context.ActActivos
                .Where(x => x.AceActivoEmpleados.Any(a => a.EmpId == id)).ToList();

            // For Confirmation
            string uid = Guid.NewGuid().ToString();
            
            // Loop
            foreach (var oneActive in ActivosAssign)
            {
                string oldstate = oneActive.ActEstado;
                switch (oneActive.ActEstado)
                {
                    case "pre-asignado":
                        oneActive.ActEstado = "asignado";
                        break;

                    case "pre-retiro":
                        oneActive.ActEstado = "soporte";

                        // Remove active from employee
                        ActivoEmpleado = new AceActivoEmpleado();
                        ActivoEmpleado.EmpId = (long)id;
                        ActivoEmpleado.ActId = oneActive.ActId;

                        _context.AceActivoEmpleados.Remove(ActivoEmpleado);
                        await _context.SaveChangesAsync();
                        break;
                }

                // Update state in Active
                _context.Attach(oneActive).State = EntityState.Modified;

                // Audit
                AudAuditorium OneAudit = Audit(oneActive, (long)id, oldstate, uid, UtilitiesHandler.ValidateSession(HttpContext, _context));
                _context.Attach(OneAudit).State = EntityState.Added;
            }

            // Confirmed && Signed
            OneConfirmacion.ConId = uid;
            _context.ConConfirmacions.Add(OneConfirmacion);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Management/MyActives", routeValues: new { id });
        }


        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                .Where(y => y.Count > 0)
                .ToList();
                //return Page();
            }

            long idemp = (long)id;
            long idactive = Convert.ToInt32(Request.Form["idactive"]);

            if (ActivoEmpleadoExist(idemp, idactive))
            {
                OneActivo = (ActActivo)_context.ActActivos
                .FirstOrDefault(x => x.ActId == idactive);

                string oldstate = OneActivo.ActEstado;
                OneActivo.ActEstado = "pre-retiro";

                _context.Attach(OneActivo).State = EntityState.Modified;
                

                // Audit
                AudAuditorium OneAudit = Audit(OneActivo, (long)id, oldstate, null, UtilitiesHandler.ValidateSession(HttpContext, _context));
                _context.Attach(OneAudit).State = EntityState.Added;
                
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Management/MyActives", routeValues: new { id });
        }

 
        private bool ActivoEmpleadoExist(long id, long idactive)
        {
            return _context.AceActivoEmpleados
                .Where(e => e.ActId == idactive)
                .Any(d => d.EmpId == id);
        }

    }
}
