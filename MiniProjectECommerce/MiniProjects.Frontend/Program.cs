using MiniProjects.Frontend.Components;
using MiniProjects.Frontend.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);



// ลงทะเบียน Cart Service 
builder.Services.AddScoped<ICartService, CartService>();

// ลงทะเบียน Razor Components 
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); 

// ลงทะเบียน HttpClient (สำหรับเรียก Backend API)
builder.Services.AddScoped(sp => new HttpClient
{
    // ตรวจสอบ BaseAddress ให้ตรงกับพอร์ตของ Backend (ECommerce.Api)
    BaseAddress = new Uri("https://localhost:7097/")
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// แมป Razor Components และอนุญาตให้ใช้ Interactive Server Render Mode
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); 

app.Run();