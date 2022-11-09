using System;
using System.IO;
using itmanager.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using itmanager.Data;
using System.Drawing;
using QRCoder;
using Microsoft.EntityFrameworkCore;

namespace itmanager.Class
{
    public static class UtilitiesHandler
    {
        //------------------------------------------------------------------------------------------------------------
        // Validate Session
        //------------------------------------------------------------------------------------------------------------

        public static BreadcrumbInfo ValidateSession2(HttpContext httpContext, itmanagerContext dboContext)
        {
            BreadcrumbInfo sessionBreadcrumb = new BreadcrumbInfo();

            if (httpContext.Session.IsAvailable && httpContext.Session.Keys.Count() > 0) {

                int userid = (int)httpContext.Session.GetInt32("userid");
                sessionBreadcrumb.UserID = userid;

                if(userid != null) {

                    // Config
                    sessionBreadcrumb.Config = ReadAppConfig();

                    // User
                    sessionBreadcrumb.UserSession = (UsuUsuario)dboContext.UsuUsuarios
                                                .Include(a=>a.UroUsuarioRols)    
                                                .FirstOrDefault(x => x.UsuId.Equals(userid));
                    // Menu
                    if (sessionBreadcrumb.UserSession.UsuAdmin)
                    {
                        // All menu
                        sessionBreadcrumb.Menu = dboContext.OpcOpcions.ToList();
                    }
                    else
                    {
                        int RoleId = (int)sessionBreadcrumb.UserSession.UroUsuarioRols.FirstOrDefault().RolId;
                        List<OpcOpcion> options = dboContext.OpcOpcions
                                      .Include(x => x.OroOpcionRols)
                                      .Include(x => x.OroOpcionRols.Where(a => a.RolId == (int)RoleId))
                                      .ToList();
                        List<OpcOpcion> menuNav = options.Where(x => x.OroOpcionRols.Count > 0).ToList();
                        sessionBreadcrumb.Menu = menuNav;
                    }

                    return sessionBreadcrumb;
                }
            }
            return null;
        }

        public static UsuUsuario ValidateSession(HttpContext httpContext, itmanagerContext dboContext)
        {
            if (httpContext.Session.IsAvailable && httpContext.Session.Keys.Count() > 0)
            {

                UsuUsuario OneUsuario;
                int userid = (int)httpContext.Session.GetInt32("userid");

                OneUsuario = (UsuUsuario)dboContext.UsuUsuarios
                    .Include(a => a.UroUsuarioRols)
                    .FirstOrDefault(x => x.UsuId.Equals(userid));

                // UroUsuarioRol UsuRol = (UroUsuarioRol)OneUsuario.UroUsuarioRols.FirstOrDefault();

                // Set static vars
                //UtilitiesHandler.UserId = (int)UsuRol.UsuId;
                //UtilitiesHandler.UserRole = (int)UsuRol.RolId;
                //UtilitiesHandler.UserAdmin = OneUsuario.UsuAdmin ? 1 : 0;

                return OneUsuario;
            }
            return null;
        }

        //------------------------------------------------------------------------------------------------------------
        // Create QRCode
        //------------------------------------------------------------------------------------------------------------
        public static string CreateQRCode(string pathQR, ActActivo info)
        {
            // Create QR Code
            string pathQRCode = String.Format("{0}{1}.png", pathQR, info.ActUid);
            string contentQR = String.Format("{0}|{1}|{2}", info.ActUid, info.ActSerie, info.ActModelo);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(contentQR, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20, false);

            using (MemoryStream stream = new MemoryStream(qrCodeAsPngByteArr))
            {
                Image img = Image.FromStream(stream);
                img.Save(pathQRCode);
            }
            return pathQRCode;
        }


        //------------------------------------------------------------------------------------------------------------
        // Read ConnectionString Config
        //------------------------------------------------------------------------------------------------------------
        public static ConnectionStrings ReadConnectionString()
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json").Build();

            var section = config.GetSection(nameof(ConnectionStrings));
            ConnectionStrings cnnConfig = section.Get<ConnectionStrings>();
            return cnnConfig;
        }

        //------------------------------------------------------------------------------------------------------------
        // Read AppSettings Config
        //------------------------------------------------------------------------------------------------------------
        public static AppConfig ReadAppConfig()
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json").Build();

