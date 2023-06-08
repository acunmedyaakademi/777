using _777.Data.Entities.Common;

namespace _777.Data.Entities
{
    public class Text : BaseClass
    {
        public string Title { get; set; }
        public int UserId  { get; set; }
        public UserApp User  { get; set; }
        public double SentimentScore { get; set; }
        public string Content { get; set; }
    }
}
