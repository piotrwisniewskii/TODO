using Microsoft.EntityFrameworkCore;
using System;
using TODO.Data.DataBase;
using TODO.Services.NotesServices;
using TODO.Services.TODOUsersServices;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<TODODbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<INotesService, NotesService>();
builder.Services.AddTransient<ITODOUsersService, TODOUsersService>();

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

TODODbInitializer.Seed(app);
app.Run();
