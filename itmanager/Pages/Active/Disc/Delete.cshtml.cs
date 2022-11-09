using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.Active.Disc
{
    public class DeleteModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public DeleteModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        [BindProperty]
        public DdDiscoDuro Disco { get; set; }
        public ActActivo ActActivo { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id, long? dt)
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                if (id == null)
                {
                    return NotFound();
                }

                Disco = await _context.DdDiscoDuros
                    .Include(h => h.Act)
                    .Include(h => h.Tdd).FirstOrDefaultAsync(m => m.DdId == dt);

                if (Disco == null)
                {
                    return NotFound();
                }

                ActActivo = _context.ActActivos
                    .Include(a => a.Tfa).FirstOrDefault(m => m.ActId == id);

                // Navigation Model
                sessionInfo.SelectID = id.ToString();
                sessionInfo.SelectName = ActActivo.ActModelo;
                sessionInfo.SelectImg = UtilitiesHandler.ImgRoute(base.appConfig.ImagePathActives + ActActivo.ActImagen1, TypeRouteImg.ReadSep);

                ViewData["Activo"] = ActActivo;
                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }
 
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id, long? dt)
        {
            if (dt == null)
            {
                return NotFound();
            }

            Disco = await _context.DdDiscoDuros.FindAsync(dt);

            if (Disco != null)
            {
                _context.DdDiscoDuros.Remove(Disco);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Active/Disc/Index", routeValues: new { id });
        }
    }
}
