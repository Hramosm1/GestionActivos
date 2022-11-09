using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace itmanager.Pages.Reports
{
    public class InventoryModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public InventoryModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        [BindProperty]
        public long idInventory { get; set; }
        public InvInventario Inventario { get; set; }
        public IList<VInventario> VInventario { get; set; }

        public async Task OnGetAsync()
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                ViewData["Invs"] = new SelectList(_context.InvInventarios, "InvId", "InvNombre");

                Inventario = _context.InvInventarios
                    .OrderBy(x => x.InvId)
                    .FirstOrDefault();

                VInventario = await _context.VInventarios
                    .Where(x => x.InvId == Inventario.InvId)
                    .ToListAsync();

                
                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }
        }

        public async Task<IActionResult> OnPostConfirmAsync()
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                ViewData["Invs"] = new SelectList(_context.InvInventarios, "InvId", "InvNombre");

                VInventario = await _context.VInventarios
                        .Where(x => x.InvId == idInventory)
                        .ToListAsync();

                
                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }

            return Page();
        }

    }


}
