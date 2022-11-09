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

namespace itmanager.Pages.Parameters
{
    public class EditModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public EditModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        [BindProperty]
        public ParParametro ParParametro { get; set; }

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

                ParParametro = await _context.ParParametros
                    .Include(p => p.ParTipoNavigation).FirstOrDefaultAsync(m => m.ParId == id);

                ViewData["ParTipo"] = new SelectList(_context.TipTipoParametros, "TipId", "TipNombre");

                if (ParParametro == null)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ParParametro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParParametroExists(ParParametro.ParId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ParParametroExists(long id)
        {
            return _context.ParParametros.Any(e => e.ParId == id);
        }
    }
}
