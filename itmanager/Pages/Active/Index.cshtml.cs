using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.Active
{
    public class IndexModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;
        private string baseUrl = "";

        public IndexModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        public IList<ActActivo> ActActivo { get;set; }

        public async Task OnGetAsync()
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null)
            {
                ActActivo = await _context.ActActivos
                    .Where(a => !a.ActDadodebaja)
                    .Include(a => a.Tac).ToListAsync();

                // 

                ViewData["Activo"] = ActActivo;
                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                RedirectToPage("/");
                // Clear last user session
                base.NoSession();
            }

        }

        public async Task<IActionResult> OnPostConfirm()
        {
            baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            // For Confirmation
            string uid = Guid.NewGuid().ToString();
            return Page();
        }

    }
}
