using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;

namespace itmanager.Pages.Inventory.Details
{
    public class DeleteModel : PageModel
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public DeleteModel(itmanager.Models.itmanagerContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DinDetalleInventario = await _context.DinDetalleInventarios.FindAsync(id);

            if (DinDetalleInventario != null)
            {
                _context.DinDetalleInventarios.Remove(DinDetalleInventario);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
