using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.Active.Hardware
{
    public class IndexModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;
        public IndexModel(itmanager.Models.itmanagerContext context) : base(context) { _context = context; }

        [BindProperty]
        public ActActivo ActActivo { get; set; }
        public IList<HwHardwareSpec> HwHardwareSpec { get; set; }

        public async Task OnGetAsync(long? id)
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                ActActivo = _context.ActActivos
                                 .Include(a => a.Tfa).FirstOrDefault(m => m.ActId == id);

                // Navigation Model
                sessionInfo.SelectID = id.ToString();
                sessionInfo.SelectName = ActActivo.ActModelo;
                sessionInfo.SelectImg = UtilitiesHandler.ImgRoute(base.appConfig.ImagePathActives + ActActivo.ActImagen1, TypeRouteImg.ReadSep);
                

                HwHardwareSpec = await _context.HwHardwareSpecs
                    .Include(h => h.Atc)
                    .Include(h => h.Tpr).Where(m => m.AtcId == id).ToListAsync();

                ViewData["Activo"] = ActActivo;
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
