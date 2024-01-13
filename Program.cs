using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers( options =>
{
  options.ReturnHttpNotAcceptable = true;
}
).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
builder.Services.AddSingleton<CitiesDataStore>();
builder.Services.AddDbContext <CityInfoContext>(dbContextOptions => dbContextOptions.UseSqlite(builder.Configuration.GetConnectionString("CityInfoConnection")));

builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
}
);

app.MapControllers();

// app.Run(async (context) =>
// {
//    await context.Response.WriteAsync("Hello from City API!");
// });

app.Run();
