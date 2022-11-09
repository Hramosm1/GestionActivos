using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using itmanager.Models;
using itmanager.Class;
using System.IO;
using System.Drawing;
using QRCoder;

namespace itmanager.Pages.Active
{
    public class CreateModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        // Upload File
        [BindProperty]
        public SingleFileUpload FileUpload1 { get; set; }

        [BindProperty]
        public SingleFileUpload FileUpload2 { get; set; }

        public AppConfig appConfig { get; set; }

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
                
                ViewData["TacId"] = new SelectList(_context.TacTipoActivos, "TacId", "TacNombre");
                ViewData["TfaId"] = new SelectList(_context.TfaTipoFabricantes, "TfaId", "TfaNombre");
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
        public ActActivo ActActivo { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            appConfig = base.appConfig;

            // Upload files
            if (FileUpload1.FormFile != null)
            {
                UploadInfo up = new UploadInfo();
                up.Directory = base.appConfig.ImagePathActives;
                up.FileName = FileUpload1.FormFile.FileName;
                up.FileUpload = FileUpload1;

                string filename = await UtilitiesHandler.UploadFile(up);
                ActActivo.ActImagen1 = filename;
            }

            if (FileUpload2.FormFile != null) { 
                UploadInfo up = new UploadInfo();
                up.Directory = base.appConfig.ImagePathActives;
                up.FileName = FileUpload2.FormFile.FileName;
                up.FileUpload = FileUpload2;

                string filename = await UtilitiesHandler.UploadFile(up);
                ActActivo.ActImagen2 = filename;
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                .Where(y => y.Count > 0)
                .ToList();

                return Page();
            }

            // Default state
            ActActivo.ActEstado = "almacenado";

            // Create QR Code
            ActActivo.ActUid = Guid.NewGuid().ToString().Replace("-","").Substring(1,8);
            UtilitiesHandler.CreateQRCode(appConfig.ImagePathQRCode, ActActivo);

            _context.ActActivos.Add(ActActivo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
