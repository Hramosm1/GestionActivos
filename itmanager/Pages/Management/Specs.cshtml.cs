using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.Management
{
    public class SpecsModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        public SpecsModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        public ActActivo ActActivo { get; set; }
        public IList<HwHardwareSpec> HwHardwareSpec { get; set; }
        public IList<AceActivoEmpleado> ActivoEmpleado { get; set; }
        public IList<AudAuditorium> Auditoria { get; set; }
        public IList<DinDetalleInventario> Inventario { get; set; }

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
                    .Include(a => a.Tac).FirstOrDefaultAsync(m => m.ActId == id);

                HwHardwareSpec = await _context.HwHardwareSpecs
                    .Include(h => h.Atc)
                    .Include(h => h.Tpr)
                    .Where(m => m.AtcId == id)
                    .ToListAsync();

                ActivoEmpleado = await _context.AceActivoEmpleados
                    .Include(h => h.Emp)
                    .Where(e => e.ActId == id)
                    .ToListAsync();

                Auditoria = await _context.AudAuditoria
                    .Where(h => h.ActId == id)
                    .OrderByDescending(x => x.AudFecha)
                    .ToListAsync();

                Inventario = await _context.DinDetalleInventarios
                    .Include(h => h.Inv)
                    .Where(x => x.ActId == id)
                    .OrderByDescending(x => x.DinFecha)
                    .ToListAsync();

                if (ActActivo == null)
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
