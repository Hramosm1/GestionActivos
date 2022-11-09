using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.Users
{
    public class CreateModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public CreateModel(itmanager.Models.itmanagerContext context): base(context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                //Fetch all files in the Folder (Directory).
                string[] filePaths = Directory.GetFiles(Path.Combine(base.appConfig.ApplicationPath, base.appConfig.ImagePathUserProfile));

                this.Files = new List<string>();

                //Copy File names to Model collection.
                foreach (string filePath in filePaths)
                {
                    this.Files.Add(Path.GetFileName(filePath));
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

        [BindProperty]
        public UsuUsuario UsuUsuario { get; set; }
        [BindProperty]
        public List<string> Files { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                       .Where(y => y.Count > 0)
                       .ToList();

                return Page();
            }

            UsuUsuario.UsuClave = UtilitiesHandler.CreateMD5(UsuUsuario.UsuClave);

            _context.UsuUsuarios.Add(UsuUsuario);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
