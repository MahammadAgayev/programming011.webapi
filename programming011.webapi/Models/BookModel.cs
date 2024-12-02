using System.ComponentModel.DataAnnotations;

namespace programming011.webapi.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(1000, 99999)]
        public int ReleaseYear { get; set; }
        [Required]
        public string AuthorName { get; set; }
    }
}
