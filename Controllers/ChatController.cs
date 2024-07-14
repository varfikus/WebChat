using Microsoft.AspNetCore.Mvc;
using WebChat.Models;
using WebChat.Services.Interfaces;

namespace WebChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChat(int id)
        {
            var chat = await _chatService.GetChatAsync(id);
            if (chat == null)
            {
                return NotFound();
            }
            return Ok(chat);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChats()
        {
            var chats = await _chatService.GetAllChatsAsync();
            return Ok(chats);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat(Chat chat)
        {
            var createdChat = await _chatService.CreateChatAsync(chat);
            return CreatedAtAction(nameof(GetChat), new { id = createdChat.Id }, createdChat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChat(int id, Chat chat)
        {
            if (id != chat.Id)
            {
                return BadRequest();
            }

            var updatedChat = await _chatService.UpdateChatAsync(chat);
            return Ok(updatedChat);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int id)
        {
            var result = await _chatService.DeleteChatAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
