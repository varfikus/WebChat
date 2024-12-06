namespace WebChat.Models
{
    public class MessageRequest
    {
        public int UserId { get; set; }
        public int ChatId { get; set; }
        public string Content { get; set; }
    }
}
