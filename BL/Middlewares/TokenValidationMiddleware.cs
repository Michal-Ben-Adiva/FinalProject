using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

public class TokenValidationMiddleware : IMiddleware
{
    private readonly string _secretKey;

    public TokenValidationMiddleware(string secretKey)
    {
        _secretKey = secretKey;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {

        if (!context.Request.Path.StartsWithSegments("/api/LogInController"))
        {
            await next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null && IsTokenValid(token))
        {
            // ניתן להוסיף מידע אודות המשתמש ב-HttpContext אם נדרש
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var claims = jwtToken.Claims;

            var identity = new ClaimsIdentity(claims, "jwt");
            context.User = new ClaimsPrincipal(identity);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await next(context);
    }

    private bool IsTokenValid(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ClockSkew = TimeSpan.Zero,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var expirationTime = jwtToken.ValidTo;
            var currentTime = DateTime.UtcNow;

            return expirationTime >= currentTime;
        }
        catch
        {
            return false;
        }
    }
}
