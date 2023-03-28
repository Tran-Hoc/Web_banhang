using Microsoft.EntityFrameworkCore;
using Web_banhang.Config;
using Web_banhang.DataContext;
using Web_banhang.Models;
using Web_banhang.Service;

var builder = WebApplication.CreateBuilder(args);

// CreateAsync services to the container.


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WebBanHangContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));

builder.Services.Configure<FileSystemConfig>(builder.Configuration.GetSection(FileSystemConfig.ConfigName));

builder.Services.AddScoped<IRepository<NewsVM>, NewsRepository>();
builder.Services.AddScoped<IRepository<ProdCategoryVM>, ProductCategoryRepository>();
builder.Services.AddScoped<IRepository<ProductVM>, ProductRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Categories}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
