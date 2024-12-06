using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using WebChat.Models;
using WebChat.Services.Implementations;
using WebChat.Services.Interfaces;

namespace WebChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;

        public ChatViewModel ChatViewModel { get; set; }

        public ChatController(IChatService chatService, IMessageService messageService)
        {
            _chatService = chatService;
            _messageService = messageService;
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

        [HttpGet("/Chat/{chatId}")]
        public async Task<IActionResult> Chat(int chatId)
        {
            var chat = await _chatService.GetChatAsync(chatId);
            if (chat == null)
            {
                return NotFound("Chat not found.");
            }

            var messages = await _messageService.GetMessagesByChatIdAsync(chatId);

            var viewModel = new ChatViewModel
            {
                Chat = chat,
                Messages = messages
            };

            return Ok(viewModel); 
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChats()
        {
            var chats = await _chatService.GetAllChatsAsync();
            return Ok(chats);
        }

        [HttpGet("GetUserChats")]
        public async Task<IActionResult> GetUserChats(int userId)
        {
            var chats = await _chatService.GetUserChatsAsync(userId);
            return Ok(chats);
        }

        //[HttpGet("GetUserChats")]
        //[Authorize]
        //public async Task<IActionResult> GetUserChats(int userId)
        //{
        //    var chats = await _chatService.GetUserChatsAsync(userId);

        //    if (chats == null || !chats.Any())
        //    {
        //        return NotFound("No chats found for this user.");
        //    }

        //    return Ok(chats);
        //}

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
