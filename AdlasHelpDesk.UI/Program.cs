using AdlasHelpDesk.Application;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using AdlasHelpDesk.UI.Helpers.LocalizerHelpers;
using AdlasHelpDesk.Infrastructure;
using AspNetCoreHero.ToastNotification.Extensions;
using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Web.Http;
using AdlasHelpDesk.UI.Middleware;
using Microsoft.AspNetCore.Mvc.Authorization;
using AdlasHelpDesk.Infrastructure;
using AdlasHelpDesk.UI.Helpers.LocalizerHelpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
    //to allow send very large data to post action
    options.MaxModelBindingCollectionSize = 100000;
    //to allow go deep validations 
    options.MaxModelValidationErrors = 999999;
    //options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
}).AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix).AddRazorRuntimeCompilation();

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

builder.Services.AddLocalization();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
builder.Services.AddSingleton<JsonStringLocalizer>();

builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(JsonStringLocalizerFactory));
    });
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("ar"),
        new CultureInfo("en"),
    };

    //options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = configuration["LoginURL"];
            options.AccessDeniedPath = new PathString("/Home/AccessDenied");
            options.Events = new CookieAuthenticationEvents
            {
                OnSigningIn = async context =>
                {
                    await Task.CompletedTask;
                },
                OnSignedIn = async context =>
                {
                    await Task.CompletedTask;
                },

                OnValidatePrincipal = async context =>
                {
                    await Task.CompletedTask;
                }
            };

        });

builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 5;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
}
  );
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            var exceptionHandlerPathFeature =
      context.Features.Get<IExceptionHandlerPathFeature>();
            var ex = exceptionHandlerPathFeature?.Error as HttpResponseException;
            if (ex?.Response.StatusCode == HttpStatusCode.Unauthorized)
            {
                context.Response.Redirect("/admin/auth/Logout");
            }
            else context.Response.Redirect("/Home/Error");
        });
    });
    app.UseHsts();
}
app.ConfigureCustomExceptionMiddleware();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

var supportedCultures = new[] { "en", "ar" };
var localizationOptions = new RequestLocalizationOptions()
    //.SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);
app.UseAuthentication();
app.UseAuthorization();
app.UseNotyf();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapAreaControllerRoute(
//    name: "Admin",
//    areaName: "Admin",
//    pattern: "admin/{controller=Auth}/{action=Login}/{id?}",
//        defaults: new { area = "Admin" }); // Add this line to specify the area

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "MyArea",
      pattern: "{area:exists}/{controller=Auth}/{action=login}/{id?}"
    );
});

app.Run();
