using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.ActiveType
{
    public class IndexModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public IndexModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        public IList<TacTipoActivo> TacTipoActivo { get;set; }

        public async Task OnGetAsync()
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                TacTipoActivo = await _context.TacTipoActivos.ToListAsync();
                

                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }
            
        }
    }
}
