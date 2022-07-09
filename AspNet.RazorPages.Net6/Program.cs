using AspNet.RazorPages.Net6.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
    .SetMinimumLevel(LogLevel.Trace)
    .AddConsole());

var logger = loggerFactory.CreateLogger<Program>();

var authority = builder.Configuration.SafeGet("OpenIdConnect:Authority");
logger.LogInformation("Authority: {Authority}", authority);
var clientId = builder.Configuration.SafeGet("OpenIdConnect:ClientId");
logger.LogInformation("ClientId: {ClientId}", clientId);
var tenant = builder.Configuration.SafeGet("OpenIdConnect:Tenant");
logger.LogInformation("Tenant: {Tenant}", tenant);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.Authority = authority;
        options.ClientId = clientId;
        options.ClientSecret = ""; // Not mandatory for AuthCodeWithPKCE flow

        options.ResponseType = "code"; // Authorization code flow
        options.UsePkce = true; // Turns on PKCE. True is default, so not necessary
        options.SaveTokens = true;
        options.Events.OnRedirectToIdentityProvider = context =>
        {
            context.ProtocolMessage.SetParameter(
                "acr_values", 
                $"tenant:{tenant}");
            return Task.CompletedTask;
        };
    });

builder.Services.AddRazorPages(options =>
    {
        // You can also add an AuthorizeFilter using the Authorize attribute to a page model class 
        options.Conventions.AuthorizePage("/Secret");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

