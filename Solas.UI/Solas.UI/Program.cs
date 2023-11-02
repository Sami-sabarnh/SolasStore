using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Solas.BL;
using Solas.BL.IRepositories;
using Solas.BL.Models;
using Solas.EF;
using Solas.EF.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Use IConfiguration to read app settings
//var smtpServer = builder.Configuration["SmtpSettings:Server"];
//var smtpPort = int.Parse(builder.Configuration["SmtpSettings:Port"]);
//var smtpUsername = builder.Configuration["SmtpSettings:Username"];
//var smtpPassword = builder.Configuration["SmtpSettings:Password"];

//builder.Services.AddTransient<IEmailService>(provider =>
//    new EmailService(smtpServer, smtpPort, smtpUsername, smtpPassword));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

builder.Services.AddTransient<IImageService,ImageService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("constr"));

});

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication().AddGoogle(options => {
    IConfigurationSection googleAuthentication = builder.Configuration.GetSection("Authentication:Google");
    options.ClientId = googleAuthentication["ClientId"];
    options.ClientSecret = googleAuthentication["ClientSecret"];
    

}); 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
  
AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
