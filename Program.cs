using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Core.Helpers;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<DbInstaller>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddDbContext<EfDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/User/Login";
        options.AccessDeniedPath = "/User/AccessDenied";

        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;

        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;//browser will not attach the authentication cookie
        options.Cookie.Name = "MyApp.Auth";

        options.Events.OnValidatePrincipal = async context =>
{
    var lastValidated = context.Properties.GetTokenValue("LastValidated");

    if (lastValidated != null &&
        DateTime.Parse(lastValidated).AddMinutes(5) > DateTime.UtcNow)
    {
        return;
    }

    var userIdClaim = context.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
    var db = context.HttpContext.RequestServices.GetRequiredService<EfDbContext>();

    var user = await db.Users.FindAsync(int.Parse(userIdClaim));

    if (user == null
    //|| !user.IsActive
    )
    {
        context.RejectPrincipal();
        await context.HttpContext.SignOutAsync();
    }

    context.Properties.StoreTokens(new[]
    {
        new AuthenticationToken
        {
            Name = "LastValidated",
            Value = DateTime.UtcNow.ToString()
        }
    });
};
    });




builder.Services.AddAuthorization();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

// app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Register}/{id?}");



using (var scope = app.Services.CreateScope())
{
    var installer = scope.ServiceProvider.GetRequiredService<DbInstaller>();
    installer.Install();
}
app.Run();
