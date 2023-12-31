using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ReportApp;
using ReportApp.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<IBugReportDataService, BugReportDataService>(client => client.BaseAddress = new Uri("https://localhost:7046/"));
builder.Services.AddHttpClient<IFeedbackDataService, FeedbackDataService>(client => client.BaseAddress = new Uri("https://localhost:7046/"));
builder.Services.AddHttpClient<IUserDataService, UserDataService>(client => client.BaseAddress = new Uri("https://localhost:7046/"));
builder.Services.AddHttpClient<IAttachmentService, AttachmentService>(client => client.BaseAddress = new Uri("https://localhost:7046/"));


await builder.Build().RunAsync();
