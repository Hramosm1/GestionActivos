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
    public class DetailsModel : PageModel
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public DetailsModel(itmanager.Models.itmanagerContext context)
        {
            _context = context;
        }

        public DinDetalleInventario DinDetalleInventario { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DinDetalleInventario = await _context.DinDetalleInventarios
                .Include(d => d.Act)
                .Include(d => d.Inv).FirstOrDefaultAsync(m => m.InvId == id);

            if (DinDetalleInventario == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
