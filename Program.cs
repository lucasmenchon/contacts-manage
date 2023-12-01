using ContactsManage.Data;
using ContactsManage.Helper;
using ContactsManage.Repositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("LocalDb")
    : builder.Configuration.GetConnectionString("HostDb");

builder.Services.AddDbContext<DataContext>(options => options.UseMySql(connectionString, ServerVersion.Parse("8.0.31-mysql")));

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(session =>
{
    session.Cookie.HttpOnly = true;
    session.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IContactRepositorio, ContactRepositorio>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ContactsManage.Helper.ISession, Session>();
builder.Services.AddScoped<IEmail, EmailModel>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
