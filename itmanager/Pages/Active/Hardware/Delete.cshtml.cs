using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.Active.Hardware
{
    public class DeleteModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public DeleteModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        [BindProperty]
        public HwHardwareSpec HwHardwareSpec { get; set; }
        public ActActivo ActActivo { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id, long? dt)
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                if (dt == null)
                {
                    return NotFound();
                }

                HwHardwareSpec = await _context.HwHardwareSpecs
                    .Include(h => h.Atc)
                    .Include(h => h.Tpr).FirstOrDefaultAsync(m => m.HwId == dt);

                if (HwHardwareSpec == null)
                {
                    return NotFound();
                }

                ActActivo = _context.ActActivos
                    .Include(a => a.Tfa).FirstOrDefault(m => m.ActId == id);

                // Navigation Model
                sessionInfo.SelectID = id.ToString();
                sessionInfo.SelectName = ActActivo.ActModelo;
                sessionInfo.SelectImg = UtilitiesHandler.ImgRoute(base.appConfig.ImagePathActives + ActActivo.ActImagen1, TypeRouteImg.ReadSep);
                

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

            HwHardwareSpec = await _context.HwHardwareSpecs.FindAsync(dt);

            if (HwHardwareSpec != null)
            {
                _context.HwHardwareSpecs.Remove(HwHardwareSpec);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Active/Hardware/Index", routeValues: new { id });
        }
    }
}
