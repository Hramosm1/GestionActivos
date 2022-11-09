﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;

namespace itmanager.Pages.Active.Discharged
{
    public class CreateModel : PageModelController
    {
        private readonly itmanagerContext _context;

        [TempData]
        public string MsgPageResult { get; set; }

        public CreateModel(itmanagerContext context) : base(context)
        {
            _context = context;
        }

        public MessagesCollection Messages = new MessagesCollection();
        public IList<ActActivo> Activos { get; set; }
        public ActActivo OneActivo;

        public async Task OnGetAsync(long id)
        {
            if (MsgPageResult != null)
            {
                // Messages.Collection.Add(new Message { title = "Acción en inventario", message = MsgPageResult, messageType = TypeMessage.success, timeout = 3000 });
            }

            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null)
            {
                Activos = await _context.ActActivos
                    .Include(a => a.Tac)
                    .Where(a=>!a.ActDadodebaja)
                    .Where(a => a.ActEstado == "almacenado")
                    .ToListAsync();
                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                NoSession();
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {

            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null)
            {
                string id = Request.Form["identifier"].ToString();
                long ida = long.Parse(id);

                OneActivo = _context.ActActivos
                    // .AsNoTracking()
                    .Where(x => x.ActId == ida)
                    .FirstOrDefault();

                string oldstate = OneActivo.ActEstado;

                _context.Attach(OneActivo).State = EntityState.Modified;
                OneActivo.ActDadodebaja = true;
                OneActivo.ActEstado = "dado-de-baja";
                OneActivo.ActDdbCausal = Request.Form["causal"].ToString();
                OneActivo.ActDdbComentario = Request.Form["comments"].ToString();

                // Audit
                AudAuditorium OneAudit = Audit(OneActivo, sessionInfo.UserID, oldstate, null, UtilitiesHandler.ValidateSession(HttpContext, _context), "usu");
                _context.Attach(OneAudit).State = EntityState.Added;
                await _context.SaveChangesAsync();

                ViewData["Breadcrumb"] = sessionInfo;
            }
            else
            {
                // Clear last user session
                NoSession();
            }
            return RedirectToPage("./Index");
        }


    }
}
