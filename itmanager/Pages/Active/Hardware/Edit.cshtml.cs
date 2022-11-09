using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.Active.Hardware
{
    public class EditModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public EditModel(itmanager.Models.itmanagerContext context) : base(context)
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
                if (id == null)
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
                

                ViewData["TprId"] = new SelectList(_context.TprTipoProcesadors, "TprId", "TprNombre");
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(long? id, long? dt)
        {
            HwHardwareSpec.AtcId = id;
            HwHardwareSpec.HwId = (long)dt;
            _context.Attach(HwHardwareSpec).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return RedirectToPage("/Active/Hardware/Index", routeValues: new { id });
        }

        private bool HwHardwareSpecExists(long id)
        {
            return _context.HwHardwareSpecs.Any(e => e.HwId == id);
        }
    }
}
