using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.SystemConfig
{
    public class DeleteModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public DeleteModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        [BindProperty]
        public SisSistema SisSistema { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                if (id == null)
                {
                    return NotFound();
                }

                SisSistema = await _context.SisSistemas.FirstOrDefaultAsync(m => m.SisId == id);

                if (SisSistema == null)
                {
                    return NotFound();
                }
                
                ViewData["Breadcrumb"] = sessionInfo;

            }
            else
            {
                // Clear last user session
                base.NoSession();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SisSistema = await _context.SisSistemas.FindAsync(id);

            if (SisSistema != null)
            {
                _context.SisSistemas.Remove(SisSistema);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