            var section = config.GetSection(nameof(AppConfig));
            AppConfig appConfig = section.Get<AppConfig>();
            return appConfig;
        }

        //------------------------------------------------------------------------------------------------------------
        // Get Files in Directory
        //------------------------------------------------------------------------------------------------------------
        public static List<string>GetFilesDir(string pathToGetFiles)
        {
            List<string> Files = new List<string>();

            //Fetch all files in the Folder (Directory).
            string[] filePaths = Directory.GetFiles(Path.Combine(pathToGetFiles));
            
            //Copy File names to Model collection.
            foreach (string filePath in filePaths)
            {
                Files.Add(Path.GetFileName(filePath));
            }
            return Files;
        }

        //------------------------------------------------------------------------------------------------------------
        // Get Avatar Name
        //------------------------------------------------------------------------------------------------------------
        public static string GetAvatarName(string name, string size = "40")
        {
            AppConfig appConfig = ReadAppConfig();

            string link = appConfig.ImageAvatarAPI.Replace("size=40", "size=" + size);
            string url = link + "&name=" + name;

            return url;
        }

        //------------------------------------------------------------------------------------------------------------
        // State CSS Badge
        //------------------------------------------------------------------------------------------------------------
        public static string StateCSS(string state)
        {
            string css = "";
            switch (state)
            {
                case "soporte":
                    css = "bg-warning";
                    break;

                case "asignado":
                    css = "bg-success";
                    break;

                case "inventariado":
                    css = "bg-success";
                    break;

                case "pre-asignado":
                    css = "bg-danger";
                    break;

                case "pre-asignado-soporte":
                    css = "bg-danger";
                    break;

                case "dado-de-baja":
                    css = "bg-danger";
                    break;

                default:
                    css = "bg-secondary";
                    break;
            }
            return css;
        }


        //------------------------------------------------------------------------------------------------------------
        // Upload File
        //------------------------------------------------------------------------------------------------------------
        public static async Task<string>UploadFile(UploadInfo up)
        {
            string name = CreateImgName(up);

            using (var memoryStream = new MemoryStream())
            {
                FileStream file = new FileStream(name, FileMode.Create, FileAccess.Write);
                await up.FileUpload.FormFile.CopyToAsync(memoryStream);
                memoryStream.WriteTo(file);
                memoryStream.Close();
            }
            return Path.GetFileName(name);
        }

        //------------------------------------------------------------------------------------------------------------
        // Create Name for Image, based in action 
        //------------------------------------------------------------------------------------------------------------
        public static string CreateImgName(UploadInfo up, bool withDirectory = true )  {
            if (withDirectory)
            {
                return String.Format("{0}{1}{2}", up.Directory, Guid.NewGuid().ToString(), Path.GetExtension(up.FileName));   
                // return String.Format("{0}{1}-{2}{3}", up.Directory, Path.GetFileNameWithoutExtension(up.FileName), up.position, Path.GetExtension(up.FileName));
            }
            else {
                return String.Format("{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(up.FileName));
                // return String.Format("{0}-{1}{2}", Path.GetFileNameWithoutExtension(up.FileName), up.position, Path.GetExtension(up.FileName));
            }
        }

        //------------------------------------------------------------------------------------------------------------
        // Create Route for Image
        //------------------------------------------------------------------------------------------------------------
        public static string ImgRoute(string ImgPath, TypeRouteImg type = TypeRouteImg.Read) {

            AppConfig appConfig = ReadAppConfig();

            try
            {
                FileAttributes attr = File.GetAttributes(ImgPath);

                if (attr != null)
                {
                    switch (type)
                    {
                        case TypeRouteImg.Read:
                            return ImgPath.Replace("wwwroot/", "");

                        case TypeRouteImg.ReadSep:
                            return ImgPath.Replace("wwwroot", "");

                        case TypeRouteImg.Write:
                            return Path.DirectorySeparatorChar + ImgPath;
                    }
                }
                else
                {
                    return Path.DirectorySeparatorChar + appConfig.ImagePathNoImg;
                }
            }
            catch (Exception e) {
                return Path.DirectorySeparatorChar + appConfig.ImagePathNoImg;
            }

            return Path.DirectorySeparatorChar + appConfig.ImagePathNoImg;
        }

        //------------------------------------------------------------------------------------------------------------
        // Create MD5 Hash
        //------------------------------------------------------------------------------------------------------------
        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes); 
            }
        }

    }


    //------------------------------------------------------------------------------------------------------------
    // Class For Upload File
    //------------------------------------------------------------------------------------------------------------
    public class SingleFileUpload
    {
        [Display(Name = "Archivo")]
        public IFormFile? FormFile { get; set; }
    }

    public class UploadInfo
    {
        public string Directory { get; set; }
        public string FileName { get; set; }
        public string Id { get; set; }
        public int position { get; set; }
        public SingleFileUpload FileUpload { get; set; }
    }
    //------------------------------------------------------------------------------------------------------------
    // Class For Read Settings
    //------------------------------------------------------------------------------------------------------------
    public class ConnectionStrings 
    {
        public string ITManagerConnectionString { get; set; }
    }


    public class AppConfig
    {
        public string ImagePathTypeProcessor { get; set; }
        public string ImagePathUserProfile { get; set; }
        public string ImagePathActives { get; set; }
        public string ImagePathNoImg { get; set; }
        public string ImagePathQRCode { get; set; }
        public string ImageQRCodeExt { get; set; } = ".png";
        public string Timeout { get; set; }
        public string ApplicationPath { get; set; }
        public string ImageAvatarAPI { get; set; }
        public string CameraPosition { get; set; } = "user";
    }

    public class BreadcrumbInfo
    {
        public string SelectID { get; set; }
        public string SelectName { get; set; }
        public string SelectImg { get; set; }
        public List<OpcOpcion> Menu { get; set; }
        public AppConfig Config { get; set; }
        public UsuUsuario UserSession { get; set; }
        public int UserID { get; set; }
    }

    public enum TypeRouteImg { 
        Read = 1,
        ReadSep = 2,
        Write =3
    }

}
