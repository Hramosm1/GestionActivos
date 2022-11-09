using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.Management
{
    public class ActivesListModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public ActivesListModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        public IList<ActActivo> ActActivo { get; set; }

        public async Task OnGetAsync()
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                ActActivo = await _context.ActActivos
                    .Where(a => !a.ActDadodebaja)
                    .Include(a => a.Tac)
                    .Include(c => c.AudAuditoria)
                    .Include(b => b.AceActivoEmpleados)
                        .ThenInclude(bc => bc.Emp)
                    .ToListAsync();

                ViewData["Activo"] = ActActivo;
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
