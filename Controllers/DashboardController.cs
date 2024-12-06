using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebChat.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ChatContext _context;

        public DashboardController(ChatContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("current-user")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(new { UserId = userId });
        }

        [Authorize]
        public IActionResult Dashboard(int userId)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (loggedInUserId != userId.ToString())
            {
                return Unauthorized();
            }

            ViewBag.UserId = userId;

            return View();
        }

        [HttpGet("NewMessages")]
        [Authorize]
        public IActionResult GetNewMessages()
        {
            // Пример временного фильтра - учитываем сообщения, отправленные за последние 5 минут
            var lastChecked = DateTime.UtcNow;

            // Получаем количество новых сообщений для каждого чата, которые были отправлены после указанного времени
            var newMessages = _context.Messages
                .Where(m => m.Timestamp > lastChecked)
                .GroupBy(m => m.ChatId) // Группируем по ChatId
                .Select(group => new
                {
                    ChatId = group.Key, // Идентификатор чата
                    NewMessagesCount = group.Count() // Количество новых сообщений
                })
                .ToList(); // Преобразуем результат в список

            return Ok(newMessages); // Отправляем список с нужными данными
        }
    }
}
