using iLoveIbadah.Website.Services;
using iLoveIbadah.Website.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using iLoveIbadah.Website.Services.Base;
using AutoMapper;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri(builder.Configuration["API_URL"]));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// builder.Services.AddAutoMapper(typeof(Program).Assembly);
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
