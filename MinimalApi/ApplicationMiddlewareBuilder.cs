




internal class ApplicationMiddlewareBuilder
{
    private readonly WebApplication? application;
    public ApplicationMiddlewareBuilder(WebApplication? application)
    {
        this.application = application;
    }

    public ApplicationMiddlewareBuilder UseSwagger()
    {

        application.UseSwagger();
        application.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
        return this;
    }

    public ApplicationMiddlewareBuilder UseCors()
    {

        application.UseCors(p =>
        {
            p.AllowAnyOrigin();
            p.WithMethods("GET");
            p.AllowAnyHeader();
        });

        return this;
    }

    public ApplicationMiddlewareBuilder UseAuth()
    {

        application.UseAuthentication();
        application.UseAuthorization();
        return this;
    }
}

