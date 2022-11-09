using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.Inventory.Stock
{
    public class ListModel : PageModelController
    {
        private readonly itmanager.Models.itmanagerContext _context;

        [TempData]
        public string MsgPageResult { get; set; } 

        public ListModel(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        public MessagesCollection Messages = new MessagesCollection();
        public IList<DinDetalleInventario> Stock { get; set; }
        public ActActivo OneActivo { get; set; }
        public AceActivoEmpleado ActivoEmpleado { get; set; }
        public InvInventario OneInventory { get; set; }

        public async Task OnGetAsync(long id)
        {
            if (MsgPageResult != null) {
                Messages.Collection.Add(new Message { title = "Acción en inventario", message = MsgPageResult, messageType = TypeMessage.success, timeout = 3000 });
            }

            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                

                Stock = _context.DinDetalleInventarios
                    .Include(a => a.Inv)
                    .Include(a => a.Act)
                    .Where(x => x.InvId == id)
                    .ToList();

                // Inventory Selected
                OneInventory = (InvInventario)_context.InvInventarios
                .FirstOrDefault(x => x.InvId == id);

                sessionInfo.SelectID = id.ToString();
                sessionInfo.SelectName = OneInventory.InvNombre;
                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }

        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {

            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                

                // Inventory Selected
                OneInventory = (InvInventario)_context.InvInventarios
                .FirstOrDefault(x => x.InvId == id);

                sessionInfo.SelectID = id.ToString();
                sessionInfo.SelectName = OneInventory.InvNombre;
                ViewData["Breadcrumb"] = sessionInfo;

                long iddetinv = long.Parse(Request.Form["dinid"].ToString());
                DinDetalleInventario detalle = await _context.DinDetalleInventarios.FindAsync(iddetinv);

                if (detalle != null)
                {
                    _context.DinDetalleInventarios.Remove(detalle);
                    await _context.SaveChangesAsync();
                }

                Stock = _context.DinDetalleInventarios
                    .Include(a => a.Inv)
                    .Include(a => a.Act)
                    .Where(x => x.InvId == id)
                    .ToList();

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
