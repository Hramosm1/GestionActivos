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
    public class CertificationModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public CertificationModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        [BindProperty]
        public ConConfirmacion OneConfirmacion { get; set; }
        [BindProperty]
        public EmpEmpleado OneEmployee { get; set; }

        public async Task OnGetAsync(long id, string uid)
        {
            EmpEmpleado OneEmployee;

            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                // OneConfirmation
                OneConfirmacion = _context.ConConfirmacions
                    .Include(a => a.AudAuditoria)
                    .ThenInclude(aa => aa.Act)
                    .Where(x => x.ConId == uid)
                    //.Where(t => t.AudAuditoria.Any(x => x.Act.ActEstado = "asignado"))
                    .FirstOrDefault();

                long EmpId = (long)OneConfirmacion.AudAuditoria.FirstOrDefault().EmpId;

                // OneEmployee 
                OneEmployee = _context.EmpEmpleados
                     .Where(x => x.EmpId == EmpId).FirstOrDefault();

                sessionInfo.SelectID = id.ToString();

                if (OneEmployee != null)
                    sessionInfo.SelectName = OneEmployee.EmpNombre;

                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }
        }

    }
}
