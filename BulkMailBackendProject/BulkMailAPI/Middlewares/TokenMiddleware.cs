using System.Security.Claims;
using DTOs.Configurations;
using MongoDB.Bson;

namespace BulkMailAPI.Middlewares;

public class TokenMiddleware : IMiddleware
{
    private readonly SessionInfo _sessionInfo;
    public TokenMiddleware(SessionInfo sessionInfo)
    {
        _sessionInfo = sessionInfo;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var authDetails = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        
        if (authDetails != null)
        {
            _sessionInfo.UserId = ObjectId.Parse(authDetails.Value);
        }

        await next(context);
    }
}