using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using itmanager.Models;
using itmanager.Class;
using Microsoft.EntityFrameworkCore;

namespace itmanager.Pages.Active.Hardware
{
    public class CreateModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public CreateModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        [BindProperty]
        public HwHardwareSpec HwHardwareSpec { get; set; }
        public ActActivo ActActivo { get; set; }

        public HwHardwareSpec OneHardwareSpec { get; set; }

        public IActionResult OnGet(long? id)
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                ActActivo = _context.ActActivos
                    .Include(a => a.Tfa).FirstOrDefault(m => m.ActId == id);

                // Navigation Model
                sessionInfo.SelectID = id.ToString();
                sessionInfo.SelectName = ActActivo.ActModelo;
                sessionInfo.SelectImg = UtilitiesHandler.ImgRoute(base.appConfig.ImagePathActives + ActActivo.ActImagen1, TypeRouteImg.ReadSep);
                

                ViewData["TprId"] = new SelectList(_context.TprTipoProcesadors, "TprId", "TprNombre");
                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }
            
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(long? id)
        {
            // Hardware 
            OneHardwareSpec = (HwHardwareSpec)_context.HwHardwareSpecs
                    .AsNoTracking()
                    .FirstOrDefault(x => x.AtcId == id);
            HwHardwareSpec.AtcId = id;

            // Insertar
            _context.HwHardwareSpecs.Add(HwHardwareSpec);
            
            await _context.SaveChangesAsync();
            return RedirectToPage("/Active/Hardware/Index", routeValues: new { id });

        }
    }
}
