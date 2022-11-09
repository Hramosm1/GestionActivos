using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using itmanager.Models;
using System.ComponentModel.DataAnnotations;
using itmanager.Class;

namespace itmanager.Pages.ProcessorType
{
    public class CreateModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        // Upload File
        [BindProperty]
        public SingleFileUpload FileUpload { get; set; }

        // Constructor
        public CreateModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        // ActionResult GET
        public IActionResult OnGet()
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                ViewData["Menu"] = sessionInfo.Menu;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }
            return Page();
        }


        // ActionResult POST
        [BindProperty]
        public TprTipoProcesador TprTipoProcesador { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            UploadInfo up = new UploadInfo();
            up.Directory = base.appConfig.ImagePathTypeProcessor;
            up.FileName = FileUpload.FormFile.FileName;
            up.FileUpload = FileUpload;

            string filename = await UtilitiesHandler.UploadFile(up);
            TprTipoProcesador.TprImagen = filename;

            if (!ModelState.IsValid)  {
                return Page();
            }

            _context.TprTipoProcesadors.Add(TprTipoProcesador);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }


}
