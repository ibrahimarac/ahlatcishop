using Ahlatci.Shop.UI.Authorization;
using Ahlatci.Shop.UI.Filters;
using Ahlatci.Shop.UI.Models;
using Ahlatci.Shop.UI.Services.Abstraction;
using Ahlatci.Shop.UI.Services.Implementation;
using Ahlatci.Shop.UI.Validators.Accounts;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(opt=> {
    opt.ModelValidatorProviders.Clear();
    opt.Filters.Add(new ActionExceptionFilter());
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining(typeof(LoginValidator));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(20);
});

builder.Services.AddScoped<IRestService, RestService>();


//Authentication

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    opt =>
    {
        opt.LoginPath = "/admin/login/signin";
    });


//Authorization

builder.Services.AddSingleton<IAuthorizationHandler, SessionBasedAccessHandler>();

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Admin", policy =>
    {
        policy.AddRequirements(new RoleAccessRequirement(Roles.Admin));
    });

    opt.AddPolicy("User", policy =>
    {
        policy.AddRequirements(new RoleAccessRequirement(Roles.User));
    });

    opt.AddPolicy("AdminOrUser", policy =>
    {
        policy.AddRequirements(new RoleAccessRequirement(Roles.Admin, Roles.User));
    });

});


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

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
