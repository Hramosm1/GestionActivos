using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.Inventory
{
    public class CreateModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public CreateModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                
                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }
            return Page();
        }

        [BindProperty]
        public InvInventario InvInventario { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.InvInventarios.Add(InvInventario);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
