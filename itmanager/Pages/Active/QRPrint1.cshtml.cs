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
    public class QRPrint1Model : PageModelController
    {
        private readonly itmanagerContext _context;
        private string baseUrl = "";

        [BindProperty]
        public ActActivo ActActivo { get; set; }

        [BindProperty]
        public string QRCodeIdentifier { get; set; }


        public QRPrint1Model(itmanagerContext context) : base(context)
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
                    
                GetHTMLPageAsJPG(ActActivo, 500, 500);
            }
            else
            {
                // Clear last user session
                NoSession();
            }

            
            return Page();
        }

        public void GetHTMLPageAsPDF()
        {
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            converter.Options.PdfPageOrientation = SelectPdf.PdfPageOrientation.Portrait;
            converter.Options.PdfPageSize = SelectPdf.PdfPageSize.Custom;
            converter.Options.PdfPageCustomSize = new SizeF(72, 144);
            converter.Options.WebPageWidth = 260;
            converter.Options.WebPageHeight = 260;
            converter.Options.MarginLeft = 2;
            converter.Options.MarginRight = 2;
            converter.Options.MarginTop = 2;
            converter.Options.MarginBottom = 2;

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString("<body style='background-color:grey;'><h1>QRCode</h1><img src='images/qrcodes/01d096b6.png' width='250px' height='250px'></body>", baseUrl);
            doc.Save("wwwroot/test.pdf");
            doc.Close();
        }


        public void GetHTMLPageAsJPG0(ActActivo info, int w, int h)
        {
            //SelectPdf.HtmlToImage converter = new(w, h);
            
            //string html = String.Format(
            //    "<link rel='stylesheet' href='images/qrcodes/qrcode.css' type='text/css' media='all'/>" +
            //        "<div class='card'>" +
            //            "<div class='card-header'>" +
            //                "<img src = 'images/qrcodes/{0}.png' width='240px' height='240px alt='rover' /> " +
            //            "</div> " +
            //            "<div class='card-body'>" +
            //            "<span class='tag tag-teal'>{1}</span>" +
            //            "<h4>{2}</h4>" +
            //            "<p>{3}</p>" +
            //            "<div class='user'>" +
            //            "<div class='user-info'>" +
            //                "<h2>{4}</h2>" +
            //                "<small>{5}</small>" + 
            //            "</div>" +
            //        "</div>" +
            //    "</div>" +
            //"</div>", info.ActUid, info.ActUid, info.ActSerie, info.ActModelo, info.Tac.TacNombre, info.ActFechacompra);

            //Image image = converter.ConvertHtmlString(html, baseUrl);
            //image.Save("wwwroot/test.jpg");
        }

        public void GetHTMLPageAsJPG(ActActivo info, int w, int h)
        {
            SelectPdf.HtmlToImage converter = new(w, h);

            string html = String.Format(
                "<link rel='stylesheet' href='images/qrcodes/qrcode.css' type='text/css' media='all'/>" +
                    "<div class='card'>" +
                        "<div class='card-header'>" +
                            "<img src = 'images/qrcodes/{0}.png' width='{1}px' height='{2}px alt='rover' /> " +
                        "</div> " +
                    "</div>", info.ActUid, w-10, h-10);

            this.QRCodeIdentifier = String.Format("images/labels/{0}-1.jpg", info.ActUid);

            Image image = converter.ConvertHtmlString(html, baseUrl);
            image.Save("wwwroot/" + this.QRCodeIdentifier);
        }

    }
}
