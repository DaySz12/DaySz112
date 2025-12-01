using MiniProjects.Frontend.Components;
using MiniProjects.Frontend.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.StaticFiles; // <--- สำคัญมากสำหรับ FileExtensionContentTypeProvider

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents(); 

builder.Services.AddScoped(sp => new HttpClient
{
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

// ----------------------------------------------------------------------
// **ส่วนที่ต้องแก้ไข:** การกำหนด ContentTypeProvider และเรียกใช้ StaticFiles
// ----------------------------------------------------------------------
var provider = new FileExtensionContentTypeProvider();
// บังคับให้รู้จัก .js และ .wasm ด้วย MIME Type ที่ถูกต้อง
provider.Mappings[".wasm"] = "application/wasm"; 
provider.Mappings[".js"] = "application/javascript"; 
// ต้องเรียกใช้ UseStaticFiles ด้วย provider ที่กำหนดใหม่
app.UseStaticFiles(new StaticFileOptions { ContentTypeProvider = provider }); 
// ----------------------------------------------------------------------

app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();