using JPBillJobDetail.Data;
using JPBillJobDetail.Models;
using JPBillJobDetail.Service.Implement;
using JPBillJobDetail.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<JPDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<AppSettingModel>(builder.Configuration.GetSection("AppSetting"));

builder.Services.AddScoped<IBillJobService, BillJobService>();
builder.Services.AddScoped<IDataMockUpService, DataMockUpService>();

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
