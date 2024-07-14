using WebChat.Models;

namespace WebChat.Services.Interfaces
{
    public interface IChatService
    {
        Task<Chat> GetChatAsync(int id);
        Task<IEnumerable<Chat>> GetAllChatsAsync();
        Task<Chat> CreateChatAsync(Chat chat);
        Task<Chat> UpdateChatAsync(Chat chat);
        Task<bool> DeleteChatAsync(int id);
    }
}
