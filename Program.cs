
using LojaManoelApi.AutoMapper;
using LojaManoelApi.Data.Context;
using LojaManoelApi.Data.Entities;
using LojaManoelApi.Data.Repository;
using LojaManoelApi.Interfaces;
using LojaManoelApi.Interfaces.Repositories;
using LojaManoelApi.Interfaces.Services;
using LojaManoelApi.JwtConfig;
using LojaManoelApi.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace LojaManoelApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddScoped<IPedidoServico, PedidoServico>();
            builder.Services.AddScoped<IUsuarioServico, UsuarioServico>();
            builder.Services.AddScoped<IEmpacotamentoServico, EmpacotamentoServico>();
            builder.Services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
            builder.Services.AddScoped<TokenServico>();
            builder.Services.AddScoped<AplicacaoContexto>();
            builder.Services.AddScoped<JwtPapeis>();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiEntregasMentoria", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Digite: Bearer {seu token JWT}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });
            });
            // configura jwt com variavel de ambiente
            var jwtSecret = builder.Configuration["JWT_SECRET"];
            var key = Encoding.ASCII.GetBytes(builder.Configuration["JWT_SECRET"]);
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });


            builder.Services.Configure<JwtConfiguracao>(options =>
            {
                options.Secret = builder.Configuration["JWT_SECRET"];
            });

            builder.Services.AddDbContext<AplicacaoContexto>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"), sqlOptions => sqlOptions.CommandTimeout(120)));

            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
