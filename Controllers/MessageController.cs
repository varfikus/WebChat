using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebChat.Models;
using WebChat.Services.Implementations;
using WebChat.Services.Interfaces;

namespace WebChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        private ChatContext _context;

        public MessageController(IMessageService messageService, IChatService chatService, IUserService userService, ChatContext context)
        {
            _messageService = messageService;
            _chatService = chatService;
            _userService = userService;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            var message = await _messageService.GetMessageAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        [HttpGet("NewMessages")]
        public IActionResult GetNewMessages()
        {
            var time = DateTime.Now.AddSeconds(-5);
            var newMessages = _context.Messages
                .Where(m => m.Timestamp > time)
                .GroupBy(m => m.ChatId)
                .Select(group => new
                {
                    ChatId = group.Key,
                    NewMessagesCount = group.Count()
                })
                .ToList();

            return Ok(newMessages);
        }

        [HttpGet("GetMessageAuthor/{id}")]
        public async Task<IActionResult> GetMessageAuthor(int id)
        {
            var user = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new
                {
                    Id = u.Id,
                    Name = u.Username 
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found." });
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMessages()
        {
            var messages = await _messageService.GetAllMessagesAsync();
            return Ok(messages);
        }

        [HttpPost("send")]
        public async Task<IActionResult> CreateMessage([FromBody] Message message)
        {
            if (message == null || string.IsNullOrWhiteSpace(message.Content))
            {
                return BadRequest("Message content cannot be empty.");
            }

            var chat = await _chatService.GetChatAsync(message.ChatId);
            var user = await _userService.GetUserAsync(message.UserId);

            if (chat == null || user == null)
            {
                return BadRequest("Chat or User not found.");
            }

            //message.Chat = chat;
            //message.User = user;
            message.Timestamp = DateTime.Now;

            var createdMessage = await _messageService.CreateMessageAsync(message);

            return CreatedAtAction(nameof(GetMessage), new { id = createdMessage.Id }, createdMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            var updatedMessage = await _messageService.UpdateMessageAsync(message);
            return Ok(updatedMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var result = await _messageService.DeleteMessageAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
