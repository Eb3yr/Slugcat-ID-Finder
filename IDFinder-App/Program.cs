using IDFinder_App;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.WebHost.UseStaticWebAssets();
builder.Services.AddTransient<IIndexState, IndexState>();   // https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-8.0 
// no. Use some other kind of singleton pattern, services reset when the page ("app") resets. Regular dependency injection here won't work.

var app = builder.Build();

//if (!app.Environment.IsDevelopment())	// WILL BREAK LOCALHOST
//
//	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//	app.UseHsts();
//

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();