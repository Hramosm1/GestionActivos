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

namespace itmanager.Pages.Users
{
    public class UserRoleModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        [BindProperty]
        public long idRol { get; set; }

        public UserRoleModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        public IList<UroUsuarioRol> UsuRol { get; set; }

        public IList<RolRole> RolRoles { get; set; }

        public UsuUsuario SelectedUser { get; set; }

        public async Task OnGetAsync(long id)
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                SelectedUser = (UsuUsuario)_context.UsuUsuarios
                            .FirstOrDefault(x => x.UsuId == id);

                sessionInfo.SelectID = id.ToString();
                sessionInfo.SelectName = SelectedUser.UsuNombre;

                // All Roles
                RolRoles = await _context.RolRoles.ToListAsync();

                // Selected Options
                UsuRol = await _context.UroUsuarioRols
                        .Where(a => a.UsuId == id)
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
            UroUsuarioRol UsuRolNew;

            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                // Get UserSelected
                SelectedUser = (UsuUsuario)_context.UsuUsuarios
                            .FirstOrDefault(x => x.UsuId == id);

                // Get rol in selected user
                UsuUsuario UsuRolCurr = _context.UsuUsuarios
                                    .Include(a => a.UroUsuarioRols)
                                    .Where(a => a.UsuId == id)
                                    .FirstOrDefault();

                // Delete current rol in user
                foreach (var item in UsuRolCurr.UroUsuarioRols)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }
                await _context.SaveChangesAsync();


                // Add selected rol to user
                if (Request.Form.ContainsKey("role"))
                {
                    foreach (var item in Request.Form["role"])
                    {
                        UsuRolNew = new UroUsuarioRol();
                        UsuRolNew.RolId = Int32.Parse(item);
                        UsuRolNew.UsuId = (int)id;
                        _context.UroUsuarioRols.Add(UsuRolNew);
                    }
                    await _context.SaveChangesAsync();
                }

                // ----------------------------------------------------------------------------------
                // PAGE

                // All Roles
                RolRoles = await _context.RolRoles.ToListAsync();

                // Selected Options
                UsuRol = await _context.UroUsuarioRols
                        .Where(a => a.UsuId == id)
                        .ToListAsync();

                
                ViewData["Breadcrumb"] = sessionInfo;
            }

            return Page();
        }


    }
}
