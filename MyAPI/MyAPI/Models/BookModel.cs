using Microsoft.AspNetCore.Mvc;

namespace MyAPI.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public AuthorModel Author { get; set; }
        public int AuthorModelId { get; set; }
    }
    public class BookModelDTO
    {
        public string Title { get; set; }
        public AuthorModelDTO Author { get; set; }
    }
}
