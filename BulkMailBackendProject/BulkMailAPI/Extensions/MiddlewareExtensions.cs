using BulkMailAPI.Middlewares;

namespace BulkMailAPI.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureMiddlewares(this WebApplication app)
        {
            //app.UseCors(MyAllowSpecificOrigins);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("myAllowedOriginsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<TokenMiddleware>();            
            //Rate limiter middle without options uses the options configured at service level while when options is added, the options added in the middleware limitter is used
            //app.UseRateLimiter();
            //app.UseRateLimiter(rateLimiterOptions);
            app.MapControllers();
        }
    }
}
