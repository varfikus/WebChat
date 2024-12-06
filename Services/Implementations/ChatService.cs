using Microsoft.EntityFrameworkCore;
using WebChat.Models;
using WebChat.Services.Interfaces;

namespace WebChat.Services.Implementations
{
    public class ChatService : IChatService
    {
        private readonly ChatContext _context;

        public ChatService(ChatContext context)
        {
            _context = context;
        }

        public async Task<Chat> GetChatAsync(int id)
        {
            return await _context.Chats.FindAsync(id);
        }

        public async Task<IEnumerable<Chat>> GetAllChatsAsync()
        {
            return await _context.Chats.Include(c => c.UserChats).ToListAsync();
        }

        public async Task<IEnumerable<Chat>> GetUserChatsAsync(int userId)
        {
            return await _context.UserChats
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.Chat)
                .ToListAsync();
        }

        public async Task<Chat> CreateChatAsync(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
            return chat;
        }

        public async Task<Chat> UpdateChatAsync(Chat chat)
        {
            _context.Chats.Update(chat);
            await _context.SaveChangesAsync();
            return chat;
        }

        public async Task<bool> DeleteChatAsync(int id)
        {
            var chat = await _context.Chats.FindAsync(id);
            if (chat == null)
            {
                return false;
            }

            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
