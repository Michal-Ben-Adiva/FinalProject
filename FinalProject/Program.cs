
//using Microsoft.EntityFrameworkCore;
//using MODELS.Models;
//using DAL.Data;
//using DAL.Interfaces;
//using Microsoft.Extensions.DependencyInjection.Extensions;
//using BL.Middlewares;
//using BL.Middlewares;
//using Serilog;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using System.Text;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//string myCors = "_myCors";
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
////jwt
//var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
//var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
// .AddJwtBearer(options =>
// {
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = jwtIssuer,
//         ValidAudience = jwtIssuer,
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
//     };
// });
//builder.Services.AddAuthorization();

////jwt for swagger
//builder.Services.AddSwaggerGen(op =>
//{
//    op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        In = ParameterLocation.Header,
//        Description = "Please enter token",
//        Name = "Authorization",
//        Type = SecuritySchemeType.Http,
//        BearerFormat = "JWT",
//        Scheme = "bearer"
//    });
//    op.AddSecurityRequirement(new OpenApiSecurityRequirement
//                {
//                    {
//                        new OpenApiSecurityScheme
//                        {
//                            Reference=new OpenApiReference
//                            {
//                                Type=ReferenceType.SecurityScheme,
//                                 Id="Bearer"
//                             }
//                        },
//                        new string[]{}
//                    }

//                });
//});
//builder.Services.AddSwaggerGen();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//builder.Services.AddCors(op =>
//{
//    op.AddPolicy(myCors,
//        builder =>
//        {
//            builder.WithOrigins("*")
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//        });
//});

////connection string
//builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<ModelsContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultDataBase")));
//builder.Services.AddScoped<ICV, CVData>();
//builder.Services.AddScoped<IUsers, UsersData>();
//builder.Services.AddScoped<IJob, JobData>();
//builder.Services.AddScoped<ICVJobs, CVJobsData>();





//Log.Logger = new LoggerConfiguration()
//       .WriteTo.File(@"C:\Users\DELL\Documents\לימודים\שנה ב\.netcore\New folder\FinalProject\MyLogger.txt",
//    rollingInterval: RollingInterval.Day)
//    .CreateLogger();


//var app = builder.Build();

////Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


//app.UseCors(myCors);

//app.UseHttpsRedirection();

//app.UseMiddleware<LogMiddleware>();
////app.UseMiddleware<IsTokenValidMiddleware>();

//app.UseAuthentication();
//app.UseAuthorization();




//app.MapControllers();

//app.Run();




using Microsoft.EntityFrameworkCore;
using MODELS.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using BL.Middlewares;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string myCors = "_myCors";
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// jwt
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});
builder.Services.AddAuthorization();

// jwt for swagger
builder.Services.AddSwaggerGen(op =>
{
    op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    op.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(op =>
{
    op.AddPolicy(myCors,
        builder =>
        {
            builder.WithOrigins("*")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// connection string
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ModelsContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultDataBase")));
builder.Services.AddScoped<ICV, CVData>();
builder.Services.AddScoped<IUsers, UsersData>();
builder.Services.AddScoped<IJob, JobData>();
builder.Services.AddScoped<ICVJobs, CVJobsData>();

// Register TokenValidationMiddleware with a secret key
builder.Services.AddTransient<TokenValidationMiddleware>(provider =>
    new TokenValidationMiddleware(jwtKey));

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(@"C:\Users\DELL\Documents\לימודים\שנה ב\.netcore\New folder\FinalProject\MyLogger.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(myCors);
app.UseHttpsRedirection();
app.UseMiddleware<LogMiddleware>();
app.UseMiddleware<TokenValidationMiddleware>(); // Add middleware to the pipeline
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

