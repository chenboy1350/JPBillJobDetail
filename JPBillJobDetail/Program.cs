using JPBillJobDetail.Data;
using JPBillJobDetail.Models;
using JPBillJobDetail.Service.Implement;
using JPBillJobDetail.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddDbContext<JPDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<AppSettingModel>(builder.Configuration.GetSection("AppSetting"));

builder.Services.AddScoped<IBillJobService, BillJobService>();
builder.Services.AddScoped<IDataMockUpService, DataMockUpService>();
builder.Services.AddScoped<IBillJobReportService, BillJobReportService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
