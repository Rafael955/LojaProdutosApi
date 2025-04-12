using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProdutosApp.Api.Configurations;
using ProdutosApp.Infra.Message.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddCorsConfiguration();

builder.Services.AddOpenApi();

//Registrando a classe WORKER / CONSUMER
builder.Services.AddHostedService<MessageConsumer>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddDependencyInjections();

builder.Services.AddSwaggerConfiguration();

//Criando a configuração para autenticação com JWT - JSON WEB TOKEN
builder.Services.AddAuthentication(
    auth =>
    {
        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(
        bearer =>
        {
            bearer.RequireHttpsMetadata = false;
            bearer.SaveToken = true;
            bearer.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("CE6AAEF5-CFC1-4446-951A-BA14EB7BBD8D")),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwaggerConfiguration();

app.UseCorsConfiguration();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Definindo a classe Program.cs como publica
public partial class Program { }