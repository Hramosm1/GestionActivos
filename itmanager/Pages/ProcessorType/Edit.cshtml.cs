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
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace itmanager.Pages.ProcessorType
{
    public class EditModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        [BindProperty]
        public SingleFileUpload FileUpload { get; set; }

        [BindProperty]
        public TprTipoProcesador TprTipoProcesador { get; set; }

        [BindProperty]
        public string imagen1 { get; set; }

        public AppConfig appConfig { get; set; }

        public EditModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

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

                TprTipoProcesador = await _context.TprTipoProcesadors.FirstOrDefaultAsync(m => m.TprId == id);

                if (TprTipoProcesador == null)
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


        public async Task<IActionResult> OnPostAsync()
        {
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);

            // Validate the file upload
            if (FileUpload.FormFile == null) {

                ModelState.Remove("FileUpload.FormFile");
                TprTipoProcesador.TprImagen = imagen1;

            } else {

                UploadInfo up = new UploadInfo();
                up.Directory = sessionInfo.Config.ImagePathTypeProcessor;
                up.FileName = FileUpload.FormFile.FileName;
                up.FileUpload = FileUpload;

                string filename = await UtilitiesHandler.UploadFile(up);
                TprTipoProcesador.TprImagen = filename;
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Attach(TprTipoProcesador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TprTipoProcesadorExists(TprTipoProcesador.TprId))
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

        private bool TprTipoProcesadorExists(long id)
        {
            return _context.TprTipoProcesadors.Any(e => e.TprId == id);
        }
    }
}
