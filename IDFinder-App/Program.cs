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
builder.Services.AddTransient<IWriter, Writer>();
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


namespace IDFinder_App
{
	public interface IWriter
	{
		abstract Task WriteToFile(string fileName, IEnumerable<string> strs);
	}
	public class Writer : IWriter
	{
		public Task WriteToFile(string fileName, IEnumerable<string> strs)
		{
			Task task = new(() =>
			{
				using (StreamWriter sw = new StreamWriter(fileName, false))
				{
					foreach (string str in strs)
						sw.WriteLine(str);
				}
			});
			task.Start();
			return task;
		}
	}
}