using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace itmanager.Pages.Reports
{
    public class AuditModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public AuditModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        [BindProperty]
        public long idPeriodo { get; set; }
        public AudAuditorium Auditoria { get; set; }
        public IList<VAuditorium> VAuditorium { get; set; }
        public IList<AudAuditorium> TAuditoria { get; set; }

        public async Task OnGetAsync()
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                ViewData["Period"] = new SelectList( await _context.AudAuditoria
                    .Select(p => new { p.AudAñomes })
                    .Distinct()
                    .ToListAsync(), "AudAñomes", "AudAñomes");

                Auditoria = _context.AudAuditoria   
                    .OrderBy(x => x.AudId)
                    .FirstOrDefault();

                VAuditorium = await _context.VAuditoria
                    //.Where(x => x.AudId == Auditoria.AudId)
                    .OrderByDescending(x=>x.AudFecha)
                    .ToListAsync();

                
                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                ViewData["Period"] = new SelectList(await _context.AudAuditoria
                    .Select(p => new { p.AudAñomes })
                    .Distinct()
                    .ToListAsync(), "AudAñomes", "AudAñomes");


                VAuditorium = await _context.VAuditoria
                    //.Where(x => x.AudId == Auditoria.AudId)
                    .OrderByDescending(x => x.AudFecha)
                    .ToListAsync();

                
                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostConfirmAsync()
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                ViewData["Period"] = new SelectList(await _context.AudAuditoria
                    .Select(p => new { p.AudAñomes })
                    .Distinct()
                    .ToListAsync(), "AudAñomes", "AudAñomes");

                VAuditorium = await _context.VAuditoria
                    .Where(x => x.AudAñomes == Request.Form["idPeriodo"].ToString())
                    .OrderByDescending(x => x.AudFecha)
                    .ToListAsync();

                
                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }

            return Page();
        }

    }
}
