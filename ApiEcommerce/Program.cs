using ApiEcommerce.Repository;
using ApiEcommerce.Repository.IRepository;
using ApiEcommerce.Mapping;
using Microsoft.EntityFrameworkCore;
using ApiEcommerce.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbConnectionString = builder.Configuration.GetConnectionString("ConexionSql");
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(dbConnectionString));

builder.Services.AddResponseCaching(options =>
{
  options.MaximumBodySize = 1024 * 1024;
  options.UseCaseSensitivePaths = true;
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(_ => { }, typeof(Program).Assembly);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();

var secretKey = builder.Configuration.GetValue<string>("ApiSettings:SecretKey");
if (string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("SecretKey no está configurada");
}
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = false,
        ValidateAudience  = false
    };
});

builder.Services.AddSwaggerGen(
    options =>
  {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
      Description = "Nuestra API utiliza la Autenticación JWT usando el esquema Bearer. \n\r\n\r" +
                    "Ingresa la palabra a continuación el token generado en login.\n\r\n\r" +
                    "Ejemplo: \"12345abcdef\"",
      Name = "Authorization",
      In = ParameterLocation.Header,
      Type = SecuritySchemeType.Http,
      Scheme = "Bearer"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement()
    {
      {
        new OpenApiSecuritySchemeReference("Bearer", document, null),
        new List<string>()
      }
    });

    options.SwaggerDoc("v1", new OpenApiInfo
    {
      Version = "v1",
      Title = "API ECOMMERCE V2",
      Description = "API para gestionar productos y usuarios",
      TermsOfService = new Uri("https://example.com/terms"),
      // Todo esto ya es opcional
      Contact = new OpenApiContact
      {
        Name = "Lalo104lucky",
        Url = new Uri("https://example.com/contact")
      },
      License = new OpenApiLicense
      {
        Name = "Licencia de uso",
        Url = new Uri("https://example.com/license")
      }
    });
    options.SwaggerDoc("v2", new OpenApiInfo
    {
      Version = "v2",
      Title = "API ECOMMERCE",
      Description = "API para gestionar productos y usuarios",
      TermsOfService = new Uri("https://example.com/terms"),
      // Todo esto ya es opcional
      Contact = new OpenApiContact
      {
        Name = "Lalo104lucky",
        Url = new Uri("https://example.com/contact")
      },
      License = new OpenApiLicense
      {
        Name = "Licencia de uso",
        Url = new Uri("https://example.com/license")
      }
    });
  }
);

builder.Services.AddControllers(option => 
{
  option.CacheProfiles.Add(CacheProfiles.Default10, CacheProfiles.Profile10);
  option.CacheProfiles.Add(CacheProfiles.Default20, CacheProfiles.Profile20);
}
);

var apiVersioningBuilder = builder.Services.AddApiVersioning(option =>
{
  option.AssumeDefaultVersionWhenUnspecified = true;
  option.DefaultApiVersion = new ApiVersion(1, 0);
  option.ReportApiVersions = true;
  // option.ApiVersionReader = ApiVersionReader.Combine( new QueryStringApiVersionReader("api-version")); // ?api-version
});
apiVersioningBuilder.AddApiExplorer(option =>
{
  option.GroupNameFormat = "'v'VVV"; // v1, v2, v3...
  option.SubstituteApiVersionInUrl = true; // api/v{version}/products
});

builder.Services.AddCors(options =>
{
   options.AddPolicy(PolicyNames.AllowSpecificOrigin, builder =>
   {
       builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
   });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// Add OpenAPI services to the container.
builder.Services.AddOpenApi();

var app = builder.Build();

// Configurar Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
      options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
      options.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
    });
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(PolicyNames.AllowSpecificOrigin);

app.UseResponseCaching();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
