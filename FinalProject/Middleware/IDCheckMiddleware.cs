
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MODELS.Models;

namespace FinalProject.Middleware
{
    public class IdCheckMiddleware : IMiddleware
    {
        private readonly ILogger<IdCheckMiddleware> _logger;

        public IdCheckMiddleware(ILogger<IdCheckMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Method == HttpMethods.Delete || context.Request.Method == HttpMethods.Get)
            {
                await next(context);
                return;
            }

            // Check if the path starts with /api/Users
            if (!context.Request.Path.StartsWithSegments("/api/UsersControllers"))
            {
                // Skip this middleware if it's not a request to /api/Users
                await next(context);
                return;
            }


            string requestBody;

            // שמירת מצב הגוף של הבקשה
            context.Request.EnableBuffering();

            // קריאת תוכן הבקשה כטקסט
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0; // החזרת מצב הגוף להתחלה
            }

            // וידוא שהתוכן לא ריק
            if (string.IsNullOrWhiteSpace(requestBody))
            {
                _logger.LogError("תוכן הבקשה ריק");
                context.Response.StatusCode = 400; // Bad Request
                await context.Response.WriteAsync("תוכן הבקשה ריק");
                return;
            }

            // המרת התוכן לאובייקט מסוג Users
            Users user;
            try
            {
                user = JsonConvert.DeserializeObject<Users>(requestBody);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "שגיאה בהמרת JSON");
                context.Response.StatusCode = 400; // Bad Request
                await context.Response.WriteAsync("שגיאה בהמרת JSON");
                return;
            }

            // בדיקת שדה id
            if (user == null || user.id == 0)
            {
                _logger.LogError("מספר תעודת זהות לא נמצא באובייקט הבקשה או שהוא 0");
                context.Response.StatusCode = 400; // Bad Request
                await context.Response.WriteAsync("מספר תעודת זהות לא נמצא באובייקט הבקשה או שהוא 0");
                return;
            }

            // בדיקת תקינות מספר תעודת הזהות
            if (!IsValidIsraeliId(user.id))
            {
                _logger.LogError("מספר תעודת זהות לא תקין");
                context.Response.StatusCode = 400; // Bad Request
                await context.Response.WriteAsync("מספר תעודת זהות לא תקין");
                return;
            }

            // רישום הודעה
            _logger.LogInformation("מספר תעודת זהות תקין: {0}", user.id);

            // המשך עיבוד הבקשה
            await next(context);
        }

        private bool IsValidIsraeliId(long id)
        {
            if (id < 100000000 || id > 999999999)
            {
                return false;
            }

            int sum = 0;
            long temp = id;
            for (int i = 8; i >= 0; i--)
            {
                int digit = (int)(temp % 10);
                temp /= 10;

                if (i % 2 == 1)
                {
                    digit *= 2;
                }

                if (digit > 9)
                {
                    digit -= 9;
                }

                sum += digit;
            }

            return sum % 10 == 0;
        }
    }
}













