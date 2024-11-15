using cumulative1.Models;
using Cumulative1.Controllers;
using Cumulative1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<TeacherAPIController>();
builder.Services.AddScoped<SchoolDbContext>();
builder.Services.AddScoped<StudentAPIController>();
builder.Services.AddScoped<CourseAPIController>();
var app = builder.Build();

// Configure the HTTP request 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS (HTTP Strict Transport Security) duration is 30 days. 
    // Consider adjusting this setting for production environments. 
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();