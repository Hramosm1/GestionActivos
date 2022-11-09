using itmanager.Data;
using itmanager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace itmanager.Class
{
    public class PageModelController : PageModel
    {
        // Context
        public IITManager context { get; }
        private itmanagerContext contextChild;

        public AppConfig appConfig { get; set; }

        // Context Model::Main
        public PageModelController(IITManager _context)
        {
            context = _context;
            appConfig = UtilitiesHandler.ReadAppConfig();
        }

        // Context Model::Child
        public PageModelController(itmanagerContext _context)
        {

            contextChild = _context;
            appConfig = UtilitiesHandler.ReadAppConfig();
        }

        public IActionResult NoSession()
        {
            return RedirectToPage("/");
        }

        public AudAuditorium Audit(ActActivo act, long personId, string oldstate, string ConfirmationUID, UsuUsuario user, string typeAudit = "emp") {
            
            AudAuditorium OneAudit;
            EmpEmpleado OneEmployee;
            UsuUsuario OneUser;

            string personName = "";

            if (typeAudit == "emp") {
                // Employee
                OneEmployee = contextChild.EmpEmpleados.
                    Where(x => x.EmpId == personId).FirstOrDefault();
                personName = OneEmployee.EmpNombre;
            }

            if (typeAudit == "usu")
            {
                // Employee
                OneUser = contextChild.UsuUsuarios.
                    Where(x => x.UsuId == personId).FirstOrDefault();
                personName = OneUser.UsuNombre;
            }

            // Insert in Audit
            OneAudit = new AudAuditorium();
            OneAudit.ActId = act.ActId;
            OneAudit.AudUid = act.ActUid;
            OneAudit.AudEstadoAnterior = oldstate;
            OneAudit.AudModelo = act.ActModelo;
            OneAudit.AudSerie = act.ActSerie;
            OneAudit.EmpId = personId;
            OneAudit.EmpNombre = personName;
            OneAudit.ConId = ConfirmationUID;
            OneAudit.UsuModificadoPor = user.UsuLogin;
            OneAudit.AudEstadoNuevo = act.ActEstado;
            OneAudit.AudTipoPersona= typeAudit;

            return OneAudit;
        }

    }




}
