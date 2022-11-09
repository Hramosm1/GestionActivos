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

namespace itmanager.Pages.Roles
{
    public class RoleOptionsModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        [BindProperty]
        public long idRol { get; set; }

        public RoleOptionsModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        public IList<OroOpcionRol> OpcRol { get; set; }

        public IList<OpcOpcion> OpcOpcion { get; set; }

        public RolRole SelectedRol { get; set; }

        public async Task OnGetAsync(long id)
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                SelectedRol = (RolRole)_context.RolRoles
                            .FirstOrDefault(x => x.RolId == id);

                sessionInfo.SelectID = id.ToString();
                sessionInfo.SelectName = SelectedRol.RolNombre;

                // All Options
                OpcOpcion = await _context.OpcOpcions.ToListAsync();

                // Selected Options
                OpcRol = await _context.OroOpcionRols
                        .Where(a => a.RolId == id)
                        .ToListAsync();

                
                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }
        }


        public async Task<IActionResult> OnPostSave(long? id)
        {
            OroOpcionRol OpcRolNew;

            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                // Get RolSelected
                SelectedRol = (RolRole)_context.RolRoles
                        .FirstOrDefault(x => x.RolId == id);

                // Get Options from Rol
                RolRole OpcRolCurr = _context.RolRoles
                                    .Include(a => a.OroOpcionRols)
                                    .Where(a => a.RolId == id)
                                    .FirstOrDefault();

                // Delete current options in rol
                foreach (var item in OpcRolCurr.OroOpcionRols)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }
                await _context.SaveChangesAsync();

                // Add selected options to Rol 
                if (Request.Form.ContainsKey("rols[]"))
                {
                    foreach (var item in Request.Form["rols[]"])
                    {
                        OpcRolNew = new OroOpcionRol();
                        OpcRolNew.OpcId = Int32.Parse(item);
                        OpcRolNew.RolId = (int)id;
                        _context.OroOpcionRols.Add(OpcRolNew);
                    }
                    await _context.SaveChangesAsync();
                }

                // ----------------------------------------------------------------------------------
                // PAGE

                // All Options
                OpcOpcion = await _context.OpcOpcions.ToListAsync();

                // Selected Options
                OpcRol = await _context.OroOpcionRols
                        .Where(a => a.RolId == id)
                        .ToListAsync();

                
                ViewData["Breadcrumb"] = sessionInfo;
            }

            return Page();
        }


    }
}
