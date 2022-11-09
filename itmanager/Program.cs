using itmanager.Data;
using itmanager.Class;
using itmanager.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddScoped<IITManager, ITManagerSql>();

//builder.Services.AddControllers().AddNewtonSoft();

// Add connection to database
builder.Services.AddDbContextPool<itmanagerContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("ITManagerConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

//app.UseMiddleware<DeChunkerMiddleware>();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.MapControllerRoute(name: "default",
               pattern: "{controller=Home}/{action=Index}");

    IWebHostEnvironment env = app.Environment;
    //RotativaConfiguration.Setup((Microsoft.AspNetCore.Hosting.IHostingEnvironment)env);

app.Run();