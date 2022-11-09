using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;

namespace itmanager.Pages.Inventory.Details
{
    public class EditModel : PageModel
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public EditModel(itmanager.Models.itmanagerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DinDetalleInventario DinDetalleInventario { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DinDetalleInventario = await _context.DinDetalleInventarios
                .Include(d => d.Act)
                .Include(d => d.Inv).FirstOrDefaultAsync(m => m.DinId == id);

            if (DinDetalleInventario == null)
            {
                return NotFound();
            }
           ViewData["ActId"] = new SelectList(_context.ActActivos, "ActId", "ActId");
           ViewData["InvId"] = new SelectList(_context.InvInventarios, "InvId", "InvId");
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

            _context.Attach(DinDetalleInventario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DinDetalleInventarioExists(DinDetalleInventario.DinId))
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

        private bool DinDetalleInventarioExists(long id)
        {
            return _context.DinDetalleInventarios.Any(e => e.DinId == id);
        }
    }
}
