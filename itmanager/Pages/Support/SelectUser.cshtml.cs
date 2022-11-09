using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.Support
{
    public class SelectUserModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public SelectUserModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        public IList<UsuUsuario> UsuUsuarios { get;set; }

        public async Task OnGetAsync()
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                
                UsuUsuarios = await _context.UsuUsuarios.Where(e => e.UsuAsignaActivos.Equals(true)).ToListAsync();
                
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
