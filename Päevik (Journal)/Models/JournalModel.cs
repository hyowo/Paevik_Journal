namespace Journal.Models
{
    public class JournalModel
    {
        public int Id { get; set; }
        public bool IsPrivate { get; set; }
        public string OriginalPoster { get; set; }
        public DateTime PostedAt { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
