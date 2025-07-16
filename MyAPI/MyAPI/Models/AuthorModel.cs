using Microsoft.AspNetCore.Mvc;

namespace MyAPI.Models
{
    [BindProperties]
    public class AuthorModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
    }
    public class AuthorModelDTO
    {
        public string Name { get; set; }
    }
}
