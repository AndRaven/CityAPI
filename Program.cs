using System.Text;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
builder.Services.AddDbContext <CityInfoContext>(dbContextOptions => dbContextOptions.UseSqlite(builder.Configuration.GetConnectionString("CityInfoDBConnectionString")));

builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretForKey"] ?? "")),

    };
});

builder.Services.AddApiVersioning( setupAction =>
  {
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    setupAction.ReportApiVersions = true;
  }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHtpsRedirection();
app.UseRouting();

//add JWT token authentication

app.UseAuthentication();

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
