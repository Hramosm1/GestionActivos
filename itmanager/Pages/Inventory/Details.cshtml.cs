﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.Inventory
{
    public class DetailsModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public DetailsModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        public InvInventario InvInventario { get; set; }

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

                InvInventario = await _context.InvInventarios.FirstOrDefaultAsync(m => m.InvId == id);

                if (InvInventario == null)
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
    }
}
