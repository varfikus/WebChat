using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebChat.Controllers;
using WebChat.Models;
using WebChat.Services.Interfaces;

namespace WebChat.Pages
{
    public class ChatModel : PageModel
    {
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;

        public ChatModel(IChatService chatService, IMessageService messageService)
        {
            _chatService = chatService;
            _messageService = messageService;
        }

        public Chat Chat { get; set; }
        public IEnumerable<Message> Messages { get; set; }

        public async Task OnGetAsync(int chatId)
        {
            Chat = await _chatService.GetChatAsync(chatId);
            Messages = await _messageService.GetMessagesByChatIdAsync(chatId);
        }
    }
}
