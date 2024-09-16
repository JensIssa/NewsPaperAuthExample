namespace NewsPaperAuthExample.Entities
{
    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
