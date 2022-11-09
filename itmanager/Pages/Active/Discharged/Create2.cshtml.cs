using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using itmanager.Models;
using itmanager.Class;
using Microsoft.EntityFrameworkCore;

namespace itmanager.Pages.Active.Discharged
{
    public class Create2Model : PageModelController
    { 
        private readonly itmanager.Models.itmanagerContext _context;

        [TempData]
        public string MsgPageResult { get; set; }

        public MessagesCollection Messages = new MessagesCollection();

        public Create2Model(itmanager.Models.itmanagerContext context) : base(context)
        {
            _context = context;
        }

        public InvInventario OneInventory { get; set; }
        public ActActivo OneActivo { get; set; }

        public IActionResult OnGet(long? id)
        {
            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                

                if (id == null)
                {
                    return NotFound();
                }

                // Detail Selected
                OneInventory = (InvInventario)_context.InvInventarios
                    .AsNoTracking()
                    .FirstOrDefault(x => x.InvId == id);

                sessionInfo.SelectID = id.ToString();
                sessionInfo.SelectName = OneInventory.InvNombre;

                ViewData["ActId"] = new SelectList(_context.ActActivos, "ActId", "ActId");
                ViewData["InvId"] = new SelectList(_context.InvInventarios, "InvId", "InvId");
                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                base.NoSession();
            }
            return Page();
        }

        // Binding Property of dataform
        [BindProperty]
        public DinDetalleInventario DinDetalleInventario { get; set; }


        public async Task<IActionResult> OnPostAsync(long? id)
        {
            String IdDetailInv = "";
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);

            // Active Selected
            OneActivo = (ActActivo)_context.ActActivos
                    .AsNoTracking()
                    .FirstOrDefault(x => x.ActUid == DinDetalleInventario.ActUid);

            DinDetalleInventario.InvId = (long)id;
            DinDetalleInventario.ActId = OneActivo.ActId;
            DinDetalleInventario.DinFecha = DateTime.Now;
            DinDetalleInventario.DinUsuario = sessionInfo.UserSession.UsuLogin;

            long IDDetalle = DetalleInventarioExists((long)id, DinDetalleInventario.ActUid);
   
            if (IDDetalle != 0)
            {
                DinDetalleInventario.DinId = IDDetalle;
                _context.Attach(DinDetalleInventario).State = EntityState.Modified;
                MsgPageResult = "El inventario fue actualizado con exito";  //TempData
            }
            else {
                _context.DinDetalleInventarios.Add(DinDetalleInventario);
                MsgPageResult = "El inventario se creo con exito";  //TempData
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("/Inventory/Stock/List", routeValues: new { id });
        }

        private long DetalleInventarioExists(long idInv, string uidAct)
        {
            DinDetalleInventario exist = _context.DinDetalleInventarios
                                        .AsNoTracking()
                                        .FirstOrDefault(e => e.InvId == idInv && e.ActUid == uidAct);
            try
            {
                if (exist != null)
                {
                    return exist.DinId;
                }
                else
                {
                    return 0;
                }
            }
            finally {
                exist = null;
            }
        }

    }
}
