
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

class ApplicationBuilder
{

    private readonly WebApplicationBuilder builder;

    public ApplicationBuilder(string[] args)
    {
        builder = WebApplication.CreateBuilder(args);
    }


    public ApplicationBuilder AddApplicationServices()
    {

        builder.Services.AddScoped<IPersonRepository, PersonRepository>();

        return this;
    }

    public ApplicationBuilder AddCors()
    {

        builder.Services.AddCors();

        return this;

    }

    public ApplicationBuilder AddAuth()
    {

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.Authority = builder.Configuration.GetValue<string>("jwtAuthority");
            options.Audience = builder.Configuration.GetValue<string>("jwtAudience");
            options.TokenValidationParameters.ValidateLifetime = false;
            options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
        });
        return this;
    }

    public ApplicationBuilder AddOpenApi()
    {

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }});
        });
        return this;
    }

    internal WebApplication Build()
    {
        return builder.Build();
    }
}

