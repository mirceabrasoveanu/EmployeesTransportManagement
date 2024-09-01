using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using EmployeesTransportManagement.Data;
using EmployeesTransportManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<EmailService>();
builder.Services.AddControllersWithViews();

// Add authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

})
.AddCookie()
.AddOpenIdConnect(options =>
{
    options.ClientId = "503933460100-306ouvv7q8mr3mvudqihl8l396gsc83s.apps.googleusercontent.com";
    //options.ClientId = builder.Configuration["Authentication:ClientId"];
    options.Authority = "https://accounts.google.com";
    //options.Authority = builder.Configuration["Authentication:Authority"];
    options.ClientSecret = "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";
    //options.ClientSecret = builder.Configuration["Authentication:ClientSecret"];
    options.CallbackPath = new PathString("/Home/Privacy");
    options.ResponseType = OpenIdConnectResponseType.Code;
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;
    options.RequireHttpsMetadata = false;
    options.SkipUnrecognizedRequests = true;
    options.Events = new OpenIdConnectEvents
    {
        OnRedirectToIdentityProvider = context =>
        {
            // Custom logic before redirecting to the identity provider
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            // Custom logic after token is validated
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
