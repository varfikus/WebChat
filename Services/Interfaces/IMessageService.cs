using WebChat.Models;

namespace WebChat.Services.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetMessagesByChatIdAsync(int chatId);
        Task<IEnumerable<Message>> GetMessageAsync(int id);
        Task<IEnumerable<Message>> GetAllMessagesAsync();
        Task<Message> CreateMessageAsync(Message message);
        Task<Message> UpdateMessageAsync(Message message);
        Task<bool> DeleteMessageAsync(int id);
        Task<User?> GetMessageAuthor(int id);
    }
}
