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
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace itmanager.Pages.Active
{
    public class EditModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        // Upload File
        [ValidateNever]
        [BindProperty]
        public SingleFileUpload? FileUpload1 { get; set; }

        [ValidateNever]
        [BindProperty]
        public SingleFileUpload? FileUpload2 { get; set; }
        
        [BindProperty]
        public string imagen1 { get; set; }
        
        [BindProperty]
        public string imagen2 { get; set; }
        
        public AppConfig appConfig { get; set; }

        public EditModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        [BindProperty]
        public ActActivo ActActivo { get; set; }

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

                ActActivo = await _context.ActActivos
                    .Include(a => a.Tac)
                    .Include(a => a.Tfa)
                    .FirstOrDefaultAsync(m => m.ActId == id);

                if (ActActivo == null)
                {
                    return NotFound();
                }

                ViewData["TacId"] = new SelectList(_context.TacTipoActivos, "TacId", "TacNombre");
                ViewData["TfaId"] = new SelectList(_context.TfaTipoFabricantes, "TfaId", "TfaNombre");
                ViewData["Breadcrumb"] = sessionInfo;

                ModelState.Remove("FileUpload1.FormFile");
                ModelState.Remove("imagen1");
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
            appConfig = base.appConfig;

            // Upload file 1
            if (FileUpload1.FormFile == null)
            {
                ActActivo.ActImagen1 = imagen1;
                ModelState.Remove("FileUpload1.FormFile");
                ModelState.Remove("imagen1");
            }
            else
            {
                ModelState.Remove("imagen1");
                UploadInfo up = new UploadInfo();
                up.Directory = base.appConfig.ImagePathActives;
                up.FileName = FileUpload1.FormFile.FileName;
                up.FileUpload = FileUpload1;

                string filename = await UtilitiesHandler.UploadFile(up);
                ActActivo.ActImagen1 = filename;
            }

            // Upload file 2
            if (FileUpload2.FormFile == null)
            {
                ActActivo.ActImagen2 = imagen2;
                ModelState.Remove("FileUpload2.FormFile");
                ModelState.Remove("imagen2");
            }
            else
            {
                ModelState.Remove("imagen2");
                UploadInfo up = new UploadInfo();
                up.Directory = base.appConfig.ImagePathActives;
                up.FileName = FileUpload2.FormFile.FileName;
                up.FileUpload = FileUpload2;

                string filename = await UtilitiesHandler.UploadFile(up);
                ActActivo.ActImagen2 = filename;
            }

            // If not have UID
            if (ActActivo.ActUid == null || ActActivo.ActUid == "") {
                ActActivo.ActUid = Guid.NewGuid().ToString().Replace("-", "").Substring(1, 8);
            }

            // Create QRCode
            UtilitiesHandler.CreateQRCode(appConfig.ImagePathQRCode, ActActivo);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                    .Where(y => y.Count > 0)
                    .ToList();

                return Page();
            }
            _context.Attach(ActActivo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActActivoExists(ActActivo.ActId))
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

        private bool ActActivoExists(long id)
        {
            return _context.ActActivos.Any(e => e.ActId == id);
        }
    }
}
