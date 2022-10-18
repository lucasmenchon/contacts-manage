using DawnPoets.Data;
using DawnPoets.Repositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BancoContext>(options => options.UseMySql("server=localhost;database=contatosdb;uid=root;pwd=", ServerVersion.Parse("8.0.30-mysql")));
builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

var app = builder.Build();
//builder.Services.AddEntityFrameworkMySql().AddDbContext<BancoContext>();
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
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();