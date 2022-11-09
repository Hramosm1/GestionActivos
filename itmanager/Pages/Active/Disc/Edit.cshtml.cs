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

namespace itmanager.Pages.Active.Disc
{
    public class EditModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public EditModel(itmanager.Models.itmanagerContext context) : base(context)
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
                

                ViewData["TddId"] = new SelectList(_context.TddTipoDiscos, "TddId", "TddNombre");
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
            Disco.ActId = id;
            Disco.DdId = (long)dt;
            _context.Attach(Disco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return RedirectToPage("/Active/Disc/Index", routeValues: new { id });
        }

        private bool DiscoExists(long id)
        {
            return _context.DdDiscoDuros.Any(e => e.DdId == id);
        }
    }
}
