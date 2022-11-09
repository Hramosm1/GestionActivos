using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itmanager.Models;
using itmanager.Class;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

namespace itmanager.Pages.Active
{
    public class QRPrint2Model : PageModelController
    {
        private readonly itmanagerContext _context;
        private string baseUrl = "";

        [BindProperty]
        public string QRCodeIdentifier { get; set; }

        [BindProperty]
        public ActActivo ActActivo { get; set; }

        public QRPrint2Model(itmanagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string qrcode, int label)
        {
            baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

            // Session Validate
            BreadcrumbInfo sessionInfo = UtilitiesHandler.ValidateSession2(HttpContext, _context);
            if (sessionInfo != null) 
            {
                ViewData["Breadcrumb"] = sessionInfo;

                ActActivo = await _context.ActActivos
                    .Include(a => a.Tac)
                    .Include(a => a.Tfa)
                    .FirstOrDefaultAsync(m => m.ActUid == qrcode);
                
                GetHTMLPageAsJPG(ActActivo, 1000, 333);
            }
            else
            {
                // Clear last user session
                NoSession();
            }

            
            return Page();
        }

        public void GetHTMLPageAsJPG0(ActActivo info, int w, int h)
        {
            SelectPdf.HtmlToImage converter = new(w, h);

            string html = String.Format(
                "<link rel='stylesheet'  href='images/qrcodes/qrcode.css' type='text/css' media='all'/>" +
                    "<div class='card'>" +
                        "<div class='card-header'>" +
                            "<img src='images/qrcodes/{0}.png' width='{2}px'  height='{2}px'/> " +
                        "</div> " +
                    "</div>" +
                "</div>" +
            "</div>", info.ActUid, w, h - 10);

            Image image = converter.ConvertHtmlString(html, baseUrl);
            image.Save("wwwroot/test.jpg");
        }

        public void GetHTMLPageAsJPG(ActActivo info, int w, int h)
        {
            SelectPdf.HtmlToImage converter = new(w, h);

            string html = String.Format(
                "<link rel='stylesheet' href='images/qrcodes/qrcode2.css' type='text/css' media='all'/>" +
                    "<div class='courses-container'><div class='course'><div class='course-preview'>" +
                           "<img src='images/qrcodes/{0}.png' width='{2}px' height='{2}px' /> " +
                           "</div> " +
                           "<div class='course-info'>" +
                               "<h6>{3}</h6>" +
                               "<h6>{4}</h6>" +
                               "<h6>{5}</h6>" +
                               "<h6>{6}</h6>" +
                               "<img class='course-img' src='images/qrcodes/logo.jpg' width='256px' height='48px' />" +
                          "</div>" +
                        "</div>" +
                   " </div>", info.ActUid, w, h - 10, info.ActModelo, info.Tac.TacNombre, info.ActUid, info.ActSerie);

            this.QRCodeIdentifier = String.Format("images/labels/{0}-2.jpg", info.ActUid);

            Image image = converter.ConvertHtmlString(html, baseUrl);
            image.Save("wwwroot/" + this.QRCodeIdentifier);
        }


    }
}
