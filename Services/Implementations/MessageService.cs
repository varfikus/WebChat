using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebChat.Models;
using WebChat.Services.Interfaces;

namespace WebChat.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly ChatContext _context;

        public MessageService(ChatContext context)
        {
            _context = context;
        }

        public async Task<User?> GetMessageAuthor(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Message>> GetMessagesByChatIdAsync(int chatId)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetMessageAsync(int chatId)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<Message> CreateMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<Message> UpdateMessageAsync(Message message)
        {
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<bool> DeleteMessageAsync(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return false;
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
