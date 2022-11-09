using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.Users
{
    public class EditModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public EditModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        
        [BindProperty]
        public UsuUsuario UsuUsuario { get; set; }
        public AppConfig appConfig { get; set; }
        public List<string> Files { get; set; }

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

                UsuUsuario = await _context.UsuUsuarios.FirstOrDefaultAsync(m => m.UsuId == id);

                if (UsuUsuario == null)
                {
                    return NotFound();
                }

                appConfig = base.appConfig;
                this.Files = UtilitiesHandler.GetFilesDir(Path.Combine(appConfig.ApplicationPath, appConfig.ImagePathUserProfile));

                
                ViewData["Breadcrumb"] = sessionInfo;

            }
            else
            {
                // Clear last user session
                base.NoSession();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);

            //ViewData["Menu"] = sessionInfo.Menu;
            //ViewData["Config"] = sessionInfo.Config;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            UsuUsuario UsuDB = await _context.UsuUsuarios
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(m => m.UsuId == UsuUsuario.UsuId);
            // Usu clave
            if (UsuUsuario.UsuClave == null || UsuUsuario.UsuClave.Length == 0)
            {
                UsuUsuario.UsuClave = UsuDB.UsuClave;
            }
            else {
                if (UtilitiesHandler.CreateMD5(UsuUsuario.UsuClave) != UsuDB.UsuClave) {
                    UsuUsuario.UsuClave = UtilitiesHandler.CreateMD5(UsuUsuario.UsuClave);
                }
            }

            _context.Attach(UsuUsuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuUsuarioExists(UsuUsuario.UsuId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UsuUsuarioExists(long id)
        {
            return _context.UsuUsuarios.Any(e => e.UsuId == id);
        }
    }
}
