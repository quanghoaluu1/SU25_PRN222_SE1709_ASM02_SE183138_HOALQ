using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using SchoolMedical.BlazorWebApp.HoaLQ.Components;
using SchoolMedical.Repositories.HoaLQ;
using SchoolMedical.Repositories.HoaLQ.Auth;
using SchoolMedical.Services.HoaLQ;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Đường dẫn đến trang đăng nhập. Khi người dùng chưa xác thực cố gắng truy cập
        // một trang được bảo vệ, hệ thống sẽ tự động chuyển hướng họ đến đây.
        options.LoginPath = "/login";

        // (Tùy chọn) Thời gian sống của cookie xác thực
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

        // (Tùy chọn) Đường dẫn khi người dùng đã đăng nhập nhưng không có quyền truy cập
        // vào một tài nguyên cụ thể (lỗi 403 Forbidden).
        options.AccessDeniedPath = "/access-denied"; 
    });
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IServiceProviders,ServiceProviders>();
builder.Services.AddScoped<ISystemUserAccountService, SystemUserAccountService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>(); // Rất quan trọng!

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();