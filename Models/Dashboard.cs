namespace WebChat.Models
{
    public class Dashboard
    {
        public int Id { get; set; }
        public virtual ICollection<UserChat> UserChats { get; set; } = new List<UserChat>();

        public virtual ICollection<Chat> Chats
        {
            get => UserChats.Select(uc => uc.Chat).ToList();
        }
    }
}
