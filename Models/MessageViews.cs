namespace WebChat.Models
{
    public class MessageView
    {
        public int Id { get; set; } 

        public int UserId { get; set; } 
        public int MessageId { get; set; }

        public bool IsViewed { get; set; } 

        public User User { get; set; } = null!;
        public Message Message { get; set; } = null!;
    }
}
