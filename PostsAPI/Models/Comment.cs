using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PostsAPI.Models
{
    public class Comment
    {
        public int Id{get; set;}

        [Required(ErrorMessage = "El contenido del comentario es obligatorio.")]
        [MaxLength(300, ErrorMessage = "El comentario no puede tener más de 300 caracteres.")]
        public string Text{get; set;} = string.Empty;
        
        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

        public int PostId{get; set;}

        [JsonIgnore]
        public Post? Post{get; set;}
    }
}