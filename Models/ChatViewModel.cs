namespace WebChat.Models
{
    public class ChatViewModel
    {
        public Chat Chat { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
