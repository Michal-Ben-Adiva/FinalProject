using Microsoft.EntityFrameworkCore;
using MODELS.Models;
using Microsoft.Extensions.Logging;

namespace FinalProject.Middleware
{
    public class AllowingAccessMiddleware : IMiddleware
    {
        private readonly ILogger<AllowingAccessMiddleware> _logger;
        private readonly ModelsContext _context;

        public AllowingAccessMiddleware(ILogger<AllowingAccessMiddleware> logger, ModelsContext context)
        {
            _logger = logger;
            _context = context;

        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogInformation("AllowingAccessMiddleware: מתחיל!!!!!!!!!!!!!!!");

            // Check if the path starts with /api/UsersControllers
            if (!context.Request.Path.StartsWithSegments("/api/UsersControllers/AllUsers"))
            {
                await next(context);
                return;
            }

            _logger.LogInformation("AllowingAccessMiddleware: בדק ניתוב");

            // Check if the method is GET
            if (context.Request.Method != HttpMethods.Get)
            {

                await next(context);
                return;
            }

            _logger.LogInformation("AllowingAccessMiddleware: בדק סוג בקשה");

            // Get the id from the route data
            if (context.Request.RouteValues.TryGetValue("id", out var idValue) && long.TryParse(idValue.ToString(), out var userId))
            {
                _logger.LogInformation($"AllowingAccessMiddleware: פרמטר id נמצא: {userId}");

                Users user = await GetUser(userId);
                if (user != null)
                {
                    _logger.LogInformation("AllowingAccessMiddleware: משתמש נמצא והוסף ל-context");

                    context.Items["User"] = user;
                }
                else
                {
                    _logger.LogInformation("AllowingAccessMiddleware: משתמש לא נמצא!");

                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync("User not found");
                    return;
                }
            }
            else
            {
                _logger.LogInformation("AllowingAccessMiddleware: פרמטר id לא נמצא");

                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid or missing userId");
                return;
            }

            var userObj = context.Items["User"] as Users;
            if (userObj != null && userObj.isAdmin == 1)
            {
                _logger.LogInformation("Admin user accessed the endpoint.");
            }
            else
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Forbidden: Admin access required.");
                return;
            }

            await next(context);
        }

        private async Task<Users> GetUser(long id)
        {
            return await _context.users.FindAsync(id);
        }

    }
}



